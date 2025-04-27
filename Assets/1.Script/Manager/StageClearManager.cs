using System.Collections;
using UnityEngine;

#region Class: StageClearManager
/// <summary>
/// �������� Ŭ���� �� ����(ī�޶� ��ȯ, UI ���� ��) �� ���� ������ �����ϴ� Ŭ����
/// </summary>
public class StageClearManager : Singleton<StageClearManager>
{
    #region Serialized Fields
    [Header("UI & Camera")]
    public GameObject GameEndUI;       // �������� Ŭ���� UI
    public GameObject PlayerStateUI;   // �÷��̾� ���� UI
    public GameObject EndCam;          // ���� ī�޶�
    #endregion

    #region Settings
    [Header("Player Rotation Settings")]
    public float ro;        // ���� �� �÷��̾� �� ȸ����
    public float playerRo;  // ���� ���� �� �÷��̾� ȸ����
    #endregion

    #region State
    private bool isTriggered;  // EndSequence�� �� ���� ����ǵ��� �ϴ� �÷���
    #endregion

    #region Public Methods
    /// <summary>
    /// �������� Ŭ��� �����մϴ�. �� ���� ȣ��
    /// </summary>
    public void GameEnd()
    {
        if (isTriggered) return;
        StartCoroutine(EndSequence());
        isTriggered = true;
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// Ŭ���� ���� ������: �÷��̾� ���� �� ī�޶� ��ȯ �� ��� �� �ִϸ��̼� �� UI ǥ�� �� ����
    /// </summary>
    private IEnumerator EndSequence()
    {
        Debug.Log("Stage Clear Start");

        PreparePlayer();
        SwapCameras();

        yield return new WaitForSeconds(2f);

        RotatePlayer();
        Player.Instance.ani.SetTrigger("Clear");

        yield return new WaitForSeconds(1f);

        DisplayGameEnd();
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// �÷��̾� ��ġ/ȸ�� ���� �� ���� UI, ���� �����
    /// </summary>
    private void PreparePlayer()
    {
        Player.Instance.transform.rotation = Quaternion.Euler(0, playerRo, 0);
        Player.Instance.isGameEnd = true;
        PlayerStateUI.SetActive(false);
        GameManager.Instance.Sword.SetActive(false);
    }

    /// <summary>
    /// ���� ī�޶󿡼� ���� ī�޶�� ��ȯ
    /// </summary>
    private void SwapCameras()
    {
        Camera.main.gameObject.SetActive(false);
        EndCam.SetActive(true);
    }

    /// <summary>
    /// ���� �� ȸ���� ����
    /// </summary>
    private void RotatePlayer()
    {
        Player.Instance.axis.transform.eulerAngles = new Vector3(0, ro, 0);
    }

    /// <summary>
    /// Ŭ���� UI�� ǥ���ϰ�, ������ ����
    /// </summary>
    private void DisplayGameEnd()
    {
        GameEndUI.SetActive(true);
        var mgr = PlayGameManger.Instance;
        mgr.Money += GameManager.Instance.coid;
        mgr.isStageClear[GameManager.Instance.StageIndex] = true;
    }
    #endregion
}
#endregion
