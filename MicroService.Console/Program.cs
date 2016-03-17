using Autofac;
using Autofac.Core;
using MicroFramework;
using MicroFramework.Impl;
using MicroService.Application.Command;
using MicroService.Application.Command.Handler;
using MicroService.Application.Event;
using ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using MicroService.Application.Query;
using System.Reflection;
using TinyIoC;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Support;
using Spring.Context;
using Spring.Context.Support;
using MicroService.Application.Event.Handler;
using System.Reactive.Linq;
using MicroService.Application.Domain;
using System.Reactive;
using System.Reactive.Disposables;
namespace MicroService.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Init();

            var bus = ServiceLocator.Current.GetInstance<IServiceBus>();
            
            bus.ToSubscribe<AddUserEvent>(new UserEventHandler());

            Enumerable.Range(1, 20).Select(p =>
            {
                var command = new AddUserCommand() { Name = "dsd-", CommandId = Guid.NewGuid() };

                bus.SendAsync(command).ToObservable().Subscribe(r =>
                {
                    Console.WriteLine(r.Status + "|" + r.Result + "|" + r.CommandId);
                    bus.PublishAsync(new AddUserEvent() { Name = r.MessageBody.ToString() });
                });
                return p;
            }).ToArray();
          

            Console.WriteLine("Query:{0}", bus.QueryAsync(new GetAll()).Result);
            Console.WriteLine(".....");
            Console.ReadKey();
        }

        static void Init()
        {
            UseAutofac();
        }

        static void UseSpringNet()
        {
            ServiceLocator.SetLocatorProvider(() => new SpringServiceLocatorAdapter(ContextRegistry.GetContext()));
        }

        static void UseTinyIoC()
        {
            var continer = TinyIoCContainer.Current;
            continer.Register<IEventBus, DefaultEventBus>();
            continer.Register<ICommandBus, DefaultCommandBus>();
            continer.Register<IQueryBus, DefaultQueryBus>();
            continer.Register<IServiceBus, DefaultServiceBus>();
            continer.Register<IQueryHandler<GetAll, List<User>>,QueryUserHandler>();
            continer.Register<ICommandHandler<AddUserCommand>>((c, p) => new UserCommandHandler());
            ServiceLocator.SetLocatorProvider(() => new TinyIoCServiceLocator());
        }

        static void UseAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.Load("MicroService.Application"), Assembly.Load("MicroFramework")).AsImplementedInterfaces();
            builder.RegisterType<DefaultEventBus>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<DefaultCommandBus>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<DefaultQueryBus>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<DefaultServiceBus>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<DefaultServiceBus>().AsImplementedInterfaces().SingleInstance();
            var Container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));
        }
    }
}
