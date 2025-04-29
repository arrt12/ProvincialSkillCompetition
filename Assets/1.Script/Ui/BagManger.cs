using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Class: BagManger
/// <summary>
/// 플레이어 가방 슬롯을 관리하고 아이템 추가/사용 시 UI 갱신을 수행하는 클래스
/// </summary>
public class BagManger : Singleton<BagManger>
{
    #region Serialized Fields
    [Header("Bag Settings")]
    public int BagValue;                // 가방 슬롯 수
    public GameObject[] slot;           // 슬롯 오브젝트 배열

    [Header("Icon Sprites")]
    public Sprite[] Iconsprites;        // 아이템 인덱스별 스프라이트
    public Sprite bSprite;              // 비어 있지 않은 슬롯 기본 스프라이트
    public Sprite resetSprite;          // 초기화시 슬롯 비활성 스프라이트
    #endregion

    #region State
    /// <summary>
    /// 슬롯에 보관된 아이템 인덱스 목록
    /// </summary>
    public List<int> itemIndex = new();
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        ItemUpdate();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// 새로운 아이템을 가방에 추가하고 상태 및 UI를 갱신
    /// </summary>
    public void AddItem(int index)
    {
        itemIndex.Add(index);
        Player.Instance.state.Bag += 20;
        Player.Instance.CheckBag();
        ItemUpdate();
    }

    /// <summary>
    /// 가방에서 첫 번째 아이템을 사용(제거)하고 상태 및 UI를 갱신
    /// </summary>
    public void UseItem()
    {
        if (itemIndex.Count == 0) return;
        itemIndex.RemoveAt(0);
        Player.Instance.state.Bag -= 20;
        Player.Instance.CheckBag();
        ItemUpdate();
    }
    #endregion

    #region UI Update
    /// <summary>
    /// 모든 슬롯을 초기화(sprites reset)한 후, 현재 아이템 인덱스에 따라 슬롯 스프라이트를 설정
    /// </summary>
    private void ItemUpdate()
    {
        ResetSlots();

        for (int i = 0; i < BagValue && i < slot.Length; i++)
        {
            Sprite sprite = (i < itemIndex.Count && itemIndex[i] < Iconsprites.Length)
                ? Iconsprites[itemIndex[i]]
                : bSprite;

            slot[i].GetComponent<Image>().sprite = sprite;
        }
    }
  
    /// <summary>
    /// 모든 슬롯의 이미지를 resetSprite로 초기화
    /// </summary>
    private void ResetSlots()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].GetComponent<Image>().sprite = resetSprite;
        }
    }
    #endregion
}
#endregion
