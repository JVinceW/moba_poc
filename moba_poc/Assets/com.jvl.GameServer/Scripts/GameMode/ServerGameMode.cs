using Com.JVL.Game.Common;
using Com.JVL.Game.GameMode;
using Fusion;
using UnityEngine;
using VContainer;

namespace Com.JVL.Game.Server
{
	public class ServerGameMode : BaseGameMode
	{
		private GameLifeTimeScopeServer _gameLifeTimeScope;
		
		[Inject]
		public void InstallDependencies(GameLifeTimeScopeServer lifeTimeScopeServer)
		{
			Debug.Log("Inject lifetime scope into server game mode");
			_gameLifeTimeScope = lifeTimeScopeServer;
		}

		public void Init()
		{
			Debug.Log("init Server game Mode");
			// spawn game state object
			var gameState = GameModeConfiguration.GetGameState;
			GetRunner.Spawn(gameState, Vector3.zero, Quaternion.identity, null, (runner, o) => {
				var baseGameState =  o.GetComponent<BaseGameState>();
				// This should be call before other process that use the dependencies run
				if (baseGameState is ICustomInjection inject)
				{
					inject.SetDependencies(_gameLifeTimeScope);
				}
				if (baseGameState is IBeforeSpawn beforeSpawn)
				{
					beforeSpawn.InitializeObjBeforeSpawn(runner, o);
				}
			});
		}

		public override void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
		{
			base.OnPlayerJoined(runner, player);
			Debug.Log($"Player joined: {player.PlayerId}");
			
			
			
			
			// Create a unique position for the player
			// Vector3 spawnPosition =
			// new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
			// NetworkObject networkPlayerObject =
			// runner.Spawn(_gameModeConfiguration.GetPlayerCharacter, spawnPosition, Quaternion.identity, player);
			// Keep track of the player avatars so we can remove it when they disconnect
			// _spawnedCharacters.Add(player, networkPlayerObject);
		}
	}
}