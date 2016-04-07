using MicroFramework;
using MicroService.Application.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Domain
{
    public class User : Aggregate<string>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public User()
            : base(Guid.NewGuid().ToString("N"))
        {
        }
        public User(string id) : base(id) { }

      private void Apply(AddUserEvent @event)
        {
            this.Name = @event.Name;
            this.Age = @event.Age;
            Console.WriteLine("修改:"+@event.Name + "|" + @event.Age + "|" + @event.Id);
        }

        public void ChangeName(string newname)
        {

            this.ApplyChange(
                new AddUserEvent()
                {
                    Name = newname,
                     Id=this.Id
                }
            );
        }
    }

    

}
