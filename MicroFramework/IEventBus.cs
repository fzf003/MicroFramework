using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework
{
    public interface IEventBus
    {
        void Publish(MicroFramework.IEvent @event);
        //Task Publish(string topicname, IEvent @event);
        void Publish(System.Collections.Generic.IEnumerable<MicroFramework.IEvent> events);
        //Task Publish(string topicname, IEnumerable<IEvent> events);
        IObservable<TEvent> ToMessage<TEvent>();
    }
}
