using UnityEngine;

#region Class: ChestBox
/// <summary>
/// ChestBoxObject�� ����Ͽ� ���ڸ� �� �� �������� �������� ȹ���ϴ� Ŭ����
/// </summary>
public class ChestBox : ChestBoxObject
{
    #region Methods
    /// <summary>
    /// ���ڸ� ������ �� ȣ��Ǿ�, BagManger���� ���� �������� �߰��Ѵ�
    /// </summary>
    protected override void Open()
    {
        int index = Random.Range(0, BagManger.Instance.Iconsprites.Length);
        BagManger.Instance.AddItem(index);
    }
    #endregion
}
#endregion
