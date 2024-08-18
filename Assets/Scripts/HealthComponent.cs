using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private int _maxHealth = 10;

    [SerializeField]
    private Hittable[] _hittables = Array.Empty<Hittable>();

    private int _currentHealth;

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
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }   
}
