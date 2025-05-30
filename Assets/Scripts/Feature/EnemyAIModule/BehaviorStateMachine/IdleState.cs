using UnityEngine;

namespace ShootingCar.Feature.EnemyAIModule.BehaviorStateMachine
{
    public class IdleState : IBehaviorState
    {
        private Animator _animator;
        private string stickmanBlendParameter = "Blend";
    
        public IdleState(Animator animator)
        {
            _animator = animator;
        }
    
        public void Enter()
        {
            _animator.SetFloat(stickmanBlendParameter, 0f);
        }

        public void Exit() { }
    
        public void Update() { }
    }
}
