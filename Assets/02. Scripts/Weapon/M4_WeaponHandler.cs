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

        // ���� �÷��� ���
        if (muzzleFlash != null)
        {
            muzzleFlash.ShowFlash();
        }
        else
        {
            Debug.LogWarning("MuzzleFlash is not assigned in M4_WeaponHandler.");
        }

        // �Ѿ� �� �߸� ����
        CreateProjectile(Controller.LookDirection, 0f);
    }
}
