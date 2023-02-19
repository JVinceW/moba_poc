using Com.JVL.Game.Player;
using UnityEngine;

namespace GameClient.Scripts.Player
{
	public class GameLocalPlayerClient : BaseGameLocalPlayer
	{
		private void OnGUI()
		{
			if (GUI.Button(new Rect(0, 40, 200, 40), "Client"))
			{
				StartGame();
			}
		}
	}
}