using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ǽ���ݹ����� ��Ҫ����һ���Ի�ȡ���� ����֮���ٴζ�ε�����ȡ
/// </summary>
public class WallDataManager : SingleBaseManger<WallDataManager>
{
    //�洢ǽ������
    public List<Wall> Walls = new List<Wall>();
    /// <summary>
    /// ���ǽ�ķ���
    /// </summary>
    public void AddWall(Wall wall)
    {
        Walls.Add(wall);
    }
    /// <summary>
    /// ɾ��ǽ�ķ���
    /// </summary>
    public void RemoveWall(Wall wall)
    {
        Walls.Remove(wall);
    }
    /// <summary>
    /// ���ǽ�ķ���
    /// </summary>
    public void Clear()
    {
        Walls.Clear();
    }
}
