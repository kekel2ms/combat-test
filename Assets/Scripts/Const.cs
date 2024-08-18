using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorConst
{
    public static readonly int DirectionY = Animator.StringToHash("Direction Y");
    public static readonly int IsAttacking = Animator.StringToHash("Is Attacking");
    public static readonly int Attack = Animator.StringToHash("Attack");
}

public static class InputConst
{
    public const string Vertical = "Vertical";
    public const string Horizontal = "Horizontal";
    public const string MouseX = "Mouse X";
    public const string MouseY = "Mouse Y";
}