using System;
using UnityEngine;

#region Class: PuzzleClear
/// <summary>
/// 배열에 등록된 모든 퍼즐 조각이 클리어되면
/// 애니메이터와 퍼즐 오브젝트를 비활성화하고
/// 클리어 사운드 재생 및 스테이지 클리어를 호출
/// </summary>
public class PuzzleClear : MonoBehaviour
{
    #region Serialized Fields
    [Header("Puzzle Pieces to Check")]
    public Puzzle[] puzzles;           // 검사할 퍼즐 조각 배열

    [Header("Animator & Puzzle Container")]
    public GameObject StartAnimator;   // 시작 애니메이터 오브젝트
    public GameObject puzzle;          // 퍼즐 전체 컨테이너
    #endregion

    #region State
    private bool isTriggered;          // 한 번만 실행되도록 플래그
    #endregion

    #region Unity Callbacks
    private void Update() => CheckClear(ToggleClearState);
    #endregion

    #region Clear Logic
    /// <summary>
    /// 모든 퍼즐 조각이 클리어되었는지 검사한 뒤, 
    /// 클리어되었으면 지정된 액션을 실행합니다.
    /// </summary>
    private void CheckClear(Action action)
    {
        if (IsClear())
            action.Invoke();
    }

    /// <summary>
    /// 배열 내 모든 퍼즐 조각의 isClear 플래그가 true인지 확인
    /// </summary>
    private bool IsClear()
    {
        foreach (var piece in puzzles)
        {
            if (!piece.isClear)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 클리어 상태를 토글하여 이펙트, UI 변경, 스테이지 클리어를 실행
    /// </summary>
    private void ToggleClearState()
    {
        if (isTriggered) return;

        StartAnimator.SetActive(false);
        puzzle.SetActive(false);

        GameManager.Instance.clearAudio.Play();
        StageClearManager.Instance.GameEnd();

        isTriggered = true;
    }
    #endregion
}
#endregion
