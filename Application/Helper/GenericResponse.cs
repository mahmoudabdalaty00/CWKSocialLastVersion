using Domain.Models.Conasts;

namespace Application.Helper
{

    public class GenericResponseMessages
    {
        private string _subject;
        private readonly string? _itemName;
        private readonly string? _itemId;
        public GenericResponseMessages(string subject, string? itemName = null, string? itemId = null)
        {
            _subject = subject;
            _itemName = itemName;
            _itemId = itemId;
        }

        private string FormatMessage(string message)
             => _itemName != null && _itemId != null
                 ? $"The {_itemName} {_subject} {message} with Id {_itemId}"
                 : _itemName != null ? $"The {_itemName} {_subject} {message}" : $"The {_subject} {message} with Id {_itemId}";

        // CRUD Messages
        public string SuccessfullyCreated => FormatMessage("Was Created Successfully");
        public string CreateFailed => FormatMessage("Creation Process Failed");
        public string UpdateSuccess => FormatMessage("Was Updated Successfully");
        public string UpdateFailed => FormatMessage("Update Process Failed");
        public string DeleteSuccess => FormatMessage("Was Deleted Successfully");
        public string DeleteFailed => FormatMessage("Delete Process Failed");
        public string NotFound => FormatMessage("Was Not Found");

        // Other Action Messages
        public string CopyFromGroupsSuccess => FormatMessage("Copy From Groups Was Created Successfully");
        public string CopyFromGroupFailed => FormatMessage("Copy From Groups Creation Process Failed");
        public string UpdateUnitRating => "Update Unit Rate Processed Successfully";
        public string LockUserSuccess => FormatMessage("Succeeded To Lock User");
        public string LockUserFailed => FormatMessage("Failed To Lock User");
        public string UnLockUserSuccess => FormatMessage("Succeeded To Unlock User");
        public string UnLockUserFailed => FormatMessage("Failed To Unlock User");
    }

    public class GenericResponse
    {
        public static string Message<T>(T CreatedType, CommandResultType? type = null) => type switch
        {
            CommandResultType.CreationSuccess => new GenericResponseMessages(nameof(T)).SuccessfullyCreated,
            CommandResultType.CopyFromGroupsSuccess => new GenericResponseMessages(nameof(T)).CopyFromGroupsSuccess,
            CommandResultType.CopyFromGroupsFailed => new GenericResponseMessages(nameof(T)).CopyFromGroupFailed,
            CommandResultType.CreationFailed => new GenericResponseMessages(nameof(T)).CreateFailed,
            CommandResultType.UpdateSuccess => new GenericResponseMessages(nameof(T)).UpdateSuccess,
            CommandResultType.UpdateFailed => new GenericResponseMessages(nameof(T)).UpdateFailed,
            CommandResultType.DeleteSuccess => new GenericResponseMessages(nameof(T)).DeleteSuccess,
            CommandResultType.DeleteFailed => new GenericResponseMessages(nameof(T)).DeleteFailed,
            CommandResultType.NotFound => new GenericResponseMessages(nameof(T)).NotFound,
            _ => "An Error Has Occured"
        };


    }
}
