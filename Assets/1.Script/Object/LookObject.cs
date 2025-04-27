using UnityEngine;

#region Class: LookObject
/// <summary>
/// ���� ī�޶� �ٶ󺸰� X�� ȸ���� 0���� �����ϴ� ������Ʈ
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
    /// ���� ī�޶� �ٶ󺸵��� ȸ���� �����ϰ�
    /// X/Z���� ����
    /// </summary>
    private void FaceCamera()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
    #endregion
}
#endregion
