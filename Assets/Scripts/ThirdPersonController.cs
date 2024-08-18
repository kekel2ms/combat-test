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
    private float _comboResetDelay;

    [SerializeField]
    private float _bodyRotationSpeed = 360f;

    private bool _isAttacking;
    private bool _isComboResetting;
    private float _currentComboResetTimer;

    // Update is called once per frame
    private void Update()
    {
        ProcessMovement();
        ProcessAttackInput();
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
