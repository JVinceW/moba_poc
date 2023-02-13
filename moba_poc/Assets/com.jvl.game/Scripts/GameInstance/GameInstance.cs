using System;
using System.Collections.Generic;
using System.Linq;
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
	public class GameInstance : IGameInstance, IAsyncStartable, IDisposable
	{
		private BaseGameModeConfiguration _gameModeConfiguration;
		private readonly List<IGameManager> _gameManagers = new();
		private readonly List<BaseLocalPlayer> _localPlayers = new();
		private GameSceneManager _gameSceneManager;

		#region === Accessor ===
		public List<BaseLocalPlayer> LocalPlayers => _localPlayers;
		public T GetGameManager<T>() where T : IGameManager
		{
			return (T)_gameManagers.FirstOrDefault(x => x.GetType() == typeof(T));
		}

		public T GameModeConfiguration<T>() where T : BaseGameModeConfiguration
		{
			return _gameModeConfiguration as T;
		}
		#endregion === Accessor ===

		[Inject]
		public void InstallDependencies(GameSceneManager gameSceneManager, BaseGameModeConfiguration gameModeConfiguration)
		{
			_gameSceneManager = gameSceneManager;
			// Add manager to list for initialize code more organized
			_gameManagers.Add(_gameSceneManager);
			
			// Assign game mode configuration
			_gameModeConfiguration = gameModeConfiguration;
		}

		public async UniTask StartAsync(CancellationToken cancellation)
		{
			await InitializeSubManagers();

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
			var sceneName = string.IsNullOrEmpty(_gameModeConfiguration.GetServerSceneToLoad)
				? _gameModeConfiguration.GetDefaultSceneToLoad
				: _gameModeConfiguration.GetServerSceneToLoad; 
			await Addressables.LoadSceneAsync(sceneName);
		}

		private async UniTask LoadClientMainScene()
		{
			var sceneName = string.IsNullOrEmpty(_gameModeConfiguration.GetClientSceneToLoad)
				? _gameModeConfiguration.GetDefaultSceneToLoad
				: _gameModeConfiguration.GetClientSceneToLoad; 
			Debug.Log("[GameInstance] Execute load Client MainScene");
			await Addressables.LoadSceneAsync(sceneName);
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