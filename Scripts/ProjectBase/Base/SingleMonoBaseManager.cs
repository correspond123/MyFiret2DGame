using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̳�Mono�ĵ���ģʽ����
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingleMonoBaseManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance=>instance;

    private SingleMonoBaseManager() 
    {
        if (instance == null)
        {
            GameObject obj = new GameObject(typeof(T).ToString());
            instance= obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
        }
    }
}
