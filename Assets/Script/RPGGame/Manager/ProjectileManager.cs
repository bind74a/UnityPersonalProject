using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance {  get { return instance; } }

    [SerializeField] private GameObject[] projectilPrefabs;

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
        projectileController.Init(direction, rangeWeaponHandler);
    }
}
