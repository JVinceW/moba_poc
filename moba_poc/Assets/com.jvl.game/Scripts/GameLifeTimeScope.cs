using Com.JVL.Game.Managers.GameSceneManager;
using GameCore.Scripts.Framework;
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
		
		private GameSceneManager _gameSceneManager;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		protected override void Configure(IContainerBuilder builder)
		{
			// Initialize manager class
			_gameSceneManager ??= new GameSceneManager();
			
			// Register game managers
			builder.RegisterInstance(_gameSceneManager);
			builder.RegisterInstance(_gameModeConfiguration);
			builder.RegisterEntryPoint<GameInstance>().AsSelf();
		}
	}
}