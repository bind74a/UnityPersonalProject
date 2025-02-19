using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();

        //ĳ���� �� �ٶ󺸰� �մ°����� �̵��� ��ǥ
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x,
            collideBoxSize,0, Vector2.zero,0,target);

        if(hit.collider != null)
        {
            ResourcController resourcController = hit.collider.GetComponent<ResourcController>();
            if(resourcController != null)
            {
                resourcController.ChangeHealth(-power);
                if(IsOnKnockback)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if(controller != null)
                    {
                            controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
        }

        //�ڽ� ����ĳ��Ʈ �ð�ȭ êGPT ���� ��Ź��
        Vector2 origin = transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x;
        Vector2 size = collideBoxSize;
        float angle = 0;
        Vector2 direction = Vector2.zero;
        float distance = 0;

        RaycastHit2D hitview = Physics2D.BoxCast(origin, size, angle, direction, distance, target);

        Vector2 topLeft = origin + new Vector2(-size.x / 2, size.y / 2);
        Vector2 topRight = origin + new Vector2(size.x / 2, size.y / 2);
        Vector2 bottomLeft = origin + new Vector2(-size.x / 2, -size.y / 2);
        Vector2 bottomRight = origin + new Vector2(size.x / 2, -size.y / 2);

        Debug.DrawRay(topLeft, topRight - topLeft, Color.green);    // ���� ��
        Debug.DrawRay(topRight, bottomRight - topRight, Color.green); // ������ ��
        Debug.DrawRay(bottomRight, bottomLeft - bottomRight, Color.green); // �Ʒ��� ��
        Debug.DrawRay(bottomLeft, topLeft - bottomLeft, Color.green); // ���� ��

    }

    public override void Rotate(bool isLeft)
    {
        if(isLeft)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
