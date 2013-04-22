using System.Collections.Generic;
using KilnApi.Helpers;

namespace KilnApi {
    /// <summary>
    /// Represents defenitions for all available Kiln API 1.0 calls.
    /// </summary>
    public sealed class KilnApiCall {
        /// <summary>
        /// URL's path that specific for the call.
        /// </summary>
        /// <remarks>
        /// Could contain placeholders for parameters.
        /// </remarks>
        public string Path { get; private set; }

        /// <summary>
        /// URL's path with required query string parameters.
        /// </summary>
        /// <remarks>
        /// Could contain placeholders for parameters.
        /// </remarks>
        public string PathWithRequiredParameters { get; private set; }

        /// <summary>
        /// HTTP method that will be used during request.
        /// </summary>
        public string HttpMethod { get; private set; }

        /// <summary>
        /// Private constructor that initializes Path
        /// and PathWithRequiredParameters properties.
        /// </summary>
        private KilnApiCall(string path, string queryString, string httpMethod) {
            Path = ApiCommonPart + path;
            PathWithRequiredParameters = Path + queryString;
            HttpMethod = httpMethod;
        }

        /// <summary>
        /// Build up the path with specified parameters.
        /// </summary>
        /// <param name="parameters">Strongly typed parameters dictionary for an URL.</param>
        public string GetPathAndQuery(IDictionary<string, string> parameters) {
            return PatternParser.MatchUrlPlaceholders(PathWithRequiredParameters, parameters);
        }

        /// <summary>
        /// Build up the path with specified parameters.
        /// </summary>
        /// <param name="parameters">Anonymous type that represents parameters for an URL.</param>
        public string GetPathAndQuery(object parameters) {
            return PatternParser.MatchUrlPlaceholders(PathWithRequiredParameters, parameters);
        }

        /// <summary>
        /// Common part of the URL for all Kiln API 2.0 calls.
        /// </summary>
        private const string ApiCommonPart = "Api/2.0/";

