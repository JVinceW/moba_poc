using System;
using System.Collections.Generic;
using Com.JVL.Game.GameMode;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Com.JVL.Game.Player
{
	public class BaseGameLocalPlayer : MonoBehaviour, INetworkRunnerCallbacks
	{
		[SerializeField]
		private NetworkRunner _runner;

		[SerializeField]
		private NetworkSceneManagerBase _networkSceneManager;

		[Inject]
		private BaseGameModeConfiguration _gameModeConfiguration;

		public NetworkRunner Runner => _runner;

		protected virtual string GetSessionName()
		{
			return "Default";
		}

		// ReSharper disable once MemberCanBeProtected.Global
		public virtual async UniTask StartGame()
		{
			_runner.ProvideInput = true;
			var args = new StartGameArgs {
				GameMode = Fusion.GameMode.Client,
				SessionName = GetSessionName(),
				Scene = SceneManager.GetActiveScene().buildIndex,
				SceneManager = _networkSceneManager
			};
			await _runner.StartGame(args).AsUniTask();
		}

		private void Reset()
		{
			_runner = GetComponent<NetworkRunner>();
			_networkSceneManager = GetComponent<NetworkSceneManagerBase>();
		}

		public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }

		public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

		public void OnInput(NetworkRunner runner, NetworkInput input) { }

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

		public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

		public void OnSceneLoadDone(NetworkRunner runner) { }

		public void OnSceneLoadStart(NetworkRunner runner) { }
	}
}