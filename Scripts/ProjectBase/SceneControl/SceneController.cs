using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// �����л�ģ��
/// </summary>
public class SceneController : SingleBaseManger<SceneController>
{
    /// <summary>
    /// ͬ�����س���
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name,UnityAction action)
    {
        SceneManager.LoadScene(name);
        action?.Invoke();
    }

    /// <summary>
    /// �ṩ���ⲿ �� �첽���س���
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void LoadSceneAsyn(string name, UnityAction action)
    {
        MonoManager.Instance.StartCoroutine(ReallyLoadSceneAsyn(name, action));
    }
    /// <summary>
    /// Э���첽���س���
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadSceneAsyn(string name,UnityAction action)
    {
        AsyncOperation ao =  SceneManager.LoadSceneAsync(name);
        //�õ����ؽ���
        while (!ao.isDone)
        {
            //��������½�����
            //ͨ�� �¼����� ȥ���������������¼�
            EventCenter.Instance.EventTrigger("����������",ao.progress);
            yield return ao.progress;
        }
        action?.Invoke();
        
    }
    
}
