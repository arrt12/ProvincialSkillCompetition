using UnityEngine;

#region Class: BigChest
/// <summary>
/// ChestBoxObject�� ����Ͽ� ū ���ڸ� �� �� ���ΰ� ����Ʈ�� �����ϰ� �ڽ��� �����ϴ� Ŭ����
/// </summary>
public class BigChest : ChestBoxObject
{
    #region Serialized Fields
    [Header("Big Chest Settings")]
    public GameObject effect;    // ���� ���� �� ������ ����Ʈ
    public GameObject Coin;      // ���� ���� �� ������ ���� ������
    #endregion

    #region Unity Callbacks
    /// <summary>
    /// ���� �� ItemEffectManager�� �ڽ��� ����Ͽ� ��ġ ���� ����� Ȱ��ȭ
    /// </summary>
    private void Start()
    {
        ItemEffectManager.Instance.SetBigChests(this);
    }
    #endregion

    #region Open Logic
    /// <summary>
    /// ���ڸ� ������ �� ȣ��Ǿ�, ���ΰ� ����Ʈ�� �����ϰ� ���� ������Ʈ�� �ı��Ѵ�
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
