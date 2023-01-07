using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameClient.Scripts
{
	public class Client_GameLifeTimeScope : LifetimeScope
	{
		[SerializeField] private LocalPlayerBehaviour _localPlayerBehaviour;
		private LocalGamePlayer _localGamePlayer;

		protected override void Configure(IContainerBuilder builder)
		{
			// Testing Plain C# class with VContainer
			builder.RegisterEntryPoint<LocalGamePlayer>();
			builder.Register<PlayerInputService>(Lifetime.Singleton);

			// Testing Monobehavior class with VContainer
			// builder.RegisterComponent(_localPlayerBehaviour);
			// Result: if we make the gameobject that contain this component to prefab, then it's will register the component of the prefab but not create anything new


			// builder.RegisterComponentInHierarchy<LocalPlayerBehaviour>();

			// builder.RegisterComponentInNewPrefab(_localPlayerBehaviour, Lifetime.Singleton);
			builder.RegisterComponentOnNewGameObject<LocalPlayerBehaviour>(Lifetime.Singleton, "My Test NewObject")
				.DontDestroyOnLoad();
		}
	}
}