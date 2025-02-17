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
            Debug.LogError("애니메이션 에러");
        }
        if (rigid == null)
        {
            Debug.LogError("중력 컴포넌트 에러");
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
        if (isDead) return; //비행기가 죽게되면 리턴이 되서 아래기능이 전부 실행이 안됀다

        Vector3 velocity = rigid.velocity;

        velocity.x = forwardSpeed;

        if(isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rigid.velocity = velocity; //위에서 연산이 끝난 velocity의 총값을 현재 오브젝트의 값에 넣는다

        float angle = Mathf.Clamp(rigid.velocity.y*10, -90, 90); 
        //Mathf.Clamp(현재값, 최소값, 최대값); 각도가 변할때 제한을 두는함수
        transform.rotation = Quaternion.Euler(0, 0, angle); 
        //Quaternion.Euler(x, y, z); 오브젝트을 회전 시키는 함수 (원기둥을 생각하면 됀다)

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        isDead = true;
        deathCooldown = 1f;

        anim.SetInteger("isDie", 1);//"isDie" 라는 이름의 애니메이션 수치를 1로 만든다
        gameManager.GameOver();
    }
}
