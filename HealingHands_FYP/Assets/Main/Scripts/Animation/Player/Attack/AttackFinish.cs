using UnityEngine;

public class AttackFinish : StateMachineBehaviour
{
    private PlayerControls _playerControls;
    private bool _clipHasEnded;
    private float _duration;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerControls = animator.GetComponent<PlayerControls>();
        _duration = stateInfo.length;
        _clipHasEnded = false;

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ClipDurationCountdown();
        if (_clipHasEnded == true)
        { 
            _playerControls.CancelAttackInput();
        }
    }

    private void ClipDurationCountdown()
    {
        if (_duration > 0.1f)
        {
            _duration -= Time.deltaTime;
        }
        else
        {
            _clipHasEnded = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
