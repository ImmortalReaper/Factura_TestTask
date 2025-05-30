using UnityEngine;

public class AttackState : IBehaviorState
{
    private StickmanBrain _stickmanBrain;
    private PlayerEntityModel _playerEntityModel;
    private string stickmanBlendParameter = "Blend";

    private Transform _carTransform;
    
    public AttackState(PlayerEntityModel playerEntityModel, StickmanBrain stickmanBrain)
    {
        _playerEntityModel = playerEntityModel;
        _stickmanBrain = stickmanBrain;
    }

    public void Enter()
    {
        _stickmanBrain.StickmanAnimator.SetFloat(stickmanBlendParameter, 1f);
        _carTransform = _playerEntityModel.PlayerEntity.GetComponent<CarController>().CarTransform;
    }
    
    public void Exit() { }
    
    public void Update()
    {
        if (_playerEntityModel.PlayerEntity == null) return;

        Vector3 direction = (_carTransform.position - _stickmanBrain.transform.position).normalized;
        if (direction != Vector3.zero)
            _stickmanBrain.transform.rotation = Quaternion.LookRotation(direction);
        _stickmanBrain.transform.position += direction * (_stickmanBrain.EnemyData.EnemySpeed * Time.deltaTime);
    }
}
