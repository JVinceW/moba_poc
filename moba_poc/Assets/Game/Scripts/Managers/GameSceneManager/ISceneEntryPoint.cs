using Cysharp.Threading.Tasks;

namespace Com.JVL.Game.Managers.GameSceneManager
{
	/// <summary>
	/// Interface of scene instance, use for DI process of VContainer
	/// </summary>
	public interface ISceneEntryPoint
	{
		public UniTask OnSceneLoaded()
		{
			return UniTask.CompletedTask;
		}

		public UniTask OnSceneUnLoaded()
		{
			return UniTask.CompletedTask;
		}
	}
}