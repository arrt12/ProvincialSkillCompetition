using UnityEngine;

#region Class: StartManager
/// <summary>
/// ���� �ִϸ��̼� ���� �� �÷��̾� UI Ȱ��ȭ, ī�޶� �̵� Ȱ��ȭ�ϰ�
/// ���� ���� �÷��� ����, ����� ��ȯ, �ִϸ����� �� ��ũ��Ʈ ��Ȱ��ȭ
/// </summary>
public class StartManager : MonoBehaviour
{
    #region Serialized Fields
    [Header("UI Elements")]
    public GameObject PlayerUI;  // �÷��̾� UI ������Ʈ
    #endregion

    #region Public Methods
    /// <summary>
    /// ���� �ִϸ����Ͱ� ���� �� ȣ���ϰ�
    /// �Ϸ��� �ʱ�ȭ ��ƾ�� ���������� ����
    /// </summary>
    public void EndStartAnimator()
    {
        EnablePlayerUI();
        EnableCameraMove();
        StartGame();
        SwapAudio();
        DisableAnimator();
        DisableThisScript();
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// �÷��̾� UI�� Ȱ��ȭ�Ѵ�.
    /// </summary>
    private void EnablePlayerUI() => PlayerUI.SetActive(true);

    /// <summary>
    /// CamMove ������Ʈ�� Ȱ��ȭ�Ͽ� ī�޶� �̵��� ���
    /// </summary>
    private void EnableCameraMove() => GetComponent<CamMove>().enabled = true;

    /// <summary>
    /// �÷��̾� ���� �÷��׿� ���� ���� �÷��׸� ����
    /// </summary>
    private void StartGame()
    {
        Player.Instance.isStart = true;
        PlayGameManger.Instance.isGame = true;
    }

    /// <summary>
    /// Ÿ��Ʋ ������� �����ϰ� ���� ���� ������� ���
    /// </summary>
    private void SwapAudio()
    {
        GameManager.Instance.StartAudio.Stop();
        GameManager.Instance.GameStartAudio.Play();
    }

    /// <summary>
    /// �ڽ��� Animator ������Ʈ�� ��Ȱ��ȭ�մϴ�.
    /// </summary>
    private void DisableAnimator() => GetComponent<Animator>().enabled = false;

    /// <summary>
    /// �� ��ũ��Ʈ�� ��Ȱ��ȭ�Ͽ� �� �̻� ȣ����� �ʵ��� �Ѵ�
    /// </summary>
    private void DisableThisScript() => enabled = false;
    #endregion
}
#endregion
