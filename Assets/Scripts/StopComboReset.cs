using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopComboReset : StateMachineBehaviour
{
    private ThirdPersonController _controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller== null)
        {
            _controller = animator.GetComponent<ThirdPersonController>();
        }

        _controller.StopComboResetDelay();
    } 
}
