using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ������ڵ� ������
/// </summary>
public class PoolData
{
    //����������
    public GameObject father;
    //��������
    public List<GameObject> poolList;
    /// <summary>
    /// ���캯�� ���� ���ö�����
    /// </summary>
    /// <param name="obj">Ҫ��ŵĶ���</param>
    /// <param name="poolObj">Ҫ����ڵ� ����ض���</param>
    public PoolData(GameObject obj,GameObject poolObj)
    {
        father = new GameObject(obj.name);
        father.transform.parent = poolObj.transform;
        poolList = new List<GameObject>() {};
        PushObj(obj);
    }
    /// <summary>
    /// �Ž������
    /// </summary>
    /// <param name="obj">��Ҫ����Ķ���</param>
    public void PushObj(GameObject obj)
    {
        poolList.Add(obj);
        //���ø�����
        obj.transform.parent = father.transform;
        //���뻺��ض���Ҫ��ʧ��
        obj.SetActive(false);
    }
    /// <summary>
    /// ��������ȡ������
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = null;
        //ȡ����һ�� �����б��� �Ƴ�
        obj = poolList[0];
        poolList.RemoveAt(0);
        //�ó� ����ض���Ҫ �ȼ���
        obj.SetActive(true);
        //�Ͽ� �뻺��� �Ĺ�ϵ
        obj.transform.parent = null;
        return obj;
    }
}

/// <summary>
/// �����ģ��
/// </summary>
public class PoolMgr : SingleBaseManger<PoolMgr>
{
    //���ڴ洢���ֵ�
    public Dictionary<string , PoolData> poolDic =new Dictionary<string, PoolData>();
    //����ض���
    private GameObject poolObj;
    /// <summary>
    /// ȡ������
    /// </summary>
    /// <param name="name">��Ҫ�ӻ������ȡ�Ķ�����</param>
    /// <returns></returns>
    public void GetObj(string name,UnityAction<GameObject> callBack)
    {
        //������� ����Ҫ����
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            callBack(poolDic[name].GetObj()); 
        //������� û����Ҫ����
        else
        {
            //�첽������Դ
            ResourcesManager.Instance.LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                callBack(o);
            });
        }
    }
    /// <summary>
    /// �Ž������
    /// </summary>
    /// <param name="name">���뻺�����������</param>
    /// <param name="obj">��Ҫ����Ķ���</param>
    public void PushObj(string name, GameObject obj)
    {
        //�ж��Ƿ���ڻ���� ��������ھʹ���
        if(poolObj==null)
            poolObj = new GameObject("Pool");
        //���� �г���
        if (poolDic.ContainsKey(name))
            poolDic[name].PushObj(obj);
        //���� û�г���
        else
            poolDic.Add(name, new PoolData(obj,poolObj));

    }
    /// <summary>
    /// ���ڹ�����ʱ Ҫ ��ջ������
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
