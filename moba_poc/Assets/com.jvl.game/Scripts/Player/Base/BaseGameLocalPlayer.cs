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

		protected NetworkRunner NetworkRunner => _runner;

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

		public virtual void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }

		public virtual void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

		public virtual void OnInput(NetworkRunner runner, NetworkInput input) { }

		public virtual void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

		public virtual void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

		public virtual void OnConnectedToServer(NetworkRunner runner) { }

		public virtual void OnDisconnectedFromServer(NetworkRunner runner) { }

		public virtual void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
			byte[] token) { }

		public virtual void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress,
			NetConnectFailedReason reason) { }

		public virtual void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

		public virtual void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

		public virtual void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

		public virtual void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

		public virtual void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

		public virtual void OnSceneLoadDone(NetworkRunner runner) { }

		public virtual void OnSceneLoadStart(NetworkRunner runner) { }
	}
}