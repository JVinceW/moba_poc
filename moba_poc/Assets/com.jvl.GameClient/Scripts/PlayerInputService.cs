using System;
using UnityEngine;
using VContainer.Unity;

namespace GameClient.Scripts
{
	public class PlayerInputService : ITickable, IStartable
	{
		private string _myRandomGuid;

		public string MyRandomGuid => _myRandomGuid;

		public PlayerInputService()
		{
			_myRandomGuid = Guid.NewGuid().ToString()[..5];
			Debug.Log($"Init GUID in PlayerInputService constructor {_myRandomGuid}");
		}

		public void Tick()
		{
			Debug.Log("Hello, Im ticking the PlayerInputService");
		}

		public void Start()
		{
			Debug.Log("Hello, this is the Start of the PlayerInputService");
		}
	}
}