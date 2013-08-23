using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using KilnApi.Helpers;

namespace KilnApi {
    public sealed partial class Kiln {
        /// <summary>
        /// Get specified record type from Kiln.
        /// </summary>
        /// <typeparam name="TResult">Type of record that should be fetched.</typeparam>
        /// <param name="call">The API call that will be performed.</param>
        /// <param name="parameters">Anonymous type that represents parameters for an URL.</param>
        public TResult Call<TResult>(KilnApiCall call, object parameters) {
            var paramsDict = PatternParser.ParseAnonymous(parameters);
            return Call<TResult>(call, paramsDict);
        }

        /// <summary>
        /// Get specified record type from Kiln.
        /// </summary>
        /// <typeparam name="TResult">Type of record that should be fetched.</typeparam>
        /// <param name="call">The API call that will be performed.</param>
        /// <param name="parameters">Dictionary that contains parameters for an URL.</param>
        public TResult Call<TResult>(KilnApiCall call, Dictionary<string, string> parameters) {
            parameters = parameters ?? new Dictionary<string, string>();

            // Automatically add token if it's not specified in parameters list.
            if (!parameters.ContainsKey("token")) {
                parameters.Add("token", Token);
            }

            // Perform specified call.
            string response = PerformRequest(m_baseUrl +
                DenormalizeChangesetsParameter(call.GetPathAndQuery(parameters), call),
                call.HttpMethod);

            // Try to deserialize response message and return
            // deserialized value if operation succeeded.
            TResult result;
            if (TryDeserializeJson<TResult>(response, out result)) {
                return result;
            }

            // Try to detect Kiln errors within response message.
            KilnErrorsWrapper errorsWrapper;
            if (TryDeserializeJson<KilnErrorsWrapper>(response, out errorsWrapper)) {
                throw new KilnApiException(errorsWrapper.Errors);
            }

            // If response is unknown, throw an exception.
            throw new InvalidOperationException(response);
        }

        /// <summary>
        /// NOTE: This method is temporary and is used only for Review/Create API call.
        /// It denormalizes ixChangesets parameter since Kiln API 1.0 doesn't accept
        /// values specified as array. If this will be fixed in the future the method
        /// will be removed.
        /// </summary>
        private string DenormalizeChangesetsParameter(string path, KilnApiCall call) {
            // Omit denormalization if specified call isn't Review/Create (POST).
            if (call != KilnApiCall.ReviewCreate)
                return path;

            // Return path if it doesn't include ixChangesets parameter.
            Match match = Regex.Match(path, "ixChangesets=(?<value>.*)&");
            if (!match.Success || match.Groups["value"] == null)
                return path;

            // If path includes ixChangesets parameter,
            // denormalize its value.
            Group group = match.Groups["value"];
            string[] changesets = HttpUtility.UrlDecode(group.Value).Split(',');

            // Remove old parameter.
            path = path.Remove(match.Index, match.Length);

            // Append denormalized values to the path.
            foreach (string c in changesets)
                path += string.Format("&ixChangesets={0}", c);

            return path;
        }

        /// <summary>
        /// Get person with specified ID.
        /// </summary>
        public Person[] GetPersons() {
            return Call<Person[]>(KilnApiCall.Persons, new { token = Token });
        }  
		
		/// <summary>
		/// Get changesets for a case.
		/// </summary>
		public Changeset[] GetChangesets(int bug) {
			return Call<Changeset[]>(KilnApiCall.Bug, new { token = Token, ixBug = bug});
		}

		/// <summary>
		/// Get related repos
		/// </summary>
		public Repository[] GetRelated(string tail) {
			return Call<Repository[]>(KilnApiCall.Related, new { token = Token, revTails = tail });
		}

        /// <summary>
        /// Get all project records from Kiln.
        /// </summary>
        public Project[] GetProjects() {
            return Call<Project[]>(KilnApiCall.Projects, new { token = Token });
        }

        /// <summary>
        /// Get project with specified ID.
        /// </summary>
        public Project GetProject(int id) {
            return Call<Project>(KilnApiCall.Project, new { ixProject = id, token = Token });
        }

        /// <summary>
        /// Delete the project with specified ID.
        /// </summary>
        public bool DeleteProject(int id) {
            return Call<bool>(KilnApiCall.ProjectDelete, new { ixProject = id, token = Token });
        }

