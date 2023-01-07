using System.Collections.Generic;
using Com.JVL.Game.Managers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Com.JVL.Game
{
	/// <summary>
	/// The main class control almost all important process of the game. And this class should be exist through game lifecyle
	/// </summary>
	public class GameInstance : IStartable
	{
		private List<IGameManager> _gameManagers = new();

		public GameInstance()
		{
			Debug.LogWarning("Constructor of game instance");
		}
		
		// Initialize game managers
		public async UniTask InitializeSubManagers()
		{
			foreach (var manager in _gameManagers)
			{
				await manager.Initialize();
			}
		}

		public void Start()
		{
			Debug.Log("Start Game Instance");
		}
	}
}