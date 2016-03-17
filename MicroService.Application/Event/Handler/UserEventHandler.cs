using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Event.Handler
{
    public class UserEventHandler : IObserver<AddUserEvent>
    {
        public void OnCompleted()
        {
             
        }

        public void OnError(Exception error)
        {
        
        }

        public void OnNext(AddUserEvent value)
        {
            Console.WriteLine("Event:{0}", value.Name);
        }
    }
}
