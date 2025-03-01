using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Events;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
	public class PlayerController
	{
		private EventService eventService;
		private PlayerView playerView;
		private CoroutineRunner coroutineRunner;

        private float x;
		private float y;
		private float moveSpeed;
		private Transform playerTransform;
		private Transform enemy;
		private bool isGrounded=true;
		private bool isCrouched=false;
		private bool isFacingRight=true;

		public PlayerController(EventService eventService, PlayerView playerView, Transform enemy){
			this.eventService = eventService;
			this.playerView = playerView;
			playerView.SetPlayerController(this);
			playerTransform = playerView.GetPlayerTransform();
			this.enemy = enemy;
			coroutineRunner = CoroutineRunner.Instance;
        }
		

		public void Update()
		{
            //facing toward enemy
			if(playerTransform.position.x < enemy.position.x)
            {
                coroutineRunner.StartCoroutine(FaceLeft());
            }
            else
            {
                coroutineRunner.StartCoroutine(FaceRight());
            }

            //movements
            x = Input.GetAxis("Horizontal");
			y = Input.GetAxis("Vertical");
			Move();
			JumpAndCrouch();
		}

		#region movements
		IEnumerator FaceRight()
        {
            if(isFacingRight){
                isFacingRight = false;
                yield return new WaitForSeconds(0.15f);
                playerTransform.Rotate(0, 180, 0);
            }
        }

        IEnumerator FaceLeft()
        {
            if(!isFacingRight){
                isFacingRight = true;
                yield return new WaitForSeconds(0.15f);
                playerTransform.Rotate(0, -180, 0);
            }
        }

        private void Move()
		{
			moveSpeed = playerView.GetMoveSpeed();
            if( isGrounded && !isCrouched){// if grounded and not crouched player can move
                if (x > 0)
                {
                    playerTransform.position += Vector3.right * moveSpeed * Time.deltaTime;
                    playerView.GetAnimator().SetBool("Right", true);
                }

                if (x < 0)
                {
                    playerTransform.position += Vector3.left * moveSpeed * Time.deltaTime;
                    playerView.GetAnimator().SetBool("Left", true);
                }
            }
			if(x==0){
				playerView.GetAnimator().SetBool("Right", false);
				playerView.GetAnimator().SetBool("Left", false);
			}	
		}
		private void JumpAndCrouch()
		{
			//ground Check
			isGrounded = Physics.CheckSphere(playerView.GetGroundCheck().position, 1f, playerView.GetGroundLayer());

			if (isGrounded && Input.GetKeyDown(KeyCode.W))
			{
				playerView.GetAnimator().SetTrigger("Jump");
			}
			if (y < 0)
			{
				isCrouched = true;
				playerView.GetAnimator().SetBool("Crouch", true);
			}
			if (y == 0)
			{
				isCrouched =false;
				playerView.GetAnimator().SetBool("Crouch", false);
			}
		}
		#endregion
	}
}