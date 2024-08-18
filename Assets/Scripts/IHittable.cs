public interface IHittable
{
    public void Hit(int damage);
    public Tag Tags { get; }
}

public enum Tag
{
    Enemy = 0,
    Ally = 1,
}