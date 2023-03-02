using Com.JVL.Game;
using Com.JVL.Game.Player;
using UnityEngine;
using VContainer;

namespace GameClient.Scripts.Player
{
	public class GameLocalPlayer : BaseGameLocalPlayer
	{
		[Inject]
		private GameInstance _gameInstance;

		[Inject]
		private ClientGameInstance _clientGameInstance;
		
		

		private void OnGUI()
		{
			if (GUI.Button(new Rect(0, 40, 200, 40), "Client"))
			{
				StartGame();
			}
		}
	}
}