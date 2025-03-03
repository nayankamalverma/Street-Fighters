﻿using System.Collections;
using Assets.Scripts.Player;
using Assets.Scripts.Utilities.Events;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Scripts.Enemy
{
    public enum EnemyState { Idle, Chase, Retreat, Attack, Block, Dead }

    public class EnemyController : MonoBehaviour
    {

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float stopDistance;
        [SerializeField]
        private float attackSpeed;

        private float distance;
        private EnemyStateMachine stateMachine;
        
        public AnimatorStateInfo stateInfoLayer0{ get; private set; }
        public EventService eventService {  get; private set; }
        public Transform playerTransform {  get; private set; }
        public bool isFacingRight { get; private set; }
        public bool canMove { get; private set; }
        public bool isInAttackRange { get; private set; }

        public void SetService(EventService eventService , Transform playerTransform)
        {
            this.eventService = eventService;
            this.playerTransform = playerTransform;
            stateMachine = new EnemyStateMachine(this);
        }

        private void Start()
        {
            StartCoroutine(FaceLeft());
            ChangeState(EnemyState.Idle);
            canMove = true;
        }

        private void Update()
        {
            distance = Vector3.Distance( transform.position, playerTransform.position);
            if (distance >= stopDistance)isInAttackRange = false;
            else isInAttackRange = true;

            stateInfoLayer0 = animator.GetCurrentAnimatorStateInfo(0);
            FaceTowardPlayer();
            stateMachine.Update();
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

        public void PauseMovement(){
            StartCoroutine(CanMove());
        }
        
        private IEnumerator CanMove(){
            canMove = false;
            yield return new WaitForSeconds(0.6f);
            canMove = true;
        }
        public Animator GetAnimator()=> animator;
        public float MoveSpeed => moveSpeed;
        public float StopDistance => stopDistance;
        public float AttackSpeed => attackSpeed;    
        
    }
}