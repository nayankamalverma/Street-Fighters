using UnityEngine;

namespace Assets.Scripts.Enemy
{
	public class IdleState : IState
	{
		public IdleState(EnemyController controller) : base(controller) { }

		public override void Enter()
		{
			Debug.Log("idle");
			controller.GetAnimator().SetBool("Left", false);
			controller.GetAnimator().SetBool("Right", false);
			controller.GetAnimator().SetBool("Crouch", false);
		}
		public override void Update()
		{
            if (!controller.isInAttackRange)
			{
				controller.ChangeState(EnemyState.Chase);
			}
			if (controller.isInAttackRange) 
			{
				controller.ChangeState(EnemyState.Attack);
			}
		}
	}
}