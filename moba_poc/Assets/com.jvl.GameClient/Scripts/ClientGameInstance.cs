using System;
using System.Linq;
using System.Threading;
using Com.JVL.Game;
using Cysharp.Threading.Tasks;
using GameCore.Scripts.Framework;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameClient.Scripts
{
	public class ClientGameInstance : IInitializable, IAsyncStartable, IDisposable
	{
		private GameInstance _gameInstance;
		private GameModeConfiguration _gameModeConfiguration;

		public GameInstance GameInstance => _gameInstance;

		public ClientGameInstance(GameModeConfiguration gameModeConfiguration)
		{
			_gameModeConfiguration = gameModeConfiguration;
		}

		[Inject]
		public void InstallDependencies(GameLifeTimeScope gameLifeTimeScope)
		{
			_gameInstance = gameLifeTimeScope.Container.Resolve<GameInstance>();
		}

		/// <summary>
		/// In client side, there always exist only 1 local player. So this is equal to GetLocalPlayer 
		/// </summary>
		/// <returns></returns>
		public BaseLocalPlayer GetFirstLocalPlayer()
		{
			return _gameInstance.LocalPlayers.FirstOrDefault();
		}

		public UniTask StartAsync(CancellationToken cancellation)
		{
			return UniTask.CompletedTask;
		}

		public void CreateLocalPlayer()
		{
			var localPlayer = _gameModeConfiguration.GetLocalPlayer;
			var instance = GameObject.Instantiate(localPlayer);
			_gameInstance.AddLocalPlayer(instance);
		}

		public void Dispose() { }
		public void Initialize()
		{
			CreateLocalPlayer();
		}
	}
}