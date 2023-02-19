using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Com.JVL.Game;
using Com.JVL.Game.GameMode;
using Com.JVL.Game.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameClient.Scripts
{
	public class ClientGameInstance : IInitializable, IAsyncStartable, IDisposable
	{
		private GameInstance _gameInstance;
		private BaseGameModeConfiguration _gameModeConfiguration;

		public GameInstance GameInstance => _gameInstance;
		private readonly List<BaseGameLocalPlayer> _localPlayers = new();
		public List<BaseGameLocalPlayer> LocalPlayers => _localPlayers;


		[Inject]
		public void InstallDependencies(GameLifeTimeScope gameLifeTimeScope)
		{
			_gameInstance = gameLifeTimeScope.Container.Resolve<GameInstance>();
			_gameModeConfiguration = _gameInstance.GameModeConfiguration<BaseGameModeConfiguration>();
		}

		/// <summary>
		/// In client side, there always exist only 1 local player. So this is equal to GetLocalPlayer 
		/// </summary>
		/// <returns></returns>
		public BaseGameLocalPlayer GetFirstLocalPlayer()
		{
			return LocalPlayers.FirstOrDefault();
		}

		public UniTask StartAsync(CancellationToken cancellation)
		{
			return UniTask.CompletedTask;
		}

		public void CreateLocalPlayer()
		{
			var localPlayer = _gameModeConfiguration.GetLocalPlayer;
			var instance = GameObject.Instantiate(localPlayer);
			_localPlayers.Add(instance);
		}

		public void Dispose() { }
		public void Initialize()
		{
			CreateLocalPlayer();
		}
	}
}