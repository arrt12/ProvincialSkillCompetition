using UnityEngine;

#region Class: ChestBoxObject
/// <summary>
/// ���� ���� ���¿� �ִϸ��̼��� �����ϴ� �⺻ Ŭ����
/// ���� ���� ������ �ڽ� Ŭ�������� Open() �޼��� ����
/// </summary>
public abstract class ChestBoxObject : MonoBehaviour
{
    #region Serialized Fields
    [Header("Chest Box Settings")]
    public bool isOpen;    // ���� ���� ����
    public Animator ani;   // ���� �ִϸ�����
    #endregion

    #region Abstract Methods
    /// <summary>
    /// ���ڰ� ���� �� ����� ������ ����
    /// </summary>
    protected abstract void Open();
    #endregion
}
#endregion
