using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ƽ̨�߼�������
/// </summary>
public class PlatformLogic:SingleBaseManger<PlatformLogic>
{
    //��Ϸ����ǰ����ƽ̨
    private Platform nowPlatform=null;
    //ƽ̨����
    private List<Platform> platformDate;
    
    /// <summary>
    /// �ṩ���ⲿ����߼����ķ���
    /// </summary>
    public void AddUpdateCheck(EntityObj obj)
    {
        EventCenter.Instance.AddEventListener<EntityObj>($"{obj.gameObject.name}��ƽ̨�߼�����",UpdateCheck);
    }
    /// <summary>
    /// �ṩ���ⲿ�Ƴ�ƽ̨�����ķ���
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveUpdateCheck(EntityObj obj)
    {
        EventCenter.Instance.RemoveEventListener<EntityObj>($"{obj.gameObject.name}��ƽ̨�߼�����", UpdateCheck);
    }
    /// <summary>
    /// ����ÿ֡�����ұ仯�ĺ���
    /// </summary>
    public void UpdateCheck(EntityObj obj)
    {
        if (obj == null)
            return;
        //��ÿһ�εı�����Ѱ�ҵ�ƽ̨ ����һ˲����ߵ�ƽ̨ ������������Ծ�����е�ƽ̨
        nowPlatform = null;
        platformDate = PlatformDataManager.Instance.platforms;
        for (int i = 0; i < platformDate.Count; i++)
        {
            //�ж�����Ƿ���ĳ��ƽ̨�������� ���Ҵ������ƽ̨���ߵ�ǰƽ̨Ϊ��
            if (platformDate[i].CheckObjOnFallMe(obj.transform.position) &&
                (nowPlatform == null || nowPlatform.Y < platformDate[i].Y ))
            {
                //��������ƽ̨����
                nowPlatform = platformDate[i];
                //�ı�����ƽ̨����
                //��� ԭʼƽ̨ ����ƽ̨����� �� ����Ϊ���ƽ̨
                if ( nowPlatform != null && obj.transform.position.y - nowPlatform.Y < 0.1f)
                    obj.SetNowFallPlatformData(nowPlatform);
                obj.ChangePlatformData(nowPlatform.Y,nowPlatform.left,nowPlatform.right,nowPlatform.canFall);
            }
        }

        //��⵱ǰƽ̨ �Ƿ�������ƽ̨������
        if(nowPlatform != null&&
            !nowPlatform.CheckObjOnFallMe(obj.transform.position))
        {

            if ((obj as PlayerController))
                (obj as PlayerController).Fall();
        }
    }
}
