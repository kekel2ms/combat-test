using System;
using UnityEngine;

public class SimpleEnemyLogic : MonoBehaviour
{
    [SerializeField]
    private SphereCollider _enemyDetector;

    [SerializeField]
    private Hittable[] _hittables = Array.Empty<Hittable>();

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _attackRate;

    [SerializeField]
    private float _delayFirstAttack = 0.15f;

    [SerializeField]
    private float _dampTime = 0.1f;

    [SerializeField]
    private float _staggeredDuration = 1.5f;

    [SerializeField]
    private float _offsetRadius = 1.5f;

    [SerializeField]
    private Tag _targetTag = Tag.Player;

    [SerializeField]
    private LayerMask _layerMask;

    private float _currentAttackDelay;
    private Transform _target;
    private Vector3 _offset;
    private Collider[] _colliderTemp = new Collider[10];


    private void Awake()
    {
        for (int i = 0; i < _hittables.Length; i++)
        {
            _hittables[i].OnHit.AddListener(OnHit);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _hittables.Length; i++)
        {
            if (_hittables[i] != null)
            {
                _hittables[i].OnHit.RemoveListener(OnHit);
            }
        }
    }

    private void Update()
    {
        SearchTarget();
        ChaseTarget();
    }

    private void SearchTarget()
    {
        if (_target == null)
        {
            float radius = _enemyDetector.transform.localScale.x / 2f;
            radius /= _enemyDetector.radius;

            int count = Physics.OverlapSphereNonAlloc(_enemyDetector.transform.position, radius, _colliderTemp, _layerMask.value, QueryTriggerInteraction.Collide);

            for (int i = 0; i < count; i++)
            {
                var collider = _colliderTemp[i];
                var hittable = collider.GetComponent<IHittable>();

                if (hittable == null)
                {
                    continue;
                }

                if (hittable.Tags != _targetTag)
                {
                    continue;
                }

                _target = hittable.GameObject.transform;
                _offset = UnityEngine.Random.insideUnitSphere;
                _offset.y = 0f;
                _offset = _offset.normalized * _offsetRadius;

                break;
            }
        }
    }

    private void ChaseTarget()
    {
        if (_target == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, _target.position) > 2)
        {
            Vector3 targetPosition = _target.position + _offset;
            targetPosition.y = transform.position.y;

            transform.LookAt(targetPosition);
            _animator.SetFloat(AnimatorConst.Speed, 1f, _dampTime, Time.deltaTime);
            _currentAttackDelay = _delayFirstAttack;
        }
        else
        {
            Vector3 targetPosition = _target.position;
            targetPosition.y = transform.position.y;

            transform.LookAt(targetPosition);

            _animator.SetFloat(AnimatorConst.Speed, 0f, _dampTime, Time.deltaTime);

            if (_animator.GetFloat(AnimatorConst.Speed) < 0.05f)
            {
                _animator.SetFloat(AnimatorConst.Speed, 0f);
                _currentAttackDelay -= Time.deltaTime;

                if (_currentAttackDelay < 0f)
                {
                    _currentAttackDelay = 1 / _attackRate;

                    _animator.SetTrigger(AnimatorConst.Attack);
                }
            }
        }
    }

    public void OnDeath()
    {
        this.enabled = false;
    }

    private void OnHit(int damage)
    {
        _currentAttackDelay = _staggeredDuration;
        _animator.ResetTrigger(AnimatorConst.Attack);
    }
}
