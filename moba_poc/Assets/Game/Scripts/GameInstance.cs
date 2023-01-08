using System.Collections.Generic;
using System.Threading;
using Com.JVL.Game.Managers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Com.JVL.Game
{
	/// <summary>
	/// The main class control almost all important process of the game. And this class should be exist through game lifecyle
	/// </summary>
	public class GameInstance : IAsyncStartable
	{
		private List<IGameManager> _gameManagers = new();

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

		public async UniTask StartAsync(CancellationToken cancellation)
		{
			Debug.Log("Start Game Instance");
			await InitializeSubManagers();
		}
	}
}