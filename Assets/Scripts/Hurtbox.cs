using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    [SerializeField]
    private Tag _targetTag;

    public UnityEvent<Collision> OnHit = new UnityEvent<Collision>();

    private HashSet<GameObject> _hittedTarget = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        var hit = other.GetComponent<IHittable>();

        if (hit == null)
        {
            return;
        }

        if (!_hittedTarget.Contains(hit.GameObject))
        {
            _hittedTarget.Add(hit.GameObject);

            if (_targetTag == hit.Tags)
            {
                hit.Hit(1, collision);
                OnHit.Invoke(collision);
            }
        }
    }

    private void OnDisable()
    {
        _hittedTarget.Clear();
    }
}
