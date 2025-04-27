using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#region Class: PlayGameManger
/// <summary>
/// ���� ������ ����(��, ���, ���� ��)�� ���� �Ŵ����� �����ϴ� Ŭ����
/// </summary>
public class PlayGameManger : Singleton<PlayGameManger>
{
    #region Serialized Fields
    [Header("Game Player State")]
    public float Money = 0f;
    public float oxygen;
    public float bag;
    public float bagValue;

    [Header("Game State")]
    public float time;
    public bool isCheat;
    public bool isGame;

    [Header("Stage Clearance Flags")]
    public bool[] isStageClear;

    [Header("Return Managers")]
    public List<ReturyManager> returyManagers = new();
    #endregion

    #region Unity Callbacks
    /// <summary>
    /// �̱��� �ʱ�ȭ �� �ı� ���� ������ �����մϴ�.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// �� ������ ���� �ð��� �����մϴ�.
    /// </summary>
    private void Update() => UpdateGameTime();
    #endregion

    #region Public Methods
    /// <summary>
    /// ���ο� ReturyManager�� ����Ѵ�.
    /// </summary>
    public void SetReturyManager(ReturyManager returyManager) => returyManagers.Add(returyManager);

    /// <summary>
    /// ��ϵ� ReturyManager ����Ʈ�� �ʱ�ȭ
    /// </summary>
    public void ReturyManagerReset()
    {
        returyManagers = new List<ReturyManager>();
    }

    /// <summary>
    /// �̹� Ŭ����� ���������� ReturyManager ������Ʈ���� ��Ȱ��ȭ
    /// </summary>
    public void DestroyObjects()
    {
        int idx = GameManager.Instance.StageIndex;
        if (!isStageClear[idx])
            return;

        foreach (var mgr in returyManagers.ToList())
            mgr.gameObject.SetActive(false);
    }
    #endregion

    #region Helpers
    /// <summary>
    /// ������ ���� ���� ���� time�� ����
    /// </summary>
    private void UpdateGameTime()
    {
        if (!isGame)
            return;

        time += Time.deltaTime;
    }
    #endregion
}
#endregion
