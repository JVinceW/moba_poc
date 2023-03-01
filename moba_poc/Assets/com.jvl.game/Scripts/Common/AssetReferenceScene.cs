using System;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Com.JVL.Game.Common
{
	/// <summary>
	/// <see cref="https://forum.unity.com/threads/something-like-assetreferencet-sceneasset.812085/#post-5432133"/>
	/// </summary>
	[Serializable]
	public class AssetReferenceScene : AssetReference
	{
		public AssetReferenceScene(string guid) : base(guid) { }

		public override bool ValidateAsset(string path)
		{
#if UNITY_EDITOR
			var type = UnityEditor.AssetDatabase.GetMainAssetTypeAtPath(path);
			return typeof(UnityEditor.SceneAsset).IsAssignableFrom(type);
#else
        return false;
#endif
		}

		public override bool ValidateAsset(Object obj)
		{
#if UNITY_EDITOR
			var type = obj.GetType();
			return typeof(UnityEditor.SceneAsset).IsAssignableFrom(type);
#else
        return false;
#endif
		}
		
#if UNITY_EDITOR
		/// <summary>
		/// Type-specific override of parent editorAsset.  Used by the editor to represent the asset referenced.
		/// </summary>
		public new UnityEditor.SceneAsset editorAsset => (UnityEditor.SceneAsset)base.editorAsset;
#endif
	}
}