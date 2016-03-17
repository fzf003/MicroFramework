using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Event
{
    public class AddUserEvent : IEvent
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
