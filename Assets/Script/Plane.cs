using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;

    public bool godMode = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
  
    }
    // Start is called before the first frame update
    void Start()
    {

        if (anim == null)
        {
            Debug.LogError("�ִϸ��̼� ����");
        }
        if (rigid == null)
        {
            Debug.LogError("�߷� ������Ʈ ����");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {

        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                isFlap = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = rigid.velocity;

        velocity.x = forwardSpeed;

        if(isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rigid.velocity = velocity; //������ ������ ���� velocity�� �Ѱ��� ���� ������Ʈ�� ���� �ִ´�
    }
}
