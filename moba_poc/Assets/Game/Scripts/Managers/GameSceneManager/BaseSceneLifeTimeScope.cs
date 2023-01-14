using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Managers.GameSceneManager
{
	/// <summary>
	/// Each scene can have a unique container builder
	/// </summary>
	public class BaseSceneLifeTimeScope : LifetimeScope
	{
		[SerializeField] private BaseSceneEntryPoint _sceneEntryPoint;
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponent(_sceneEntryPoint);
		}
	}
}