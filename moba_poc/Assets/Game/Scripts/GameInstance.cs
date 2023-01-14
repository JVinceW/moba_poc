using System.Collections.Generic;
using System.Threading;
using Com.JVL.Game.Managers;
using Com.JVL.Game.Managers.GameSceneManager;
using Cysharp.Threading.Tasks;
using GameClient.Scripts.TestScene;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game
{
	/// <summary>
	/// The main class control almost all important process of the game. And this class should be exist through game lifecyle
	/// </summary>
	public class GameInstance : IAsyncStartable
	{
		private List<IGameManager> _gameManagers = new();
		private readonly GameSceneManager _gameSceneManager;
		private readonly PlayerManager _playerManager;

		// Initialize game managers
		public async UniTask InitializeSubManagers()
		{
			Debug.Log("[GameInstance] Start initialize game managers");
			foreach (var manager in _gameManagers)
			{
				await manager.Initialize();
			}

			Debug.Log("[GameInstance] Finished initialize game managers");
		}

		[Inject]
		public GameInstance(GameSceneManager gameSceneManager, PlayerManager playerManager)
		{
			Debug.Log("Start Inject manager");
			_gameSceneManager = gameSceneManager;
			_playerManager = playerManager;
			_gameManagers.Add(gameSceneManager);
			_gameManagers.Add(playerManager);
			Debug.Log("Finished Inject manager");
		}

		public async UniTask StartAsync(CancellationToken cancellation)
		{
			Debug.Log("Start Game Instance");
			await InitializeSubManagers();
			await _gameSceneManager.ProcessLoadScene(new SceneTask_TestSceneScheduler(new SceneContext_TestScene()), true);
		}
	}
}