using UnityEngine;

#region Enum: ClearObjects
/// <summary>
/// 트리거할 오브젝트 종류를 정의
/// </summary>
public enum ClearObjects
{
    none,
    ClearFire,
    Box
}
#endregion

#region Class: CheckClearObject
/// <summary>
/// 지정된 ClearObject 배열을 검사하여 모두 클리어 시 해당 트리거 애니메이션을 실행
/// </summary>
public class CheckClearObject : MonoBehaviour
{
    #region Serialized Fields
    [Header("Clear Object Type")]
    [SerializeField] public ClearObjects clearObjects;

    [Header("Clear Objects Array")]
    public ClearObject[] isClearObjects;

    [Header("Trigger Animator")]
    public TriggerAnimator triggerAnimator;
    #endregion

    #region State
    private bool isTriggered;
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        EvaluateClearState();
    }
    #endregion

    #region Clear Logic
    /// <summary>
    /// clearObjects 타입에 따라 클리어 체크 메서드를 분기합니다.
    /// </summary>
    private void EvaluateClearState()
    {
        switch (clearObjects)
        {
            case ClearObjects.ClearFire:
                EvaluateFireClear();
                break;
            case ClearObjects.Box:
                EvaluateBoxClear();
                break;
        }
    }

    /// <summary>
    /// ClearFire 타입의 두 개 객체가 모두 클리어되었을 때 트리거를 실행합니다.
    /// </summary>
    private void EvaluateFireClear()
    {
        if (isClearObjects.Length < 2) return;

        if (isClearObjects[0].isClear && isClearObjects[1].isClear && !isTriggered)
        {
            triggerAnimator.WallMove();
            isTriggered = true;
        }
    }

    /// <summary>
    /// Box 타입의 객체가 클리어되었을 때 트리거를 실행
    /// </summary>
    private void EvaluateBoxClear()
    {
        if (isClearObjects.Length < 1) return;

        if (isClearObjects[0].isClear && !triggerAnimator.isBoxTigger)
        {
            triggerAnimator.BoxWallMove();
            triggerAnimator.isBoxTigger = true;
        }
    }
    #endregion
}
#endregion
