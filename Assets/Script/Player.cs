using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playRigi;
    CapsuleCollider2D playColl;
    SpriteRenderer playSprite;

    public float playSpeed;


    //플레이어 입력 변수
    public Vector2 inputVec;


    private void Awake()
    {
        playRigi = GetComponent<Rigidbody2D>();
        playColl = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 speedLimit = inputVec.normalized * playSpeed * Time.fixedDeltaTime;//변화값 고정
        playRigi.MovePosition(playRigi.position + speedLimit);//.MovePosition(현재위치 + 변화를 줄 수치)
    }

    private void LateUpdate()
    {
        if(inputVec.x != 0)
        {
            playSprite.flipX = inputVec.x < 0;//왼쪽키 입력시에만 true 값으로 변환
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)//지역 도착시 미니게임 실행 구현
    {
        
    }
}
