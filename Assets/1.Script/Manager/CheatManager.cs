using UnityEngine;

#region Class: CheatManager
/// <summary>
/// Ű �Է�(F1~F7)�� ���� ġƮ ��带 Ȱ��ȭ�ϰų�
/// �پ��� ����� ���(HP ȸ��, �������� �̵�, �Ͻ����� ��)�� �����ϴ� �Ŵ��� Ŭ����
/// </summary>
public class CheatManager : MonoBehaviour
{
    #region Serialized Fields
    [Header("Cheat Settings")]
    public string name;      // �� ��ȯ�� �� �̸�
    public GameObject Item;  // Ȱ��ȭ�� ������ ������Ʈ
    public Transform pos;    // �ڷ���Ʈ ��ġ
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        ProcessCheatInput();
    }
    #endregion

    #region Input Processing
    /// <summary>
    /// F1~F7 Ű �Է��� �����Ͽ� ���� ġƮ ����� ȣ��
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
    /// �÷��̾� HP�� ��Ҹ� �ִ�ġ�� ȸ���Ѵ�.
    /// </summary>
    private void HealPlayer()
    {
        if (Player.Instance == null) return;

        Player.Instance.state.Hp = Player.Instance.state.MaxHp;
        Player.Instance.state.Oxygen = Player.Instance.state.MaxOxygen;
    }

    /// <summary>
    /// ���� ġƮ ���¸� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ ��Ų��.
    /// </summary>
    private void SetCheatMode(bool isCheat)
    {
        if (PlayGameManger.Instance == null) return;
        PlayGameManger.Instance.isCheat = isCheat;
    }

    /// <summary>
    /// �� ������ ��Ų��.
    /// </summary>
    private void MoveScene()
    {
        if (SceneMove.Instance == null) return;
        SceneMove.Instance.GameSceneMove(name);
    }

    /// <summary>
    /// �������� Ŭ���� ó�� ��ƾ�� ȣ���Ѵ�.
    /// </summary>
    private void ClearStage()
    {
        if (StageClearManager.Instance == null) return;
        StageClearManager.Instance.GameEnd();
    }

    /// <summary>
    /// ������ �Ͻ����� ���·� ����� (Time.timeScale = 0).
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// ���� ���� ���¸� ���� (Time.timeScale = 1).
    /// </summary>
    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// ������ ������ ������Ʈ�� Ȱ��ȭ
    /// </summary>
    private void ActivateItem()
    {
        if (Item == null) return;
        Item.SetActive(true);
    }

    /// <summary>
    /// �÷��̾ ������ ��ġ�� �����̵�
    /// </summary>
    private void TeleportPlayer()
    {
        if (Player.Instance == null || pos == null) return;
        Player.Instance.transform.position = pos.position;
    }
    #endregion
}
#endregion
