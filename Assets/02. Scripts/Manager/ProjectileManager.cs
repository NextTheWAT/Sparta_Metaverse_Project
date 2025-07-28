using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    [Header("파티클 프리팹 (원본)")]
    [SerializeField] private ParticleSystem impactParticlePrefab;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this);
    }

    public void CreateImpactParticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        if (impactParticlePrefab == null) return;

        // 새 인스턴스를 만들고, 위치를 지정
        ParticleSystem fx = Instantiate(impactParticlePrefab, position, Quaternion.identity);

        // 설정 조정
        var emission = fx.emission;
        emission.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

        var mainModule = fx.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;

        // 재생
        fx.Play();

        // 자동 삭제
        Destroy(fx.gameObject, fx.main.duration);
    }
}
