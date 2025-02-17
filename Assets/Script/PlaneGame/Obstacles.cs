using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    //장애물 위치 지정 변수
    public float highPosY = 1f;
    public float lowPosy = -1f;

    //장애물 중앙 간격 지정 변수
    public float holeSizeMin = 1f;
    public float holeSizeMex = 3f;

    public Transform topObject;
    public Transform bottomObject;

    //장애물 폭 간격 지정 변수
    public float widthPadding = 4f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    /// <summary>
    /// 장애물 랜덤배치 연산 로직
    /// </summary>
    /// <param name="lastPosition"></param>
    /// <param name="obstaclCount"></param>
    /// <returns></returns>
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMex);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

        placePosition.y = Random.Range(lowPosy,highPosY);

        transform.position = placePosition;
        return placePosition;
    }
    /// <summary>
    /// 충돌후 충돌 범위를 나올때 점수 증가 로직
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Plane player = collision.gameObject.GetComponent<Plane>();//충돌한 오브젝트에 Plane 컴파운드가 있스면 true 없으면 null
        if (player != null)
        {
            gameManager.PlaneAddScore(1);
        }
    }
}
