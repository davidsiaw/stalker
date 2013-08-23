using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using BlueBlocksLib;
using BlueBlocksLib.AsyncComms;
using System.Threading;
using BlueBlocksLib.TypeUtils;
using BlueBlocksLib.BaseClasses;
using KilnApi;
using System.Collections.Generic;
using System.Linq;

namespace FogBugzNet
{
    public struct Filter
    {
        public string Name;
        public string FilterType;
        public string ID;
    }

	public struct Status {
		public string Name;
		public int ID;
		public int CategoryID;

		public override string ToString() {
			return Name;
		}
	}

    public struct Project
    {
        public string Name;
        public int ID;
		
		public override string ToString() {
			return Name;
		}
    }

    public class ECommandFailed : Exception
    {
        public enum Code
        {
            // These correlate to the error code values documented here: http://www.fogcreek.com/FogBugz/docs/60/topics/advanced/API.html
            InvalidSearch = 10,
            TimeTrackingProblem = 7
        };
        public int ErrorCode;
        public ECommandFailed(string reason, int errorCode)
            : base(reason)
        {
            ErrorCode = errorCode;
        }
    }
	
    public class FogBugz
    {
		Kiln kiln = null;

		public Kiln Kiln { get { return kiln; } }

        private string token_;
        public string AuthToken { get { return token_; } }

        public bool IsLoggedIn { get { return AuthToken != null && token_.Length > 0; } }

        private string BaseURL_;

        public string BaseURL
        {
            get
            {
                return BaseURL_;
            }

        }

		class CommandInfo {

			public CommandInfo(string command, string[] parms, Action<string> callback, BlueBlocksLib.Action<string, string[], Action<string>> action) {
				this.command = command;
				this.parms = parms;
				this.action = action;
				this.callback = callback;
			}

			public readonly string command;
			public readonly string[] parms;
			public readonly BlueBlocksLib.Action<string, string[], Action<string>> action;
			public readonly Action<string> callback;
		}


		Agent<CommandInfo> commsAgent;

        public FogBugz(string baseURL)
        {
            this.BaseURL_ = baseURL;
			commsAgent = new Agent<CommandInfo>(
				() => { },
				() => { },
				ci => {
					ci.action(ci.command, ci.parms, ci.callback);
					return NextAction.WaitForNextMessage; 
				});

			commsAgent.Start();

			CasesRefreshed += new EventHandler<GenericEventArgs<Case[]>>((o, e) => { });
        }

		string kilnBaseURL = null;

		public string Email {
			get;
			private set;
		}

        public bool LogOn(string email, string password)
        {
            try {
				Email = email;
				email = HttpUtility.UrlEncode(email);
				password = HttpUtility.UrlEncode(password);
				string ret = fbCommandSync("logon", "email=" + email, "password=" + password);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ret);
                token_ = doc.SelectSingleNode("//token").InnerText;

				try {
					kilnBaseURL = this.BaseURL_.Replace("https://", "").Split('.')[0] + ".kilnhg.com" + "/";

					kiln = Kiln.Authenticate(
						kilnBaseURL,
						Uri.UnescapeDataString(email),
						Uri.UnescapeDataString(password));
					
				} catch (Exception e) {
					Console.WriteLine(e.Message);
				}

                return true;
            }
            catch (ECommandFailed e)
            {
                Utils.Log.ErrorFormat("Error while logging on: {0}, code: {1}", e.Message, e.ErrorCode);
            }
            catch (EServerError e)
            {
                Utils.Log.Error("Error during logon: " + e.ToString());
            }
            return false;
        }


        private string FormatHttpGetRequest(string command, params string[] args)
        {

            string arguments = "";
            if ((IsLoggedIn) && !command.Equals("logon"))
                arguments += "&token=" + AuthToken;

            if (args != null)
                foreach (string arg in args)
                    arguments += "&" + arg;
            return BaseURL + "/api.asp?cmd=" + command + arguments;

        }

        public delegate void OnFbCommandDone(XmlDocument response);
        public delegate void OnFbError(Exception x);

        private void CheckForFbError(string resXML)
        {
            Utils.Log.Debug("Parsing XML response for errors...");
            if (xmlDoc(resXML).SelectNodes("//error").Count > 0)
            {
                string err = xmlDoc(resXML).SelectSingleNode("//error").InnerText;
                Utils.Log.WarnFormat("Server returned error: {0}", err);
                int code = int.Parse(xmlDoc(resXML).SelectSingleNode("//error").Attributes["code"].Value);
                throw new ECommandFailed(err, code);
            }
        }

