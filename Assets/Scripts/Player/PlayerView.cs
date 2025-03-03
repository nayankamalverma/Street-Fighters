using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Player
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField]
		private Animator animator;
		[SerializeField]
		private PlayerAction playerAction;
		[SerializeField]
		private Rigidbody rb;
		[SerializeField]
		private float moveSpeed;
		[SerializeField]
		private float jumpForce;

		private PlayerController playerController;

		public void SetPlayerController(PlayerController playerController)
		{
			this.playerController = playerController;
			playerAction.SetReferences(transform, jumpForce);
		}

		public Transform GetPlayerTransform() => transform;
		public Animator GetAnimator() => animator;
		public Rigidbody GetRigidBody() => rb;
		public float GetMoveSpeed() => moveSpeed;

		private void Update()
		{
			playerController.Update();
		}

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("KickLight") || other.CompareTag("LightPunch"))
            {
                if(!playerController.stateInfoLayer0.IsTag("Block")){
                    playerAction.ReactHeadHit();
                }
            }
            if (other.CompareTag("HeavyKick") || other.CompareTag("HeavyPunch"))
            {
				if (!playerController.stateInfoLayer0.IsTag("Block"))
				{
					playerAction.ReactHeavyHit();
				}
            }
        }
    }
}