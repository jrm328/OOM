using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]

public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField]
    public BulletDataSO BulletData { get; set; }

    [field: SerializeField]
    [field: Range(0, 100)]
    public int AmmoCapacity { get; set; } = 100;

    [field: SerializeField]
    public bool AutomaticFire { get; internal set; } = false;

    [field: SerializeField]
    [field: Range(0.1f, 2f)]
    public float WeaponDelay { get; internal set; } = 0.1f;

    [field: SerializeField]
    [field: Range(0, 10)]
    public float SpreadAngle { get; set; } = 5;

    [SerializeField]
    private bool multiBulletShoot = false;

    [SerializeField]
    [Range(1,10)]
    private int bullerCount = 0;

    internal int GetBulletCountToSpawn()
    {
        if (multiBulletShoot)
        {
            return bullerCount;
        }
        return 1;
    }
}
