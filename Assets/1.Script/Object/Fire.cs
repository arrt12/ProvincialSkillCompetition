using UnityEngine;

#region Class: Fire
/// <summary>
/// 화염 오브젝트를 관리하는 클래스
/// isfire가 true일 때 fire GameObject를 활성화하여 불효과를 표시
/// </summary>
public class Fire : MonoBehaviour
{
    #region Serialized Fields
    [Header("Fire Object")]
    public GameObject fire;  // 불 효과로 사용할 GameObject
    #endregion

    #region State
    /// <summary>
    /// 현재 화염 상태 여부
    /// </summary>
    public bool isfire;
    #endregion
}
#endregion
