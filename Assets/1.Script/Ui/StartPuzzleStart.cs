using System.Collections;
using UnityEngine;

#region Class: StartPuzzleStart
/// <summary>
/// 퍼즐 시작 시 애니메이션을 재생하고 플레이어 UI를 비활성화한 뒤
/// 지정된 시간 후 퍼즐을 활성화하는 매니저 클래스
/// </summary>
public class StartPuzzleStart : Singleton<StartPuzzleStart>
{
    #region Serialized Fields
    [Header("Puzzle Settings")]
    public GameObject startAnimator;  // 퍼즐 시작 애니메이터
    public GameObject PlayerUI;       // 플레이어 UI
    public GameObject Puzzle;         // 실제 퍼즐 오브젝트
    #endregion

    #region Public Methods
    /// <summary>
    /// 퍼즐 시작 코루틴을 실행합니다.
    /// </summary>
    public void StartAnimator()
    {
        StartCoroutine(StartPuzzleCoroutine());
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// 플레이어 컨트롤 비활성화 → 애니메이션 재생 → 대기 → 퍼즐 활성화 순으로 처리
    /// </summary>
    private IEnumerator StartPuzzleCoroutine()
    {
        DisablePlayerControl();
        startAnimator.SetActive(true);

        yield return new WaitForSeconds(12f);

        Puzzle.SetActive(true);
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// 플레이어 UI를 숨기고 이동 및 걷기 애니메이션을 비활성화
    /// </summary>
    private void DisablePlayerControl()
    {
        PlayerUI.SetActive(false);
        Player.Instance.isMove = false;
        Player.Instance.ani.SetBool("isWalk", false);
    }
    #endregion
}
#endregion
