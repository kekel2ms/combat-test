using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxActivator : MonoBehaviour
{
    [SerializeField]
    private Hurtbox _weaponHurtbox;

    public void ActivateWeaponHurtbox()
    {
        _weaponHurtbox.gameObject.SetActive(true);
    }

    public void DeactivateWeaponHurtbox()
    {
        _weaponHurtbox.gameObject.SetActive(false);
    }
}
