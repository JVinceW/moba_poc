using Com.JVL.Game.GameMode;
using Com.JVL.Game.Player;
using Com.JVL.Game.Server.Player;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Com.JVL.Game.Server
{
	public class ServerGameMode : BaseGameMode
	{
		[Inject]
		private GameInstance _gameInstance;

		[SerializeField]
		private NetworkRunner _runner;

		[SerializeField]
		private NetworkSceneManagerBase _networkSceneManager;

		[SerializeField]
		private NetworkPrefabRef _playerStatePrefabRef;

		[SerializeField]
		private NetworkPrefabId _id;

		public void Init()
		{
			Debug.Log("init Server game Mode");
		}

		public override void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
		{
			base.OnPlayerJoined(runner, player);
			Debug.Log($"Player joined: {player.PlayerId}");
			var gamePlayerState = runner.Spawn(_playerStatePrefabRef, Vector3.zero, 
				Quaternion.identity, 
				player, (networkRunner, o) => {
					var playerStateServer = o.GetComponent<GamePlayerStateServer>();
					Debug.Log("Server assign Input authority");
					playerStateServer.NotificationSpawned(player);
				});
			// Create a unique position for the player
			// Vector3 spawnPosition =
			// new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
			// NetworkObject networkPlayerObject =
			// runner.Spawn(_gameModeConfiguration.GetPlayerCharacter, spawnPosition, Quaternion.identity, player);
			// Keep track of the player avatars so we can remove it when they disconnect
			// _spawnedCharacters.Add(player, networkPlayerObject);
		}

		private string GetSessionName()
		{
			return "Default";
		}

		public async UniTask StartServer()
		{
			_runner.ProvideInput = true;
			var args = new StartGameArgs {
				GameMode = Fusion.GameMode.Server,
				SessionName = GetSessionName(),
				Scene = SceneManager.GetActiveScene().buildIndex,
				SceneManager = _networkSceneManager
			};
			await _runner.StartGame(args).AsUniTask();
			Debug.Log("Game Server stated");
		}

		private void Reset()
		{
			_runner = GetComponent<NetworkRunner>();
			_networkSceneManager = GetComponent<NetworkSceneManagerBase>();
		}
	}
}