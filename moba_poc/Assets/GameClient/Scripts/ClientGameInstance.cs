using System;
using System.Threading;
using Com.JVL.Game;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameClient.Scripts
{
	public class ClientGameInstance : IAsyncStartable, IDisposable
	{
		private GameInstance _gameInstance;

		[Inject]
		public void Setup(GameLifeTimeScope gameLifeTimeScope)
		{
			// _gameInstance = gameLifeTimeScope.Container.
			Debug.Log($"Gamelifetime scope: {gameLifeTimeScope.name}");
		}

		[Inject]
		public void ConstructInstance()
		{
			Debug.Log($"Game instance is null? {_gameInstance == null}");
		}

		public UniTask StartAsync(CancellationToken cancellation)
		{
			return UniTask.CompletedTask;
		}

		public void Dispose() { }
	}
}