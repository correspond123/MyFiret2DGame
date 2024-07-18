using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_UI_Layer 
{ 
    bot,
    mid,
    top,
    system
}


/// <summary>
/// UI������
/// </summary>
public class UIManager : SingleBaseManger<UIManager>
{
    // �����ϴ��ڵ���� �洢����
    public Dictionary<string, BasePanel> panelDic= new Dictionary<string, BasePanel>();
    //UI���Ĳ㼶
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;
    //��¼UICavans������ 
    public RectTransform canvans;

    /// <summary>
    /// ��ȡUI�㼶������
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.bot:
                return this.bot;
            case E_UI_Layer.mid:
                return this.mid;
            case E_UI_Layer.top:
                return this.top;
            case E_UI_Layer.system:
                return this.system;
        }
        return null;
    }
    public UIManager()
    {
        //ȥ�ҵ�Cavens
        GameObject obj = ResourcesManager.Instance.Load<GameObject>("UI/Default/Canvas");
        //���������Ƴ�
        GameObject.DontDestroyOnLoad(obj);
        canvans = obj.transform as RectTransform;
        //�ҵ�����

        bot = canvans.Find("Bot");
        mid = canvans.Find("Mid");
        top = canvans.Find("Top");
        system = canvans.Find("System");

    }
    /// <summary>
    /// ��ʾ���
    /// </summary>
    /// <typeparam name="T">���Ľű�����</typeparam>
    /// <param name="panelName">����Ԥ����·��UI/</param>
    /// <param name="layer">��ʾ����һ��</param>
    /// <param name="callBack">�����ʾ�� Ҫִ�е��߼�����</param>
    public virtual void ShowPanel<T>(string panelName,E_UI_Layer layer, UnityAction<T> callBack) where T : BasePanel
    {
        //��� �Ѿ����ع������ ��ֱ�ӵ�����ʾ���� �Լ��ص����� ��ֱ�ӷ���
        if (panelDic.ContainsKey(panelName))
        {
            //��ʾ���
            panelDic[panelName].ShowMe();
            if (callBack != null)
                callBack(panelDic[panelName] as T);
            return;
        }
        ResourcesManager.Instance.LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            //����ΪCavans ��Ӧ�㼶�� �Ӷ���
            Transform father=bot;
            switch (layer)
            {
                case E_UI_Layer.bot:
                    father = bot;
                    break;
                case E_UI_Layer.mid:
                    father = mid;
                    break;
                case E_UI_Layer.top:
                    father = top;
                    break;
                case E_UI_Layer.system:
                    father = system;
                    break;
            }
            //���ø�����
            obj.transform.SetParent(father);
            //��ʼ�����λ��
            obj.transform.localPosition=Vector3.zero;
            obj.transform.localScale=Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //�õ� Ԥ�������ϵ� ���ű�
            T panel=obj.GetComponent<T>();
            //������崴����ɺ���߼�
            if(callBack!= null)
                callBack(panel);
            //�洢���
            panelDic.Add(panelName, panel);
        });
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="panelName">����Ԥ����·��UI/</param>
    public virtual void HidePanel(string panelName) 
    {
        if (panelDic.ContainsKey(panelName))
        {
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }
    /// <summary>
    /// �õ�һ���Ѿ���ʾ�����
    /// </summary>
    /// <param name="panelName">����Ԥ����·��UI/</param>
    public T GetPanel<T>(string panelName) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        return null;
    }
    /// <summary>
    /// �����Զ�������ؼ��ķ���
    /// </summary>
    /// <param name="control">�����ؼ�����</param>
    /// <param name="type">�¼�����</param>
    /// <param name="callBack">�¼�����Ӧ����</param>
    public static void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger=control.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry =new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);
        trigger.triggers.Add(entry);
    }
}
