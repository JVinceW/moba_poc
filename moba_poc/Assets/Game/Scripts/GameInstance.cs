using System.Collections.Generic;
using System.Threading;
using Com.JVL.Game.Managers;
using Com.JVL.Game.Managers.GameSceneManager;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game
{
	/// <summary>
	/// The main class control almost all important process of the game. And this class should be exist through game lifecyle
	/// </summary>
	public class GameInstance : IAsyncStartable
	{
		private readonly List<IGameManager> _gameManagers = new();
		private readonly GameSceneManager _gameSceneManager;
		private readonly PlayerManager _playerManager;
		protected GameSceneManager GameSceneManager => _gameSceneManager;
		protected PlayerManager PlayerManager => _playerManager;

		[Inject]
		public GameInstance(GameSceneManager gameSceneManager, PlayerManager playerManager)
		{
			// assign manager
			_gameSceneManager = gameSceneManager;
			_playerManager = playerManager;

			// Add manager to list for initialize code more organized
			_gameManagers.Add(_gameSceneManager);
			_gameManagers.Add(_playerManager);
		}

		public async UniTask StartAsync(CancellationToken cancellation)
		{
			await InitializeSubManagers();
			// TODO Base on define symbol, we will load game client scene or game server scene
			// Atm, I only load game client because I'm still not impl game server
			await LoadClientMainScene();
		}

		#region Subroutine
		private async UniTask LoadServerMainScene()
		{
			Debug.Log("[GameInstance] Execute load Server MainScene");
			await Addressables.LoadSceneAsync("");
		}

		private async UniTask LoadClientMainScene()
		{
			Debug.Log("[GameInstance] Execute load Client MainScene");
			await Addressables.LoadSceneAsync(_gameSceneManager.StartupSceneName);
		}

		private async UniTask InitializeSubManagers()
		{
			foreach (var manager in _gameManagers)
			{
				await manager.Initialize();
			}
		}
		#endregion
	}
}