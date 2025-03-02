using System.Collections;
using Assets.Scripts.Player;
using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public enum EnemyState { Idle, Chase, Attack, Block, Dead }

    public class EnemyController : MonoBehaviour
    {

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float moveSpeed;


        private EnemyStateMachine stateMachine;
        private EventService eventService;
        private Transform playerTransform;
        private bool isFacingRight = false;
        
        public void SetService(EventService eventService , Transform playerTransform)
        {
            this.eventService = eventService;
            this.playerTransform = playerTransform;
            //stateMachine = new EnemyStateMachine(eventService,this);
        }

        private void Start()
        {
            StartCoroutine(FaceLeft());
        }

        private void Update()
        {
            FaceTowardPlayer();
        }

        private void FaceTowardPlayer()
        {
            if (playerTransform.position.x < transform.position.x)
            {
                StartCoroutine(FaceRight());
            }
            else
            {
                StartCoroutine(FaceLeft());
            }
        }

        IEnumerator FaceRight()
        {
            if (isFacingRight)
            {
                isFacingRight = false;
                yield return new WaitForSeconds(0.15f);
                transform.Rotate(0, 180, 0);
                animator.SetLayerWeight(0, 0);
                animator.SetLayerWeight(1, 1);
            }
        }

        IEnumerator FaceLeft()
        {
            if (!isFacingRight)
            {
                isFacingRight = true;
                yield return new WaitForSeconds(0.15f);
                transform.Rotate(0, -180, 0);
                animator.SetLayerWeight(0, 1);
                animator.SetLayerWeight(1, 0);
            }
        }


        public void ChangeState(EnemyState newState)
        {
            stateMachine.ChangeState(newState);
        }

    }
}