		private string fbCommandSync(string command, params string[] args) {

			string httpGetRequest = FormatHttpGetRequest(command, args);
			if (command != "logon")
				Utils.Log.DebugFormat("Executing web service command: {0}", httpGetRequest);

			string resXML = HttpUtils.httpGet(httpGetRequest);
			Utils.Log.DebugFormat("Size of response: {0}", resXML.Length);
			CheckForFbError(resXML);

			return resXML;
		}

		private void fbCommand(Action<string> callback, string command, params string[] args) {
			commsAgent.SendMessage(new CommandInfo(command, args, callback, (c, p, cb) => {
				try {
					cb(fbCommandSync(c, p));
				} catch (Exception e){
					Utils.Log.DebugFormat("Exception occured: {0}: {1}", e.ToString(), e.Message);
				}
			}));
		}

        // Execute a FB API URL request, where the args are: "cmd=DoThis", "param1=value1".
        // Returns the XML response.
        // This function is for debugging purposes and should be wrapped by specific 
        // command methods, such as "Logon", or "ListCases".
        public string ExecuteURL(string URLParams)
        {
            if (!IsLoggedIn)
                return "Not logged in";
            string URL = BaseURL + "/api.asp?" + URLParams + "&token=" + AuthToken;
            return HttpUtils.httpGet(URL);
        }

