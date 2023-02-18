using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.JVL.Game.Server.com.jvl.GameServer.Player
{
	public class ServerMainPlayer : MonoBehaviour
	{
		[SerializeField]
		private NetworkRunner _runner;

		[SerializeField]
		private NetworkSceneManagerBase _networkSceneManager;

		private string GetSessionName()
		{
			return "Default";
		}

		public async UniTask  StartServer()
		{
			_runner.ProvideInput = true;
			var args = new StartGameArgs {
				GameMode = Fusion.GameMode.Server,
				SessionName = GetSessionName(),
				Scene = SceneManager.GetActiveScene().buildIndex,
				SceneManager =  _networkSceneManager
			};
			await _runner.StartGame(args).AsUniTask();
		}

		private void Reset()
		{
			_runner = GetComponent<NetworkRunner>();
			_networkSceneManager = GetComponent<NetworkSceneManagerBase>();
		}
	}
}