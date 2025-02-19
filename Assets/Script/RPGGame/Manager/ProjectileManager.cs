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
        //가져온 총탄 오브젝트(origin)를 지정한 위치(startPosition)에서 각도변화 없이(Quaternion.identity) obj 변수에 지정

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        //변수에 넣은 모든 오브젝트의 ProjectileController 컴파운드 정보 값을 ProjectileController 스크립트로 보낸다
        projectileController.Init(direction, rangeWeaponHandler, this);
        //위에 정보가 들어간 상태인 projectileController 스크립트 안에 Init 메서드실행 direction, rangeWeaponHandler, this 매개변수의 데이터를 같이 보낸다
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
