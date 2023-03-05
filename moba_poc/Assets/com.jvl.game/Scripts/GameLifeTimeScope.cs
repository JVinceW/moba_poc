using Com.JVL.Game.Common;
using Com.JVL.Game.GameMode;
using Com.JVL.Game.Managers.GameSceneManager;
using Com.JVL.Game.Managers.GameTimeManager;
using Com.JVL.Game.Managers.PlayerManager;
using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game
{
	public class GameLifeTimeScope : LifetimeScope
	{
		private static LifetimeScope _lifetimeScope;
		
		[Expandable]
		[SerializeReference]
		private BaseGameModeConfiguration _gameModeConfiguration;
		
		private readonly GameSceneManager _gameSceneManager = new();

		private readonly PlayerManager _playerManager = new();

		private readonly GameTimeManager _gameTimeManager = new();

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
			_lifetimeScope = this;
		}

		protected override void Configure(IContainerBuilder builder)
		{
			// Register game managers
			builder.RegisterInstance(_gameSceneManager);
			builder.RegisterInstance(_playerManager);
			builder.RegisterInstance(_gameModeConfiguration);
			builder.RegisterInstance(_gameTimeManager);
			builder.RegisterComponentInNewPrefab(_gameModeConfiguration.GetLocalPlayer, Lifetime.Singleton);
			builder.RegisterInstance(_gameModeConfiguration.GetPlayerController);
			builder.RegisterEntryPoint<GameInstance>().AsSelf();
		}

		/// <summary>
		/// The way to inject lifetime scope into NetworkBehaviour.
		/// I know this is so dirty but I'm trying to use VContainer with it
		/// </summary>
		/// <param name="customInjection"></param>
		public static void InstallDependenciesResolver(ICustomInjection customInjection)
		{
			if (!_lifetimeScope)
			{
				_lifetimeScope = FindFirstObjectByType<LifetimeScope>();
			}
			customInjection.SetDependencies(_lifetimeScope.Container);
		}
	}
}