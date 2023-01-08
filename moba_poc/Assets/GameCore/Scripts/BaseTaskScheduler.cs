using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameCore.Scripts
{
	/// <summary>
	/// Base class of the task scheduler
	/// </summary>
	public abstract class BaseTaskScheduler
	{
		protected string _taskSchedulerName;
		protected readonly List<BaseTaskObject> _taskList = new();

		public string TaskSchedulerName => _taskSchedulerName;
		protected abstract void InitializeScheduler();

		public async UniTask ProcessScheduler(CancellationToken cancellationToken = default)
		{
			// We have to init scheduler before process it;
			InitializeScheduler();
			foreach (var taskObject in _taskList)
			{
				taskObject.CancellationToken = cancellationToken;
				await taskObject.ProcessTask();
			}
		}
	}

	public abstract class BaseTaskObject
	{
		public CancellationToken CancellationToken;
		public abstract UniTask ProcessTask();
	}
}