using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFinish : StateMachineBehaviour
{
    [SerializeField] private string animation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<AnimationController>().PlayAnimation(animation, 0.1f, stateInfo.length);
    }
}