        /// <summary>
        /// Create a new project with specified parameters.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        /// <param name="description">The description of the project.</param>
        /// <param name="defaultPermission">The default permission of the project.</param>
        /// <returns>The record that represents newly created project.</returns>
        public Project CreateProject(
            string name,
            string description = null,
            Permission? defaultPermission = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("sName", name);
            parameters.Add("token", Token);

            // Add optional parameters.
            if (description != null)
                parameters.Add("sDescription", description);

            if (defaultPermission != null)
                parameters.Add("permissionDefault", defaultPermission
                    .Value.ToString().ToLower());

            return Call<Project>(KilnApiCall.ProjectCreate, parameters);
        }

        /// <summary>
        /// Update an existing project with specified parameters.
        /// </summary>
        /// <param name="id">A unique project identifier.</param>
        /// <param name="name">A new name of the project.</param>
        /// <param name="description">A new description of the project.</param>
        /// <param name="defaultPermission">A new default permission of the project.</param>
        /// <returns>The record that represents updated project.</returns>
        public Project UpdateProject(
            int id,
            string name = null,
            string description = null,
            Permission? defaultPermission = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixProject", id.ToString());
            parameters.Add("token", Token);

            // Add optional parameters if they are provided.
            if (name != null)
                parameters.Add("sName", name);

            if (description != null)
                parameters.Add("sDescription", description);

            if (defaultPermission != null)
                parameters.Add("permissionDefault", defaultPermission
                    .Value.ToString().ToLower());

            return Call<Project>(KilnApiCall.ProjectUpdate, parameters);
        }

        /// <summary>
        /// Get project with specified ID.
        /// </summary>
        /// <param name="id">A unique project identifier.</param>
        public Repository GetRepo(int id) {
            return Call<Repository>(KilnApiCall.Repo, new { ixRepo = id, token = Token });
        }

        /// <summary>
        /// Delete the repository with specified ID.
        /// </summary>
        /// <param name="id">A unique project identifier.</param>
        public bool DeleteRepo(int id) {
            return Call<bool>(KilnApiCall.RepoDelete, new { ixRepo = id, token = Token });
        }

        /// <summary>
        /// Create a new repository with specified parameters.
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        /// <param name="repoGroupID">The repository group the new repository will belong to.</param>
        /// <param name="isCentral">True if the new repository should be central; false otherwise.</param>
        /// <param name="description">The new repository description.</param>
        /// <param name="defaultPermission">The default repository permission.</param>
        /// <param name="parentID">
        /// The parent repository. If given, the parent repository
        /// will be branched to create the new repository
        /// </param>
        public Repository CreateRepo(
            string name,
            int repoGroupID,
            int? parentID = null,
            bool? isCentral = null,
            string description = null,
            Permission? defaultPermission = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("sName", name);
            parameters.Add("ixRepoGroup", repoGroupID.ToString());
            parameters.Add("token", Token);

            // Add optional parameters if they are provided.
            if (parentID != null)
                parameters.Add("ixParent", parentID.Value.ToString());

            if (isCentral != null)
                parameters.Add("fCentral", isCentral.Value.ToString());

            if (description != null)
                parameters.Add("sDescription", description);

            if (defaultPermission != null)
                parameters.Add("permissionDefault", defaultPermission
                    .Value.ToString().ToLower());

            return Call<Repository>(KilnApiCall.RepoCreate, parameters);
        }

        /// <summary>
        /// Update an existing repository with specified parameters.
        /// </summary>
        /// <param name="id">A unique repository identifier.</param>
        /// <param name="name">The new repository name.</param>
        /// <param name="parentID">The new repository parent.</param>
        /// <param name="repoGroupID">The new repository group to move the repository.</param>
        /// <param name="description">The new repository description.</param>
        /// <param name="defaultPermission">The new default repository permission.</param>
        /// <param name="isCentral">
        /// If true, converts the branch repository into a central repository.
        /// Can only be set for branch repositories.
        /// </param>
        public Repository UpdateRepo(
            int id,
            string name = null,
            int? parentID = null,
            bool? isCentral = null,
            int? repoGroupID = null,
            string description = null,
            Permission? defaultPermission = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixRepo", id.ToString());
            parameters.Add("token", Token);

            // Add optional parameters if they are provided.
            if (name != null)
                parameters.Add("sName", name);

            if (parentID != null)
                parameters.Add("ixParent", parentID.Value.ToString());

            if (isCentral != null)
                parameters.Add("fCentral", isCentral.Value.ToString());

            if (repoGroupID != null)
                parameters.Add("ixRepoGroup", repoGroupID.Value.ToString());

            if (description != null)
                parameters.Add("sDescription", description);

            if (defaultPermission != null)
                parameters.Add("permissionDefault", defaultPermission
                    .Value.ToString().ToLower());

            return Call<Repository>(KilnApiCall.RepoUpdate, parameters);
        }

