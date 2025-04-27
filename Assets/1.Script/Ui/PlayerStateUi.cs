using UnityEngine;
using UnityEngine.UI;

#region Class: PlayerStateUi
/// <summary>
/// 플레이어의 HP, 산소, 가방 상태를 UI 이미지로 표시하는 클래스
/// </summary>
public class PlayerStateUi : MonoBehaviour
{
    #region Serialized Fields
    [Header("UI Sliders")]
    public Image hpBar;       // 플레이어 HP 표시용 이미지
    public Image oxygenBar;   // 플레이어 산소 표시용 이미지
    public Image bagBar;      // 플레이어 가방 용량 표시용 이미지
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
    /// HP 비율에 맞춰 hpBar를 업데이트
    /// </summary>
    private void UpdateHpBar()
        => hpBar.fillAmount = Player.Instance.state.Hp / Player.Instance.state.MaxHp;

    /// <summary>
    /// 산소 비율에 맞춰 oxygenBar를 업데이트
    /// </summary>
    private void UpdateOxygenBar()
        => oxygenBar.fillAmount = Player.Instance.state.Oxygen / Player.Instance.state.MaxOxygen;

    /// <summary>
    /// 가방 사용량 비율에 맞춰 bagBar를 업데이트
    /// </summary>
    private void UpdateBagBar()
        => bagBar.fillAmount = Player.Instance.state.Bag / Player.Instance.state.MaxBag;
    #endregion
}
#endregion
