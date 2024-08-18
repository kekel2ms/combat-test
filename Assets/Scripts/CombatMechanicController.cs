using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Windows;

public class CombatMechanicController : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private InputProcessor _inputProcessor;

    [SerializeField]
    private float _comboResetDelay;

    [SerializeField]
    private Hurtbox _weaponHurtbox;

    private bool _isComboResetting;
    private float _currentComboResetTimer;

    private bool _isHitstop;
    private float _currentHitStopTimer;

    private void Awake()
    {
        _weaponHurtbox.OnHit.AddListener(StartHitStop);
    }

    private void Update()
    {
        ProcessAttackInput();
        ProcessHitStop();
    }

    private void OnDestroy()
    {
        _weaponHurtbox.OnHit.RemoveListener(StartHitStop);
    }

    private void ProcessHitStop()
    {
        if (_isHitstop)
        {
            _currentHitStopTimer -= Time.deltaTime;

            _animator.SetFloat("Attacking Speed", 0.1f);

            if (_currentHitStopTimer < 0f)
            {
                _animator.SetFloat("Attacking Speed", 1f);
                _isHitstop = false;
            }
        }
    }   
    private void ProcessAttackInput()
    {
        if (_isComboResetting)
        {
            _currentComboResetTimer-= Time.deltaTime;
            if (_currentComboResetTimer < 0)
            {
                StopComboResetDelay();
                SetAttackStatus(false);
            }
        }

        if (_inputProcessor.IsAttackInputDown())
        {
            SetAttackStatus(true);
            _animator.SetTrigger(AnimatorConst.Attack);
        }
    }

    public void StartComboResetDelay()
    {
        _currentComboResetTimer = _comboResetDelay;
        _isComboResetting = true;
    }

    public void StopComboResetDelay()
    {
        _currentComboResetTimer = 0;
        _isComboResetting = false;
    }

    public void SetAttackStatus(bool isAttacking)
    {
        _animator.SetBool(AnimatorConst.IsAttacking, isAttacking);
    }

    public void ActivateWeaponHurtbox()
    {
        _weaponHurtbox.gameObject.SetActive(true);
    }

    public void DeactivateWeaponHurtbox()
    {
        _weaponHurtbox.gameObject.SetActive(false);
    }

    public void StartHitStop(Collision collision)
    {
        _isHitstop = true;
        _currentHitStopTimer = 0.15f;
    }
}