        /// <summary>
        /// Returns a list of outgoing changesets.
        /// </summary>
        /// <param name="repoID">The source repository.</param>
        /// <param name="otherRepoID">The repository to compare to.</param>
        /// <param name="earliestChangeset">
        /// Only include changesets older than this outgoing changeset.</param>
        /// <param name="changesetLimit">
        /// The number of changesets to return (defaults to 30).</param>
        public Changeset[] GetOutgoingChangesets(
            int repoID,
            int otherRepoID,
            string earliestChangeset = "tip",
            int changesetLimit = 30) {

            return Call<Changeset[]>(KilnApiCall.RepoOutgoing, new {
                ixRepo = repoID.ToString(),
                ixOtherRepo = otherRepoID.ToString(),
                revOlderThan = earliestChangeset,
                nChangesetLimit = changesetLimit,
                token = Token
            });
        }

        /// <summary>
        /// Pushes outgoing changesets from source to target repository.
        /// </summary>
        /// <param name="sourceRepoID">The source repository.</param>
        /// <param name="targetRepoID">The target repository to push to.</param>
        /// <remarks>
        /// Requires read permissions to the source repository
        /// and write permissions to to the target repository. The target
        /// repository must contain a strict subset of the changesets
        /// in the source repository; the source must be related
        /// to the target; and the push must not create new heads
        /// in the target. Takes no other parameters.
        /// </remarks>
        public bool Push(int sourceRepoID, int targetRepoID) {
            return Call<bool>(KilnApiCall.RepoPush, new {
                ixRepo = sourceRepoID,
                ixTargetRepo = targetRepoID,
                token = Token
            });
        }

        /// <summary>
        /// Returns the changesets history of a repository.
        /// </summary>
        /// <param name="repoID">The unique repository identifier.</param>
        /// <param name="changesetLimit">
        /// Number of changesets to return, with a maximum of 100 (defaults to 10).</param>
        /// <param name="oldestChangeset">Revision of the oldest changeset to return.</param>
        public Changeset[] GetHistory(
            int repoID,
            int changesetLimit = 10,
            string oldestChangeset = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixRepo", repoID.ToString());
            parameters.Add("token", Token);
            parameters.Add("nChangesetLimit", changesetLimit.ToString());

            // Add optional parameters if they are provided.
            if (oldestChangeset != null)
                parameters.Add("revOldest", oldestChangeset);

            return Call<Changeset[]>(KilnApiCall.RepoHistory, parameters);
        }

        /// <summary>
        /// Returns the whole changesets history of a repository.
        /// </summary>
        /// <param name="repoID">The unique repository identifier.</param>
        public List<Changeset> GetAllHistory(int repoID) {
            var resultSet = new List<Changeset>();

            // The maximum number of changesets
            // that Kiln API 1.0 is able to return.
            const int MAX_NUMBER_OF_CHANGESETS = 100;

            // The number of changsesets that is returned by last request.
            int numberOfChangesets = 0;
            int iteration = 0;

            do {
                string oldestRevision = (iteration * MAX_NUMBER_OF_CHANGESETS).ToString();
                Changeset[] changesets = GetHistory(repoID, MAX_NUMBER_OF_CHANGESETS, oldestRevision);

                numberOfChangesets = changesets.Length;
                resultSet.AddRange(changesets.Reverse());

                iteration++;
            } while (numberOfChangesets >= 100);

            return resultSet;
        }

        /// <summary>
        /// Returns a changeset information.
        /// </summary>
        /// <param name="repoID">The unique repository identifier.</param>
        /// <param name="revision">Revision of the changeset.</param>
        public Changeset GetChangeset(int repoID, string revision) {
            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixRepo", repoID.ToString());
            parameters.Add("rev", revision);
            parameters.Add("token", Token);

            return Call<Changeset>(KilnApiCall.RepoHistoryRevision, parameters);
        }

