using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Com.JVL.Game.GameMode
{
	/* 
	 * Game mode should not be NetworkBehavior, we don't want client know anything about this
	 */
	[RequireComponent(typeof(NetworkRunner))]
	public class BaseGameMode : MonoBehaviour, INetworkRunnerCallbacks, IGameMode
	{
		[ReadOnly]
		[Inject]
		[SerializeReference]
		protected BaseGameModeConfiguration GameModeConfiguration;
		
		[Inject]
		private GameInstance _gameInstance;

		[SerializeField]
		private NetworkRunner _runner;
		
		[SerializeField]
		private NetworkSceneManagerBase _networkSceneManager;

		protected BaseGameState BaseGameState;

		protected NetworkRunner GetRunner => _runner;

		protected virtual void Reset()
		{
			_runner = GetComponent<NetworkRunner>();
			_networkSceneManager = GetComponent<NetworkSceneManagerBase>();
		}

		private string _gameModeName;

		public T GetGameState<T>() where T : BaseGameState
		{
			return (T)BaseGameState;
		}
		
		protected virtual string GetSessionName()
		{
			return "Default";
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

		#region IGameMode Implementation
		public BaseGameModeConfiguration GameModeConfig {
			get => GameModeConfiguration;
		}

		public T GameModeConfigByType<T>() where T : BaseGameModeConfiguration
		{
			return (T)GameModeConfig;
		}

		string IGameMode.GameModeName => GameModeConfig.GetGameModeName;
		public async UniTask StartGame(Fusion.GameMode gameMode)
		{
			_runner.ProvideInput = true;
			var args = new StartGameArgs {
				GameMode = gameMode,
				SessionName = GetSessionName(),
				Scene = SceneManager.GetActiveScene().buildIndex,
				SceneManager = _networkSceneManager
			};
			await _runner.StartGame(args).AsUniTask();
			Debug.Log($"[BaseGameMode] game stated: {gameMode}");
		}
		#endregion IGameMode Implementation
	}
	
	
}