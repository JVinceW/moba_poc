using UnityEngine;
using VContainer;

namespace GameClient.Scripts
{
	public class LocalPlayerBehaviour : MonoBehaviour
	{
		[Inject]
		public LocalPlayerBehaviour()
		{
			Debug.Log("Constructor of LocalPlayerBehavior");
		}

		private void Start()
		{
			Debug.LogWarning("Start from local player mono behavior");
		}
	}
}