        /// <summary>
        /// Returns information about the file at the specified revision.
        /// </summary>
        /// <param name="repoID">The unique repository identifier.</param>
        /// <param name="path">The byte path to the file.</param>
        /// <param name="revision">The revision to read the file from.</param>
        /// <param name="annotate">If true, returns annotation records as well.</param>
        public Cat GetFile(
            int repoID,
            string bytePath,
            string revision = null,
            bool annotate = false) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixRepo", repoID.ToString());
            parameters.Add("bpPath", bytePath);
            parameters.Add("token", Token);
            parameters.Add("fAnnotate", annotate.ToString());

            if (!string.IsNullOrWhiteSpace(revision))
                parameters.Add("rev", revision);

            return Call<Cat>(KilnApiCall.RepoFile, parameters);
        }

        /// <summary>
        /// Returns a list of manifest records at the specified revision.
        /// </summary>
        /// <param name="repoID">The unique repository identifier.</param>
        /// <param name="revision">The revision to get manifests from.</param>
        public Manifest[] GetManifests(int repoID, string revision = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixRepo", repoID.ToString());
            parameters.Add("token", Token);

            if (!string.IsNullOrWhiteSpace(revision))
                parameters.Add("rev", revision);

            return Call<Manifest[]>(KilnApiCall.RepoManifest, parameters);
        }

        /// <summary>
        /// Get repository group with specified ID.
        /// </summary>
        /// <param name="id">A unique repository group identifier.</param>
        public RepositoryGroup GetRepoGroup(int id) {
            return Call<RepositoryGroup>(KilnApiCall.RepoGroup,
                new { ixRepoGroup = id, token = Token });
        }

        /// <summary>
        /// Create a new repository group with specified parameters.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <param name="projectID">The project the new repository group will belong to.</param>
        public RepositoryGroup CreateRepoGroup(string name, int projectID) {
            return Call<RepositoryGroup>(KilnApiCall.RepoGroupCreate,
                new { sName = name, ixProject = projectID, token = Token });
        }

        /// <summary>
        /// Update an existing repository group with specified parameters.
        /// </summary>
        /// <param name="id">A unique repository group identifier.</param>
        /// <param name="name">The new repository group name.</param>
        public RepositoryGroup UpdateRepoGroup(int id, string name) {
            return Call<RepositoryGroup>(KilnApiCall.RepoGroupUpdate,
                new { sName = name, ixRepoGroup = id, token = Token });
        }

        /// <summary>
        /// Delete the repository group with specified ID.
        /// </summary>
        /// <param name="id">A unique repository group identifier.</param>
        public bool DeleteRepoGroup(int id) {
            return Call<bool>(KilnApiCall.RepoGroupDelete, new { ixRepoGroup = id, token = Token });
        }

        /// <summary>
        /// Returns the review case with specified ID and related changesets.
        /// </summary>
        /// <param name="id">A unique review identifier.</param>
        public Review GetReview(string id) {
            return Call<Review>(KilnApiCall.Review, new { ixReview = id, token = Token });
        }

        /// <summary>
        /// Update an existing review with specified parameters.
        /// </summary>
        /// <param name="id">A unique review identifier.</param>
        /// <param name="status">The new review status.</param>
        /// <param name="close">Determines whether to close the review.</param>
        /// <param name="reopen">Determines whether to reopen the review</param>
        /// <remarks>
        /// Some combinations of parameters are conflicting, such as
        /// closing and reopening a review at the same time. To address this,
        /// Kiln simply mandate that "fReopen" being true takes first
        /// precedence. A review closes in the "approved" state unless
        /// otherwise specified by "sStatus".
        /// </remarks>
        public Review UpdateReview(
            int id,
            ReviewStatus? status = null,
            bool? close = null,
            bool? reopen = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixReview", id.ToString());
            parameters.Add("token", Token);

            // Add optional parameters if they are provided.
            if (status != null)
                parameters.Add("sStatus", status.Value.ToString());

            if (close != null)
                parameters.Add("fClose", close.Value.ToString());

            if (reopen != null)
                parameters.Add("fReopen", reopen.Value.ToString());

            return Call<Review>(KilnApiCall.ReviewUpdate, parameters);
        }
        /// <summary>
        /// Creates a new review case.
        /// </summary>
        /// <param name="repoID">The repository to create the new review against.</param>
        /// <param name="personID">The person to assign the new review.</param>
        /// <param name="changesetRevisions">A list of changeset identifiers to review.</param>
        /// <param name="title">The title of the new review.</param>
        /// <param name="description">The initial comment of the new review.</param>
        public Review CreateReview(
            int repoID,
            int personID,
            string[] changesetRevisions,
            string title = null,
            string description = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixRepo", repoID.ToString());
            parameters.Add("ixPerson", personID.ToString());
            parameters.Add("token", Token);

            if (changesetRevisions != null && changesetRevisions.Length > 0)
                parameters.Add("ixChangesets", string.Join(",", changesetRevisions));

            // Add optional parameters if they are provided.
            if (title != null)
                parameters.Add("sTitle", title);

            if (description != null)
                parameters.Add("sDescription", description);

            return Call<Review>(KilnApiCall.ReviewCreate, parameters);
        }

