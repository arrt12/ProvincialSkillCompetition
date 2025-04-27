using System.Collections;
using UnityEngine;

#region Class: StageClearManager
/// <summary>
/// 스테이지 클리어 시 연출(카메라 전환, UI 변경 등) 및 보상 지급을 관리하는 클래스
/// </summary>
public class StageClearManager : Singleton<StageClearManager>
{
    #region Serialized Fields
    [Header("UI & Camera")]
    public GameObject GameEndUI;       // 스테이지 클리어 UI
    public GameObject PlayerStateUI;   // 플레이어 상태 UI
    public GameObject EndCam;          // 엔딩 카메라
    #endregion

    #region Settings
    [Header("Player Rotation Settings")]
    public float ro;        // 엔딩 후 플레이어 축 회전값
    public float playerRo;  // 엔딩 시작 시 플레이어 회전값
    #endregion

    #region State
    private bool isTriggered;  // EndSequence가 한 번만 실행되도록 하는 플래그
    #endregion

    #region Public Methods
    /// <summary>
    /// 스테이지 클리어를 시작합니다. 한 번만 호출
    /// </summary>
    public void GameEnd()
    {
        if (isTriggered) return;
        StartCoroutine(EndSequence());
        isTriggered = true;
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// 클리어 연출 시퀀스: 플레이어 리셋 → 카메라 전환 → 대기 → 애니메이션 → UI 표시 및 보상
    /// </summary>
    private IEnumerator EndSequence()
    {
        Debug.Log("Stage Clear Start");

        PreparePlayer();
        SwapCameras();

        yield return new WaitForSeconds(2f);

        RotatePlayer();
        Player.Instance.ani.SetTrigger("Clear");

        yield return new WaitForSeconds(1f);

        DisplayGameEnd();
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// 플레이어 위치/회전 설정 및 상태 UI, 무기 숨기기
    /// </summary>
    private void PreparePlayer()
    {
        Player.Instance.transform.rotation = Quaternion.Euler(0, playerRo, 0);
        Player.Instance.isGameEnd = true;
        PlayerStateUI.SetActive(false);
        GameManager.Instance.Sword.SetActive(false);
    }

    /// <summary>
    /// 메인 카메라에서 엔딩 카메라로 전환
    /// </summary>
    private void SwapCameras()
    {
        Camera.main.gameObject.SetActive(false);
        EndCam.SetActive(true);
    }

    /// <summary>
    /// 엔딩 축 회전을 적용
    /// </summary>
    private void RotatePlayer()
    {
        Player.Instance.axis.transform.eulerAngles = new Vector3(0, ro, 0);
    }

    /// <summary>
    /// 클리어 UI를 표시하고, 보상을 지급
    /// </summary>
    private void DisplayGameEnd()
    {
        GameEndUI.SetActive(true);
        var mgr = PlayGameManger.Instance;
        mgr.Money += GameManager.Instance.coid;
        mgr.isStageClear[GameManager.Instance.StageIndex] = true;
    }
    #endregion
}
#endregion
