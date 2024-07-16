using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������ģ��
/// </summary>
public class InputManager : SingleBaseManger<InputManager>
{
    //���ڼ��S����״̬
    private bool sKeyState;

    //InputSystem�������ļ���
    private PlayerInputControl inputControl;
    /// <summary>
    /// ���캯�� ����Mono��Update
    /// </summary>
    public InputManager()
    {
        //�����µ�PlayerInputControl����
        inputControl = new PlayerInputControl();
        //��������İ���
        PointInput();
        //���������İ���
        MonoManager.Instance.AddUpdateListener(myUpdate);
    }

    private void myUpdate()
    {
        LastInput();
    }
    /// <summary>
    /// �ṩ���ⲿ����input�ķ���
    /// </summary>
    public void OnEnableControl()
    {
        inputControl.Enable();
    }
    /// <summary>
    /// �ṩ���ⲿ ����input�ķ���
    /// </summary>
    public void OnDisableControl()
    {
        inputControl.Disable();
    }
    /// <summary>
    /// ע�����¼��ķ���
    /// </summary>
    private void PointInput()
    {
        //��������״̬���
        RecoveryKeys();

        #region ��Ծ����
        inputControl.Gameplay.Jump.started += (context) =>
        {
            if (!sKeyState)
                EventCenter.Instance.EventTrigger<bool>("��Ծ����", true);
        };
        #endregion

        #region ��ƽ̨����
        inputControl.Gameplay.FallDownFromPlatForm.started += (context) =>
        {
            EventCenter.Instance.EventTrigger<bool>("��ƽ̨����", true);
        };
        #endregion


        #region ������������
        inputControl.Gameplay.NormalAtk.started += (context) =>
        {
            EventCenter.Instance.EventTrigger("����������");
        };
        #endregion


    }
    /// <summary>
    /// ע�᳤���¼��ķ���
    /// </summary>
    private void LastInput()
    {
        #region �����ƶ���������
        EventCenter.Instance.EventTrigger<Vector2>("WASD����", inputControl.Gameplay.Move.ReadValue<Vector2>());
        #endregion

    }
    /// <summary>
    /// ���� ����״̬
    /// </summary>
    private void RecoveryKeys()
    {
        #region S��
        inputControl.Gameplay.SKeyState.started += cxt => sKeyState = true;
        inputControl.Gameplay.SKeyState.canceled += cxt => sKeyState = false;
        #endregion

        #region �ƶ��������
        inputControl.Gameplay.Move.started += (context) =>
        {
            EventCenter.Instance.EventTrigger<bool>("WASD�ɿ�", true);
        };
        inputControl.Gameplay.Move.canceled += (context) =>
        {
            EventCenter.Instance.EventTrigger<bool>("WASD�ɿ�", false);
        };
        #endregion
    }


}
