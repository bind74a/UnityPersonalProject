using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDurationl;//투사체 의 지속시간?
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
        //2진수 연산은 너무 어렵다 내가활용할떄는 다른방법을 찾아봐야겟다
        //레이어 정보가필요하면 레이어 태그나 번호 여러방법이 있는대 굳이 왜 2진수로 변화시키는걸까.
        //외벽충돌시 기동
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f, fxOnDestroy);
        }
        else if(rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1<<collision.gameObject.layer)))
        //몬스터와 충돌시 기동
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
        if(createFx)//충돌판정 bool 값
        {
            projectileManager.CreateImpactParticlesAtPosition(positon, rangeWeaponHandler);
        }

        Destroy(this.gameObject);
    }
}
