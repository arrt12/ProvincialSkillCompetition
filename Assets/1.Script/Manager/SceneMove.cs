using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Class: SceneMove
/// <summary>
/// 씬 전환 시 블랙아웃 UI를 표시하고,
/// 게임 상태를 리셋한 뒤 지정된 씬을 로드하는 매니저 클래스
/// </summary>
public class SceneMove : Singleton<SceneMove>
{
    #region Serialized Fields
    [Header("Black Out UI")]
    public GameObject BlackOutUI;
    #endregion

    #region Public Methods
    /// <summary>
    /// 씬 전환을 위한 코루틴을 시작
    /// </summary>
    public void GameSceneMove(string sceneName)
        => StartCoroutine(BlackOutCoroutine(sceneName));
    #endregion

    #region Coroutines
    /// <summary>
    /// 블랙아웃 UI 표시 → 대기 → 게임 상태 리셋 → 씬 로드 순으로 처리
    /// </summary>
    private IEnumerator BlackOutCoroutine(string sceneName)
    {
        ShowBlackOutUI();
        yield return new WaitForSeconds(1.1f);
        ResetGameState();
        LoadScene(sceneName);
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// 블랙아웃 UI를 활성화합니다.
    /// </summary>
    private void ShowBlackOutUI()
        => BlackOutUI.SetActive(true);

    /// <summary>
    /// ReturyManager를 초기화하고 게임 진행 플래그를 해제
    /// </summary>
    private void ResetGameState()
    {
        PlayGameManger.Instance.ReturyManagerReset();
        PlayGameManger.Instance.isGame = false;
    }

    /// <summary>
    /// 지정된 씬을 로드
    /// </summary>
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}
#endregion
