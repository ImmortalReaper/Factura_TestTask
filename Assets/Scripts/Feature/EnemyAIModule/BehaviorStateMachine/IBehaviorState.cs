using ShootingCar.Core.StateMachine;

namespace ShootingCar.Feature.EnemyAIModule.BehaviorStateMachine
{
    public interface IBehaviorState : IState
    {
        public void Update();
    }
}
