using Fusion;
using NaughtyAttributes;
using UnityEngine;

namespace Com.JVL.Game.Common
{
	/// <summary>
	/// Determined the spawn position class.
	/// </summary>
	public class SpawnPoint : NetworkBehaviour
	{
		[ReadOnly]
		[SerializeField]
		private NetworkObject _target;

		public NetworkObject GetTarget => _target;
	}
}