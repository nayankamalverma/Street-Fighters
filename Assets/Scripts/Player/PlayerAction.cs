using UnityEngine;

namespace Assets.Scripts.Player
{
	public class PlayerAction :MonoBehaviour
	{
        [SerializeField]private float jumpForce;
        [SerializeField] private float jumpDistance;
        [SerializeField] private Animator animator;

        private Transform playerTransform;
        private AnimatorStateInfo stateInfoLayer0;

        public void SetReferences(Transform playerTransform, float jumpForce)
        {
            this.playerTransform = playerTransform;
            this.jumpForce = jumpForce;
        }

        private void Update()
        {
            stateInfoLayer0 = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfoLayer0.IsTag("Motion"))
            {
                Attack();
            }
            if(stateInfoLayer0.IsTag("Jump"))
            {
                JumpAttack();
            }
            if (stateInfoLayer0.IsTag("Crouch"))
            {
                CrouchAttack();
            }
            if(stateInfoLayer0.IsTag("Block"))
            {
                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    animator.SetTrigger("BlockOff");
                }
            }
        }

        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger("Punch1");
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetTrigger("Punch2");
                //add slide distance for heavy punch
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Kick1");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Kick2");
            }
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetTrigger("BlockOn");
            }
        }

        private void JumpAttack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Kick2");
            }
        }

        private void CrouchAttack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Kick2");
            }
        }

        public void ReactHeavyHit(){
            animator.SetTrigger("HeavyHit");
        }
        public void ReactHeadHit(){
            animator.SetTrigger("HeadHit");
        }

        public void Jump(){
            playerTransform.Translate(Vector3.up * jumpForce * Time.deltaTime);
        }

        public void ForwardFlip(){
            playerTransform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            playerTransform.Translate(Vector3.right * jumpDistance * Time.deltaTime);
        }

        public void BackFlip()
        {
            playerTransform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            playerTransform.Translate(Vector3.left * jumpDistance * Time.deltaTime);
        }

	}
}