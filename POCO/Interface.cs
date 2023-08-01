using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.POCO
{
    /// <summary>
    /// All Interfaces
    /// </summary>
    public interface IEvent
    {
        Guid MsgCorelationId { get; set; }
        string ServiceOperation { get; set; }
    }

    public interface IEventReponse
    {
        Guid MsgCorelationId { get; set; }
        string ServiceOperation { get; set; }
    }

    public interface IEventErrorResponse
    {
        Guid MsgCorelationId { get; set; }
        string ErrorMessage { get; set; }
    }

    public interface IIntegrationHanlder<T, U, V>
    {
        void Execute(T args, Action<U> callback, Action<V> errorCallBack);
    }

    public interface IEventBus
    {
        event EventHandler<MessageAddIntegrationEvent> OnMessageAdd;
        void Trigger(IEvent msg, Action<string, IEvent, Action<IEventReponse>, Action<IEventErrorResponse>> integrationHanlder, Action<IEventReponse> callback, Action<IEventErrorResponse> errorCallback);
    }
}
