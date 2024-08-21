using UnityEngine;

public class HitstopController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Hurtbox _weaponHurtbox;

    private bool _isHitstop;
    private float _currentHitStopTimer;

    private void Awake()
    {
        _weaponHurtbox.OnHit.AddListener(StartHitStop);
    }

    private void Update()
    {
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

            _animator.SetFloat(AnimatorConst.AttackingSpeed, 0.1f);

            if (_currentHitStopTimer < 0f)
            {
                _animator.SetFloat(AnimatorConst.AttackingSpeed, 1f);
                _isHitstop = false;
            }
        }
    }   

    public void StartHitStop(Collision collision)
    {
        _isHitstop = true;
        _currentHitStopTimer = 0.15f;
    }
}
