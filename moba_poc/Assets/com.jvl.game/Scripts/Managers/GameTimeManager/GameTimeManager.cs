using System.Collections.Generic;
using Fusion;

namespace Com.JVL.Game.Managers.GameTimeManager
{
	public class GameTimeManager : IGameManager
	{
		private readonly Dictionary<string, ObjectLifeTime> _objectLifeTimes = new();
		public void CreateObjectLifeTime(NetworkObject networkObject)
		{
			
		}
		
	}
}