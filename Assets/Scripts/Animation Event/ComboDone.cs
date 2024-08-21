using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDone : StateMachineBehaviour
{
    private NormalAttackController _controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller== null)
        {
            _controller = animator.GetComponent<NormalAttackController>();
        }

        _controller.SetAttackStatus(false);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        animator.ResetTrigger(AnimatorConst.Attack);
        _controller.SetAttackStatus(false);
    }
}
