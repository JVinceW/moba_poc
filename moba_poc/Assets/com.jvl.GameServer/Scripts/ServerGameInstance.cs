using System;
using System.Threading;
using Com.JVL.Game.Server.com.jvl.GameServer.Player;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Server.com.jvl.GameServer
{
	public class ServerGameInstance : IInitializable, IAsyncStartable, IDisposable
	{
		[Inject]
		private ServerMainPlayer _serverMainPlayer;

		public void Initialize() { }

		public UniTask StartAsync(CancellationToken cancellation)
		{
			return StartServer();
		}

		private async UniTask StartServer()
		{
			await _serverMainPlayer.StartServer();
		}

		public void Dispose() { }
	}
}