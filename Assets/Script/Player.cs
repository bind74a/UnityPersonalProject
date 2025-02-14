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


    //�÷��̾� �Է� ����
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
        Vector2 speedLimit = inputVec.normalized * playSpeed * Time.fixedDeltaTime;//��ȭ�� ����
        playRigi.MovePosition(playRigi.position + speedLimit);//.MovePosition(������ġ + ��ȭ�� �� ��ġ)
    }

    private void LateUpdate()
    {
        if(inputVec.x != 0)
        {
            playSprite.flipX = inputVec.x < 0;//����Ű �Է½ÿ��� true ������ ��ȯ
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)//���� ������ �̴ϰ��� ���� ����
    {
        
    }
}
