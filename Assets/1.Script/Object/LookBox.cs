using Unity.VisualScripting;
using UnityEngine;

#region Class: LookBox
/// <summary>
/// 지정된 Transform(pos)을 바라보고, X축 회전을 90도로 고정합니다.
/// </summary>
public class LookBox : Singleton<LookBox>
{
    #region Serialized Fields
    [Header("타겟 Transform")]
    public Transform pos;
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        LookAtTarget();
    }
    #endregion

    #region Methods
    /// <summary>
    /// pos를 바라본 뒤, X축을 90도로 설정하여 오브젝트가 평면을 유지하게 한다
    /// </summary>
    private void LookAtTarget()
    {
        transform.LookAt(pos);
        transform.rotation = Quaternion.Euler(90f, transform.eulerAngles.y, 0f);
    }
    #endregion
}
#endregion
