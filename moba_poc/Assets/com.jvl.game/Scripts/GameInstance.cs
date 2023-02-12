using System;
using System.Collections.Generic;
using System.Threading;
using Com.JVL.Game.Managers;
using Com.JVL.Game.Managers.GameSceneManager;
using Cysharp.Threading.Tasks;
using GameCore.Scripts.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game
{
	/// <summary>
	/// The main class control almost all important process of the game. And this class should be exist through game lifecyle
	/// </summary>
	public class GameInstance : IAsyncStartable, IDisposable
	{
		private readonly List<IGameManager> _gameManagers = new();
		private readonly List<BaseLocalPlayer> _localPlayers = new();
		private GameSceneManager _gameSceneManager;
		protected GameSceneManager GameSceneManager => _gameSceneManager;


		public List<BaseLocalPlayer> LocalPlayers => _localPlayers;

		[Inject]
		public void Install(GameSceneManager gameSceneManager)
		{
			// assign manager
			_gameSceneManager = gameSceneManager;

			// Add manager to list for initialize code more organized
			_gameManagers.Add(_gameSceneManager);
			Debug.Log("Game Instance created and injected finished");
		}

		public async UniTask StartAsync(CancellationToken cancellation)
		{
			await InitializeSubManagers();
			// TODO Base on define symbol, we will load game client scene or game server scene
			// Atm, I only load game client because I'm still not impl game server
			await LoadClientMainScene();
		}

		public void AddLocalPlayer(BaseLocalPlayer localPlayer)
		{
			_localPlayers.Add(localPlayer);
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

		public void Dispose()
		{
			_gameSceneManager?.Dispose();
		}
	}
}