using Fusion;

namespace Com.JVL.Game.FusionStudy
{
	public class Ball : NetworkBehaviour
	{
		[Networked]
		private TickTimer life { get; set; }

		public override void FixedUpdateNetwork()
		{
			if(life.Expired(Runner))
				Runner.Despawn(Object);
			else
				transform.position += 5 * transform.forward * Runner.DeltaTime;
		}
		public void Init()
		{
			life = TickTimer.CreateFromSeconds(Runner, 5.0f);
		}
	}
}