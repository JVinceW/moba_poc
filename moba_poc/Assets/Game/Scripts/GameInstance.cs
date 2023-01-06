using System.Collections.Generic;
using Com.JVL.Game.Managers;
using Cysharp.Threading.Tasks;

namespace Com.JVL.Game
{
	/// <summary>
	/// The main class control almost all important process of the game. And this class should be exist through game lifecyle
	/// </summary>
	public class GameInstance
	{
		private List<IGameManager> _gameManagers = new();
		
		// Initialize game managers
		public async UniTask InitializeSubManagers()
		{
			foreach (var manager in _gameManagers)
			{
				await manager.Initialize();
			}
		}
	}
}