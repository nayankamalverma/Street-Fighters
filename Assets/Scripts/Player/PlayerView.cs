using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Player
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField]
		private Transform spawnPos;
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
			playerAction.SetReferences(playerController,transform);
		}

		public Transform SpawnPos => spawnPos;
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
				if(!playerController.stateInfoLayer0.IsTag("Block"))
				{
					playerController.eventService.OnPlayer1Hit.Invoke(8);
					playerController.ReduceHealth(8);
					playerAction.ReactHeadHit();
				}
			}
			if (other.CompareTag("HeavyKick") || other.CompareTag("HeavyPunch"))
			{
				if (!playerController.stateInfoLayer0.IsTag("Block"))
				{
					playerController.eventService.OnPlayer2Hit.Invoke(15);
					playerController.ReduceHealth(15);
					playerAction.ReactHeavyHit();
				}
			}
		}
    }
}