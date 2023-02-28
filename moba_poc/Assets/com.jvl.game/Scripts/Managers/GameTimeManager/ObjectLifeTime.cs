using Fusion;

namespace Com.JVL.Game.Managers.GameTimeManager
{
	public struct ObjectLifeTime : INetworkStruct
	{
		public NetworkId NetworkId;
		public TickTimer Timer;
	}
}