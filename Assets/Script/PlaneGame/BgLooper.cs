using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5; //  현재 뒤 배경 개수

    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;


    /// <summary>
    /// 시작시 장애물 배치 로직
    /// </summary>
    void Start()
    {
        Obstacles[] obstacles = GameObject.FindObjectsOfType<Obstacles>(); //.FindObjectOfType 씬에 있는 모든 오브젝트를 찾을떄 쓴다
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length; //현재 씬에 있는 obstacles의 개체수

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
            //장애물 재배치 함수에 현재 기본 등록되있는 장애물의 위치정보를 보낸다
            
            
            /*  <기동 순서>
            현재씬에 그냥 두고있는  obstacles 오브젝트의 위치를 SetRandomPlace 함수에 보내고 오브젝트를 재배치
            재배치된 오브젝트의 정보를 obstacleLastPosition 변수에 넣는다
            */
        }
    }
    /// <summary>
    /// 플레이어가 피한 장애물 재배치 로직
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacles obstacle = collision.gameObject.GetComponent<Obstacles>();
        //collision (충돌한 오브젝트)에 Obstacles 컴포넌트 정보를 불러옴 만약 Obstacles컴포넌트가 없을시 null 값이 나옴

        if (collision.CompareTag("BackGround"))//뒷 배경 재배치 로직
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;//충돌 트리거로 오브젝트의 크기을 잴때
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
            //리턴을 준 이유는 이 if문에 걸려서 실행됫다는것이 obstacle 아니기떄문에
            //아래 if문은 실행을 시키지않게 해서 메모리 최적화 하는것
        }


        if (obstacle)//Obstacles 컴포넌트 가 있으면 작동
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
