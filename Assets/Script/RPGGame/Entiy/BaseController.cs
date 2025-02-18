using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D rigid;

    [SerializeField] private SpriteRenderer chracterRenderer;
    [SerializeField] private Transform weaponPivot;

    //�÷��̾� �̵� �⺻ ��
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    //�÷��̾� �ü� �⺻��
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    //�÷��̾� �˹� �⺻��
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
            weaponHandler = Instantiate(WeaponPrefab,weaponPivot); //�������� ������ ��ġ���� weaponHandler �ִ´�
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
        HandleAction();//�÷��̾ ����Ű���� ���� ���콺 ��ġ���� �������� movementDirection, lookDirection������ �־���
        Rotate(lookDirection);//���콺 ��ġ���� ������ ĳ������ �þ� ������ ����
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);//������ ������ �÷��̾��� �̵����� ��
        if(knockbackDuration > 0.0f)//�˹����ӽð��� 0 ���� Ŭ�� �۵�
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
        if(knockbackDuration > 0.0f)//�˹����ӽð��� 0 ���� Ŭ�� �۵�
        {
            direction *= 0.2f;
            direction += knockback;
        }

        rigid.velocity = direction;
        animationHandler.Move(direction);
    }
    /// <summary>
    /// ���� ���� �޼���
    /// </summary>
    /// <param name="direction"></param>
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//���콺�� ��ġ�� ������ ��ȯ
        bool isLeft = Mathf.Abs(rotZ) > 90f; //�÷��̾��� �þ߰� 90���̻��϶� true 

        chracterRenderer.flipX = isLeft;

        if(weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);//Quaternion.Euler ������Ʈ�� ����� �������� ȸ�� �ϴ� �Լ�
        }

        weaponHandler?.Rotate(isLeft);//weaponHandler �� null �ƴϸ� Rotate(isLeft) ���� (���ڵ带 �ְ� ����� Ȱ�� �ѹ��� �ø���)
    }

    public void ApplyKnockback(Transform other, float power,float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;//�˹�� �ð��������� ������
    }
    /// <summary>
    /// ���� ������ �޼���
    /// �����ð����� �߻��� �Ҽ��ְ� �ϴ°�
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
