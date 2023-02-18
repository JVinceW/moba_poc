using Com.JVL.Game.GameMode;
using Fusion;
using UnityEngine;
using VContainer;

namespace Com.JVL.Game.Server.com.jvl.GameServer.Scripts
{
	public class ServerGameMode : BaseGameMode
	{
		[Inject]
		private GameInstance _gameInstance;

		[Inject]
		private BaseGameModeConfiguration _gameModeConfiguration;

		public override void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
		{
			base.OnPlayerJoined(runner, player);
			// Create a unique position for the player
			Vector3 spawnPosition =
				new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
			NetworkObject networkPlayerObject =
				runner.Spawn(_gameModeConfiguration.GetPlayerCharacter, spawnPosition, Quaternion.identity, player);
			// Keep track of the player avatars so we can remove it when they disconnect
			// _spawnedCharacters.Add(player, networkPlayerObject);
		}
	}
}