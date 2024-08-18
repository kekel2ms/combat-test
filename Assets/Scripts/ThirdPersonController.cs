using JetBrains.Annotations;
using System;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private InputProcessor _inputProcessor;

    [SerializeField]
    private float _characterSpeed;

    [SerializeField]
    private float _idleDampTime = 0.1f;

    [SerializeField]
    private float _bodyRotationSpeed = 360f;


    // Update is called once per frame
    private void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        //stop processing movement if player is attacking
        if (_animator.GetBool(AnimatorConst.IsAttacking))
        {
            _animator.SetFloat(AnimatorConst.DirectionY, 0f, _idleDampTime, Time.deltaTime);
            return;
        }

        var input = _inputProcessor.GetMovementInput();

        if (input != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(input);
            _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation, rotation, _bodyRotationSpeed * Time.deltaTime);
            _animator.SetFloat(AnimatorConst.DirectionY, 1f, _idleDampTime, Time.deltaTime);
        }
        else
        {
            _animator.SetFloat(AnimatorConst.DirectionY, 0f, _idleDampTime, Time.deltaTime);
        }

        _characterController.Move(input * _characterSpeed * Time.deltaTime);
    }
}
