using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera playercamera;
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        playercamera = Camera.main;
    }
    

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal,vertical). normalized;

        Vector2 mousePosition = Input.mousePosition;//마우스의 위치를 변수로 지정
        Vector2 worldPos = playercamera.ScreenToWorldPoint(mousePosition);//마우스의 위치를 씬화면 기준으로 움직인다

        lookDirection = (worldPos - (Vector2)transform.position);
        
        if(lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }

        isAttacking = Input.GetMouseButton(0);
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }
}
