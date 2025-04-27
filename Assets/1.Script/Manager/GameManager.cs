using UnityEngine;

#region Class: GameManager
/// <summary>
/// ���� ������ �ٽ� ���(������ ��ġ, �����, UI ��ȯ ��)�� �����ϴ� Ŭ����
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region Serialized Fields
    [Header("Reset & UI")]
    public Transform resetPos;         // �÷��̾� ���� ��ġ
    public Animator BlackOut;          // ȭ�� ��ȯ�� ���ƿ� �ִϸ�����

    [Header("Weapons & Effects")]
    public GameObject Sword;           // �÷��̾� ���� ���� ������Ʈ

    [Header("Stage Settings")]
    public int StageIndex;             // ���� �������� �ε���

    [Header("Counters")]
    public float coid;                 // ȹ���� ���� ��
    public float oxygen;               // �⺻ ��� ���ҷ�
    #endregion

    #region Audio Sources
    [Header("Audio Sources")]
    public AudioSource StartAudio;     // Ÿ��Ʋ ���� �����
    public AudioSource GameStartAudio; // ���� ���� �����
    public AudioSource clearAudio;     // �������� Ŭ���� ����
    public AudioSource attackAudio;    // ���� ����
    public AudioSource OpenAudio;      // ���� ���� ����
    public AudioSource hitAudio;       // �ǰ� ����
    public AudioSource boxOpen;        // ū ���� ���� ����
    #endregion

    #region Unity Callbacks
    /// <summary>
    /// ���� ���� �� PlayGameManger�� ���� �Ŵ��� ������Ʈ�� ��Ȱ��ȭ
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
