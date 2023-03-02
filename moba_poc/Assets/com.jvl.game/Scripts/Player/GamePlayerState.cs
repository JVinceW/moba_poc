using Com.JVL.Game.Common;
using Fusion;
using UnityEngine;

namespace Com.JVL.Game.Player
{
	public partial class GamePlayerState : NetworkBehaviour, IBeforeSpawn
	{
		public void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj) { }

		public override void Spawned()
		{
			Debug.Log("GamePlayerState", gameObject);
			if (Runner.IsServer)
			{
				Debug.Log("GamePlayerState Server", gameObject);
			} else if (Runner.IsClient)
			{
				Debug.Log("GamePlayerState Client", gameObject);
			}
		}
	}
}