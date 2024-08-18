using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    [Serializable]
    public class HitEvent : UnityEvent<Transform> { }

    [SerializeField]
    private HitEvent OnHit;

    private HashSet<GameObject> _hittedTarget = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        var hit = other.GetComponent<IHittable>();

        if (hit == null)
        {
            return;
        }

        if (!_hittedTarget.Contains(other.gameObject))
        {
            _hittedTarget.Add(other.gameObject);
            hit.Hit(0);

            OnHit.Invoke(other.transform);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    var hit = other.GetComponent<IHittable>();

    //    if (hit == null)
    //    {
    //        return;
    //    }

    //    if (!_hittedTarget.Contains(other.gameObject))
    //    {
    //        _hittedTarget.Add(other.gameObject);
    //        hit.Hit(0);

    //        OnHit.Invoke(other.transform);
    //    }
    //}

    private void OnDisable()
    {
        _hittedTarget.Clear();
    }
}
