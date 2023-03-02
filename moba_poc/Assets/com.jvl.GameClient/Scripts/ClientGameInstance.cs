using System;
using System.Threading;
using Com.JVL.Game;
using Com.JVL.Game.GameMode;
using Com.JVL.Game.Player;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace GameClient.Scripts
{
	public class ClientGameInstance : IInitializable, IAsyncStartable, IDisposable
	{
		[Inject]
		private GameInstance _gameInstance;

		[Inject]
		private BaseGameModeConfiguration _gameModeConfiguration;

		private BaseGameLocalPlayer _gameLocalPlayer;

		public BaseGameLocalPlayer GetGameLocalPlayer => _gameLocalPlayer;

		public UniTask StartAsync(CancellationToken cancellation)
		{
			return UniTask.CompletedTask;
		}

		private void CreateLocalPlayer()
		{
			var localPlayer = _gameModeConfiguration.GetLocalPlayer;
			_gameLocalPlayer = Object.Instantiate(localPlayer);
		}

		public void Dispose() { }

		public void Initialize()
		{
			CreateLocalPlayer();
		}
	}
}