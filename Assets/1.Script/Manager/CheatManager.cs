using UnityEngine;

#region Class: CheatManager
/// <summary>
/// 키 입력(F1~F7)에 따라 치트 모드를 활성화하거나
/// 다양한 디버그 기능(HP 회복, 스테이지 이동, 일시정지 등)을 수행하는 매니저 클래스
/// </summary>
public class CheatManager : MonoBehaviour
{
    #region Serialized Fields
    [Header("Cheat Settings")]
    public string name;      // 씬 전환용 씬 이름
    public GameObject Item;  // 활성화할 아이템 오브젝트
    public Transform pos;    // 텔레포트 위치
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        ProcessCheatInput();
    }
    #endregion

    #region Input Processing
    /// <summary>
    /// F1~F7 키 입력을 감지하여 각종 치트 기능을 호출
    /// </summary>
    private void ProcessCheatInput()
    {
        if (Input.GetKeyDown(KeyCode.F1)) HealPlayer();
        if (Input.GetKeyDown(KeyCode.F2)) SetCheatMode(true);
        if (Input.GetKeyUp(KeyCode.F2)) SetCheatMode(false);
        if (Input.GetKeyDown(KeyCode.F3)) MoveScene();
        if (Input.GetKeyDown(KeyCode.F4)) ClearStage();
        if (Input.GetKeyDown(KeyCode.F5)) PauseGame();
        if (Input.GetKeyUp(KeyCode.F5)) ResumeGame();
        if (Input.GetKeyDown(KeyCode.F6)) ActivateItem();
        if (Input.GetKeyDown(KeyCode.F7)) TeleportPlayer();
    }
    #endregion

    #region Cheat Actions
    /// <summary>
    /// 플레이어 HP와 산소를 최대치로 회복한다.
    /// </summary>
    private void HealPlayer()
    {
        if (Player.Instance == null) return;

        Player.Instance.state.Hp = Player.Instance.state.MaxHp;
        Player.Instance.state.Oxygen = Player.Instance.state.MaxOxygen;
    }

    /// <summary>
    /// 상점 치트 상태를 활성화 또는 비활성화 시킨다.
    /// </summary>
    private void SetCheatMode(bool isCheat)
    {
        if (PlayGameManger.Instance == null) return;
        PlayGameManger.Instance.isCheat = isCheat;
    }

    /// <summary>
    /// 씬 리셋을 시킨다.
    /// </summary>
    private void MoveScene()
    {
        if (SceneMove.Instance == null) return;
        SceneMove.Instance.GameSceneMove(name);
    }

    /// <summary>
    /// 스테이지 클리어 처리 루틴을 호출한다.
    /// </summary>
    private void ClearStage()
    {
        if (StageClearManager.Instance == null) return;
        StageClearManager.Instance.GameEnd();
    }

    /// <summary>
    /// 게임을 일시정지 상태로 만든다 (Time.timeScale = 0).
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 게임 정지 상태를 해제 (Time.timeScale = 1).
    /// </summary>
    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 지정된 아이템 오브젝트를 활성화
    /// </summary>
    private void ActivateItem()
    {
        if (Item == null) return;
        Item.SetActive(true);
    }

    /// <summary>
    /// 플레이어를 지정된 위치로 순간이동
    /// </summary>
    private void TeleportPlayer()
    {
        if (Player.Instance == null || pos == null) return;
        Player.Instance.transform.position = pos.position;
    }
    #endregion
}
#endregion
