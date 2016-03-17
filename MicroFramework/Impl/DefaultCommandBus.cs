using ServiceLocation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework.Impl
{
    public class DefaultCommandBus : ICommandBus
    {
        class CommandExecutionDetails
        {
            public Type CommandHandlerType { get; set; }
            public Func<ICommandHandler, ICommand, Task<object>> Invoker { get; set; }
            public ICommandContext CommandContext { get; set; }
        }

        ConcurrentDictionary<Type, CommandExecutionDetails> CommandExecutionMap = new ConcurrentDictionary<Type, CommandExecutionDetails>();
 
        private readonly CommandResultProcessor _CommandResultProcessor;
        private readonly BlockingCollection<ICommand> _ExecuteCommandQueue;
        private readonly Worker _worker;
        public DefaultCommandBus()
        {
            this._CommandResultProcessor = new CommandResultProcessor();
            this._ExecuteCommandQueue = new BlockingCollection<ICommand>(new ConcurrentQueue<ICommand>());
            this._worker = new Worker("ExecuteCommand", () => ProcessMessage(this._ExecuteCommandQueue.Take()));
            this._worker.Start();
        }

        public Task<CommandResult> Dispatch(ICommand command)
        {
            var tcs = new TaskCompletionSource<CommandResult>();

            this._CommandResultProcessor.RegisterProcessingCommand(command, tcs);

            this._ExecuteCommandQueue.Add(command);
                       
            return tcs.Task;
        }


        void ProcessMessage(ICommand command)
        {
            
            var handler = GetCommandExecutionDetails(command.GetType());

            var handle = ServiceLocator.Current.GetInstance(handler.CommandHandlerType);

            var context = new DefaultCommandContext(command, (ICommandHandler)handle);
            try
            {
                handler.Invoker(context.CommandHandler, context.Command).ContinueWith(task =>
                {
                    if (task.Status == TaskStatus.RanToCompletion)
                    {
                        this._CommandResultProcessor.ProcessSuccessCommand(context.Command, task.Result);
                    }

                    if (task.Status == TaskStatus.Faulted)
                    {
                        this._CommandResultProcessor.ProcessFailedSendingCommand(context.Command);
                    }
                });

            }
            catch (Exception ex)
            {
                  
                Console.WriteLine("agg:{0}", ex.InnerException.Message);
                this._CommandResultProcessor.ProcessFailedSendingCommand(context.Command,ex);
                 
            }

        }


        CommandExecutionDetails GetCommandExecutionDetails(Type commandType)
        {

            return CommandExecutionMap.GetOrAdd(
                commandType,
                t =>
                {
                    var commandInterfaceType = t
                        .GetInterfaces()
                        .Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<,>));

                    var commandTypes = commandInterfaceType.GetGenericArguments();

                    var commandHandlerType = typeof(ICommandHandler<>)
                                             .MakeGenericType(commandType);

                    var invoker = commandHandlerType.GetMethod("ExecuteAsync");

                    return new CommandExecutionDetails
                    {
                        CommandHandlerType = commandHandlerType,
                        Invoker = ((h, command) =>
                        {
                            return (Task<object>)invoker.Invoke(h, new object[] { command });
                        })
                    };
                });
        }
    }
}
