using Com.JVL.Game.Player;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Com.JVL.Game.Client.Player
{
	public class GameLocalPlayer : BaseGameLocalPlayer
	{
		[Inject]
		private GameInstance _gameInstance;

		[Inject]
		private ClientGameInstance _clientGameInstance;

		[Inject]
		private GamePlayerController _playerController;

		#region - Lifecycle -
		
		private async UniTask Start()
		{
			await StartGame();
			// AddRunnerCallback(this);
			SpawnPlayerController();
		}
		
		
		#endregion - Lifecycle -

		#region - Subroutine -
		private void SpawnPlayerController()
		{
			
		}
		
		#endregion - Subroutine -

		// private void OnGUI()
		// {
		// 	if (GUI.Button(new Rect(0, 40, 200, 40), "Client"))
		// 	{
		// 		StartGame();
		// 	}
		// }
	}
}