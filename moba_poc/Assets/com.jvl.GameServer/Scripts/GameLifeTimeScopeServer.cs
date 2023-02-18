using Com.JVL.Game.Server.com.jvl.GameServer.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Server.com.jvl.GameServer
{
	public class GameLifeTimeScopeServer : LifetimeScope
	{
		[SerializeField]
		private ServerMainPlayer _serverMainPlayer;
		
		private void Start()
		{
			DontDestroyOnLoad(this);
		}
		
		protected override void Configure(IContainerBuilder builder)
		{
			
			builder.RegisterComponentInNewPrefab(_serverMainPlayer, Lifetime.Singleton);
			builder.RegisterEntryPoint<ServerGameInstance>().AsSelf();
		}
	}
}