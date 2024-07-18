using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}
public class EventInfo<T> :IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
public class EventInfo : IEventInfo 
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

/// <summary>
/// �¼�����ģ��
/// </summary>
public class EventCenter : SingleBaseManger<EventCenter>
{
    //��� �¼��� ����
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// ��Ӽ��� ��1���������¼�
    /// </summary>
    /// <param name="name">��Ӽ����¼���</param>
    /// <param name="action">���ί��</param>
    public void AddEventListener<T>(string name,UnityAction<T> action)
    {
        //�¼����� ���޶�Ӧ�¼�
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            //���ί��
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }

    /// <summary>
    /// ��Ӽ��� �޲������¼�
    /// </summary>
    /// <param name="name">��Ӽ����¼���</param>
    /// <param name="action">���ί��</param>
    public void AddEventListener(string name, UnityAction action)
    {
        //�¼����� ���޶�Ӧ�¼�
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            //���ί��
            eventDic.Add(name, new EventInfo(action));
        }
    }
    /// <summary>
    /// �Ƴ� �в������¼�
    /// </summary>
    /// <param name="name">�Ƴ��¼���</param>
    /// <param name="action">��Ӧ��ӵ�ί�к���</param>
    public void RemoveEventListener<T>(string name,UnityAction<T> action)
    {
        if(eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions -= action;

    }
    /// <summary>
    /// �Ƴ� �޲������¼��¼�
    /// </summary>
    /// <param name="name">�Ƴ��¼���</param>
    /// <param name="action">��Ӧ��ӵ�ί�к���</param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;
    }
    /// <summary>
    /// ���� �в������¼�
    /// </summary>
    /// <param name="name">Ҫ�������¼���</param>
    public void EventTrigger<T>(string name,T info)
    {
        //�¼����� ���޶�Ӧ�¼�
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
    }
    /// <summary>
    /// ���� �޲������¼�
    /// </summary>
    /// <param name="name">Ҫ�������¼���</param>
    public void EventTrigger(string name)
    {
        //�¼����� ���޶�Ӧ�¼�
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions?.Invoke();
    }
    /// <summary>
    /// ����¼�����
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
