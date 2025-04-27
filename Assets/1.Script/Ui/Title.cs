using System;
using UnityEngine;
using UnityEngine.UI;

#region Class: Title
/// <summary>
/// Ÿ��Ʋ ȭ�鿡�� �������� ��ư Ȱ��ȭ, ���� ����, ��ŷ �Է� ó���� ����ϴ� Ŭ����
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
        // ��� ���� �ɼ� ����
        oxygenBuy.onClick.AddListener(() =>
            GameBuy(300f, oxygenBuy.gameObject, () => OxygenBuy(200f))
        );
        oxygenBuy2.onClick.AddListener(() =>
            GameBuy(400f, oxygenBuy2.gameObject, () => OxygenBuy(300f))
        );
        oxygenBuy3.onClick.AddListener(() =>
            GameBuy(500f, oxygenBuy3.gameObject, () => OxygenBuy(400f))
        );

        // ���� ���� �ɼ� ����
        bagBuy.onClick.AddListener(() =>
            GameBuy(300f, bagBuy.gameObject, () => BugBuy(250f, 6f))
        );
        bagBuy2.onClick.AddListener(() =>
            GameBuy(400f, bagBuy2.gameObject, () => BugBuy(400f, 8f))
        );

        // ���� ��ŷ ������ �ε�
        Ranking.Instance.LoadData();
    }

    private void Update()
    {
        // �������� Ŭ���� ���¿� ���� ��ư Ȱ��ȭ
        var mgr = PlayGameManger.Instance;
        Stage2.SetActive(mgr.isStageClear[0]);
        Stage3.SetActive(mgr.isStageClear[1]);
        Stage4.SetActive(mgr.isStageClear[2]);
        Stage5.SetActive(mgr.isStageClear[3]);

        // ������ �������� Ŭ���� �� ��ŷ �Է� UI Ȱ��ȭ
        rankingInput.SetActive(mgr.isStageClear[4]);

        // ���� ���� ���� ǥ��
        coin.text = mgr.Money.ToString();
    }
    #endregion

    #region Shop & Purchases
    /// <summary>
    /// ������ ����� �����ϰ� ���� �׼��� ����
    /// ġƮ ����� ��� ��� ���� ���� ����
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
    /// ��� �ִ�ġ�� ����
    /// </summary>
    public void OxygenBuy(float value)
    {
        PlayGameManger.Instance.oxygen = value;
    }

    /// <summary>
    /// ���� �뷮�� ���� ���� ���� ����
    /// </summary>
    public void BugBuy(float value, float slot)
    {
        PlayGameManger.Instance.bag = value;
        PlayGameManger.Instance.bagValue = slot;
    }
    #endregion

    #region Ranking Input
    /// <summary>
    /// �÷��̾� �̸��� �ð��� ��ŷ �ý��ۿ� �����ϰ� UI�� ������Ʈ
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
