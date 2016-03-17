using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework.Impl
{
    internal class CommandResultProcessor 
    {
        private readonly ConcurrentDictionary<string, CommandTaskCompletionSource> _commandTaskDict;

        public CommandResultProcessor()
        {
            this._commandTaskDict = new ConcurrentDictionary<string, CommandTaskCompletionSource>();
        }

        public void RegisterProcessingCommand(ICommand command, TaskCompletionSource<CommandResult> taskCompletionSource)
        {
            if (!_commandTaskDict.TryAdd(command.ToString(), new CommandTaskCompletionSource { TaskCompletionSource = taskCompletionSource }))
            {
                throw new Exception(string.Format("重复处理请求, type:{0}, id:{1}", command.GetType().Name, command.ToString()));
            }
        }

        public void ProcessFailedSendingCommand(ICommand command)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryRemove(command.ToString(), out commandTaskCompletionSource))
            {
                var commandResult = new CommandResult { Status = CommandStatus.Failed, CommandId = command.ToString(), Result = "请求执行失败." };
                commandTaskCompletionSource.TaskCompletionSource.TrySetResult(commandResult);
            }
        }

        public void ProcessFailedSendingCommand(ICommand command,Exception ex)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryRemove(command.ToString(), out commandTaskCompletionSource))
            {
                 commandTaskCompletionSource.TaskCompletionSource.TrySetException(ex);
            }
        }

        public void ProcessSuccessCommand(ICommand command)
        {
            ProcessSuccessCommand(command, null);
        }

        public void ProcessSuccessCommand(ICommand command, object response)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryRemove(command.ToString(), out commandTaskCompletionSource))
            {
                var commandResult = new CommandResult { Status = CommandStatus.Success, CommandId = command.ToString(), Result = "执行成功",  MessageBody= response };
                commandTaskCompletionSource.TaskCompletionSource.TrySetResult(commandResult);
            }
        }

      
    }


    class CommandTaskCompletionSource
    {
        public TaskCompletionSource<CommandResult> TaskCompletionSource { get; set; }
    }


    public class CommandResult
    {
        public CommandStatus Status { get; set; }

        public string CommandId { get; set; }

        public string Result { get; set; }
 
        public CommandResult() { }

        public object MessageBody { get; set; }
     }

    public enum CommandStatus
    {
        None = 0,
        Success = 1,
        Failed = 2
    }
}
