using Fusion;
using UnityEngine;

namespace Com.JVL.Game.FusionStudy
{
	public class PhysicBall : NetworkBehaviour
	{
		[Networked] private TickTimer life { get; set; }

		public void Init(Vector3 forward)
		{
			life = TickTimer.CreateFromSeconds(Runner, 5.0f);
			GetComponent<Rigidbody>().velocity = forward;
		}

		public override void FixedUpdateNetwork()
		{
			if(life.Expired(Runner))
				Runner.Despawn(Object);
		}
	}
}