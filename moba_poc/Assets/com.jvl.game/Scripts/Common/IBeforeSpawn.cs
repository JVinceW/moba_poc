using Fusion;

namespace Com.JVL.Game.Common
{
	public interface IBeforeSpawn
	{
		void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj);
	}
}