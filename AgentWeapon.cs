using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    protected float desiredAngle;

    [SerializeField]
    protected WeaponRender weaponRender;

    [SerializeField]
    protected Weapon weapon;

    private void Awake()
    {
        AssignWeapon();
    }

    private void AssignWeapon()
    {
        weaponRender = GetComponentInChildren<WeaponRender>();
        weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPosition)
    {
        var aimDirection = (Vector3)pointerPosition - transform.position;
        desiredAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(desiredAngle,Vector3.forward);
    }

    protected void AdjustWeaponRendering()
    {
        if (weaponRender != null)
        {
            weaponRender.FlipSprite(desiredAngle > 90 || desiredAngle < -90);
            weaponRender.RenderBehind(desiredAngle < 180 && desiredAngle > 0);
        }
    }

    public void Shoot()
    {
        if (weapon != null)
        {
            weapon.TryShooting();
        }
    }

    public void StopShooting()
    {
        if(weapon != null)
        {
            weapon.StopShooting();
        }
    }
}
