using UnityEngine;

public interface IHittable
{
    public void Hit(int damage, Collision collision);
    public Tag Tags { get; }

    public GameObject GameObject { get; }
}

public enum Tag
{
    Enemy = 0,
    Player = 1,
}