        /// <summary>
        /// Returns code reviews assigned to you and opened by you.
        /// </summary>
        public ReviewsWrapper GetReviewsRelatedToMe() {
            return Call<ReviewsWrapper>(KilnApiCall.Reviews, new { token = Token });
        }

        /// <summary>
        /// Returns repository alias by its ID.
        /// </summary>
        /// <param name="aliasID">ID of the repository alias.</param>
        public Alias GetAlias(int aliasID) {
            return Call<Alias>(KilnApiCall.RepoAlias, new { ixRepoAlias = aliasID, token = Token });
        }

        /// <summary>
        /// Finds repository alias by project and alias name.
        /// </summary>
        /// <param name="projectSlug">Slugified project name.</param>
        /// <param name="aliasSlug">Slugified project name.</param>
        public Alias FindAlias(string projectSlug, string aliasSlug) {
            return Call<Alias>(KilnApiCall.RepoAliasFind,
                new { sProjectSlug = projectSlug, sAliasSlug = aliasSlug, token = Token });
        }

        /// <summary>
        /// Creates repository alias with specified name.
        /// </summary>
        /// <param name="name">The name of the alias.</param>
        /// <param name="repoID">The repository ID to attach the alias to.</param>
        public Alias CreateAlias(string name, int repoID) {

            if (!IsAliasNameValid(name))
                throw new ArgumentException("Specified name of the alias is not valid.", "name");

            return Call<Alias>(KilnApiCall.RepoAliasCreate,
                new { ixRepo = repoID, sName = name, token = Token });
        }

        /// <summary>
        /// Deletes alias with specified ID.
        /// </summary>
        /// <param name="aliasID">Unique identifier of the alias.</param>
        public object DeleteAlias(int aliasID) {
            return Call<object>(KilnApiCall.RepoAliasDelete, new { ixRepoAlias = aliasID, token = Token });
        }

        /// <summary>
        /// Updates repository alias with specified ID.
        /// </summary>
        /// <param name="aliasID">Unique identifier of the alias.</param>
        /// <param name="name">New name of the alias.</param>
        /// <param name="repoID">New repository to attach alias to.</param>
        public Alias UpdateAlias(int aliasID, string name = null, int? repoID = null) {

            // Add required parameters.
            var parameters = new Dictionary<string, string>();
            parameters.Add("ixRepoAlias", aliasID.ToString());
            parameters.Add("token", Token);

            if (name == null && repoID == null) {
                throw new InvalidOperationException(
                    "Nothing to update. At least one of the alias parameters should be specified.");
            }

            // Add optional parameters.
            if (name != null) {
                if (!IsAliasNameValid(name))
                    throw new ArgumentException("Specified name of the alias is not valid.", "name");
                parameters.Add("sName", name);
            }

            if (repoID != null)
                parameters.Add("ixRepo", repoID.ToString());

            return Call<Alias>(KilnApiCall.RepoAliasUpdate, parameters);
        }

        /// <summary>
        /// Validate alias name.
        /// </summary>
        /// <param name="name">Name of the alias.</param>
        /// <returns>Returns true on success.</returns>
        private bool IsAliasNameValid(string name) {
            return Regex.IsMatch(name, @"^[\w- \(\)]+$");
        }
    }
}
