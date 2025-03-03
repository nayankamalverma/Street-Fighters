using Assets.Scripts.Utilities;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
	public class AttackState : IState
	{
		public AttackState(EnemyController controller) : base(controller){ }

		public override void Enter()
		{
			CoroutineRunner.Instance.StartCoroutine(RandomAttack());
        }

		public override void Update()
        {
            if (!controller.isInAttackRange)
            {
				Debug.Log("attack chase");
                controller.ChangeState(EnemyState.Chase);
			}
		}

		private void PerformRandomAttack()
		{
			int attackType = Random.Range(1, 6);
			switch (attackType)
			{
				case 1:
					Punch1();
					break;
				case 2:
					HookedPunch();
					break;
				case 3:
					Kick();
					break;
				case 4:
					HeadButt();
					break;
				//case 5:
				//	LegSweep();
				//	break;
				default:
					Debug.LogWarning("Invalid attack type selected.");
					break;
			}
			CoroutineRunner.Instance.StartCoroutine(IdleState());

		}
		IEnumerator RandomAttack(){
			yield return new WaitForSeconds(1.3f);
			PerformRandomAttack();
		}

		IEnumerator IdleState(){
			yield return new WaitForSeconds(0.6f);
			controller.ChangeState(EnemyState.Idle);
		}

		private void Punch1()
        {
			Debug.Log("p1");
			controller.GetAnimator().SetTrigger("Punch1");
        }

        private void HookedPunch()
        {
            controller.GetAnimator().SetTrigger("Punch2");
        }

        private void Kick()
        {
            controller.GetAnimator().SetTrigger("Kick1");
        }

        private void HeadButt()
        {
            controller.GetAnimator().SetTrigger("Kick2");
        }

        private void LegSweep()
        {
			controller.GetAnimator().SetBool("Crouch",true);
            controller.GetAnimator().SetTrigger("Kick2");
        }
	}
}