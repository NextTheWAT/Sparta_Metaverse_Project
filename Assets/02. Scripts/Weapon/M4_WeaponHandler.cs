using UnityEngine;

public class M4_WeaponHandler : RangeWeaponHandler
{
    [Header("M4 MuzzleFlash")]
    [SerializeField] private MuzzleFlash muzzleFlash;

    protected override void Start()
    {
        base.Start();

        if (muzzleFlash == null)
            muzzleFlash = GetComponentInChildren<MuzzleFlash>();
    }

    public override void Attack()
    {
        base.Attack();

        // 머즐 플래시 출력
        if (muzzleFlash != null)
        {
            muzzleFlash.ShowFlash();
        }
        else
        {
            Debug.LogWarning("MuzzleFlash is not assigned in M4_WeaponHandler.");
        }

        // 총알 한 발만 생성
        CreateProjectile(Controller.LookDirection, 0f);
    }
}
