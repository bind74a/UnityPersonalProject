using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f; //공격 딜레이
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f; //투사체의 크기? 아니면 무기의 크기
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }
    
    [SerializeField] public float power = 1f; //공격력
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f;//무기의 각도 회전 속도?
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;//넉백 판정 여부
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;//넉백에 가해지는 힘
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;//넉백의 시간
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("isAttack");

    public BaseController Controller { get; private set; }

    private Animator anim;
    private SpriteRenderer weaponRenderer;

    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        anim = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        anim.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        anim.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
