using Fusion;
using UnityEngine;

namespace Com.JVL.Game.FusionStudy
{
	public class Player : NetworkBehaviour
	{
		private NetworkCharacterControllerPrototype _cc;
		[SerializeField] private Ball _prefabBall;

		[Networked]
		private TickTimer delay { get; set; }

		private Vector3 _forward;

		private void Awake()
		{
			_cc = GetComponent<NetworkCharacterControllerPrototype>();
			_forward = transform.forward;
		}

		public override void FixedUpdateNetwork()
		{
			if (GetInput(out NetworkInputData data))
			{
				data.direction.Normalize();
				_cc.Move(5 * data.direction * Runner.DeltaTime);

				if (data.direction.sqrMagnitude > 0)
					_forward = data.direction;

				if (delay.ExpiredOrNotRunning(Runner))
				{
					if ((data.buttons & NetworkInputData.MOUSEBUTTON1) != 0)
					{
						delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
						Runner.Spawn(_prefabBall,
							transform.position + _forward, Quaternion.LookRotation(_forward),
							Object.InputAuthority, (runner, o) => {
								// Initialize the Ball before synchronizing it
								o.GetComponent<Ball>().Init();
							});
					}
				}
			}
		}
	}
}