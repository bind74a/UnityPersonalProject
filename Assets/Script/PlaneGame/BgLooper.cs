using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5; //  ���� �� ��� ����

    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;


    /// <summary>
    /// ���۽� ��ֹ� ��ġ ����
    /// </summary>
    void Start()
    {
        Obstacles[] obstacles = GameObject.FindObjectsOfType<Obstacles>(); //.FindObjectOfType ���� �ִ� ��� ������Ʈ�� ã���� ����
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length; //���� ���� �ִ� obstacles�� ��ü��

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
            //��ֹ� ���ġ �Լ��� ���� �⺻ ��ϵ��ִ� ��ֹ��� ��ġ������ ������
            
            
            /*  <�⵿ ����>
            ������� �׳� �ΰ��ִ�  obstacles ������Ʈ�� ��ġ�� SetRandomPlace �Լ��� ������ ������Ʈ�� ���ġ
            ���ġ�� ������Ʈ�� ������ obstacleLastPosition ������ �ִ´�
            */
        }
    }
    /// <summary>
    /// �÷��̾ ���� ��ֹ� ���ġ ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacles obstacle = collision.gameObject.GetComponent<Obstacles>();
        //collision (�浹�� ������Ʈ)�� Obstacles ������Ʈ ������ �ҷ��� ���� Obstacles������Ʈ�� ������ null ���� ����

        if (collision.CompareTag("BackGround"))//�� ��� ���ġ ����
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;//�浹 Ʈ���ŷ� ������Ʈ�� ũ���� �궧
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
            //������ �� ������ �� if���� �ɷ��� ����̴ٴ°��� obstacle �ƴϱ⋚����
            //�Ʒ� if���� ������ ��Ű���ʰ� �ؼ� �޸� ����ȭ �ϴ°�
        }


        if (obstacle)//Obstacles ������Ʈ �� ������ �۵�
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
