using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("isMove");//.StringToHash 문자열 값을 int로 변환하여 숫자값으로 쓸수있는 함수
    private static readonly int IsDamage = Animator.StringToHash("isDamage");

    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        anim.SetBool(IsMove, obj.magnitude > 0.5f);//여기서 .magnitude 는 오브젝트가 움직인 값
        //.magnitude 는 두 지점 간의 거리을 잴때 쓰는 함수
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
