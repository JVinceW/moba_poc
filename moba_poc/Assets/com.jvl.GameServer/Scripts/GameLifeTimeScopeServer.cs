using Com.JVL.Game.Server.com.jvl.GameServer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Server
{
	public class GameLifeTimeScopeServer : LifetimeScope
	{
		[SerializeReference]
		private ServerGameMode _serverGameMode;

		private void Start()
		{
			DontDestroyOnLoad(this);
		}
		
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponentInNewPrefab(_serverGameMode, Lifetime.Singleton);
			builder.RegisterEntryPoint<ServerGameInstance>().AsSelf();
		}
	}
}