using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    //��ֹ� ��ġ ���� ����
    public float highPosY = 1f;
    public float lowPosy = -1f;

    //��ֹ� �߾� ���� ���� ����
    public float holeSizeMin = 1f;
    public float holeSizeMex = 3f;

    public Transform topObject;
    public Transform bottomObject;

    //��ֹ� �� ���� ���� ����
    public float widthPadding = 4f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    /// <summary>
    /// ��ֹ� ������ġ ���� ����
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
    /// �浹�� �浹 ������ ���ö� ���� ���� ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Plane player = collision.gameObject.GetComponent<Plane>();//�浹�� ������Ʈ�� Plane ���Ŀ�尡 �ֽ��� true ������ null
        if (player != null)
        {
            gameManager.PlaneAddScore(1);
        }
    }
}
