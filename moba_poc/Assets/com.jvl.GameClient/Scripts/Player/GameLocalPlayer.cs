using Fusion;
using GameClient.Scripts;
using GameCore.Scripts.Framework;
using UnityEngine;
using VContainer;

namespace Com.JVL.Game.Client.Scripts.Player
{
	/// <summary>
	/// Local player class is inherit MonoBehaviour class.
	/// Every player will have this object on client side, server side dont hold any information of this object.
	/// </summary>
	public class GameLocalPlayer : BaseLocalPlayer, INetworkRunnerCallbacks
	{
		private ClientGameInstance _clientGameInstance;
		private GameInstance _gameInstance;
		[Inject]
		void InstallDependencies(ClientGameInstance clientGameInstance)
		{
			_clientGameInstance = clientGameInstance;
			_gameInstance = clientGameInstance.GameInstance;
		}
		
		private void Awake()
		{
			NetworkRunnerCallbacksImplementation = this;
		}
		
	}
}