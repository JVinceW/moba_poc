using Com.JVL.Game.Common;
using Fusion;
using UnityEngine;

namespace Com.JVL.Game.Player
{
	public partial class GamePlayerState : NetworkBehaviour, IBeforeSpawn
	{
		public void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj) { }

		protected void Spawned()
		{
			if (Runner.IsServer)
			{
				Debug.Log("Player State spawned on server");
			} else if (Runner.IsClient)
			{
				Debug.Log("Player State spawned on client");
			}
		}
	}
}