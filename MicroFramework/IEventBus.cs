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
        void Publish(System.Collections.Generic.IEnumerable<MicroFramework.IEvent> events);
        IObservable<TEvent> ToMessage<TEvent>();
    }
}
