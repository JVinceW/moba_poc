using Com.JVL.Game.Managers;
using Com.JVL.Game.Managers.GameSceneManager;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game
{
	public class GameLifeTimeScope : LifetimeScope
	{
		private GameSceneManager _gameSceneManager;
		private PlayerManager _playerManager;

		protected override void Configure(IContainerBuilder builder)
		{
			_gameSceneManager ??= new GameSceneManager();
			_playerManager ??= new PlayerManager();
			
			builder.RegisterEntryPoint<GameInstance>();
			
			// Register game managers
			builder.RegisterInstance(_gameSceneManager);
			builder.RegisterInstance(_playerManager);
		}
	}
}