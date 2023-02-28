using System.Collections.Generic;
using Com.JVL.Game.Player;
using Fusion;

namespace Com.JVL.Game.GameMode
{
	public class BaseGameState : NetworkBehaviour
	{
		private readonly Dictionary<PlayerRef, NetworkObject> _playerNWObjects = new();

		private readonly List<GamePlayerState> _playerStates = new();

		public void PlayerJoined(PlayerRef playerRef, NetworkObject playerNWObject)
		{
			if (!_playerNWObjects.ContainsKey(playerRef))
			{
				_playerNWObjects.Add(playerRef, playerNWObject);
			}
		}

		void PlayerLeft(PlayerRef playerRef)
		{
			_playerNWObjects.Remove(playerRef);
		}
	}
}