using UnityEngine;

interface IStateComponent
{
    void OnStateEnter(StateMachine stateMachine);
    void OnStateExit();
}
