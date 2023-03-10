using Com.JVL.Game.Player;
using Fusion;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Com.JVL.Game.GameMode
{
	public class BaseGameModeConfiguration : ScriptableObject
	{
		// ReSharper disable once MemberCanBePrivate.Global
		protected const string EGeneralCategory = "General";
		// ReSharper disable once MemberCanBePrivate.Global
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
		protected NetworkPrefabRef PlayerController;
		
		[Foldout(EGeneralCategory)]
		[SerializeField]
		protected NetworkPrefabRef PlayerState;

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
		
		[Foldout(ESceneCategory)]
		[SerializeField]
		protected int GameMainSceneIndex;

		public AssetReference GetDefaultSceneToLoad => DefaultSceneToLoad;

		public AssetReference GetClientSceneToLoad => ClientSceneToLoad;

		public AssetReference GetServerSceneToLoad => ServerSceneToLoad;
		
		public int GetGameMainSceneIndex => GameMainSceneIndex;

		public BaseGameLocalPlayer GetLocalPlayer => LocalPlayer;

		public NetworkPrefabRef GetPlayerCharacter => PlayerCharacter;

		public NetworkPrefabRef GetPlayerController => PlayerController;

		public NetworkPrefabRef GetPlayerState => PlayerState;

		public NetworkPrefabRef GetGameState => GameState;

		public string GetGameModeName => GameModeName;
	}
}