using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("isMove");//.StringToHash ���ڿ� ���� int�� ��ȯ�Ͽ� ���ڰ����� �����ִ� �Լ�
    private static readonly int IsDamage = Animator.StringToHash("isDamage");

    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        anim.SetBool(IsMove, obj.magnitude > 0.5f);//���⼭ .magnitude �� ������Ʈ�� ������ ��
        //.magnitude �� �� ���� ���� �Ÿ��� �궧 ���� �Լ�
    }

    public void Damage()
    {
        anim.SetBool(IsDamage, true);
    }

    public void InvincibilityEnd()
    {
        anim.SetBool(IsDamage, false);
    }
}
