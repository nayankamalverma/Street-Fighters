using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    public class EnemyStateMachine
    {
        private EnemyController Owner;
        private IState currentState;
        private EnemyState currState;
        private IdleState idleState;

        public Dictionary<EnemyState, IState> States = new Dictionary<EnemyState, IState>();

        public EnemyStateMachine(EnemyController enemyController)
        {
            Owner = enemyController;
            currState = EnemyState.Idle;
            CreateState();
        }

        private void CreateState()
        {
            idleState = new IdleState(Owner);
            States.Add(EnemyState.Idle, idleState);
            States.Add(EnemyState.Chase, new ChaseState(Owner));
            States.Add(EnemyState.Attack, new AttackState(Owner));
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

        public void ReactHeavyHit()=> idleState.ReactHeavyHit();
        public void ReactHeadHit()=> idleState.ReactHeadHit();
        
    }
}