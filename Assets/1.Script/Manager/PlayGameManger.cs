using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#region Class: PlayGameManger
/// <summary>
/// 게임 전반의 상태(돈, 산소, 가방 등)와 복귀 매니저를 관리하는 클래스
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
    /// 싱글톤 초기화 후 파괴 방지 설정을 적용합니다.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 매 프레임 게임 시간을 갱신합니다.
    /// </summary>
    private void Update() => UpdateGameTime();
    #endregion

    #region Public Methods
    /// <summary>
    /// 새로운 ReturyManager를 등록한다.
    /// </summary>
    public void SetReturyManager(ReturyManager returyManager) => returyManagers.Add(returyManager);

    /// <summary>
    /// 등록된 ReturyManager 리스트를 초기화
    /// </summary>
    public void ReturyManagerReset()
    {
        returyManagers = new List<ReturyManager>();
    }

    /// <summary>
    /// 이미 클리어된 스테이지의 ReturyManager 오브젝트들을 비활성화
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
    /// 게임이 진행 중일 때만 time을 증가
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
