using Com.JVL.Game.Player;
using Fusion;
using UnityEngine;

namespace Com.JVL.Game.Server.Player
{
	public class GamePlayerStateServer : GamePlayerState
	{
		private PlayerRef Player;
		private TickTimer life;
		public void NotificationSpawned(PlayerRef player)
		{
			life = TickTimer.CreateFromSeconds(Runner, 10.0f);
			Player = player;
			Debug.Log("Spawned Server", gameObject);
		}

		public override void Spawned()
		{
			Debug.Log("Spawned callback");
			Debug.Log($"HasStateAuthority: {Object.HasStateAuthority} - HasInputAuthority: {Object.HasInputAuthority} - IsProxies: {Object.IsProxy}");
			// if (Runner.IsServer)
			// {
			// 	Debug.Log("Server sending RPC");
			// 	RPC_ClientSpawnedNotification(Player);
			// }
		}


		[Rpc(RpcSources.StateAuthority, RpcTargets.InputAuthority, Channel = RpcChannel.Reliable, HostMode = RpcHostMode.SourceIsServer)]
		public void RPC_ClientSpawnedNotification([RpcTarget] PlayerRef player)
		{
			Debug.Log($"Client {player.PlayerId}");
		}

		public override void FixedUpdateNetwork()
		{
			if (Runner.IsServer)
			{
				if (life.Expired(Runner))
				{
					Debug.Log("Server tick timer Expired");
					RPC_ClientSpawnedNotification(Player);
				}
			}
		}
	}
}