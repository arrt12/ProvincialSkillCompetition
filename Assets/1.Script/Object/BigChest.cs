using UnityEngine;

#region Class: BigChest
/// <summary>
/// ChestBoxObject를 상속하여 큰 상자를 열 때 코인과 이펙트를 생성하고 자신을 제거하는 클래스
/// </summary>
public class BigChest : ChestBoxObject
{
    #region Serialized Fields
    [Header("Big Chest Settings")]
    public GameObject effect;    // 상자 오픈 시 생성할 이펙트
    public GameObject Coin;      // 상자 오픈 시 생성할 코인 프리팹
    #endregion

    #region Unity Callbacks
    /// <summary>
    /// 시작 시 ItemEffectManager에 자신을 등록하여 위치 지정 기능을 활성화
    /// </summary>
    private void Start()
    {
        ItemEffectManager.Instance.SetBigChests(this);
    }
    #endregion

    #region Open Logic
    /// <summary>
    /// 상자를 열었을 때 호출되어, 코인과 이펙트를 생성하고 상자 오브젝트를 파괴한다
    /// </summary>
    protected override void Open()
    {
        Instantiate(Coin, transform.position, Quaternion.identity);
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    #endregion
}
#endregion
