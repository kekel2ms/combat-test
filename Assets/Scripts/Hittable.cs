using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour, IHittable
{
    public UnityEvent<int> OnHit = new UnityEvent<int>();

    [SerializeField]
    private GameObject _parent;

    [SerializeField]
    private Tag _tags;

    public Tag Tags => _tags;

    public GameObject GameObject => _parent;

    public void Hit(int damage)
    {
        OnHit.Invoke(damage);
    }
}
