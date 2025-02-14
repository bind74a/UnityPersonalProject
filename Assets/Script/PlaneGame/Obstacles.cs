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

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMex);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

        placePosition.y = Random.Range(lowPosy,highPosY);

        return placePosition;
    }
}
