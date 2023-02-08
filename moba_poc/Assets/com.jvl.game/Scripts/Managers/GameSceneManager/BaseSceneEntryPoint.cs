using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Com.JVL.Game.Managers.GameSceneManager
{
	/// <summary>
	/// The entry point of a scene. The init/cleanup process should write here.
	/// </summary>
	/// LifeTimeScope ExeOrder have range(-20, -10)
	[DefaultExecutionOrder(-15)]
	public class BaseSceneEntryPoint : MonoBehaviour, ISceneEntryPoint
	{
		protected GameSceneManager _gameSceneManager;
		protected ISceneTaskScheduler _sceneTaskScheduler;

		[Inject]
		public void ConstructSceneBasicDependencies(GameSceneManager gameSceneManager)
		{
			_gameSceneManager = gameSceneManager;
			_sceneTaskScheduler = gameSceneManager.CurrentSceneScheduler;
			_sceneTaskScheduler.SceneEntryPoint = this;
			Debug.LogWarning("Finished injection to scene scope");
		}

		public virtual UniTask OnSceneLoaded()
		{
			Debug.Log("[BaseSceneEntryPoint] Execute: OnSceneLoaded");
			return UniTask.CompletedTask;
		}

		public virtual UniTask OnSceneUnLoaded()
		{
			Debug.Log("[BaseSceneEntryPoint] Execute: OnSceneUnLoaded");
			return UniTask.CompletedTask;
		}
	}
}