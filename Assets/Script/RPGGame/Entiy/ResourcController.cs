using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;//무적시간

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler anim;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.Health;

    public AudioClip damageClip;

    private void Awake()
    {
        baseController = GetComponent<BaseController>();
        statHandler = GetComponent<StatHandler>();
        anim = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health;
    }
    /// <summary>
    /// 무적시간 메서드
    /// </summary>
    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)//float.MaxValue < 0.5f
        { 
            timeSinceLastChange += Time.deltaTime;//float.MaxValue 프레임 순으로 증가
            if (timeSinceLastChange >= healthChangeDelay)// 두값이 같거나 float.MaxValue 클때
            {
                anim.InvincibilityEnd();//무적시간 종료
            }
        }
    }
    /// <summary>
    /// 데미지 계산 메서드
    /// </summary>
    /// <param name="change"></param>
    /// <returns></returns>
    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth; //체력이 최대값을 넘을때 최대값고정
        //CurrentHealth 이 MaxHealth 보다 클시 MaxHealth 값 아닐시 CurrentHealth 값
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;//체력이 0 이하로 않떨어지가 하는곳
        //CurrentHealth 이 0 보다 작을시 0 아날시 CurrentHealth 값

        if (change <0)
        {
            anim.Damage();

            if(damageClip != null)
            {
                SoundManager.PlayClip(damageClip);
            }
        }

        if(CurrentHealth <= 0)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        baseController.Death();
    }
}
