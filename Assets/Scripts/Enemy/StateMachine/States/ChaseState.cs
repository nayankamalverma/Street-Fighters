using UnityEngine;

namespace Assets.Scripts.Enemy
{
	public class ChaseState : IState
	{
		public ChaseState(EnemyController controller) : base(controller)
		{
			
		}

		public override void Enter()
		{
            controller.PauseMovement();

        }
        public override void Update()
        {
            if (controller.isInAttackRange)
            {
                controller.ChangeState(EnemyState.Idle);
                return; 
            }
            if (controller.canMove && controller.stateInfoLayer0.IsTag("Motion"))
            {
                Vector3 direction = controller.isFacingRight ? Vector3.right : Vector3.left;
                controller.transform.position += direction * controller.MoveSpeed * Time.deltaTime;

                controller.GetAnimator().SetBool("Right", controller.isFacingRight);
                controller.GetAnimator().SetBool("Left", !controller.isFacingRight);
            }
        }
		public override void Exit()
		{
            controller.GetAnimator().SetBool("Left", false);
            controller.GetAnimator().SetBool("Right", false);
        }
	}
}