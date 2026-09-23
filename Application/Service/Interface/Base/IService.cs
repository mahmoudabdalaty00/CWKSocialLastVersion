namespace Application.Service.Interface.Base
{
    public interface IService
    {
        //a refrence to the eventlogger handler, the event handler shall be provided with the event listeners in the controller
        event ServiceLoggerEventHandler ServiceLogger;
    }

    //function declaration for the service logger event
    public delegate void ServiceLoggerEventHandler(object eventInvoker, string message);
}