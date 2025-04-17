using UnityEngine;

[CreateAssetMenu(fileName = "IsEnemyEntityParticleAlive", menuName = "Scriptable Objects /State Machine /Decisions /IsEnemyEntityParticleAlive")]
public class IsEnemyEntityDeadParticleAliveDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsEnemyEntityDeadParticleAlive();
}

public class IsEnemyEntityDeadParticleAlive : Decision
{
    private EffectController _effect;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _effect = stateMachine.GetComponent<EffectController>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _effect.DeathParticle.IsAlive();
}
