using System.Collections;
using UnityEngine;

#region Class: StartPuzzleStart
/// <summary>
/// ���� ���� �� �ִϸ��̼��� ����ϰ� �÷��̾� UI�� ��Ȱ��ȭ�� ��
/// ������ �ð� �� ������ Ȱ��ȭ�ϴ� �Ŵ��� Ŭ����
/// </summary>
public class StartPuzzleStart : Singleton<StartPuzzleStart>
{
    #region Serialized Fields
    [Header("Puzzle Settings")]
    public GameObject startAnimator;  // ���� ���� �ִϸ�����
    public GameObject PlayerUI;       // �÷��̾� UI
    public GameObject Puzzle;         // ���� ���� ������Ʈ
    #endregion

    #region Public Methods
    /// <summary>
    /// ���� ���� �ڷ�ƾ�� �����մϴ�.
    /// </summary>
    public void StartAnimator()
    {
        StartCoroutine(StartPuzzleCoroutine());
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// �÷��̾� ��Ʈ�� ��Ȱ��ȭ �� �ִϸ��̼� ��� �� ��� �� ���� Ȱ��ȭ ������ ó��
    /// </summary>
    private IEnumerator StartPuzzleCoroutine()
    {
        DisablePlayerControl();
        startAnimator.SetActive(true);

        yield return new WaitForSeconds(12f);

        Puzzle.SetActive(true);
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// �÷��̾� UI�� ����� �̵� �� �ȱ� �ִϸ��̼��� ��Ȱ��ȭ
    /// </summary>
    private void DisablePlayerControl()
    {
        PlayerUI.SetActive(false);
        Player.Instance.isMove = false;
        Player.Instance.ani.SetBool("isWalk", false);
    }
    #endregion
}
#endregion
