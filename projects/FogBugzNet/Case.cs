using System;
using System.Collections.Generic;
using System.Text;
using KilnApi;

namespace FogBugzNet {


    public class Case
    {

		public class Events { 
			public Event[] @event;
		}

		public class Event {

			public int bugid;
			public int ixBugEvent;
			public string sVerb;
			public int ixPerson;
			public string sPerson;
			public int ixPersonAssignedTo;
			public string dt;
			public string s;
			public string sHtml;
			public bool fEmail;
			public bool fExternal;
			public bool fHTML;
			public string sFormat;
			public string sChanges;
			public string evtDescription;
			public Attachments rgAttachments;

			public string sFrom;
			public string sTo;
			public string sCC;
			public string sReplyTo;
			public string sSubject;
			public string sDate;
			public string sBodyText;
			public string sBodyHTML;

		}

		public class Attachments {
			public Attachment[] attachment;
		}

		public class Attachment {
			public string sFileName;
			public string sURL;
		}

		public class ChangesetData {
			public Kiln kilnapi;
			public Changeset changeSet;
			public Repository repo {
				get {
					return kilnapi.GetRelated(changeSet.Revision)[0];
				}
			}
		}

		public Func<ChangesetData[]> getchangeSets { set; get; }
		ChangesetData[] csets = null;
		public ChangesetData[] changeSets {
			get {
				if (csets == null) {
					csets = getchangeSets();
				}
				return csets;
			}
		}

		public int ID;
		public string Name;
		public string Status;
		public int Priority;

        public TimeSpan Elapsed;
        public TimeSpan Estimate;
        public Project ParentProject;

        public string Area;
        public string AssignedTo;
        public int ParentCaseID = 0;
        public string Category;

		public Review kilnReview = null; // The review in kiln
		public bool reviewAssignedToMe = false;
		public bool assignedReviewToSelf = false;

		public Events caseEvents;
		public bool closed;

        public MileStone ParentMileStone = new MileStone();
		public int CategoryID;

        public string ShortDescription { get { return ID + ": " + Name; } }

        private static string FormatTimeSpan(TimeSpan ts)
        {
            return String.Format("{0}:{1}",
                        ts.Hours.ToString("0#"),
                        ts.Minutes.ToString("0#"));
        }

        public string ElapsedTime_h_m // returns elapsed time in h:m format: 20:04
        {
            get
            {
                return FormatTimeSpan(Elapsed);
            }
        }
        public string EstimatedTime_h_m // returns elapsed time in h:m format: 20:04
        {

            get
            {
                return FormatTimeSpan(Estimate);
            }
        }
        public string LongDescription
        {
            get
            {
                return String.Format("{0}:{1}:{2}:{3} - {4}", ParentProject.Name, Area, AssignedTo, ID, Name);
            }
        }


    }

}
