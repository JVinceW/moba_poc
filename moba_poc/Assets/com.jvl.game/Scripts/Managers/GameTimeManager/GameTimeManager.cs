using System.Collections.Generic;
using Com.JVL.Game.GameMode;
using Fusion;

namespace Com.JVL.Game.Managers.GameTimeManager
{
	public class GameTimeManager : IGameManager
	{
		private readonly Dictionary<string, ObjectLifeTime> _objectLifeTimes = new();
		private BaseGameState _gameState;
		private NetworkRunner _runner;

		public void SetDependencies(BaseGameState gameState, NetworkRunner runner)
		{
			_gameState = gameState;
			_runner = runner;
		}
	}
}