        /// <summary>
        /// Represents authentication Kiln API call.
        /// </summary>
        public static readonly KilnApiCall AuthLogin = new KilnApiCall(
            path: "Auth/Login",
            queryString: "?sUser={sUser}&sPassword={sPassword}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that returns
        /// a list of all persons readable by the user.
        /// </summary>
        public static readonly KilnApiCall Persons = new KilnApiCall(
            path: "Person",
            queryString: "?token={token}",
            httpMethod: "GET"
		);  
		
		/// <summary>
		/// Represents the Kiln API call that returns
		/// a list of changesets for a bug
		/// </summary>
		public static readonly KilnApiCall Bug = new KilnApiCall(
			path: "Bug/{ixBug}",
			queryString: "?token={token}",
			httpMethod: "GET"
		);

		/// <summary>
		/// Represents the Kiln API call that returns
		/// a list of changesets for a bug
		/// </summary>
		public static readonly KilnApiCall Related = new KilnApiCall(
			path: "Repo/Related/",
			queryString: "?token={token}&revTails={revTails}",
			httpMethod: "GET"
		);

        /// <summary>
        /// Represents the Kiln API call that returns
        /// a list of all projects.
        /// </summary>
        public static readonly KilnApiCall Projects = new KilnApiCall(
            path: "Project",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that returns
        /// the project with specified ID.
        /// </summary>
        public static readonly KilnApiCall Project = new KilnApiCall(
            path: "Project/{ixProject}",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that creates a new project.
        /// </summary>
        public static readonly KilnApiCall ProjectCreate = new KilnApiCall(
            path: "Project/Create",
            queryString: "?token={token}&sName={sName}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that updates an existing project.
        /// </summary>
        public static readonly KilnApiCall ProjectUpdate = new KilnApiCall(
            path: "Project/{ixProject}",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that deletes specified project.
        /// </summary>
        public static readonly KilnApiCall ProjectDelete = new KilnApiCall(
            path: "Project/{ixProject}/Delete",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that returns
        /// the repository with specified ID.
        /// </summary>
        public static readonly KilnApiCall Repo = new KilnApiCall(
            path: "Repo/{ixRepo}",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that creates a new repository.
        /// </summary>
        public static readonly KilnApiCall RepoCreate = new KilnApiCall(
            path: "Repo/Create",
            queryString: "?token={token}&sName={sName}&ixRepoGroup={ixRepoGroup}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that updates an existing repository.
        /// </summary>
        public static readonly KilnApiCall RepoUpdate = new KilnApiCall(
            path: "Repo/{ixRepo}",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that deletes specified repository.
        /// </summary>
        public static readonly KilnApiCall RepoDelete = new KilnApiCall(
            path: "Repo/{ixRepo}/Delete",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that returns a list of outgoing changesets.
        /// </summary>
        public static readonly KilnApiCall RepoOutgoing = new KilnApiCall(
            path: "Repo/{ixRepo}/Outgoing",
            queryString: "?token={token}&ixOtherRepo={ixOtherRepo}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that pushes outgoing changesets
        /// from source to target repository.
        /// </summary>
        public static readonly KilnApiCall RepoPush = new KilnApiCall(
            path: "Repo/{ixRepo}/Push/{ixTargetRepo}",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that returns
        /// the changesets history of a repository.
        /// </summary>
        public static readonly KilnApiCall RepoHistory = new KilnApiCall(
            path: "Repo/{ixRepo}/History",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that returns
        /// a changeset information.
        /// </summary>
        public static readonly KilnApiCall RepoHistoryRevision = new KilnApiCall(
            path: "Repo/{ixRepo}/History/{rev}",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Returns information about the file at the specified revision.
        /// </summary>
        public static readonly KilnApiCall RepoFile = new KilnApiCall(
            path: "Repo/{ixRepo}/File/{bpPath}",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Returns a list of manifest records at the specified revision.
        /// </summary>
        public static readonly KilnApiCall RepoManifest = new KilnApiCall(
            path: "Repo/{ixRepo}/Manifest",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that creates a new repository group.
        /// </summary>
        public static readonly KilnApiCall RepoGroupCreate = new KilnApiCall(
            path: "RepoGroup/Create",
            queryString: "?sName={sName}&ixProject={ixProject}&token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that returns
        /// the repository group with specified ID.
        /// </summary>
        public static readonly KilnApiCall RepoGroup = new KilnApiCall(
            path: "RepoGroup/{ixRepoGroup}",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that updates
        /// an existing repository group.
        /// </summary>
        public static readonly KilnApiCall RepoGroupUpdate = new KilnApiCall(
            path: "RepoGroup/{ixRepoGroup}",
            queryString: "?sName={sName}&token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that deletes specified repository group.
        /// </summary>
        public static readonly KilnApiCall RepoGroupDelete = new KilnApiCall(
            path: "RepoGroup/{ixRepoGroup}/Delete",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that creates a new review case.
        /// </summary>
        public static readonly KilnApiCall ReviewCreate = new KilnApiCall(
            path: "Review/Create",
            queryString: "?ixRepo={ixRepo}&ixPerson={ixPerson}&ixChangesets={ixChangesets}&token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that returns the review
        /// case with specified ID and related changesets.
        /// </summary>
        public static readonly KilnApiCall Review = new KilnApiCall(
            path: "Review/{ixReview}",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that updates
        /// an existing review case.
        /// </summary>
        public static readonly KilnApiCall ReviewUpdate = new KilnApiCall(
            path: "Review/{ixReview}",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that returns
        /// code reviews assigned to you and opened by you.
        /// </summary>
        public static readonly KilnApiCall Reviews = new KilnApiCall(
            path: "Reviews",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that
        /// fetches repository alias.
        /// </summary>
        public static readonly KilnApiCall RepoAlias = new KilnApiCall(
            path: "RepoAlias/{ixRepoAlias}",
            queryString: "?token={token}",
            httpMethod: "GET"
        );

        /// <summary>
        /// Represents the Kiln API call that updates
        /// specified repository alias.
        /// </summary>
        public static readonly KilnApiCall RepoAliasUpdate = new KilnApiCall(
            path: "RepoAlias/{ixRepoAlias}",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that creates repository alias
        /// with specified name for specified repository.
        /// </summary>
        public static readonly KilnApiCall RepoAliasCreate = new KilnApiCall(
            path: "RepoAlias/Create",
            queryString: "?ixRepo={ixRepo}&sName={sName}&token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that deletes
        /// specified repository alias.
        /// </summary>
        public static readonly KilnApiCall RepoAliasDelete = new KilnApiCall(
            path: "RepoAlias/{ixRepoAlias}/Delete",
            queryString: "?token={token}",
            httpMethod: "POST"
        );

        /// <summary>
        /// Represents the Kiln API call that returns
        /// repository alias by project and alias name.
        /// </summary>
        public static readonly KilnApiCall RepoAliasFind = new KilnApiCall(
            path: "RepoAlias/Find",
            queryString: "?sProjectSlug={sProjectSlug}&sAliasSlug={sAliasSlug}&token={token}",
            httpMethod: "GET"
        );
    }
}
