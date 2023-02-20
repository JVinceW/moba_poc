using Com.JVL.Game.Player;
using Fusion;
using UnityEngine;

namespace Com.JVL.Game.Server.Player
{
	public class GamePlayerStateServer : GamePlayerState
	{
		public void NotificationSpawned()
		{
			Debug.Log("Spawned Server", gameObject);

			NetworkRunner.OnBeforeSpawned handle = (runner, o) => { };
		}

		public override void Spawned()
		{
			Debug.Log("Spawned callback");
			RPC_ClientSpawnedNotification();
		}
		

		[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
		public void RPC_ClientSpawnedNotification()
		{
			Debug.Log("Client");
		}
	}
}