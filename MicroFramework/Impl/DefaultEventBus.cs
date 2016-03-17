using System;
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

        public DefaultEventBus()
        {
            this._eventsource = new ReplaySubject<IEvent>();
        }

        public void Publish(IEvent @event)
        {
            this._eventsource.OnNext(@event);
        }

        public void Publish(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                Publish(@event);
            }
        }

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
    }
}
