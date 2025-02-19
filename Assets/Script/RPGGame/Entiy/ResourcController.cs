using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;//�����ð�

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
    /// �����ð� �޼���
    /// </summary>
    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)//float.MaxValue < 0.5f
        { 
            timeSinceLastChange += Time.deltaTime;//float.MaxValue ������ ������ ����
            if (timeSinceLastChange >= healthChangeDelay)// �ΰ��� ���ų� float.MaxValue Ŭ��
            {
                anim.InvincibilityEnd();//�����ð� ����
            }
        }
    }
    /// <summary>
    /// ������ ��� �޼���
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
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth; //ü���� �ִ밪�� ������ �ִ밪����
        //CurrentHealth �� MaxHealth ���� Ŭ�� MaxHealth �� �ƴҽ� CurrentHealth ��
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;//ü���� 0 ���Ϸ� �ʶ������� �ϴ°�
        //CurrentHealth �� 0 ���� ������ 0 �Ƴ��� CurrentHealth ��

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
