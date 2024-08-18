using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartComboReset : StateMachineBehaviour
{
    private CombatMechanicController _controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller== null)
        {
            _controller = animator.GetComponent<CombatMechanicController>();
        }

        _controller.StartComboResetDelay();
    } 
}
