using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework
{
    public abstract class Aggregate<T>:IAggregateRoot<T>
    {
        private readonly IList<IEvent> _uncommittedEvents = new List<IEvent>();
        public Aggregate(T id)
        {
            this.Id = id;
        }
        public T Id { get;private set; }
        public int Version { get; set; }

        public IEnumerable<IEvent> GetUncommittedEvents()
        {
            return new List<IEvent>(_uncommittedEvents);
        }

        public void AcceptUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public void LoadsFromHistory(IEnumerable<IEvent> events)
        {
            foreach (var evt in events)
            {
                InvokeEventMethod(evt);
            }
        }

        protected void ApplyChange(IEvent evt)
        {
            _uncommittedEvents.Add(evt);

            InvokeEventMethod(evt);
        }

         void InvokeEventMethod(IEvent evt)
        {
            var args = new object[] { evt };

            var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic )
                .Where(m => m.Name == "Apply")
                .Where(m =>
                {
                    ParameterInfo[] parameters = m.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == evt.GetType();
                });

            var method = methods.FirstOrDefault();

            if (method != null)
            {
                method.Invoke(this, args);
            }
        }
    }
}
