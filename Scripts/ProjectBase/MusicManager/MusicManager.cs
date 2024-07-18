using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : SingleBaseManger<MusicManager>
{
    //Ψһ�����������
    private AudioSource bkMusic=null;
    //��Ч�������
    private GameObject soundObj=null;
    //�洢��Ч�б�
    private List<AudioSource> soundList=new List<AudioSource>();
    //�������ִ�С
    private float bkValue=1;
    //��Ч��С
    private float soundValue=1;

    public MusicManager()
    {
        MonoManager.Instance.AddUpdateListener(myUpdate);
    }

    private void myUpdate()
    {
        for (int i=soundList.Count-1; i>=0;i--)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// ���ű�������
    /// </summary>
    /// <param name="path">����·��Music/BK/</param>
    public void PlayBKMusic(string path)
    {
        if (bkMusic == null)
        {
            GameObject obj = new GameObject("BkMusicObj");
            bkMusic = obj.AddComponent<AudioSource>();
        }
        //�첽���ر������� ������
        ResourcesManager.Instance.LoadAsync<AudioClip>("Music/BK/" + path, (clip) =>
        {
            bkMusic.clip = clip;
            bkMusic.volume = bkValue;
            bkMusic.loop = true;
            bkMusic.Play();
        });
    }
    /// <summary>
    /// ��ͣ��������
    /// </summary>
    public void PauseBKMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Pause();
    }
    /// <summary>
    /// ֹͣ��������
    /// </summary>
    public void StopBKMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Stop();
    }
    /// <summary>
    /// �ı䱳��������С
    /// </summary>
    /// <param name="value"></param>
    public void ChangeBKValue(float value)
    {
        bkValue = value;
        if (bkMusic != null)
            return;
        bkMusic.volume=bkValue;
    }
    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="path">��Ч·��Music/Sound/</param>
    /// <param name="isLoop">��Ч�Ƿ�ѭ��</param>
    /// <param name="callBack">�ص�����</param>
    public void PlaySound(string path, bool isLoop=false,UnityAction<AudioSource> callBack=null)
    {
        if (soundObj == null)
        {
            soundObj= new GameObject("soundObj");
        }
        //�첽������Ч ������
        ResourcesManager.Instance.LoadAsync<AudioClip>("Sound" + path, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = bkValue;
            source.Play();
            soundList.Add(source);

            if (callBack != null)
                callBack(source);
        });

    }
    /// <summary>
    /// ֹͣ��Ч
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
    /// <summary>
    /// �ı���Ч��С
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundVaule(float value)
    {
        soundValue= value;
        for (int i = 0; i < soundList.Count; i++)
            soundList[i].volume=soundValue;
    }

}
