using UnityEngine;

#region Class: ChestBox
/// <summary>
/// ChestBoxObject를 상속하여 상자를 열 때 랜덤으로 아이템을 획득하는 클래스
/// </summary>
public class ChestBox : ChestBoxObject
{
    #region Methods
    /// <summary>
    /// 상자를 열었을 때 호출되어, BagManger에서 랜덤 아이템을 추가한다
    /// </summary>
    protected override void Open()
    {
        int index = Random.Range(0, BagManger.Instance.Iconsprites.Length);
        BagManger.Instance.AddItem(index);
    }
    #endregion
}
#endregion
