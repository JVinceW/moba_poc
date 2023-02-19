using Com.JVL.Game.GameMode;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Com.JVL.Game.Player
{
	public class BaseGameLocalPlayer : MonoBehaviour
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
			Debug.Log("Game Stated client");
		}

		private void Reset()
		{
			_runner = GetComponent<NetworkRunner>();
			_networkSceneManager = GetComponent<NetworkSceneManagerBase>();
		}
	}
}