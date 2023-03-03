using System;
using System.Threading;
using Com.JVL.Game.GameMode;
using Com.JVL.Game.Player;
using Cysharp.Threading.Tasks;
using Fusion;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Client
{
	public class ClientGameInstance : IInitializable, IAsyncStartable, IDisposable
	{
		[Inject]
		private GameInstance _gameInstance;

		[Inject]
		private BaseGameModeConfiguration _gameModeConfiguration;

		[Inject]
		private BaseGameLocalPlayer _gameLocalPlayer;

		private NetworkRunner _clientNetworkRunner;
		

		public BaseGameLocalPlayer GetGameLocalPlayer => _gameLocalPlayer;

		public NetworkRunner GetClientNetworkRunner => _clientNetworkRunner;

		#region - Lifecycle -
		public UniTask StartAsync(CancellationToken cancellation)
		{
			return UniTask.CompletedTask;
		}

		public void Initialize()
		{
			_clientNetworkRunner = _gameLocalPlayer.Runner;
		}

		public void Dispose() { }
		#endregion - Lifecycle -

		#region - Method -
		#endregion - Method -

		#region - Subroutine -
		#endregion - Subroutine -
	}
}