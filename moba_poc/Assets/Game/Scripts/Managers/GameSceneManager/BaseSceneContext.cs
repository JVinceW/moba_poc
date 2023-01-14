using System;
using UnityEngine.SceneManagement;

namespace Com.JVL.Game.Managers.GameSceneManager
{
	/// <summary>
	/// Context data to send to this scene from the previous scene. This is the data of gameplay or something
	/// you want to pass to this scene to initialize scene
	/// </summary>
	[Serializable]
	public class BaseSceneContext
	{
		/// <summary>
		/// Name of the scene
		/// </summary>
		public string SceneName;

		/// <summary>
		/// This value is use for load the scene asset
		/// </summary>
		public string SceneAssetAddress;

		/// <summary>
		/// Mode to load scene
		/// </summary>
		public LoadSceneMode LoadSceneMode;

		/// <summary>
		/// Name of the previous scene
		/// </summary>
		public string PreviousSceneName;

		/// <summary>
		/// This flag use to control the scene history. If true, this scene will be add to scene stack and we can use BackScene method of game scene manager;
		/// <see cref="GameSceneManager.BackToPreviousScene"/>
		/// </summary>
		public bool IsAddToSceneStack;
	}
}