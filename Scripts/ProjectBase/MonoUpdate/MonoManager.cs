using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Mono��Update�Ĺ�����
/// 1.ͳһ��Update���й���
/// 2.���Э��
/// </summary>
public class MonoManager : SingleBaseManger<MonoManager>
{
    public MonoUpdateController controller;

    public MonoManager()
    {
        GameObject obj = new GameObject("MonoUpdateController");
        controller = obj.AddComponent<MonoUpdateController>();
    }

    /// <summary>
    /// ���֡�����¼�
    /// </summary>
    /// <param name="action">��ӵ�ί��</param>
    public void AddUpdateListener(UnityAction action)
    {
        controller.AddUpdateListener(action);
    }
    /// <summary>
    /// ɾ��֡�����¼�
    /// </summary>
    /// <param name="action">ɾ����ί��</param>
    public void RemoveUpdateListener(UnityAction action)
    {
        controller.RemoveUpdateListener(action);
    }

    #region ���ⲿ����Э�̵ĺ���
    /// <summary>
    /// ����Э�̵ĺ���
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    /// <summary>
    /// ����Э�̵ĺ���
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }
    /// <summary>
    /// ����Э�̵ĺ���
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }
    #endregion

    #region �ر�Э�̵ĺ���
    /// <summary>
    /// �ر�Э�̵ĺ���
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public void StopCoroutine(Coroutine routine)
    {
        controller.StopCoroutine(routine);
    }
    /// <summary>
    /// �ر�Э�̵ĺ���
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public void StopCoroutine(IEnumerator routine)
    {
        controller.StopCoroutine(routine);
    }
    #endregion

}
