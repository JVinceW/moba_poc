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
			Debug.Log("Inject lifetime scope into server game mode");
			_gameLifeTimeScope = lifeTimeScopeServer;
		}

		public void Init()
		{
			Debug.Log("[ServerGameMode] Start init server");
			// spawn game state object
			SpawnGameState();
		}

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

		public override void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
		{
			var playerStateRef = GameModeConfiguration.GetPlayerState;
			var playerState = runner.Spawn(playerStateRef, Vector3.zero, Quaternion.identity,
				onBeforeSpawned: (inRunner, o) => {
					var gamePlayerStateComponent = o.GetComponent<GamePlayerState>();
					if (gamePlayerStateComponent is IBeforeSpawn beforeSpawn)
					{
						beforeSpawn.InitializeObjBeforeSpawn(inRunner, o);
					}
				});
		}
	}
}