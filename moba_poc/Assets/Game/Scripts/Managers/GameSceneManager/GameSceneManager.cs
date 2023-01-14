using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly List<BaseSceneTaskScheduler> _sceneHistory = new();
		private readonly List<string> _currentScenes = new();
		#endregion

		#region State
		private LoadSceneStatus _currentLoadSceneStatus;

		private BaseSceneTaskScheduler _currentLoadingSceneScheduler;
		#endregion

		#region Accessor
		public BaseSceneTaskScheduler CurrentLoadingSceneScheduler => _currentLoadingSceneScheduler;
		#endregion

		#region Lifecycle
		public UniTask Initialize(params object[] args)
		{
			Debug.Log("Initialize GameSceneManager");
			return UniTask.CompletedTask;
		}
		#endregion

		#region Method
		public async UniTask ProcessLoadScene(BaseSceneTaskScheduler loadSceneScheduler, bool bForcedLoadScene = false)
		{
			var nextSceneName = loadSceneScheduler.SceneContext.SceneName;
			if (_currentScenes.Contains(nextSceneName) && !bForcedLoadScene)
			{
				Debug.Log(
					$"[Information][GameSceneManager] Scene {nextSceneName} is already loaded. LoadScene process Stopped");
				return;
			}

			await ExecuteLoadScene(loadSceneScheduler);
		}

		public void BackToPreviousScene()
		{
			//...
		}
		#endregion

		#region Subroutine
		private async UniTask ExecuteLoadScene(BaseSceneTaskScheduler sceneTaskScheduler)
		{
			var sceneContext = sceneTaskScheduler.SceneContext;

			if (sceneContext.IsAddToSceneStack)
			{
				_sceneHistory.Add(sceneTaskScheduler);
			}

			_currentScenes.Add(sceneContext.SceneName);
			_currentLoadSceneStatus = LoadSceneStatus.Start;
			_currentLoadingSceneScheduler = sceneTaskScheduler;

			var cs = new CancellationTokenSource();
			// Run all the task before actually load scene asset
			await sceneTaskScheduler.ProcessScheduler(cs.Token);
			await LoadSceneInstance(sceneTaskScheduler);
		}

		private static async UniTask LoadSceneInstance(BaseSceneTaskScheduler sceneTaskScheduler)
		{
			var sceneContext = sceneTaskScheduler.SceneContext;
			var sceneAsset =
				await Addressables.LoadSceneAsync(sceneContext.SceneAssetAddress, sceneContext.LoadSceneMode, false);

			//... Do some process before active it (if needed)
			sceneTaskScheduler.SceneInstance = sceneAsset;
			await sceneAsset.ActivateAsync();
		}

		private void ExecuteBackToPreviousScene() { }
		#endregion

		public void Dispose() { }
	}
}