        private XmlDocument xmlDoc(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

		Filter[] filters = null;

        public Filter[] GetFilters()
        {
			return filters;
        }

		public Status[] GetStatuses(int category) {
			return statuses.Where(x => x.CategoryID == category).ToArray();
		}

		public void CommentOnCase(int caseID, string comments) {
			Utils.Log.Debug("Commenting on case");

			fbCommand(res => { }, "edit", 
				"sEvent=" + Uri.EscapeDataString(comments),
				"ixBug=" + caseID
				);

		}

		private void QueryFilters() {
			Utils.Log.Debug("Querying filters");

			string res = fbCommandSync("listFilters", null);
			XmlNodeList filters = xmlDoc(res).SelectNodes("//filter");

			ArrayList ret = new ArrayList();
			foreach (XmlNode node in filters) {
				Filter f = new Filter();
				f.Name = node.InnerText;
				f.ID = node.SelectSingleNode("@sFilter").Value;
				f.FilterType = node.SelectSingleNode("@type").Value;
				ret.Add(f);
			}
			this.filters = (Filter[])ret.ToArray(typeof(Filter));
		}

        // Return all cases in current filter
        public Case[] GetCurrentFilterCases()
        {
			return UpdateAllStuff("", (x, y) => { });
        }

        public void SetFilter(Filter f)
        {
			fbCommand(resp => { }, "saveFilter", "sFilter=" + f.ID.ToString());
        }


        private Case ParseCaseNode(XmlNode node)
        {
            Case c = new Case();
            try
            {
                c.Name = node.SelectSingleNode("sTitle").InnerText;
                c.ParentProject.Name = node.SelectSingleNode("sProject").InnerText;
                c.ParentProject.ID = int.Parse(node.SelectSingleNode("ixProject").InnerText);
				c.Status = node.SelectSingleNode("sStatus").InnerText;

                c.AssignedTo = node.SelectSingleNode("sPersonAssignedTo").InnerText;
                c.Area = node.SelectSingleNode("sArea").InnerText;
                c.ID = int.Parse(node.SelectSingleNode("@ixBug").Value);
                c.ParentCaseID = 0;
                if (node.SelectSingleNode("ixBugParent").InnerText != "")
                    c.ParentCaseID = int.Parse(node.SelectSingleNode("ixBugParent").InnerText);

                double hrsElapsed = double.Parse(node.SelectSingleNode("hrsElapsed").InnerText);
                c.Elapsed = new TimeSpan((long)(hrsElapsed * 36000000000.0));

                double hrsEstimate = double.Parse(node.SelectSingleNode("hrsCurrEst").InnerText);
                c.Estimate = new TimeSpan((long)(hrsEstimate * 36000000000.0));
                c.ParentMileStone.ID = int.Parse(node.SelectSingleNode("ixFixFor").InnerText);
                c.ParentMileStone.Name = node.SelectSingleNode("sFixFor").InnerText;

				DateTime.TryParse(node.SelectSingleNode("dtFixFor").InnerText, out c.ParentMileStone.Date);

				c.Category = node.SelectSingleNode("sCategory").InnerText;
				c.CategoryID = int.Parse(node.SelectSingleNode("ixCategory").InnerText);
				c.Priority = int.Parse(node.SelectSingleNode("ixPriority").InnerText);

				XmlNode tags = node.SelectSingleNode("tags");
				c.Tags = tags.SelectNodes("tag").Cast<XmlNode>().Select(x => x.InnerText).ToArray();

				c.caseEvents = TypeTools.XMLFunnel<Case.Events>(node.SelectSingleNode("events"));

				c.getchangeSets = () => kiln.GetChangesets(c.ID).Select(x => new FogBugzNet.Case.ChangesetData() {
					changeSet = x,
					kilnapi = kiln
				}).ToArray();
            }
            catch (System.Exception e)
            {
                Utils.Log.ErrorFormat("Error parsing case XML: {0}\nError: {1}", node.InnerXml, e.ToString());
                throw;
            }
            return c;
        }


		public Case[] ParseCasesXML(XmlDocument doc, System.Action<string, int> progressDelegate)
        {
            Utils.Log.Debug("Parsing response XML as DOM...");
            XmlNodeList nodes = doc.SelectNodes("//case");
            Utils.Log.DebugFormat("Got {0} cases", nodes.Count);

            ArrayList ret = new ArrayList();
			double count = 0;
			foreach (XmlNode node in nodes) {
				progressDelegate(
					"Thanking the slow network for case " + count + " out of" + nodes.Count,
					(int)(30 + (count / nodes.Count) * 70));
				ret.Add(ParseCaseNode(node));
				count++;
			}
            return (Case[])ret.ToArray(typeof(Case));
        }

		public event EventHandler<GenericEventArgs<Case[]>> CasesRefreshed;

		Case[] cases = null;

		class CaseEquality : IEqualityComparer<Case> {

			#region IEqualityComparer<Case> Members

			public bool Equals(Case x, Case y) {
				return x.ID == y.ID;
			}

			public int GetHashCode(Case obj) {
				return obj.ID.GetHashCode();
			}

			#endregion
		}
		
        // Return all cases that match search (as in the web page search box)
		// Yes. theoretically and the nomenclature suggests it. But what this
		// function REALLY does is get ALL INFORMATION ABOUT EVERYTHING
		// BWAAHAHAHAHAHAHAHAHAHAHA
		public Case[] UpdateAllStuff(string search, System.Action<string, int> progressDelegate, bool getKilnCases = false) {

			closedCases = new HashSet<int>();

			progressDelegate("Getting Statuses...", 10);
			QueryStatuses();
			progressDelegate("Getting Projects...", 10);
			QueryProjects();
			progressDelegate("Getting Intervals...", 15);
			QueryIntervals();
			progressDelegate("Getting Filters...", 20);
			QueryFilters();
			progressDelegate("Getting cases you worked on...", 25);
			GetWorkedOnFromFB();

			Utils.Log.DebugFormat("Querying for all cases that match '{0}'", search);
			cases = GetCasesMatchingSearch(search, progressDelegate);

			CasesRefreshed(this, new GenericEventArgs<Case[]>(cases));

			Dictionary<int, Case> idToCase = cases
				.ToDictionary(x => x.ID);

			List<Case> fullCaseResults = new List<Case>(cases);

			if (kiln != null && getKilnCases) {
				progressDelegate("Getting reviews...", 30);
				var reviews = kiln.GetReviewsRelatedToMe();

				foreach (Review review in reviews.Approved) {
					if (!review.ID.StartsWith("K")) {
						continue;
					}
					Review r = review;
					if (review.Changesets == null) {
						r = kiln.GetReview(review.ID);
					}
					foreach (var ch in r.Changesets) {
						if (ch.AssociatedCases.Any(x => idToCase.ContainsKey(x))) {
							var relc = ch.AssociatedCases.First(x => idToCase.ContainsKey(x));
							MakeNewCaseForReview(fullCaseResults, review, relc);
							break;
						}
					}
				}

				foreach (Review review in reviews.Rejected) {
					if (!review.ID.StartsWith("K")) {
						continue;
					}
					Review r = review;
					if (review.Changesets == null) {
						r = kiln.GetReview(review.ID);
					}
					foreach (var ch in r.Changesets) {
						if (ch.AssociatedCases.Any(x => idToCase.ContainsKey(x))) {
							var relc = ch.AssociatedCases.First(x => idToCase.ContainsKey(x));
							MakeNewCaseForReview(fullCaseResults, review, relc);
							break;
						}
					}
				}

				//foreach (Review review in reviews.Author) {
				//    int fogbugzReviewID;
				//    if (int.TryParse(review.ID, out fogbugzReviewID)) {
				//        if (idToCase.ContainsKey(fogbugzReviewID)) {
				//            idToCase[fogbugzReviewID].kilnReview = review;
				//        }
				//    }

				//}

				//foreach (Review review in reviews.AwaitingReview) {

				//    int fogbugzReviewID;
				//    if (int.TryParse(review.ID, out fogbugzReviewID)) {

				//        if (idToCase.ContainsKey(fogbugzReviewID)) {
				//            // if you are assigned a review that you own 
				//            // (assigned a review to yourself)
				//            idToCase[fogbugzReviewID].kilnReview = review;
				//            idToCase[fogbugzReviewID].assignedReviewToSelf = true;

				//        } else if (review.Status != ReviewStatus.Approved) {
				//        }
				//    }
				//}
			}

			return fullCaseResults.Where(c => !closedCases.Contains(c.ID)).ToArray();
		}

		public Case[] GetCases() {
			return cases;
		}

		HashSet<int> closedCases = new HashSet<int>();

		public void CloseCase(Case c) {
			closedCases.Add(c.ID);
			c.closed = true;
		}

		private void MakeNewCaseForReview(List<Case> fullCaseResults, Review review, int relatedCaseID) {
			if (review.ID.StartsWith("K")) {
				Case c = new Case();
				c.kilnReview = review;
				c.ID = relatedCaseID;
				c.reviewAssignedToMe = true;
				fullCaseResults.Add(c);
			}
		}

		public Case[] GetCasesMatchingSearch(string search, System.Action<string, int> progressDelegate) {

			Case[] cs;

			progressDelegate("Asking for your cases...", 30);
			string res = fbCommandSync("search", "q=" + search, "cols=sTitle,sStatus,sProject,ixProject,sPersonAssignedTo,sArea,hrsElapsed,hrsCurrEst,ixBugParent,ixFixFor,sFixFor,sCategory,ixCategory,ixPriority,events,tags,dtFixFor");

			cs = ParseCasesXML(xmlDoc(res), progressDelegate);
			return cs;
		}

        private string OneWeekAgoIsoDate()
        {
            DateTime oneWeekAgo = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));

