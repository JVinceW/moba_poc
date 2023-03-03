using Com.JVL.Game.Common;
using Fusion;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Player
{
	public partial class GamePlayerController : NetworkBehaviour, ICustomInjection
	{
		private GameInstance _gameInstance;
		private BaseGameLocalPlayer _gameLocalPlayer;
		
		private GamePlayerCharacter _playerCharacter;

		GamePlayerCharacter GetPlayerCharacter() => _playerCharacter;

		public void InstallDependencies(GamePlayerCharacter playerCharacter)
		{
			_playerCharacter = playerCharacter;
		}

		public override void Spawned()
		{
			if (Runner.IsServer)
			{
				Server_Spawned();
			} else if (Runner.IsClient)
			{
				Client_Spawned();
			}
		}

		public void SetDependencies(IObjectResolver objectResolver)
		{
			_gameLocalPlayer = objectResolver.Resolve<BaseGameLocalPlayer>();
			_gameInstance = objectResolver.Resolve<GameInstance>();
		}
	}
}