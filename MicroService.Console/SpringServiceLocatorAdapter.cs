using ServiceLocation;
using Spring.Objects.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.ConsoleApp
{
    public class SpringServiceLocatorAdapter : ServiceLocatorImplBase
    {
  
        private readonly IListableObjectFactory _factory;

        
        public SpringServiceLocatorAdapter(IListableObjectFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory", "A factory is mandatory");
            }
            _factory = factory;
        }

     
        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (key == null)
            {
                IEnumerator it = DoGetAllInstances(serviceType).GetEnumerator();
                if (it.MoveNext())
                {
                    return it.Current;
                }
                throw new ObjectCreationException(string.Format("no services of type '{0}' defined", serviceType.FullName));
            }
            return _factory.GetObject(key, serviceType);
        }

      
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            foreach (object o in _factory.GetObjectsOfType(serviceType).Values)
            {
                yield return o;
            }
        }
    }
}
