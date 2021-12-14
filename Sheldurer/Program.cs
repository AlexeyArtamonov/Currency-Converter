using System;
using TaskScheduler;

namespace Sheldurer
{
    class Program
    {
        static void Main()
        {
            ITaskService TaskSheldurer = new TaskSchedulerClass();
            TaskSheldurer.Connect();


            ITaskDefinition Task = TaskSheldurer.NewTask(0);
            Task.Settings.Enabled = true;
            Task.Settings.Compatibility = _TASK_COMPATIBILITY.TASK_COMPATIBILITY_V2_1;

            
            ITriggerCollection Triggers = Task.Triggers;

            ITrigger TimeTrigger = Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME);
            TimeTrigger.Repetition.Interval = "PT24H";
            TimeTrigger.StartBoundary = DateTime.Now.Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            TimeTrigger.EndBoundary = DateTime.Now.Date.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            TimeTrigger.Enabled = true;

            ITrigger LogOnTrigger = Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON);
            LogOnTrigger.Enabled = true;

            
            
            IActionCollection actions = Task.Actions;
            _TASK_ACTION_TYPE actionType = _TASK_ACTION_TYPE.TASK_ACTION_EXEC;

            
            IAction action = actions.Create(actionType);
            IExecAction execAction = action as IExecAction;
            execAction.Path = @"C:\Users\Alexey\source\repos\Currency Converter\Currency Converter\bin\Debug\Currency Converter.exe --update";
            ITaskFolder rootFolder = TaskSheldurer.GetFolder(@"\");

            
            rootFolder.RegisterTaskDefinition("Update curr", Task, 6, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);
        }
    }
}
