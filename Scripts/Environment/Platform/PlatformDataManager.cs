using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƽ̨���ݹ����� ��Ҫ����һ���Ի�ȡ���� ����֮���ٴζ�ε�����ȡ
/// </summary>
public class PlatformDataManager : SingleBaseManger<PlatformDataManager>
{
    //�洢ƽ̨������
    public List<Platform> platforms = new List<Platform>();
    /// <summary>
    /// ���ƽ̨�ķ���
    /// </summary>
    /// <param name="plat"></param>
    public void AddPlatform(Platform plat)
    {
        platforms.Add(plat);
    }
    /// <summary>
    /// �Ƴ�ƽ̨�ķ���
    /// </summary>
    /// <param name="plat"></param>
    public void RemovePlatform(Platform plat)
    {
        platforms.Remove(plat);
    }
    /// <summary>
    /// �������ƽ̨�ķ���
    /// </summary>
    public void Clear()
    {
        platforms.Clear();
    }
}
