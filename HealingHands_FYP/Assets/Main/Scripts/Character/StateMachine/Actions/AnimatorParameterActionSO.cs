using UnityEngine;

[CreateAssetMenu(fileName = "AnimatorParameter", menuName = "Scriptable Objects /State Machine /Actions /AnimatorParameter")]
public class AnimatorParameterActionSO : StateAction
{
    private Animator _animator;

    public string ParameterName;
    public ParameterType ParameterType;
    public WhenToRun WhenToRun;

    public bool BoolValue = default;
    public float FloatValue = default;
    public int IntValue = default;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        _animator = stateMachine.GetComponent<Animator>();

        if (WhenToRun == WhenToRun.OnStateEnter)
            SetParameter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        if (WhenToRun == WhenToRun.OnStateExit)
            SetParameter();
    }

    public override void OnUpdate() { }
    public override void OnFixedUpdate() { }

    private void SetParameter()
    { 
        switch (ParameterType) 
        {
                case ParameterType.Int:
                    _animator.SetInteger(ParameterName, IntValue);
                    break;

                case ParameterType.Float:
                    _animator.SetFloat(ParameterName, FloatValue);
                    break; 

                case ParameterType.Bool: 
                    _animator.SetBool(ParameterName, BoolValue);
                    break;

                case ParameterType.Trigger: 
                    _animator.SetTrigger(ParameterName);
                    break; 
        }
    }
}

public enum ParameterType
{ 
    Int, Float, Bool, Trigger,
}

public enum WhenToRun
{ OnStateEnter, OnStateExit, }
