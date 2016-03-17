using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Domain
{
    public class User : IAggregateRoot
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int Id { get; set; }

        public void ChangeName(string newname)
        {
            this.Name = newname;
        }
    }
}
