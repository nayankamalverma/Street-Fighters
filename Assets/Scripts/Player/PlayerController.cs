using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.Player
{
	public class PlayerController
	{
		private EventService eventService;
        private PlayerView playerView;

        private float x;
        private float y;
        private float moveSpeed;
        private Transform playerTransform;

        public PlayerController(EventService eventService, PlayerView playerView){
			this.eventService = eventService;
            this.playerView = playerView;
            playerView.SetPlayerController(this);
            playerTransform = playerView.GetPlayerTransform();
        }
        

        public void Update()
        {
            //movements
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            Move();
            JumpAndCrouch();
        }

        #region movements

        private void Move()
        {
            moveSpeed = playerView.GetMoveSpeed();
            switch (x)
            {
                case > 0:
                    playerTransform.position += Vector3.right * moveSpeed * Time.deltaTime;
                    playerView.GetAnimator().SetBool("Right", true);
                    break;
                case < 0:
                    playerTransform.position += Vector3.left * moveSpeed * Time.deltaTime;
                    playerView.GetAnimator().SetBool("Left", true);
                    break;
                case 0:
                    playerView.GetAnimator().SetBool("Right", false);
                    playerView.GetAnimator().SetBool("Left", false);
                    break;
            }
        }
        private void JumpAndCrouch()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerView.GetAnimator().SetTrigger("Jump");
            }
            if (y < 0)
            {
                playerView.GetAnimator().SetBool("Crouch", true);
            }
            if (y == 0)
            {
                playerView.GetAnimator().SetBool("Crouch", false);
            }
        }

        public void Jump()
        {
            playerView.GetRigidBody().AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        #endregion
    }
}