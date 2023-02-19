using Com.JVL.Game.Player;
using UnityEngine;

namespace GameClient.Scripts.Player
{
	public class GamePlayerStateClient : GamePlayerState
	{
		public void NotificationSpawned()
		{
			Debug.Log("Spawned Client");
		}

		public override void Spawned()
		{
			base.Spawned();
			Debug.Log("Client Spawned");
			NotificationSpawned();
		}
	}
}