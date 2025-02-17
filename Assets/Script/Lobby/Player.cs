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

    public float playSpeed;


    //�÷��̾� �Է� ����
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
        playanim.SetFloat("isRun", inputVec.magnitude);//.magnitude ������ �� ��ȭ ���� �Լ�
        if (inputVec.x != 0)
        {
            playSprite.flipX = inputVec.x < 0;//����Ű �Է½ÿ��� true ������ ��ȯ
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)//���� ������ �̴ϰ��� ���� ����
    {
        switch(collision.gameObject.name)
        {
            case "PlaneGamePanel":
                SceneManager.LoadScene("Game1Scene");

                break;
        }
    }
}
