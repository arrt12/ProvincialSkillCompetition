using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Class: BagManger
/// <summary>
/// �÷��̾� ���� ������ �����ϰ� ������ �߰�/��� �� UI ������ �����ϴ� Ŭ����
/// </summary>
public class BagManger : Singleton<BagManger>
{
    #region Serialized Fields
    [Header("Bag Settings")]
    public int BagValue;                // ���� ���� ��
    public GameObject[] slot;           // ���� ������Ʈ �迭

    [Header("Icon Sprites")]
    public Sprite[] Iconsprites;        // ������ �ε����� ��������Ʈ
    public Sprite bSprite;              // ��� ���� ���� ���� �⺻ ��������Ʈ
    public Sprite resetSprite;          // �ʱ�ȭ�� ���� ��Ȱ�� ��������Ʈ
    #endregion

    #region State
    /// <summary>
    /// ���Կ� ������ ������ �ε��� ���
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
    /// ���ο� �������� ���濡 �߰��ϰ� ���� �� UI�� ����
    /// </summary>
    public void AddItem(int index)
    {
        itemIndex.Add(index);
        Player.Instance.state.Bag += 20;
        Player.Instance.CheckBag();
        ItemUpdate();
    }

    /// <summary>
    /// ���濡�� ù ��° �������� ���(����)�ϰ� ���� �� UI�� ����
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
    /// ��� ������ �ʱ�ȭ(sprites reset)�� ��, ���� ������ �ε����� ���� ���� ��������Ʈ�� ����
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
    /// ��� ������ �̹����� resetSprite�� �ʱ�ȭ
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
