using Com.JVL.Game.Managers;
using Com.JVL.Game.Managers.GameSceneManager;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game
{
	public class GameLifeTimeScope : LifetimeScope
	{
		[SerializeField] private string _clientInstanceScene = "";
		[SerializeField] private string _serverInstanceScene = "";

		private GameSceneManager _gameSceneManager;
		private PlayerManager _playerManager;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		protected override void Configure(IContainerBuilder builder)
		{
			//TODO: add define symbol to check if server or client then we will use the scene Name base on that
			_gameSceneManager ??= new GameSceneManager(_clientInstanceScene);
			_playerManager ??= new PlayerManager();

			builder.RegisterEntryPoint<GameInstance>();

			// Register game managers
			builder.RegisterInstance(_playerManager);
			builder.RegisterInstance(_gameSceneManager);
		}
	}
}