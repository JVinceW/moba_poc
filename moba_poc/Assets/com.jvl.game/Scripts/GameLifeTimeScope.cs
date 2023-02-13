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
		
		[SerializeField]
		private string _clientInstanceScene = "";

		[SerializeField]
		private string _serverInstanceScene = "";
		
		private GameSceneManager _gameSceneManager;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		protected override void Configure(IContainerBuilder builder)
		{
			//TODO: add define symbol to check if server or client then we will use the scene Name base on that
			_gameSceneManager ??= new GameSceneManager(_clientInstanceScene);

			// Register game managers
			builder.RegisterInstance(_gameSceneManager);
			builder.RegisterEntryPoint<GameInstance>().AsSelf();
			builder.RegisterInstance(_gameModeConfiguration);
		}
	}
}