using MicroService.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Command
{
    /// <summary>
    /// 添加用户
    /// </summary>
    public class AddUserCommand : BaseCommand<User>
    {
        public string Name
        {
            get;
            set;
        }

        public int Age { get; set; }
    }
}
