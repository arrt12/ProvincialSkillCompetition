using UnityEngine;

#region Class: ChestBoxObject
/// <summary>
/// 상자 열림 상태와 애니메이션을 관리하는 기본 클래스
/// 실제 열림 로직은 자식 클래스에서 Open() 메서드 실핼
/// </summary>
public abstract class ChestBoxObject : MonoBehaviour
{
    #region Serialized Fields
    [Header("Chest Box Settings")]
    public bool isOpen;    // 상자 열림 여부
    public Animator ani;   // 열림 애니메이터
    #endregion

    #region Abstract Methods
    /// <summary>
    /// 상자가 열릴 때 실행될 로직을 실행
    /// </summary>
    protected abstract void Open();
    #endregion
}
#endregion
