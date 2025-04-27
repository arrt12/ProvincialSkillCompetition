using UnityEngine;

#region Class: GameManager
/// <summary>
/// 게임 전반의 핵심 기능(리스폰 위치, 오디오, UI 전환 등)을 관리하는 클래스
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region Serialized Fields
    [Header("Reset & UI")]
    public Transform resetPos;         // 플레이어 리셋 위치
    public Animator BlackOut;          // 화면 전환용 블랙아웃 애니메이터

    [Header("Weapons & Effects")]
    public GameObject Sword;           // 플레이어 무기 게임 오브젝트

    [Header("Stage Settings")]
    public int StageIndex;             // 현재 스테이지 인덱스

    [Header("Counters")]
    public float coid;                 // 획득한 코인 수
    public float oxygen;               // 기본 산소 감소량
    #endregion

    #region Audio Sources
    [Header("Audio Sources")]
    public AudioSource StartAudio;     // 타이틀 시작 오디오
    public AudioSource GameStartAudio; // 게임 시작 오디오
    public AudioSource clearAudio;     // 스테이지 클리어 사운드
    public AudioSource attackAudio;    // 공격 사운드
    public AudioSource OpenAudio;      // 상자 오픈 사운드
    public AudioSource hitAudio;       // 피격 사운드
    public AudioSource boxOpen;        // 큰 상자 오픈 사운드
    #endregion

    #region Unity Callbacks
    /// <summary>
    /// 게임 시작 시 PlayGameManger의 복귀 매니저 오브젝트를 비활성화
    /// </summary>
    private void Start()
    {
        if (PlayGameManger.Instance == null)
            return;

        PlayGameManger.Instance.DestroyObjects();
    }
    #endregion
}
#endregion
