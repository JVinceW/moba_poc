using Com.JVL.Game.Common;
using Com.JVL.Game.Managers.GameTimeManager;
using Fusion;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.GameMode
{
	public class BasicGameState : BaseGameState, IBeforeSpawn, ICustomInjection
	{
		private GameTimeManager _gameTimeManager;

		#region IBeforeSpawn Implementation
		public void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
		{
			_gameTimeManager.SetDependencies(this, runner);
		}
		#endregion IBeforeSpawn Implementation

		#region ICustomInjection Implementation
		public void SetDependencies(LifetimeScope currentScope)
		{
			_gameTimeManager = currentScope.Container.Resolve<GameTimeManager>();
			Debug.Log($"Game time manager: {_gameTimeManager == null}");
		}
		#endregion ICustomInjection Implementation
	}
}