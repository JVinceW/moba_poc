using Com.JVL.Game.Common;
using Com.JVL.Game.Managers.GameTimeManager;
using Fusion;
using VContainer;

namespace Com.JVL.Game.GameMode
{
	public class BasicGameState : BaseGameState, IBeforeSpawn
	{
		protected GameTimeManager GameTimeManager;

		#region IBeforeSpawn Implementation
		public void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
		{
			GameTimeManager.SetDependencies(this, runner);
		}
		#endregion IBeforeSpawn Implementation

		#region - ICustomInjection Implementation -
		public override void SetDependencies(IObjectResolver objectResolver)
		{
			base.SetDependencies(objectResolver);
			GameTimeManager = objectResolver.Resolve<GameTimeManager>();
		}
		#endregion - ICustomInjection Implementation -
	}
}