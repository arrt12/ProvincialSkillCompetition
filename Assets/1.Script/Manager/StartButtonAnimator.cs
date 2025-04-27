using UnityEngine;

#region Class: StartButtonAnimator
/// <summary>
/// 시작 버튼을 눌렀을 때 애니메이션과 사운드를 재생하는 클래스
/// </summary>
public class StartButtonAnimator : MonoBehaviour
{
    #region Serialized Fields
    [Header("Animator & Audio Settings")]
    public Animator ani;           // 버튼 애니메이션용 Animator
    public AudioSource StartAudio; // 버튼 클릭 사운드용 AudioSource
    #endregion

    #region Public Methods
    /// <summary>
    /// UI 버튼의 OnClick 이벤트에 연결하여 호출
    /// "Clear" 트리거를 애니메이터에 설정하고 사운드를 재생
    /// </summary>
    public void Button_Start()
    {
        ani.SetTrigger("Clear");
        StartAudio.Play();
    }
    #endregion
}
#endregion