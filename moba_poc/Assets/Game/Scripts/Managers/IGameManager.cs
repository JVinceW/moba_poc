using Cysharp.Threading.Tasks;

namespace Com.JVL.Game.Managers
{
	public interface IGameManager
	{
		/// <summary>
		/// This method will be called immediately right after the object constructed
		/// </summary>
		/// <param name="args"></param>
		public async UniTask Initialize(params object[] args)
		{
			
		}
	}
}