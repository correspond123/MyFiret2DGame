using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono��Update�Ŀ�����
/// ��Ҫ����û�м̳�Mono���� ����Update����
/// ͳһ��Update���й���
/// </summary>
public class MonoUpdateController : MonoBehaviour
{
    private event UnityAction updateEvent;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        updateEvent?.Invoke();
    }
    /// <summary>
    /// ���֡�����¼�
    /// </summary>
    /// <param name="action">��ӵ�ί��</param>
    public void AddUpdateListener(UnityAction action)
    {
        updateEvent += action;
    }
    /// <summary>
    /// ɾ��֡�����¼�
    /// </summary>
    /// <param name="action">ɾ����ί��</param>
    public void RemoveUpdateListener(UnityAction action)
    {
        updateEvent -= action;
    }
}
