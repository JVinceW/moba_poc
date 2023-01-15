using System.Collections.Generic;
using System.Threading;
using Com.JVL.Game.Managers;
using Com.JVL.Game.Managers.GameSceneManager;
using Cysharp.Threading.Tasks;
using GameClient.Scripts.TestScene;
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

		// Initialize game managers

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
			await _gameSceneManager.ProcessLoadScene(new SceneTask_TestSceneScheduler(new SceneContext_TestScene()),
				true);
		}

		private async UniTask InitializeSubManagers()
		{
			foreach (var manager in _gameManagers)
			{
				await manager.Initialize();
			}
		}
	}
}