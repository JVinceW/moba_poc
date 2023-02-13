using UnityEngine;

namespace GameCore.Scripts.Framework
{
	[CreateAssetMenu(fileName = "XXX_GameMode", menuName = "Project/Create Game Mode Config", order = 0)]

	public class BaseGameModeConfiguration : ScriptableObject
	{
		[SerializeField]
		protected string GameModeName;

		[SerializeField]
		protected BaseLocalPlayer LocalPlayer;

		[SerializeField]
		protected string DefaultSceneToLoad;

		[SerializeField]
		protected string ClientSceneToLoad;

		[SerializeField]
		protected string ServerSceneToLoad;

		public string GetDefaultSceneToLoad => DefaultSceneToLoad;

		public string GetClientSceneToLoad => ClientSceneToLoad;

		public string GetServerSceneToLoad => ServerSceneToLoad;

		public BaseLocalPlayer GetLocalPlayer => LocalPlayer;

		public string GetGameModeName => GameModeName;
	}
}