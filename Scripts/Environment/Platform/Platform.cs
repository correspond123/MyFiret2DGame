using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    /// <summary>
    /// ƽ̨��Y
    /// </summary>
    public float Y=>this.transform.position.y;//�������Ը�ֵ����֮������ ��̬ƽ̨
    /// <summary>
    /// ƽ̨�Ŀ�
    /// </summary>
    public float width = 5;
    /// <summary>
    /// ƽ̨����߽�
    /// </summary>
    public float left=>this.transform.position.x-width/2;
    /// <summary>
    /// ƽ̨���ұ߽�
    /// </summary>
    public float right=>this.transform.position.x+width/2;
    /// <summary>
    /// ƽ̨�Ƿ��������
    /// </summary>
    public bool canFall=true;
    public void Start()
    {
        PlatformDataManager.Instance.AddPlatform(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position-Vector3.right*width/2, this.transform.position + Vector3.right * width / 2);
    }
    /// <summary>
    /// �ṩ�ⲿ�������Ƿ��������ƽ̨��
    /// </summary>
    /// <returns></returns>
    public bool CheckObjOnFallMe(Vector3 pos)
    {
        //�����Y ����֮�� �������ҵı߽���
        if(pos.y>= Y && pos.x <=right&&pos.x>=left)
            return true;
        return false;
    }
    /// <summary>
    /// �������Ƿ���ƽ̨��
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool CheckObjOnMe(Vector3 pos)
    {
        if(pos.y-Y<0.1f && pos.x <= right && pos.x >= left)
            return true;
        return false;
    }
}
