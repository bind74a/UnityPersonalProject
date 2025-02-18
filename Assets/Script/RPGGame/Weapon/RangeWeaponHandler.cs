using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1F; //����ü ũ��
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;//�Ѿ��� ��Ÿ�
    public float Duration { get { return duration; } }

    [SerializeField] private float spread; //����ü�� ź ����
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot; //����ü�� ����
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectileAngle;//����ä ������ �� ź�� ź����
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

        float minAlge = -(numberOfProjectilePerShot / 2f) * projectileAngleSpace; //ĳ���Ͱ� ����ü�� �߻� �Ҽ��ִ� ����

        for (int i = 0; i < numberOfProjectilePerShot; i++)//�߻�ä ���� ��ŭ �ݺ�
        {
            float angle = minAlge + projectileAngleSpace * i;//�� ����ü�� �߻� ������ angle�� ����
            float randomSpread = Random.Range(-spread, spread);//�������� �߰��� ź ����
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
        return Quaternion.Euler(0, 0, degree) * v;//���� ������Ʈ�� ȸ�� ��Ű�� ��
    }
}
