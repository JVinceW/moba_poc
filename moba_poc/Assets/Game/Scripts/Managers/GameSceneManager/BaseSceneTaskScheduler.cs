using GameCore.Scripts;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Com.JVL.Game.Managers.GameSceneManager
{
	/// <summary>
	/// Base task scheduler task use for load scene
	/// </summary>
	public class BaseSceneTaskScheduler : BaseTaskScheduler
	{
		/// <summary>
		/// Context of the scene
		/// </summary>
		public BaseSceneContext SceneContext;

		/// <summary>
		/// Instance of the scene. 
		/// </summary>
		public SceneInstance SceneInstance;

		public bool IsNullSceneInstance => string.IsNullOrEmpty(SceneInstance.Scene.name);

		/// <summary>
		///Initialization of scheduler.
		/// </summary>
		protected override void InitializeScheduler() { }
	}
}