using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackController : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private InputProcessor _inputProcessor;

    [SerializeField]
    private PlayerMovementController _playerMovementController;

    [SerializeField]
    private NormalAttackController _normalAttackController;

    [SerializeField]
    private Image _cooldownIndicator;

    [SerializeField]
    private float _skillCooldown = 3f;

    private float _currentCooldown;

    private void Update()
    {
        ProcessSpecialAttack();
    }

    private void ProcessSpecialAttack()
    {
        _currentCooldown -= Time.deltaTime;

        UpdateIndicator();

        if (_currentCooldown < 0f)
        {
            if (_inputProcessor.IsSkillButtonDown())
            {
                _currentCooldown = _skillCooldown;
                _animator.SetTrigger(AnimatorConst.SpecialAttack);
                _animator.SetBool(AnimatorConst.IsAttacking, true);

                _normalAttackController.enabled = false;
                _playerMovementController.enabled = false;
            }
        }
    }

    private void UpdateIndicator()
    {
        var currentCooldownPercent = _currentCooldown / _skillCooldown;
        _cooldownIndicator.fillAmount = currentCooldownPercent;
    }

    public void SkillDone()
    {
        _animator.SetBool(AnimatorConst.IsAttacking, false);
        _normalAttackController.enabled = true;
        _playerMovementController.enabled = true;
    }
}
