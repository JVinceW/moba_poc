using Com.JVL.Game.Client.Scripts.Player;
using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameClient.Scripts
{
	public class GameClient_GameLifeTimeScope : LifetimeScope
	{
		[Expandable]
		[SerializeField]
		private GameModeConfiguration _gameModeConfiguration;

		private void Start()
		{
			DontDestroyOnLoad(this);
		}

		protected override void Configure(IContainerBuilder builder)
		{
			// Memo: AsSelf is important if we want to use this instance in many places
			builder.RegisterEntryPoint<ClientGameInstance>()
				.AsSelf()
				.WithParameter(_gameModeConfiguration);
		}
	}
}