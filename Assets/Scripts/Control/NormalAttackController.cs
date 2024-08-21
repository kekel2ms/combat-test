using UnityEngine;

public class NormalAttackController : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private InputProcessor _inputProcessor;

    [SerializeField]
    private float _comboResetDelay;

    private bool _isComboResetting;
    private float _currentComboResetTimer;

    private void Update()
    {
        ProcessAttackInput();
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
}
