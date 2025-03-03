namespace Assets.Scripts.Enemy
{
    public abstract class IState
    {
        protected EnemyController controller;

        protected IState(EnemyController controller)
        {
            this.controller = controller;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}