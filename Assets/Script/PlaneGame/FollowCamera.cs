using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //�� ī�޶� ������ ī�޶� �÷��̾� ���� ���⶧����

    public Transform target;
    float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x;//ī�޶� x �� - Ÿ������ ���� ������Ʈ�� x�� ���� ���� offsetX�� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return ;

        Vector3 pos = transform.position;//ī�޶��� ��ġ�� 
        // ������ ���� �����Ҷ��� ������ �����ϰ� ����ؾ��Ѵ�
        pos.x = target.position.x + offsetX; 
        transform.position = pos;
    }
}
