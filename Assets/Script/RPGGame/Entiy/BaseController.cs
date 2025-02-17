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

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
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
        direction = direction * 5;
        if(knockbackDuration > 0.0f)//�˹����ӽð��� 0 ���� Ŭ�� �۵�
        {
            direction *= 0.2f;
            direction += knockback;
        }

        rigid.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f; //�÷��̾��� �þ߰� 90���̻��϶� true 

        chracterRenderer.flipX = isLeft;

        if(weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    public void ApplyKnockback(Transform other, float power,float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;//�˹�� �ð��������� ������
    }
}
