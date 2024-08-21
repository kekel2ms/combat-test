using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackDone : StateMachineBehaviour
{
    private SpecialAttackController _controller;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (_controller == null)
        {
            _controller = animator.GetComponent<SpecialAttackController>();
        }

        _controller.SkillDone();
    }
}
