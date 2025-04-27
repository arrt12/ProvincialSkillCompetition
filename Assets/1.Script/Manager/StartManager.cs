using UnityEngine;

#region Class: StartManager
/// <summary>
/// 시작 애니메이션 종료 시 플레이어 UI 활성화, 카메라 이동 활성화하고
/// 게임 시작 플래그 설정, 오디오 전환, 애니메이터 및 스크립트 비활성화
/// </summary>
public class StartManager : MonoBehaviour
{
    #region Serialized Fields
    [Header("UI Elements")]
    public GameObject PlayerUI;  // 플레이어 UI 오브젝트
    #endregion

    #region Public Methods
    /// <summary>
    /// 시작 애니메이터가 끝난 후 호출하고
    /// 일련의 초기화 루틴을 순차적으로 실행
    /// </summary>
    public void EndStartAnimator()
    {
        EnablePlayerUI();
        EnableCameraMove();
        StartGame();
        SwapAudio();
        DisableAnimator();
        DisableThisScript();
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// 플레이어 UI를 활성화한다.
    /// </summary>
    private void EnablePlayerUI() => PlayerUI.SetActive(true);

    /// <summary>
    /// CamMove 컴포넌트를 활성화하여 카메라 이동을 허용
    /// </summary>
    private void EnableCameraMove() => GetComponent<CamMove>().enabled = true;

    /// <summary>
    /// 플레이어 시작 플래그와 게임 진행 플래그를 설정
    /// </summary>
    private void StartGame()
    {
        Player.Instance.isStart = true;
        PlayGameManger.Instance.isGame = true;
    }

    /// <summary>
    /// 타이틀 오디오를 중지하고 게임 시작 오디오를 재생
    /// </summary>
    private void SwapAudio()
    {
        GameManager.Instance.StartAudio.Stop();
        GameManager.Instance.GameStartAudio.Play();
    }

    /// <summary>
    /// 자신의 Animator 컴포넌트를 비활성화합니다.
    /// </summary>
    private void DisableAnimator() => GetComponent<Animator>().enabled = false;

    /// <summary>
    /// 이 스크립트를 비활성화하여 더 이상 호출되지 않도록 한다
    /// </summary>
    private void DisableThisScript() => enabled = false;
    #endregion
}
#endregion
