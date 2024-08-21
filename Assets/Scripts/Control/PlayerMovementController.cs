using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private InputProcessor _inputProcessor;

    [SerializeField]
    private Hittable[] _hitboxes = Array.Empty<Hittable>();

    [SerializeField]
    private Image _dodgeIndicator;

    [SerializeField]
    private float _characterSpeed;

    [SerializeField]
    private float _rollSpeed;

    [SerializeField]
    private float _dodgeCooldown;

    [SerializeField]
    private float _idleDampTime = 0.1f;

    [SerializeField]
    private float _bodyRotationSpeed = 360f;


    private Vector3 _dodgeDirection;
    private bool _isDodging;
    private float _currentDodgeCooldown;

    private bool _enableMovementInputStatus = true;

    // Update is called once per frame
    private void Update()
    {
        if (!_enableMovementInputStatus)
        {
            return;
        }

        ProcessMovement();
        ProcessDodge();
    }

    public void DoneDodge()
    {
        _isDodging = false;

        for (int i = 0; i < _hitboxes.Length; i++)
        {
            _hitboxes[i].gameObject.SetActive(true);
        }
    }

    public void SetMovementInputStatus(bool status)
    {
        _enableMovementInputStatus = status;
    }

    private void ProcessDodge()
    {
        _currentDodgeCooldown -= Time.deltaTime;

        UpdateIndicator();

        bool canDodge = _inputProcessor.IsDodgeButtonDown() && !_isDodging && _currentDodgeCooldown <= 0f;

        if (canDodge)
        {
            _isDodging = true;

            _currentDodgeCooldown = _dodgeCooldown;

            _dodgeDirection = _inputProcessor.GetMovementInput();
            _animator.SetTrigger(AnimatorConst.Dodge);

            for (int i = 0; i < _hitboxes.Length; i++)
            {
                _hitboxes[i].gameObject.SetActive(false);
            }

            if (_dodgeDirection == Vector3.zero)
            {
                _dodgeDirection = _characterController.transform.rotation * Vector3.forward;
            }
        }

        if (_isDodging)
        {
            var rotation = Quaternion.LookRotation(_dodgeDirection);
            _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation, rotation, _bodyRotationSpeed * Time.deltaTime);

            _characterController.Move(_dodgeDirection * _rollSpeed * Time.deltaTime);
        }
    }

    private void UpdateIndicator()
    {
        var currentCooldownPercent = _currentDodgeCooldown / _dodgeCooldown;
        _dodgeIndicator.fillAmount = currentCooldownPercent;
    }

    private void ProcessMovement()
    {
        if (_animator.GetBool(AnimatorConst.IsAttacking) || _isDodging)
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
