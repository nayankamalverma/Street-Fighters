using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Events;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
	public class PlayerController
	{
		public EventService eventService { get; private set; }
		private PlayerView playerView;
		private CoroutineRunner coroutineRunner;

		public AnimatorStateInfo stateInfoLayer0 { get; private set; }

        private float x;
		private float y;
		private float moveSpeed;
        private int health=100;
		private Transform playerTransform;
		private Transform enemy;
		private bool isFacingRight=true;
		private bool isJumping=false;
        public bool isActive=false;

		public PlayerController(EventService eventService, PlayerView playerView, Transform enemy){
			this.eventService = eventService;
			this.playerView = playerView;
			playerView.SetPlayerController(this);
			playerTransform = playerView.GetPlayerTransform();
			this.enemy = enemy;
			coroutineRunner = CoroutineRunner.Instance;
        }
		
        public void OnRoundStart(){
            playerView.GetAnimator().Play("Idle");
            playerTransform.position = playerView.SpawnPos.position;
            playerTransform.rotation = playerView.SpawnPos.rotation;
            isFacingRight = true;
            health = 100;
        }

        public void Update()
		{
            if(isActive){
                //animation state
                stateInfoLayer0 = playerView.GetAnimator().GetCurrentAnimatorStateInfo(0);
                //facing toward enemy
                if (playerTransform.position.x < enemy.position.x)
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
        }

        #region health
        public void ReduceHealth(int dmg){
            health -= dmg;
            if (health <= 0)
            {
                playerView.GetAnimator().SetTrigger("KO");
                eventService.OnNextRound.Invoke(2);
            }//if player knocked out means player 2 win 
        }
        #endregion

        #region movements
        IEnumerator FaceRight()
        {
            if(isFacingRight){
                
                isFacingRight = false;
                yield return new WaitForSeconds(0.15f);
                playerTransform.Rotate(0, 180, 0);
				playerView.GetAnimator().SetLayerWeight(0, 0);
				playerView.GetAnimator().SetLayerWeight(1, 1);
            }
        }

        IEnumerator FaceLeft()
        {
            if(!isFacingRight){
                
                isFacingRight = true;
                yield return new WaitForSeconds(0.15f);
                playerTransform.Rotate(0, -180, 0);
				playerView.GetAnimator().SetLayerWeight(0, 1);
                playerView.GetAnimator().SetLayerWeight(1, 0);
            }
        }

        private void Move()
		{
			moveSpeed = playerView.GetMoveSpeed();
            if(stateInfoLayer0.IsTag("Motion")){// if grounded and not crouched player can move
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
			if (Input.GetKeyDown(KeyCode.W))
			{
				if(!isJumping)
                {
                    isJumping = true;
                    playerView.GetAnimator().SetTrigger("Jump");
                }
                coroutineRunner.StartCoroutine(JumpPause());
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

        private IEnumerator JumpPause()
        {
            yield return new WaitForSeconds(0.5f);
            isJumping = false;
        }

        #endregion
	}
}