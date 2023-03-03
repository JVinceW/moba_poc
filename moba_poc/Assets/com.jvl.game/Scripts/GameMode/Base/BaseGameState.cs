using System.Collections.Generic;
using Com.JVL.Game.Common;
using Com.JVL.Game.Player;
using Fusion;
using VContainer;

namespace Com.JVL.Game.GameMode
{
	public class BaseGameState : NetworkBehaviour, ICustomInjection
	{
		private IObjectResolver _objectResolver;
		private Dictionary<PlayerRef, GamePlayerState> PlayerStates = new();

		#region - Methods -
		public void PlayerJoin(PlayerRef player, GamePlayerState playerState)
		{
			PlayerStates.Add(player, playerState);
		}

		public void PlayerLeft(PlayerRef player)
		{
			PlayerStates.Remove(player);
		}

		#region - Implementation ICustomInjection -
		public virtual void SetDependencies(IObjectResolver objectResolver)
		{
			_objectResolver = objectResolver;
		}
		#endregion - Implementation ICustomInjection -
		#endregion - Methods -
	}
}