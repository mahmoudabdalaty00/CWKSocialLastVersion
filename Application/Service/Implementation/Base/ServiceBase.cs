using Application.Helper;
using Application.Service.Interface.Base;
using Domain.Models.Conasts;

namespace Application.Service.Implementation.Base
{

    public abstract class ServiceBase : IService

    {
        public event ServiceLoggerEventHandler ServiceLogger;

        protected void PushLog(string message)
        {
            //invokes the event handler and fires the queued events
            ServiceLogger?.Invoke(this, message);
        }
    }

    public abstract class ServiceBase<TEntity> : IService

    {
        public event ServiceLoggerEventHandler ServiceLogger;

        protected void PushLog(string message)
        {
            ServiceLogger?.Invoke(this, message);
        }

        protected string Message(CommandResultType? type = null, string? itemName = null, string? itemId = null) => type switch
            {
            // Generic Messages for crud
            CommandResultType.CreationSuccess => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).SuccessfullyCreated,
            CommandResultType.CreationFailed => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).CreateFailed,
            CommandResultType.UpdateSuccess => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).UpdateSuccess,
            CommandResultType.UpdateFailed => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).UpdateFailed,
            CommandResultType.DeleteSuccess => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).DeleteSuccess,
            CommandResultType.DeleteFailed => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).DeleteFailed,
            CommandResultType.NotFound => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).NotFound,


                // Generic Messages for other actions
                CommandResultType.CopyFromGroupsSuccess => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).CopyFromGroupsSuccess,
                CommandResultType.CopyFromGroupsFailed => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).CopyFromGroupFailed,
                CommandResultType.LockUserSuccess => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).LockUserSuccess,
                CommandResultType.LockUserFailed => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).LockUserFailed,
                CommandResultType.UnLockUserSuccess => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).UnLockUserSuccess,
                CommandResultType.UnLockUserFailed => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).UnLockUserFailed,
                CommandResultType.UpdateUnitRating => new GenericResponseMessages(typeof(TEntity).Name, itemName, itemId).UpdateUnitRating,
                _ => "An Error Has Occured"
        };


    }
}
