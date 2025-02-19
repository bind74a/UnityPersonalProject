using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDurationl;//����ü �� ���ӽð�?
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigid;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestroy = true;

    ProjectileManager projectileManager;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady) return;

        currentDurationl += Time.deltaTime;

        if(currentDurationl > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigid.velocity = direction * rangeWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(levelCollisionLayer.value ==(levelCollisionLayer.value | (1<< collision.gameObject.layer)))
        //2���� ������ �ʹ� ��ƴ� ����Ȱ���ҋ��� �ٸ������ ã�ƺ��߰ٴ�
        //���̾� �������ʿ��ϸ� ���̾� �±׳� ��ȣ ��������� �ִ´� ���� �� 2������ ��ȭ��Ű�°ɱ�.
        //�ܺ��浹�� �⵿
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f, fxOnDestroy);
        }
        else if(rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1<<collision.gameObject.layer)))
        //���Ϳ� �浹�� �⵿
        {
            ResourcController resourcController = collision.GetComponent<ResourcController>();
            if (resourcController != null)
            {
                resourcController.ChangeHealth(-rangeWeaponHandler.power);
                if (rangeWeaponHandler.IsOnKnockback)
                {
                    BaseController controller = collision.GetComponentInParent<BaseController>();
                    if(controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }

            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;

        rangeWeaponHandler = weaponHandler;

        this.direction = direction;
        currentDurationl = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this.direction;

        if (direction.x <0)
        {
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        }
        else
        {
            pivot.localRotation = Quaternion.Euler(0, 0, 0);
        }
        isReady = true;
    }

    private void DestroyProjectile(Vector3 positon,bool createFx)
    {
        if(createFx)//�浹���� bool ��
        {
            projectileManager.CreateImpactParticlesAtPosition(positon, rangeWeaponHandler);
        }

        Destroy(this.gameObject);
    }
}
