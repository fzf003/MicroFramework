using ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace MicroService.ConsoleApp
{
    public class TinyIoCServiceLocator : ServiceLocatorImplBase
    {
        public TinyIoCServiceLocator()
        {

        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            return key != null ? TinyIoCContainer.Current.Resolve(serviceType, key) : TinyIoCContainer.Current.Resolve(serviceType);
        }


        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            return TinyIoCContainer.Current.ResolveAll(serviceType);

        }
    }
}
