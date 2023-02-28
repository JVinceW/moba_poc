using Com.JVL.Game.Player;
using Fusion;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Com.JVL.Game.GameMode
{

	public class BaseGameModeConfiguration : ScriptableObject
	{
		protected const string EGeneralCategory = "General";
		protected const string ESceneCategory = "Scene";

		[Foldout(EGeneralCategory)]
		[SerializeField]
		protected string GameModeName;

		[Foldout(EGeneralCategory)]
		[SerializeReference]
		protected BaseGameLocalPlayer LocalPlayer;

		[Foldout(EGeneralCategory)]
		[SerializeField]
		protected NetworkPrefabRef PlayerCharacter;

		[Foldout(EGeneralCategory)]
		[SerializeField]
		protected NetworkPrefabRef GameState;

		[Foldout(ESceneCategory)]
		[SerializeField]
		protected AssetReference DefaultSceneToLoad;

		[Foldout(ESceneCategory)]
		[SerializeField]
		protected AssetReference ClientSceneToLoad;

		[Foldout(ESceneCategory)]
		[SerializeField]
		protected AssetReference ServerSceneToLoad;

		public AssetReference GetDefaultSceneToLoad => DefaultSceneToLoad;

		public AssetReference GetClientSceneToLoad => ClientSceneToLoad;

		public AssetReference GetServerSceneToLoad => ServerSceneToLoad;

		public BaseGameLocalPlayer GetLocalPlayer => LocalPlayer;

		public NetworkPrefabRef GetPlayerCharacter => PlayerCharacter;

		public NetworkPrefabRef GetGameState => GameState;

		public string GetGameModeName => GameModeName;
	}
}