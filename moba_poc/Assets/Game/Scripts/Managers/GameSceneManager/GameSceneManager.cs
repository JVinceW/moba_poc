using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;

namespace Com.JVL.Game.Managers.GameSceneManager
{
	public class GameSceneManager : IGameManager, IDisposable
	{
		public enum LoadSceneStatus
		{
			Start,
			Processing,
			Finsihed
		}

		#region Cache
		private readonly Queue<BaseSceneTaskScheduler> _sceneHistory = new();
		private readonly string _currentSceneName = string.Empty;
		private GameLifeTimeScope _gameLifeTimeScope;
		#endregion

		#region State
		private LoadSceneStatus _currentLoadSceneStatus;

		private BaseSceneTaskScheduler _currentSceneScheduler;
		#endregion

		#region Accessor
		public BaseSceneTaskScheduler CurrentSceneScheduler => _currentSceneScheduler;
		#endregion

		#region Lifecycle
		public UniTask Initialize(params object[] args)
		{
			_gameLifeTimeScope = (GameLifeTimeScope)GameLifeTimeScope.Find<GameLifeTimeScope>();
			return UniTask.CompletedTask;
		}
		#endregion

		#region Method
		public async UniTask ProcessLoadScene(BaseSceneTaskScheduler loadSceneScheduler, bool bForcedLoadScene = false)
		{
			var nextSceneName = loadSceneScheduler.SceneContext.SceneName;
			if (_currentSceneName == nextSceneName && !bForcedLoadScene)
			{
				Debug.Log(
					$"[Information][GameSceneManager] Scene {nextSceneName} is already loaded. LoadScene process Stopped");
				return;
			}

			await ExecuteLoadScene(loadSceneScheduler);
		}

		public async UniTask ProcessLoadAdditionScene(BaseSceneTaskScheduler loadSceneScheduler)
		{
			// TODO:  not sure but will implement later
		}

		public async UniTask BackToPreviousScene()
		{
			await ExecuteBackToPreviousScene();
		}
		#endregion

		#region Subroutine
		private async UniTask ExecuteLoadScene(BaseSceneTaskScheduler sceneTaskScheduler)
		{
			_currentLoadSceneStatus = LoadSceneStatus.Start;
			var cs = new CancellationTokenSource();
			await UnloadCurrentScene(cs);
			// Run all the task before actually load scene asset
			await sceneTaskScheduler.ProcessScheduler(cs.Token);
			await LoadSceneInstance(sceneTaskScheduler, cs);
		}

		private async UniTask UnloadCurrentScene(CancellationTokenSource cancellationTokenSource)
		{
			// ReSharper disable once UseNullPropagation
			if (_currentSceneScheduler == null)
				return;
			var entryPoint = _currentSceneScheduler.SceneEntryPoint;
			if (entryPoint != null)
			{
				await entryPoint.OnSceneUnLoaded()
					.AttachExternalCancellation(cancellationTokenSource.Token);
			}
		}

		private async UniTask LoadSceneInstance(BaseSceneTaskScheduler sceneTaskScheduler,
			CancellationTokenSource cs = default)
		{
			var sceneContext = sceneTaskScheduler.SceneContext;
			_currentLoadSceneStatus = LoadSceneStatus.Processing;
			_currentSceneScheduler = sceneTaskScheduler;
			var sceneAsset = await Addressables.LoadSceneAsync(sceneContext.SceneAssetAddress);
			//...
			// Do some process before active it (if needed)
			//...
			sceneTaskScheduler.SceneInstance = sceneAsset;
			if (sceneTaskScheduler.SceneEntryPoint != null)
			{
				await sceneTaskScheduler.SceneEntryPoint.OnSceneLoaded();
			}

			_currentLoadSceneStatus = LoadSceneStatus.Finsihed;
		}

		/// <summary>
		/// Back to previous scene
		/// TODO Test this later.
		/// </summary>
		private async UniTask ExecuteBackToPreviousScene()
		{
			var previousScheduler = _sceneHistory.Dequeue();
			await ExecuteLoadScene(previousScheduler);
		}
		#endregion

		public void Dispose() { }
	}
}