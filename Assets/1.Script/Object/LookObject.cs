using UnityEngine;

#region Class: LookObject
/// <summary>
/// 메인 카메라를 바라보고 X축 회전을 0도로 고정하는 컴포넌트
/// </summary>
public class LookObject : MonoBehaviour
{
    #region Unity Callbacks
    private void Update()
    {
        if (Camera.main == null)
            return;

        FaceCamera();
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// 메인 카메라를 바라보도록 회전을 설정하고
    /// X/Z축은 고정
    /// </summary>
    private void FaceCamera()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
    #endregion
}
#endregion