            return Utils.ToIsoTimeString(oneWeekAgo);
        }

		XmlDocument intervals;

        private XmlDocument ListIntervals()
        {
			return intervals;
        }

		private void QueryIntervals() {

			// Get list of all recorded time intervals from the last week
			Utils.Log.Debug("Querying server for user's work intervals");
			string res = fbCommandSync("listIntervals", "dtStart=" + OneWeekAgoIsoDate()); 

			XmlDocument doc = xmlDoc(res);

			// If none found during last week, query for all-time.
			if (null == doc.SelectSingleNode("//interval[last()]")) {
				string result = fbCommandSync("listIntervals");
				intervals = xmlDoc(result);

			} else {
				intervals = doc;
			}

		}

		int workedon = 0;

        // The id of the case the user is working on right now
        public int CaseWorkedOnNow
        {
            get
            {
				return workedon;
            }

			set {
				workedon = value;
			}
        }

		private void GetWorkedOnFromFB() {

			XmlDocument doc = ListIntervals();

			// If the last time interval has no "End" value, then it's still 
			// active -> this is the case we're working on.
			XmlNode lastInterval = doc.SelectSingleNode("//interval[last()]");
			if (lastInterval == null) {
				workedon = 0;
				return;
			}

			XmlNode lastEndTime = lastInterval.SelectSingleNode("dtEnd");
			
			if (lastEndTime.InnerText.Length == 0) {
				workedon = int.Parse(lastInterval.SelectSingleNode("ixBug").InnerText);
			} else {
				workedon = 0;
			}
		}


        public void StopWorking()
        {
            Utils.Log.Debug("Stopping work...");
			fbCommand(res => { }, "stopWork", null);
			workedon = 0;
        }

        // returns false if case has no estimate (or cannot work on it for any other reason)
        public bool StartWorking(int id) {
			workedon = id;
            Utils.Log.DebugFormat("Starting work on {0}", id);
            try
            {
				fbCommand(ret => { }, "startWork", "ixBug=" + id.ToString());
            }
            catch (ECommandFailed x)
            {
                if (x.ErrorCode == (int)ECommandFailed.Code.TimeTrackingProblem)
                    return false;
                throw;
            }
            return true;
        }


        public void ResolveCase(int id)
        {
            Utils.Log.InfoFormat("Resolving case {0}", id);
			fbCommand(ret => { Utils.Log.Debug(ret); }, "resolve", "ixBug=" + id.ToString(), "ixStatus=2");
           
        }

        public void ResolveAndCloseCase(int id)
        {
            Utils.Log.InfoFormat("Resolving and closing case {0}", id);
			fbCommand(ret => { Utils.Log.Debug(ret); }, "close", "ixBug=" + id.ToString());
            
        }

		public string ReviewURL(string reviewID) {
			return "https://" + kilnBaseURL + "/Review/" + reviewID;
		}

        // Returns the URL to edit this case (by id)
        public string CaseEditURL(int caseid)
        {
            return BaseURL + "/Default.asp?" + caseid.ToString();
        }

        public string NewCaseURL
        {
            get
            {
                return BaseURL + "/default.asp?command=new&pg=pgEditBug";
            }
        }

        public string NewSubCaseURL(int parentID)
        {
            return NewCaseURL + "&ixBugParent=" + parentID.ToString();
        }

        public string ViewOutlineURL(int caseid)
        {
            return BaseURL + "/default.asp?search=2&searchFor=outline:" + caseid.ToString();
        }


        public bool SetEstimate(int caseid, string estimate)
        {
            Utils.Log.InfoFormat("Estimating case {0} at {1} hours", caseid, estimate);

			fbCommand(res => {
				TimeSpan newEstimate = UpdateAllStuff(caseid.ToString(), (x, y) => { })[0].Estimate;

			}, "edit", "ixBug=" + caseid.ToString(), "hrsCurrEst=" + estimate);

			return true;
        }

		public bool SetStatus(int caseid, int statusID) {
			Utils.Log.InfoFormat("Set case status {0} to {1}", caseid, statusID);

			fbCommand(res => {
				TimeSpan newEstimate = UpdateAllStuff(caseid.ToString(), (x, y) => { })[0].Estimate;

			}, "edit", "ixBug=" + caseid.ToString(), "ixStatus=" + statusID);

			return true;
		}

		Project[] projects;

        public Project[] ListProjects()
        {
			return projects;

        }

		Status[] statuses;

		private void QueryStatuses() {
			Utils.Log.Debug("Query list of statuses");
			List<Status> ret = new List<Status>();

			string res = fbCommandSync("listStatuses");
			XmlDocument doc = xmlDoc(res);
			XmlNodeList projs = doc.SelectNodes("//status");
			foreach (XmlNode proj in projs) {
				Status p = new Status();
				p.ID = int.Parse(proj.SelectSingleNode("./ixStatus").InnerText);
				p.Name = proj.SelectSingleNode("./sStatus").InnerText;
				p.CategoryID = int.Parse(proj.SelectSingleNode("./ixCategory").InnerText);
				ret.Add(p);
			}

			statuses = ret.ToArray();
		}

		private void QueryProjects() {
			Utils.Log.Debug("Query list of projects");
			ArrayList ret = new ArrayList();

			string res = fbCommandSync("listProjects");
			XmlDocument doc = xmlDoc(res);
			XmlNodeList projs = doc.SelectNodes("//project");
			foreach (XmlNode proj in projs) {
				Project p = new Project();
				p.ID = int.Parse(proj.SelectSingleNode("./ixProject").InnerText);
				p.Name = proj.SelectSingleNode("./sProject").InnerText;
				ret.Add(p);
			}

			projects = (Project[])ret.ToArray(typeof(Project));
		}

        public void SetParent(Case c, int parentID)
        {
            Utils.Log.InfoFormat("Setting paret of case {0} to be {1}", c.ID, parentID);
			fbCommand(res => { }, "edit", "ixBug=" + c.ID.ToString(), "ixBugParent=" + parentID.ToString());
        }

        public void AddNote(int id, string note)
        {
            Utils.Log.InfoFormat("Adding a note to case {0}: {1}", id, note);

			fbCommand(res => { }, "edit", "ixBug=" + id.ToString(), "sEvent=" + note);
        }

      

    }
}
