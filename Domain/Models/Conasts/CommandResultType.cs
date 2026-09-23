namespace Domain.Models.Conasts
{
    public enum CommandResultType
    {
        CreationSuccess,
        CreationFailed,
        UpdateSuccess,
        UpdateFailed,
        DeleteSuccess,
        DeleteFailed,
        NotFound,
        GenericError,
        LockUserSuccess,
        LockUserFailed,
        UnLockUserSuccess,
        UnLockUserFailed,
        UpdateUnitRating,
        CreationGroupSuccess,
        CreationGroupFailed,
        UpdateGroupSuccess,
        UpdateGroupFailed,
        DeleteGroupSuccess,
        DeleteGroupFailed,
        CopyFromGroupsSuccess,
        CopyFromGroupsFailed,
        DuplicateSuccess,
        DuplicateFailed,

    }

}
