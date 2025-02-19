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
    //����ĳ��Ʈ�� ������ ������Ʈ�� �����ϴ� ����
    GameObject scanObject;


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

        //��ȭ â �׽�Ʈ 
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            gameManager.LobbyAction(scanObject);
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 speedLimit = inputVec.normalized * playSpeed * Time.fixedDeltaTime;//��ȭ�� ����
        playRigi.MovePosition(playRigi.position + speedLimit);//.MovePosition(������ġ + ��ȭ�� �� ��ġ)

        //����ĳ��Ʈ ĳ���� �� ������Ʈ ��ĵ
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
