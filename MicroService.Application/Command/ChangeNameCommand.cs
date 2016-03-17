using MicroService.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Command
{
    /// <summary>
    /// 更改用户名
    /// </summary>
    public class ChangeNameCommand : BaseCommand<User>
    {
        public string Name
        {
            get;
            set;
        }

        public int Id { get; set; }
    }
}
