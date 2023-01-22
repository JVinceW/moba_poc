using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Com.JVL.Game.Managers.GameAssetManager
{
	public class GameAssetManager : IGameManager, IDisposable
	{
		public async UniTask Initialize(params object[] args)
		{
			await Addressables.InitializeAsync();
		}

		public void Dispose()
		{
			Debug.Log("[GameAssetManager] Disposed");
		}
	}
}