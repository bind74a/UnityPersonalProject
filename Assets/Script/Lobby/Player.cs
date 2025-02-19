using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playRigi;
    CapsuleCollider2D playColl;
    SpriteRenderer playSprite;
    Animator playanim;

    public GameManager gameManager;

    public float playSpeed;

    Vector3 playDirection;
    //레이캐스트로 감지된 오브젝트를 보관하는 변수
    GameObject scanObject;


    //플레이어 입력 변수
    public Vector2 inputVec;


    private void Awake()
    {
        playRigi = GetComponent<Rigidbody2D>();
        playColl = GetComponent<CapsuleCollider2D>();
        playSprite = GetComponent<SpriteRenderer>();
        playanim = GetComponent<Animator>();
    }

    void Update()
    {
        inputVec.x = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        inputVec.y = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        if(inputVec.y == 1)
        {
            playDirection = Vector3.up;
        }
        else if (inputVec.y == -1)
        {
            playDirection = Vector3.down;
        }
        else if(inputVec.x == 1)
        {
            playDirection = Vector3.right;
        }
        else if(inputVec.x == -1)
        {
            playDirection = Vector3.left;
        }

        //대화 창 테스트 
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            gameManager.LobbyAction(scanObject);
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 speedLimit = inputVec.normalized * playSpeed * Time.fixedDeltaTime;//변화값 고정
        playRigi.MovePosition(playRigi.position + speedLimit);//.MovePosition(현재위치 + 변화를 줄 수치)

        //레이캐스트 캐릭터 앞 오브젝트 스캔
        Debug.DrawRay(playRigi.position,playDirection * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(playRigi.position, playDirection,0.7f,LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }

    private void LateUpdate()
    {
        playanim.SetFloat("isRun", inputVec.magnitude);//.magnitude 데이터 값 변화 감지 함수
        if (inputVec.x != 0)
        {
            playSprite.flipX = inputVec.x < 0;//왼쪽키 입력시에만 true 값으로 변환
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)//지역 도착시 미니게임 실행 구현
    {
        switch(collision.gameObject.name)
        {
            case "PlaneGamePanel":
                SceneManager.LoadScene("Game1Scene");

                break;
        }
    }
}
