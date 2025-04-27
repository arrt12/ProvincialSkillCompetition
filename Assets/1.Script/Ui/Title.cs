using System;
using UnityEngine;
using UnityEngine.UI;

#region Class: Title
/// <summary>
/// 타이틀 화면에서 스테이지 버튼 활성화, 상점 구매, 랭킹 입력 처리를 담당하는 클래스
/// </summary>
public class Title : MonoBehaviour
{
    #region Serialized Fields
    [Header("Stage Buttons")]
    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject Stage4;
    public GameObject Stage5;

    [Header("Shop Buttons")]
    public Button oxygenBuy;
    public Button oxygenBuy2;
    public Button oxygenBuy3;
    public Button bagBuy;
    public Button bagBuy2;

    [Header("UI Elements")]
    public Text coin;
    public GameObject rankingInput;
    public InputField nameText;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        // 산소 구매 옵션 설정
        oxygenBuy.onClick.AddListener(() =>
            GameBuy(300f, oxygenBuy.gameObject, () => OxygenBuy(200f))
        );
        oxygenBuy2.onClick.AddListener(() =>
            GameBuy(400f, oxygenBuy2.gameObject, () => OxygenBuy(300f))
        );
        oxygenBuy3.onClick.AddListener(() =>
            GameBuy(500f, oxygenBuy3.gameObject, () => OxygenBuy(400f))
        );

        // 가방 구매 옵션 설정
        bagBuy.onClick.AddListener(() =>
            GameBuy(300f, bagBuy.gameObject, () => BugBuy(250f, 6f))
        );
        bagBuy2.onClick.AddListener(() =>
            GameBuy(400f, bagBuy2.gameObject, () => BugBuy(400f, 8f))
        );

        // 기존 랭킹 데이터 로드
        Ranking.Instance.LoadData();
    }

    private void Update()
    {
        // 스테이지 클리어 상태에 따라 버튼 활성화
        var mgr = PlayGameManger.Instance;
        Stage2.SetActive(mgr.isStageClear[0]);
        Stage3.SetActive(mgr.isStageClear[1]);
        Stage4.SetActive(mgr.isStageClear[2]);
        Stage5.SetActive(mgr.isStageClear[3]);

        // 마지막 스테이지 클리어 후 랭킹 입력 UI 활성화
        rankingInput.SetActive(mgr.isStageClear[4]);

        // 현재 보유 코인 표시
        coin.text = mgr.Money.ToString();
    }
    #endregion

    #region Shop & Purchases
    /// <summary>
    /// 지정된 비용을 지불하고 구매 액션을 실행
    /// 치트 모드일 경우 비용 차감 없이 실행
    /// </summary>
    public void GameBuy(float cost, GameObject buttonObj, Action onSuccess)
    {
        var mgr = PlayGameManger.Instance;
        if (mgr.Money >= cost || mgr.isCheat)
        {
            if (!mgr.isCheat)
                mgr.Money -= cost;

            onSuccess?.Invoke();
            buttonObj.SetActive(false);
        }
    }

    /// <summary>
    /// 산소 최대치를 설정
    /// </summary>
    public void OxygenBuy(float value)
    {
        PlayGameManger.Instance.oxygen = value;
    }

    /// <summary>
    /// 가방 용량과 가방 슬롯 값을 설정
    /// </summary>
    public void BugBuy(float value, float slot)
    {
        PlayGameManger.Instance.bag = value;
        PlayGameManger.Instance.bagValue = slot;
    }
    #endregion

    #region Ranking Input
    /// <summary>
    /// 플레이어 이름과 시간을 랭킹 시스템에 저장하고 UI를 업데이트
    /// </summary>
    public void RankingInput()
    {
        var ranking = Ranking.Instance;
        var mgr = PlayGameManger.Instance;

        ranking.playerName = nameText.text;
        ranking.playerTime = mgr.time;
        ranking.SaveData();
    }
    #endregion
}
#endregion
