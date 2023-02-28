using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Server.com.jvl.GameServer
{
	public class ServerGameInstance : IInitializable, IAsyncStartable, IDisposable
	{
		[Inject]
		private ServerGameMode _gameMode;

		public void Initialize() { }

		public UniTask StartAsync(CancellationToken cancellation)
		{
			return StartServer();
		}

		private async UniTask StartServer()
		{
			await _gameMode.StartGame(Fusion.GameMode.Server);
			_gameMode.Init();
		}

		public void Dispose() { }
	}
}