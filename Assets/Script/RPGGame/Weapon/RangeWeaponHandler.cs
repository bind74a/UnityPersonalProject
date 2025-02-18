using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1F; //투사체 크기
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;//총알의 사거리
    public float Duration { get { return duration; } }

    [SerializeField] private float spread; //투사체의 탄 퍼짐
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot; //투사체의 갯수
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectileAngle;//투사채 개수의 각 탄의 탄퍼짐
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }
    public override void Attack()
    {
        base.Attack();

        float projectileAngleSpace = multipleProjectileAngle;
        int numberOfProjectilePerShot = numberofProjectilesPerShot;

        float minAlge = -(numberOfProjectilePerShot / 2f) * projectileAngleSpace; //캐릭터가 투사체을 발사 할수있는 각도

        for (int i = 0; i < numberOfProjectilePerShot; i++)//발사채 갯수 만큼 반복
        {
            float angle = minAlge + projectileAngleSpace * i;//각 투사체의 발사 각도을 angle에 지정
            float randomSpread = Random.Range(-spread, spread);//랜덤성이 추가된 탄 퍼짐
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(this, projectileSpawnPosition.position, RotateVector2(_lookDirection, angle));
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;//들어온 오브젝트를 회전 시키는 곳
    }
}
