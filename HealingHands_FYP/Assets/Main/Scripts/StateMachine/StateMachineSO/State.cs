using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "Scriptable Objects/State")]
public class State : BaseState
{

    private BaseState[] _nextState;
    private StateTransition _transition;
}

