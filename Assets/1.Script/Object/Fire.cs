using UnityEngine;

#region Class: Fire
/// <summary>
/// ȭ�� ������Ʈ�� �����ϴ� Ŭ����
/// isfire�� true�� �� fire GameObject�� Ȱ��ȭ�Ͽ� ��ȿ���� ǥ��
/// </summary>
public class Fire : MonoBehaviour
{
    #region Serialized Fields
    [Header("Fire Object")]
    public GameObject fire;  // �� ȿ���� ����� GameObject
    #endregion

    #region State
    /// <summary>
    /// ���� ȭ�� ���� ����
    /// </summary>
    public bool isfire;
    #endregion
}
#endregion
