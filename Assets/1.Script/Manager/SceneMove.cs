using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Class: SceneMove
/// <summary>
/// �� ��ȯ �� ���ƿ� UI�� ǥ���ϰ�,
/// ���� ���¸� ������ �� ������ ���� �ε��ϴ� �Ŵ��� Ŭ����
/// </summary>
public class SceneMove : Singleton<SceneMove>
{
    #region Serialized Fields
    [Header("Black Out UI")]
    public GameObject BlackOutUI;
    #endregion

    #region Public Methods
    /// <summary>
    /// �� ��ȯ�� ���� �ڷ�ƾ�� ����
    /// </summary>
    public void GameSceneMove(string sceneName)
        => StartCoroutine(BlackOutCoroutine(sceneName));
    #endregion

    #region Coroutines
    /// <summary>
    /// ���ƿ� UI ǥ�� �� ��� �� ���� ���� ���� �� �� �ε� ������ ó��
    /// </summary>
    private IEnumerator BlackOutCoroutine(string sceneName)
    {
        ShowBlackOutUI();
        yield return new WaitForSeconds(1.1f);
        ResetGameState();
        LoadScene(sceneName);
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// ���ƿ� UI�� Ȱ��ȭ�մϴ�.
    /// </summary>
    private void ShowBlackOutUI()
        => BlackOutUI.SetActive(true);

    /// <summary>
    /// ReturyManager�� �ʱ�ȭ�ϰ� ���� ���� �÷��׸� ����
    /// </summary>
    private void ResetGameState()
    {
        PlayGameManger.Instance.ReturyManagerReset();
        PlayGameManger.Instance.isGame = false;
    }

    /// <summary>
    /// ������ ���� �ε�
    /// </summary>
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}
#endregion
