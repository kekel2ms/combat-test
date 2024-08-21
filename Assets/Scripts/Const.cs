using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorConst
{
    public static readonly int DirectionY = Animator.StringToHash("Direction Y");
    public static readonly int IsAttacking = Animator.StringToHash("Is Attacking");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Damaged = Animator.StringToHash("Damaged");
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int Death = Animator.StringToHash("Death");
    public static readonly int Dodge = Animator.StringToHash("Dodge");
    public static readonly int SpecialAttack = Animator.StringToHash("Special Attack");
    public static readonly int AttackingSpeed = Animator.StringToHash("Attacking Speed");
}

public static class InputConst
{
    public const string Vertical = "Vertical";
    public const string Horizontal = "Horizontal";
    public const string MouseX = "Mouse X";
    public const string MouseY = "Mouse Y";
    public const string Attack = "Attack";
    public const string Dodge = "Dodge";
    public const string SpecialAttack = "Special Attack";
}