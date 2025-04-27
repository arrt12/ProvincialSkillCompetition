using UnityEngine;

#region Class: ObjectCoillder
/// <summary>
/// �÷��̾ ��ȣ�ۿ� ����(GetPut)�� �����ϰų� Ż���� ��
/// �ش� ������ ������Ʈ�� Ÿ���� Player.Instance�� ����/�����մϴ�.
/// </summary>
public class ObjectCoillder : MonoBehaviour
{
    #region Serialized Fields
    [Header("ȹ�� ������ ������ ����")]
    public GetItem item;
    #endregion

    #region Unity Callbacks
    private void OnTriggerEnter(Collider other)
    {
        // �̹� �������� ��� �ִٸ� ����
        if (Player.Instance.isGetB)
            return;

        if (other.CompareTag("GetPut"))
        {
            Player.Instance.GetItemObject = gameObject;
            Player.Instance.getItem = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �̹� �������� ��� �ִٸ� ����
        if (Player.Instance.isGetB)
            return;

        if (other.CompareTag("GetPut"))
        {
            Player.Instance.GetItemObject = null;
            Player.Instance.getItem = GetItem.None;
        }
    }
    #endregion
}
#endregion
