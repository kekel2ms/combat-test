using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private int _maxHealth = 10;

    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private Hittable[] _hittables = Array.Empty<Hittable>();

    private int _currentHealth;

    public UnityEvent OnDeath = new UnityEvent();

    private void Awake()
    {
        _currentHealth = _maxHealth;
        for (int i = 0; i < _hittables.Length; i++)
        {
            _hittables[i].OnHit.AddListener(DoDamage);
        }
    }

    public void DoDamage(int damage)
    {
        _animator.SetTrigger(AnimatorConst.Damaged);

        _currentHealth -= damage;
        _healthBar.fillAmount = (float)_currentHealth / (float)_maxHealth;

        if (_currentHealth <= 0)
        {
            _animator.SetTrigger("Death");
            OnDeath.Invoke();
        }
    }   

    public void DestroyCharacter()
    {
        Destroy(gameObject);
    }
}
