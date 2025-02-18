using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D rigid;

    [SerializeField] private SpriteRenderer chracterRenderer;
    [SerializeField] private Transform weaponPivot;

    //플레이어 이동 기본 값
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    //플레이어 시선 기본값
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    //플레이어 넉백 기본값
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;

    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

        if (WeaponPrefab != null)
        {
            weaponHandler = Instantiate(WeaponPrefab,weaponPivot); //프리펩의 정보와 위치값를 weaponHandler 넣는다
        }
        else
        {
            weaponHandler = GetComponentInChildren<WeaponHandler>();
        }
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();//플레이어가 방향키누른 값과 마우스 위치값이 연산으로 movementDirection, lookDirection변수에 넣어짐
        Rotate(lookDirection);//마우스 위치값을 보내고 캐릭터의 시야 각도값 연산
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);//위에서 정해진 플레이어의 이동값이 들어감
        if(knockbackDuration > 0.0f)//넉백지속시간이 0 보다 클시 작동
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }
    
    private void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed;
        if(knockbackDuration > 0.0f)//넉백지속시간이 0 보다 클시 작동
        {
            direction *= 0.2f;
            direction += knockback;
        }

        rigid.velocity = direction;
        animationHandler.Move(direction);
    }
    /// <summary>
    /// 무기 각도 메서드
    /// </summary>
    /// <param name="direction"></param>
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//마우스의 위치를 각도로 변환
        bool isLeft = Mathf.Abs(rotZ) > 90f; //플레이어의 시야가 90도이상일때 true 

        chracterRenderer.flipX = isLeft;

        if(weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);//Quaternion.Euler 오브젝트를 원기둥 기준으로 회전 하는 함수
        }

        weaponHandler?.Rotate(isLeft);//weaponHandler 가 null 아니면 Rotate(isLeft) 실행 (이코드를 넣고 실행시 활이 한번더 플립됨)
    }

    public void ApplyKnockback(Transform other, float power,float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;//넉백시 팅겨져나가는 각도값
    }
    /// <summary>
    /// 공격 딜레이 메서드
    /// 일정시간마다 발사을 할수있게 하는곳
    /// </summary>
    private void HandleAttackDelay()
    {
        if (weaponHandler == null) return;

        if(timeSinceLastAttack <= weaponHandler.Delay )
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if(lookDirection != Vector2.zero)
        {
            weaponHandler?.Attack();
        }
    }
}
