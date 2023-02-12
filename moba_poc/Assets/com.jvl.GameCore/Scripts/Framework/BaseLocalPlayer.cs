using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;

namespace GameCore.Scripts.Framework
{
	public abstract class BaseLocalPlayer : BasePlayer
	{
		private INetworkRunnerCallbacks _networkRunnerCallbacksImplementation;

		#region INetworkRunnerCallbacks Execution
		protected INetworkRunnerCallbacks NetworkRunnerCallbacksImplementation {
			get => _networkRunnerCallbacksImplementation;
			set => _networkRunnerCallbacksImplementation = value;
		}

		public virtual void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
		{
			_networkRunnerCallbacksImplementation.OnPlayerJoined(runner, player);
		}

		public virtual void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
		{
			_networkRunnerCallbacksImplementation.OnPlayerLeft(runner, player);
		}

		public virtual void OnInput(NetworkRunner runner, NetworkInput input)
		{
			_networkRunnerCallbacksImplementation?.OnInput(runner, input);
		}

		public virtual void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
		{
			_networkRunnerCallbacksImplementation?.OnInputMissing(runner, player, input);
		}

		public virtual void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
		{
			_networkRunnerCallbacksImplementation?.OnShutdown(runner, shutdownReason);
		}

		public virtual void OnConnectedToServer(NetworkRunner runner)
		{
			_networkRunnerCallbacksImplementation?.OnConnectedToServer(runner);
		}

		public virtual void OnDisconnectedFromServer(NetworkRunner runner)
		{
			_networkRunnerCallbacksImplementation?.OnDisconnectedFromServer(runner);
		}

		public virtual void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
			byte[] token)
		{
			_networkRunnerCallbacksImplementation?.OnConnectRequest(runner, request, token);
		}

		public virtual void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress,
			NetConnectFailedReason reason)
		{
			_networkRunnerCallbacksImplementation?.OnConnectFailed(runner, remoteAddress, reason);
		}

		public virtual void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
		{
			_networkRunnerCallbacksImplementation?.OnUserSimulationMessage(runner, message);
		}

		public virtual void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
		{
			_networkRunnerCallbacksImplementation?.OnSessionListUpdated(runner, sessionList);
		}

		public virtual void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
		{
			_networkRunnerCallbacksImplementation?.OnCustomAuthenticationResponse(runner, data);
		}

		public virtual void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
		{
			_networkRunnerCallbacksImplementation?.OnHostMigration(runner, hostMigrationToken);
		}

		public virtual void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
		{
			_networkRunnerCallbacksImplementation?.OnReliableDataReceived(runner, player, data);
		}

		public virtual void OnSceneLoadDone(NetworkRunner runner)
		{
			_networkRunnerCallbacksImplementation?.OnSceneLoadDone(runner);
		}

		public virtual void OnSceneLoadStart(NetworkRunner runner)
		{
			_networkRunnerCallbacksImplementation?.OnSceneLoadStart(runner);
		}
		#endregion INetworkRunnerCallbacks Execution
	}
}