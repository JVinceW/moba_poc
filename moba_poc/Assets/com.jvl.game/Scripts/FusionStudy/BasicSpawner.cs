using System;
using System.Collections.Generic;
using Com.JVL.GameInput;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.JVL.Game.FusionStudy
{
	public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
	{
		[SerializeField] private NetworkPrefabRef _playerPrefab;
		private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
		private NetworkRunner _runner;
		private PlayerInputActions _playerInputActions;
		

		public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
		{
			if (runner.IsServer)
			{
				// Create a unique position for the player
				Vector3 spawnPosition =
					new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
				NetworkObject networkPlayerObject =
					runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
				// Keep track of the player avatars so we can remove it when they disconnect
				_spawnedCharacters.Add(player, networkPlayerObject);
			}
			
			if (_playerInputActions == null)
			{
				_playerInputActions = new PlayerInputActions();
			}
			_playerInputActions.Enable();
		}

		public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
		{
			// Find and remove the players avatar
			if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
			{
				runner.Despawn(networkObject);
				_spawnedCharacters.Remove(player);
			}
			_playerInputActions.Disable();
		}

		public void OnInput(NetworkRunner runner, NetworkInput input)
		{
			var data = new NetworkInputData();
			if (_playerInputActions == null)
			{
				return;
			}
			var moving =  _playerInputActions.TestMoving.AWSD.ReadValue<Vector2>();
			switch (moving.x)
			{
				case > 0:
					data.direction += Vector3.right;
					break;
				case < 0:
					data.direction += Vector3.left;
					break;
			}

			switch (moving.y)
			{
				case > 0:
					data.direction += Vector3.forward;
					break;
				case < 0:
					data.direction += Vector3.back;
					break;
			}
			input.Set(data);
		}

		public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

		public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

		public void OnConnectedToServer(NetworkRunner runner) { }

		public void OnDisconnectedFromServer(NetworkRunner runner) { }

		public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
			byte[] token) { }

		public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

		public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

		public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

		public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

		public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

		public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
		{
			throw new NotImplementedException();
		}

		public void OnSceneLoadDone(NetworkRunner runner) { }

		public void OnSceneLoadStart(NetworkRunner runner) { }

		private async void StartGame(GameMode gameMode)
		{
			_runner = gameObject.AddComponent<NetworkRunner>();
			_runner.ProvideInput = true;


			await _runner.StartGame(new StartGameArgs {
				GameMode = gameMode,
				SessionName = "Test Room",
				Scene = SceneManager.GetActiveScene().buildIndex,
			});
		}

		private void OnGUI()
		{
			if (_runner == null)
			{
				if (GUI.Button(new Rect(0,0,200,40), "Host"))
				{
					StartGame(GameMode.Host);
				}
				if (GUI.Button(new Rect(0,40,200,40), "Join"))
				{
					StartGame(GameMode.Client);
				}
			}
		} }
}