using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedParamStateBehaviour : StateMachineBehaviour
{
    // param state behaviour needs a param name and a default state
    public string ParamName;
    public bool SetDefaultValue;
    //Time range for when the parameter is set
    public float Start, End;

    private bool _waitForExit;

    private bool _onTransitionExitTriggered;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    _waitForExit = false;
    _onTransitionExitTriggered = false;

    animator.SetBool(ParamName, !SetDefaultValue);
    }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckOnTransitionExit(animator, layerIndex)) 
        {
            OnStateTransitionExit(animator);
        }
        if (!_onTransitionExitTriggered && stateInfo.normalizedTime >= Start && stateInfo.normalizedTime <= End)
        {
            animator.SetBool(ParamName, SetDefaultValue);
        }
    }

    private void OnStateTransitionExit(Animator animator)
    {

        animator.SetBool(ParamName, !SetDefaultValue);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_onTransitionExitTriggered)
        {
            //when the animations "Exits" then we unset the value
            animator.SetBool(ParamName, !SetDefaultValue);
        }
    }

    private bool CheckOnTransitionExit(Animator animator, int layerIndex)
    {
        if(!_waitForExit && animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash == 0)
        {
            _waitForExit = true;
        }

        if (!_onTransitionExitTriggered && _waitForExit && animator.IsInTransition(layerIndex))
        {
            _onTransitionExitTriggered = true;
            return true;
        }
        return false;
    }

}
