namespace KilnApi {
    /// <summary>
    /// Defines all erros that can be returned by Kiln API 1.0
    /// </summary>
    public enum KilnErrorCode {
        /// <summary>
        /// Special reserved code. Means that the library doesn't
        /// know anything about an error with recieved code.
        /// </summary>
        Unknown = 0,

        #region Common errors that can occur on every call

        /// <summary>
        /// An invalid authentication token was passed.
        /// </summary>
        InvalidToken,

        /// <summary>
        /// You lack the proper permissions to perform this call.
        /// </summary>
        Forbidden,

        /// <summary>
        /// You left out a non-optional parameter.
        /// </summary>
        PropertyIsRequired,

        /// <summary>
        /// The string you passed is too long.
        /// </summary>
        ValueTooLong,

        /// <summary>
        /// The string you passed is too long.
        /// </summary>
        ValueXTooLong,

        /// <summary>
        /// There was an unknown problem with the backend.
        /// </summary>
        ProblemWithBackend,

        #endregion

        #region Errors specific for project calls

        /// <summary>
        /// Project names cannot be a reserved file name.
        /// </summary>
        CannotUseReservedFileName,

        /// <summary>
        /// Project names must contain enough alphanumeric characters to generate a slug.
        /// </summary>
        ProjectNameInsufficient,

        /// <summary>
        /// Project names cannot consist of all numbers.
        /// </summary>
        ProjectNameNumerical,

        /// <summary>
        /// Project names must be unique.
        /// </summary>
        ProjectNameIdentical,

        /// <summary>
        /// Project name too similar to another project name.
        /// </summary>
        NameTooSimilar,

        /// <summary>
        /// Invalid default permission.
        /// </summary>
        DefaultPermissionInvalid,

        /// <summary>
        /// Project descriptions must be at most 100 characters long.
        /// </summary>
        ProjectDescriptionTooLong,

        #endregion

        #region Errors specific for repository calls

        RepoNameHasInvalidChars,
        BranchRepoMustHaveParent,

        /// <summary>
        /// Invalid repository group.
        /// </summary>
        InvalidRepositoryGroup,

        /// <summary>
        /// Invalid default permission passed.
        /// </summary>
        InvalidPermission,

        /// <summary>
        /// Unable to create the repository due to backend exception.
        /// </summary>
        CouldNotCreateRepo,

        /// <summary>
        /// You cannot convert a central repository into a branch repository.
        /// </summary>
        CannotMakeCentralRepoABranch,

        /// <summary>
        /// No repository passed to compare to for outgoing changesets.
        /// </summary>
        NoRepoToCompareTo,

        /// <summary>
        /// Invalid repository to compare to for outgoing changesets.
        /// </summary>
        InvalidComparison,

        /// <summary>
        /// Invalid changeset or changesets specified or occurred.
        /// </summary>
        ChangesetNotFound,

        /// <summary>
        /// Pushing to this repository would create a new head.
        /// </summary>
        PushFailNotStrictSubset,

        /// <summary>
        /// The destination of the push contains changesets
        /// not in the source repository.
        /// </summary>
        PushFailNotRelated,

        /// <summary>
        /// Pushing to this repository would create a new head.
        /// </summary>
        PushFailCreatesNewHeads,

        /// <summary>
        /// Unknown pushing error occurred.
        /// </summary>
        PushFailUnknown,

        /// <summary>
        /// An invalid range of changesets was specified for History.
        /// </summary>
        InvalidRange,

        /// <summary>
        /// File was called on a directory, not a file.
        /// </summary>
        IsADirectory,

        /// <summary>
        /// File was called on a file not found.
        /// </summary>
        FileNotFound,

        /// <summary>
        /// An empty list of revision tails was passed to Related.
        /// </summary>
        InvalidRevisionTails,

        /// <summary>
        /// An identically named repository already exists in this group.
        /// </summary>
        RepoNameAlreadyUsed,

        /// <summary>
        /// Repo descriptions must be less than 200 characters long.
        /// </summary>
        RepoDescriptionTooLong,

        #endregion

        #region Errors specific for repository group calls

        /// <summary>
        /// You passed an invalid ixProject.
        /// </summary>
        InvalidProject,

        /// <summary>
        /// You cannot delete the last repository group in a project.
        /// </summary>
        CannotDeleteLastRepoGroup,

        /// <summary>
        /// A repository group's name may only contain alphanumeric
        /// characters (in addition to hyphens, spaces, and parentheses).
        /// </summary>
        GroupNameHasInvalidChars,

        /// <summary>
        /// Repository group name too similar to another repository
        /// group name, creating an invalid slug.
        /// </summary>
        NameTooSimilarToGroup,

        /// <summary>
        /// Another repository group is already untitled,
        /// and you cannot create another one.
        /// </summary>
        DuplicateUntitledRepositoryGroupCreate,

        /// <summary>
        /// Same as above, but you are trying to rename
        /// an existing repository to be untitled.
        /// </summary>
        DuplicateUntitledRepositoryGroup,

        /// <summary>
        /// Another repository group with the same name already exists.
        /// </summary>
        RepoGroupNameIdentical,

        #endregion

        #region Errors specific for review calls

        /// <summary>
        /// Invalid repository passed.
        /// </summary>
        InvalidRepository,

        /// <summary>
        /// Invalid person specified.
        /// </summary>
        InvalidPerson,

        /// <summary>
        /// Empty list of changesets passed or
        /// the list contains an invalid changeset.
        /// </summary>
        InvalidChangesets,

        /// <summary>
        /// There was an unknown problem with the Kiln-to-FogBugz request.
        /// </summary>
        CannotCreateReview,

        /// <summary>
        /// Invalid review status passed.
        /// </summary>
        InvalidStatus,

        #endregion
    }
}