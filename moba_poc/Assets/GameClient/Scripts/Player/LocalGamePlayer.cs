using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameClient.Scripts
{
	public class LocalGamePlayer : IStartable
	{
		private readonly PlayerInputService _playerInputService;
		private readonly LocalPlayerBehaviour _localPlayerBehaviour;

		[Inject]
		public LocalGamePlayer(PlayerInputService playerInputService, LocalPlayerBehaviour localPlayerBehaviour)
		{
			_playerInputService = playerInputService;
			_localPlayerBehaviour = localPlayerBehaviour;
			Debug.Log("The guid I got from PlayerInputService " + _playerInputService.MyRandomGuid);
		}

		public void Start()
		{
			Debug.LogWarning("[Start] The guid I got from PlayerInputService " + _playerInputService.MyRandomGuid);
			Debug.Log($"LocalPlayerBehaviour is ? {_localPlayerBehaviour.GetInstanceID()} ");
		}
	}
}