using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework
{
    public interface IAggregateRoot
    {
        void AcceptUncommittedEvents();
        System.Collections.Generic.IEnumerable<IEvent> GetUncommittedEvents();
        void LoadsFromHistory(System.Collections.Generic.IEnumerable<IEvent> events);
        int Version { get; set; }
    }

    public interface IAggregateRoot<TKey>:IAggregateRoot
    {
 
        TKey Id { get; }
     
    }

}
