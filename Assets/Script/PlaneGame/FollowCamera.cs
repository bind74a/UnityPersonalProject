using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //이 카메라 무빙은 카메라가 플레이어 앞을 비출때쓴다

    public Transform target;
    float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x;//카메라 x 축 - 타겟으로 잡은 오브젝트의 x축 간격 값을 offsetX에 저장
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return ;

        Vector3 pos = transform.position;//카메라의 위치값 
        // 포지션 값을 연산할때는 변수에 저장하고 사용해야한다
        pos.x = target.position.x + offsetX; 
        transform.position = pos;
    }
}
