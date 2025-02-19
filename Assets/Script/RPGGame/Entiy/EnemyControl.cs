using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;

    [SerializeField] private float followRange = 15f;

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    protected float DistacnToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()//Ÿ�ٰ� ������Ʈ�� �Ÿ� ���
    {
        return (target.position - transform.position).normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if(weaponHandler == null || target == null)
        {
            if(!movementDirection.Equals(Vector2.zero))
            {
                movementDirection = Vector2.zero;
                return;
            }
        }

        float distanc = DistacnToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;

        if (distanc <= followRange)//���� �ν� �����ȿ� �÷��̾ �մ���
        {
            lookDirection = direction;

            if(distanc < weaponHandler.AttackRange)//���� ���� �����ȿ� �÷��̾ �ִ���
            {
                int layerMaskTarget = weaponHandler.target;


                Debug.DrawRay(transform.position, direction * (weaponHandler.AttackRange * 1.5f), new Color(0, 1, 0));
                RaycastHit2D hit = Physics2D.Raycast(transform.position,
                    direction, weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);
                /*
                RaycastHit2D hit = Physics2D.Raycast(transform.position,direction,
                weaponHandler.AttackRange * 1.5f,
                LayerMask.GetMask("Enemy") �� 2������ ���ٴ°���?
                */
                if(hit.collider != null && layerMaskTarget == (layerMaskTarget | (1<<hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }


                movementDirection = Vector2.zero;
                return;
            }

            movementDirection = direction;
        }
    }

    public override void Death()
    {
        base.Death();
        enemyManager.ReMoveEnemyOnDeath(this);
    }
}
