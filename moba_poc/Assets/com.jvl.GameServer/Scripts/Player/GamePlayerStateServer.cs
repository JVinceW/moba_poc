using Com.JVL.Game.Player;
using UnityEngine;

namespace Com.JVL.Game.Server.Player
{
	public class GamePlayerStateServer : GamePlayerState
	{
		public void NotificationSpawned()
		{
			Debug.Log("Spawned Server", gameObject);
		}
	}
}