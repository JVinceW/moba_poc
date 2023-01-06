using Com.JVL.Game.Managers;
using UnityEngine.SceneManagement;
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
			builder.RegisterInstance(_gameSceneManager);
			builder.RegisterInstance(_playerManager);
		}
	}
}