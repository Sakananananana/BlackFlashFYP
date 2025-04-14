using UnityEngine;

[CreateAssetMenu(fileName = "AnimatorParameterActionSO", menuName = "Scriptable Objects /State Machine /Actions /AnimatorParameterActionSO")]
public class AnimatorParameterActionSO : StateActionSO
{
    public WhenToRun WhenToRun;
    public string ParameterName;
    public ParameterType parameterType = default;
    
    public bool BoolValue = default;
    public float FloatValue = default;
    public int IntValue = default;

    protected override StateAction CreateAction() => new AnimatorParameterAction(ParameterName);

    public enum ParameterType
    {
        Int, Float, Bool, Trigger,
    }
}

public class AnimatorParameterAction : StateAction
{
    private Animator _animator;
    private new AnimatorParameterActionSO _originSO => (AnimatorParameterActionSO)base._originSO;
    private string _parameterName;
    
    public AnimatorParameterAction(string parameterName)
    {
        _parameterName = parameterName;
    }

    public override void OnStateEnter(StateMachine stateMachine)
    {
        _animator = stateMachine.GetComponent<Animator>();

        if (_originSO.WhenToRun == WhenToRun.OnStateEnter)
        SetParameter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        if (_originSO.WhenToRun == WhenToRun.OnStateExit)
        SetParameter();
    }

    public override void OnUpdate() { }
    public override void OnFixedUpdate() { }

    private void SetParameter()
    {
        switch (_originSO.parameterType)
        {
            case AnimatorParameterActionSO.ParameterType.Int:
                _animator.SetInteger(_parameterName, _originSO.IntValue);
                break;

            case AnimatorParameterActionSO.ParameterType.Float:
                _animator.SetFloat(_parameterName, _originSO.FloatValue);
                break;

            case AnimatorParameterActionSO.ParameterType.Bool:
                _animator.SetBool(_parameterName, _originSO.BoolValue);
                break;

            case AnimatorParameterActionSO.ParameterType.Trigger:
                _animator.SetTrigger(_parameterName);
                break;
        }
    }
}


public enum WhenToRun
{ OnStateEnter, OnStateExit, }
