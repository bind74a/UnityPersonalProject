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
        direction = direction * 5;
        if(knockbackDuration > 0.0f)//넉백지속시간이 0 보다 클시 작동
        {
            direction *= 0.2f;
            direction += knockback;
        }

        rigid.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f; //플레이어의 시야가 90도이상일때 true 

        chracterRenderer.flipX = isLeft;

        if(weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    public void ApplyKnockback(Transform other, float power,float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;//넉백시 팅겨져나가는 각도값
    }
}
