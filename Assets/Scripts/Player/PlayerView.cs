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

		private void Update()
		{
			playerController.Update();
		}

        public Transform GetPlayerTransform() => transform;
		public Animator GetAnimator() => animator;
		public Rigidbody GetRigidBody() => rb;
		public float GetMoveSpeed() => moveSpeed;

	}
}