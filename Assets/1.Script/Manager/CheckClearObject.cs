using UnityEngine;

#region Enum: ClearObjects
/// <summary>
/// Ʈ������ ������Ʈ ������ ����
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
/// ������ ClearObject �迭�� �˻��Ͽ� ��� Ŭ���� �� �ش� Ʈ���� �ִϸ��̼��� ����
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
    /// clearObjects Ÿ�Կ� ���� Ŭ���� üũ �޼��带 �б��մϴ�.
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
    /// ClearFire Ÿ���� �� �� ��ü�� ��� Ŭ����Ǿ��� �� Ʈ���Ÿ� �����մϴ�.
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
    /// Box Ÿ���� ��ü�� Ŭ����Ǿ��� �� Ʈ���Ÿ� ����
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
