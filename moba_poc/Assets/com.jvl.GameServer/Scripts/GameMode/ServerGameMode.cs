using Com.JVL.Game.Common;
using Com.JVL.Game.GameMode;
using Com.JVL.Game.Player;
using Fusion;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Server
{
	public class ServerGameMode : BaseGameMode
	{
		private LifetimeScope _gameLifeTimeScope;

		[Inject]
		public void InstallDependencies(LifetimeScope lifeTimeScopeServer)
		{
			_gameLifeTimeScope = lifeTimeScopeServer;
		}

		public void Init()
		{
			// spawn game state object
			SpawnGameState();
		}

		public override void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
		{
			SpawnPlayerState(player, runner);
			SpawnPlayerCharacter(player);
			SpawnPlayerController(player);
		}

		public override void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
		{
			SpawnedCharacters.Remove(player);
			BaseGameState.PlayerLeft(player);
		}

		#region - Subroutines -
		private void SpawnGameState()
		{
			var gameState = GameModeConfiguration.GetGameState;
			var gameStateNetworkObject = Runner.Spawn(gameState, Vector3.zero, Quaternion.identity, null,
				(runner, o) => {
					var gameStateComponent = o.GetComponent<BaseGameState>();
					if (gameStateComponent is ICustomInjection inject)
					{
						inject.SetDependencies(_gameLifeTimeScope);
					}

					if (gameStateComponent is IBeforeSpawn beforeSpawn)
					{
						beforeSpawn.InitializeObjBeforeSpawn(runner, o);
					}
				});
			BaseGameState = gameStateNetworkObject.GetComponent<BaseGameState>();
		}

		private void SpawnPlayerCharacter(PlayerRef playerRef)
		{
			var playerCharacter = GameModeConfiguration.GetPlayerCharacter;
			var playerCharacterNWObject = Runner.Spawn(playerCharacter, Vector3.zero, Quaternion.identity, playerRef);
			playerCharacterNWObject.gameObject.name = $"PlayerCharacter_{playerRef.PlayerId}";
		}

		private void SpawnPlayerController(PlayerRef playerRef)
		{
			var playerController = GameModeConfiguration.GetPlayerController;
			var playerControllerNWObject = Runner.Spawn(playerController, Vector3.zero, Quaternion.identity);
			playerControllerNWObject.gameObject.name = $"PlayerController{playerRef.PlayerId}";
		}

		private void SpawnPlayerState(PlayerRef player, NetworkRunner runner)
		{
			var playerStateRef = GameModeConfiguration.GetPlayerState;
			// Spawn player state
			var playerStateObject = runner.Spawn(playerStateRef, Vector3.zero, Quaternion.identity,
				onBeforeSpawned: (inRunner, o) => {
					var gamePlayerStateComponent = o.GetComponent<GamePlayerState>();
					if (gamePlayerStateComponent is IBeforeSpawn beforeSpawn)
					{
						beforeSpawn.InitializeObjBeforeSpawn(inRunner, o);
					}
				});
			SpawnedCharacters.Add(player, playerStateObject);
			BaseGameState.PlayerJoin(player, playerStateObject.GetComponent<GamePlayerState>());
		}
		#endregion - Subroutines -
	}
}