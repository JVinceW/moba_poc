using Com.JVL.Game.GameMode;
using Com.JVL.Game.Managers.GameSceneManager;
using Com.JVL.Game.Managers.PlayerManager;
using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game
{
	public class GameLifeTimeScope : LifetimeScope
	{
		[Expandable]
		[SerializeReference]
		private BaseGameModeConfiguration _gameModeConfiguration;
		
		private readonly GameSceneManager _gameSceneManager = new();

		private readonly PlayerManager _playerManager = new();

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		protected override void Configure(IContainerBuilder builder)
		{
			// Register game managers
			builder.RegisterInstance(_gameSceneManager);
			builder.RegisterInstance(_playerManager);
			builder.RegisterInstance(_gameModeConfiguration);
			builder.RegisterEntryPoint<GameInstance>().AsSelf();
		}
	}
}