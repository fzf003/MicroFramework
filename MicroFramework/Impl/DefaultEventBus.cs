using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework.Impl
{
    public class DefaultEventBus : IEventBus
    {
        private ReplaySubject<IEvent> _eventsource;

        //ConcurrentDictionary<string, ISubject<IEvent>> subjectstore = new ConcurrentDictionary<string, ISubject<IEvent>>();

        public DefaultEventBus()
        {
            this._eventsource = new ReplaySubject<IEvent>();
        }

        /* public async Task Publish(string topicname, IEvent @event)
         {
             ISubject<IEvent> currsubject;
             if (!subjectstore.TryGetValue(topicname, out currsubject))
             {
                 currsubject = subjectstore.GetOrAdd(topicname, new ReplaySubject<IEvent>());
                 await Task.Run(() => currsubject.OnNext(@event));

             }
             else
             {
                 await Task.Run(() => currsubject.OnNext(@event));

             }

         }

         public async Task Publish(string topicname, IEnumerable<IEvent> events)
         {
             foreach (var @event in events)
             {
                 await Publish(topicname, @event);
             }
         }*/

        public IObservable<TEvent> ToMessage<TEvent>()
        {
            return GetEvents().OfType<TEvent>();
        }

        IObservable<IEvent> GetEvents()
        {
            var src = _eventsource.ObserveOn(TaskPoolScheduler.Default);

            return ToEvent(src);
        }

        IObservable<T> ToEvent<T>(IObservable<T> source)
        {
            return Observable.Create<T>(observer =>
            {
                var d = source.Subscribe(observer);

                return Disposable.Create(() =>
                {

                    d.Dispose();
                });
            });
        }

        public void Publish(IEvent @event)
        {
            this._eventsource.OnNext(@event);
        }

        public void Publish(IEnumerable<IEvent> events)
        {
           foreach(var @event in events)
           {
               Publish(@event);
           }
        }
    }
}

