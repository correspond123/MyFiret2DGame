using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //����Ŀ��
    public Transform target;
    //�ƶ��ٶ�
    public float moveSpeed=8;
    //Ŀ��λ��
    private Vector3 targetPos;
    //�����ƫ��Ŀ��Y��λ��
    private float offsetY=1.5f;
    void Update()
    {

        targetPos = target.position;
        //��Ŀ�����y��ƫ��
        targetPos.y += offsetY;
        //���������Ҫ����Z
        targetPos.z =this.transform.localPosition.z;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed*Time.deltaTime);
    }
}
