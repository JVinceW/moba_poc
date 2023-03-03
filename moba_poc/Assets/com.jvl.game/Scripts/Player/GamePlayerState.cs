using Com.JVL.Game.Common;
using Fusion;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Com.JVL.Game.Player
{
	public partial class GamePlayerState : NetworkBehaviour, ICustomInjection
	{
		private IObjectResolver _objectResolver;
		private GameInstance _gameInstance;

		#region - Lifecycle -
		public override void Spawned()
		{
			Debug.Log($"GamePlayerState - HasStateAuthority: {HasStateAuthority} - HasInputAuthority: {HasInputAuthority} - IsProxy: {IsProxy}");
			if (Runner.IsServer)
			{
				
			} else if (Runner.IsClient)
			{
				GameLifeTimeScope.InstallDependenciesResolver(this);
				Debug.LogWarning($"Is Game Instance is null? {_gameInstance == null}");
			}
		}

		[Rpc(RpcSources.StateAuthority, RpcTargets.Proxies)]
		private void RPC_Spawned()
		{
			Debug.Log("Client!!!!");
		}

		public void SetDependencies(IObjectResolver objectResolver)
		{
			_objectResolver = objectResolver;
			_gameInstance = objectResolver.Resolve<GameInstance>();
		}
		#endregion - Lifecycle -
	}
}