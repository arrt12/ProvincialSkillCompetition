using Unity.VisualScripting;
using UnityEngine;

#region Class: LookBox
/// <summary>
/// ������ Transform(pos)�� �ٶ󺸰�, X�� ȸ���� 90���� �����մϴ�.
/// </summary>
public class LookBox : Singleton<LookBox>
{
    #region Serialized Fields
    [Header("Ÿ�� Transform")]
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
    /// pos�� �ٶ� ��, X���� 90���� �����Ͽ� ������Ʈ�� ����� �����ϰ� �Ѵ�
    /// </summary>
    private void LookAtTarget()
    {
        transform.LookAt(pos);
        transform.rotation = Quaternion.Euler(90f, transform.eulerAngles.y, 0f);
    }
    #endregion
}
#endregion
