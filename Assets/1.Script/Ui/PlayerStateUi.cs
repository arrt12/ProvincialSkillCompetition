using UnityEngine;
using UnityEngine.UI;

#region Class: PlayerStateUi
/// <summary>
/// �÷��̾��� HP, ���, ���� ���¸� UI �̹����� ǥ���ϴ� Ŭ����
/// </summary>
public class PlayerStateUi : MonoBehaviour
{
    #region Serialized Fields
    [Header("UI Sliders")]
    public Image hpBar;       // �÷��̾� HP ǥ�ÿ� �̹���
    public Image oxygenBar;   // �÷��̾� ��� ǥ�ÿ� �̹���
    public Image bagBar;      // �÷��̾� ���� �뷮 ǥ�ÿ� �̹���
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        UpdateHpBar();
        UpdateOxygenBar();
        UpdateBagBar();
    }
    #endregion

    #region UI Update Methods
    /// <summary>
    /// HP ������ ���� hpBar�� ������Ʈ
    /// </summary>
    private void UpdateHpBar()
        => hpBar.fillAmount = Player.Instance.state.Hp / Player.Instance.state.MaxHp;

    /// <summary>
    /// ��� ������ ���� oxygenBar�� ������Ʈ
    /// </summary>
    private void UpdateOxygenBar()
        => oxygenBar.fillAmount = Player.Instance.state.Oxygen / Player.Instance.state.MaxOxygen;

    /// <summary>
    /// ���� ��뷮 ������ ���� bagBar�� ������Ʈ
    /// </summary>
    private void UpdateBagBar()
        => bagBar.fillAmount = Player.Instance.state.Bag / Player.Instance.state.MaxBag;
    #endregion
}
#endregion
