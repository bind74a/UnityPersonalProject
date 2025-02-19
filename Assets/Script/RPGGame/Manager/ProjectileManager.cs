using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance {  get { return instance; } }

    [SerializeField] private GameObject[] projectilPrefabs;

    [SerializeField] private ParticleSystem impactParticleSystem;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler,Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = projectilPrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);
        //������ ��ź ������Ʈ(origin)�� ������ ��ġ(startPosition)���� ������ȭ ����(Quaternion.identity) obj ������ ����

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        //������ ���� ��� ������Ʈ�� ProjectileController ���Ŀ�� ���� ���� ProjectileController ��ũ��Ʈ�� ������
        projectileController.Init(direction, rangeWeaponHandler, this);
        //���� ������ �� ������ projectileController ��ũ��Ʈ �ȿ� Init �޼������ direction, rangeWeaponHandler, this �Ű������� �����͸� ���� ������
    }

    public void CreateImpactParticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position;
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10;
        impactParticleSystem.Play();
    }
}
