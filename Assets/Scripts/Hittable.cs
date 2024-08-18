using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour, IHittable
{
    [SerializeField]
    private Tag _tags;

    public Tag Tags => _tags;

    public void Hit(int damage)
    {
        Debug.Log("HITTED");
    }
}
