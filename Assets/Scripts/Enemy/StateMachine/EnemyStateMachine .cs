using Assets.Scripts.Utilities.Events;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    public class EnemyStateMachine
    {
        private EventService eventService;
        private EnemyController Owner;
        private IState currentState;
        private EnemyState currState;

        public Dictionary<EnemyState, IState> States = new Dictionary<EnemyState, IState>();

        public EnemyStateMachine(EventService eventService, EnemyController enemyController)
        {
            this.eventService = eventService;
            Owner = enemyController;
            currState = EnemyState.Idle;
            //States.Add(EnemyState.Idle, new IdleState(enemyController));
            //States.Add(EnemyState.Chase, new ChaseState(enemyController));
            //States.Add(EnemyState.Attack, new AttackState(enemyController));
            //States.Add(EnemyState.Block, new BlockState(enemyController));
            //States.Add(EnemyState.Retreat, new RetreatState(enemyController));
            currentState = States[currState];
            CreateState();
        }

        private void CreateState()
        {
            
        }

        protected void ChangeState(IState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public void Update() => currentState?.Update();

        public EnemyState GetCurrentState() => currState;

        public void ChangeState(EnemyState newState)
        {
            ChangeState(States[newState]);
            currState = newState;
        }

    }
}