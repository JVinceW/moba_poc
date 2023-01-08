using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Com.JVL.Game.Managers.GameAssetManager
{
	public class GameAssetManager : IGameManager
	{
		public async UniTask Initialize(params object[] args)
		{
			await Addressables.InitializeAsync();
		}
	}
}