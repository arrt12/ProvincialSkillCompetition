using UnityEngine;

#region Class: ClearObject
/// <summary>
/// 퍼즐, 트랩, 상자 등 클리어 여부를 표시하는 기본 컴포넌트
/// </summary>
public class ClearObject : MonoBehaviour
{
    #region State
    [Header("Clear State")]
    public bool isClear = false;  // 클리어되었는지 여부
    #endregion
}
#endregion
