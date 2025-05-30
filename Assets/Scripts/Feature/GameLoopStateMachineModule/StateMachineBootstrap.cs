using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StateMachineBootstrap : MonoBehaviour
{
    private GameLoopStateMachine _gameLoopStateMachine;
    private WaitForInputState _waitForInputState;
    private GameplayState _gameplayState;
    private WinState _winState;
    private LoseState _loseState;
    
    [Inject]
    public void InjectDependencies(GameLoopStateMachine gameLoopStateMachine, 
        WaitForInputState waitForInputState, 
        GameplayState gameplayState, 
        WinState winState, 
        LoseState loseState)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _waitForInputState = waitForInputState;
        _gameplayState = gameplayState;
        _winState = winState;
        _loseState = loseState;
    }

    private void Start()
    {
        List<IState> states = new List<IState>() {_waitForInputState, _gameplayState, _winState, _loseState};
        _gameLoopStateMachine.SetStates(states);
        _gameLoopStateMachine.ChangeState<WaitForInputState>();
    }
}
