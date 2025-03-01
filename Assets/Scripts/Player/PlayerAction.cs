using UnityEngine;

namespace Assets.Scripts.Player
{
	public class PlayerAction :MonoBehaviour
	{
		private Transform playerTransform;
        [SerializeField]private float jumpForce;


        public void SetReferences(Transform playerTransform,float jumpForce)
        {
            this.playerTransform = playerTransform;
            this.jumpForce = jumpForce;
        }

        public void Jump(){
            playerTransform.position += Vector3.up * jumpForce * Time.deltaTime;
        }

	}
}