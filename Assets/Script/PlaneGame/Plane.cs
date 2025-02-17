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

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();

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
            if(deathCooldown <= 0f)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDead) return; //����Ⱑ �װԵǸ� ������ �Ǽ� �Ʒ������ ���� ������ �ȉ´�

        Vector3 velocity = rigid.velocity;

        velocity.x = forwardSpeed;

        if(isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rigid.velocity = velocity; //������ ������ ���� velocity�� �Ѱ��� ���� ������Ʈ�� ���� �ִ´�

        float angle = Mathf.Clamp(rigid.velocity.y*10, -90, 90); 
        //Mathf.Clamp(���簪, �ּҰ�, �ִ밪); ������ ���Ҷ� ������ �δ��Լ�
        transform.rotation = Quaternion.Euler(0, 0, angle); 
        //Quaternion.Euler(x, y, z); ������Ʈ�� ȸ�� ��Ű�� �Լ� (������� �����ϸ� �´�)

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        isDead = true;
        deathCooldown = 1f;

        anim.SetInteger("isDie", 1);//"isDie" ��� �̸��� �ִϸ��̼� ��ġ�� 1�� �����
        gameManager.GameOver();
    }
}
