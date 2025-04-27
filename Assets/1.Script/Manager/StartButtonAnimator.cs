using UnityEngine;

#region Class: StartButtonAnimator
/// <summary>
/// ���� ��ư�� ������ �� �ִϸ��̼ǰ� ���带 ����ϴ� Ŭ����
/// </summary>
public class StartButtonAnimator : MonoBehaviour
{
    #region Serialized Fields
    [Header("Animator & Audio Settings")]
    public Animator ani;           // ��ư �ִϸ��̼ǿ� Animator
    public AudioSource StartAudio; // ��ư Ŭ�� ����� AudioSource
    #endregion

    #region Public Methods
    /// <summary>
    /// UI ��ư�� OnClick �̺�Ʈ�� �����Ͽ� ȣ��
    /// "Clear" Ʈ���Ÿ� �ִϸ����Ϳ� �����ϰ� ���带 ���
    /// </summary>
    public void Button_Start()
    {
        ani.SetTrigger("Clear");
        StartAudio.Play();
    }
    #endregion
}
#endregion