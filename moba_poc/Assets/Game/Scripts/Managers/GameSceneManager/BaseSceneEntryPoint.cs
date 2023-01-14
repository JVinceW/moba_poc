using System;
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
		protected readonly GameSceneManager _gameSceneManager;
		private readonly ISceneTaskScheduler _sceneTaskScheduler;

		[Inject]
		public BaseSceneEntryPoint(GameLifeTimeScope gameLifeTimeScope)
		{
			Debug.Log("Resolve");
			_gameSceneManager = gameLifeTimeScope.Container.Resolve<GameSceneManager>();
			_sceneTaskScheduler = _gameSceneManager.CurrentLoadingSceneScheduler;
			Debug.Log("Resolve Ended");
		}

		public virtual UniTask OnSceneLoaded()
		{
			Debug.Log($"Current Loading Scene scheduler name: {_sceneTaskScheduler.SceneContext.SceneName}");
			return UniTask.CompletedTask;
		}

		public virtual UniTask OnSceneUnLoaded()
		{
			return UniTask.CompletedTask;
		}
	}
}