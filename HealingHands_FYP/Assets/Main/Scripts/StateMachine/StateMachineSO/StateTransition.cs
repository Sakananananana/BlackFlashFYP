using UnityEngine;

public class StateTransition : ScriptableObject
{
    private BaseState _currentState;
    



    public void SwitchState(BaseState newState)
    {
        if (newState != null)
        {
            _currentState.StateExit();
            _currentState = newState;
            _currentState.StateEnter();
        }
    }
}
