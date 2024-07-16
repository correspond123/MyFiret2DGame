using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��Դ����ģ��
/// </summary>
public class ResourcesManager : SingleBaseManger<ResourcesManager>
{
    /// <summary>
    /// ͬ��������Դ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T Load<T>(string path) where T : Object
    {
        T res=Resources.Load<T>(path);
        //����ΪGameObject���� ��ʵ���� �ٷ���
        if (res is GameObject)
            return GameObject.Instantiate(res);
        //���������ֱ�ӷ���
        else
            return res;
    }
    /// <summary>
    /// �첽������Դ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public void LoadAsync<T>(string path,UnityAction<T> callBack) where T : Object
    {
        MonoManager.Instance.StartCoroutine(ReallyLoadAsync<T>(path,callBack));
    }
    /// <summary>
    /// �����첽���ص�Э��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadAsync<T>(string path, UnityAction<T> callBack) where T : Object
    {
        ResourceRequest rr = Resources.LoadAsync<T>(path);
        yield return rr;

        if (rr.asset is GameObject)
            callBack(GameObject.Instantiate(rr.asset) as T);
        else
            callBack(rr.asset as T);

    }
}
