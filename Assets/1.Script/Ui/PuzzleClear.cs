using System;
using UnityEngine;

#region Class: PuzzleClear
/// <summary>
/// �迭�� ��ϵ� ��� ���� ������ Ŭ����Ǹ�
/// �ִϸ����Ϳ� ���� ������Ʈ�� ��Ȱ��ȭ�ϰ�
/// Ŭ���� ���� ��� �� �������� Ŭ��� ȣ��
/// </summary>
public class PuzzleClear : MonoBehaviour
{
    #region Serialized Fields
    [Header("Puzzle Pieces to Check")]
    public Puzzle[] puzzles;           // �˻��� ���� ���� �迭

    [Header("Animator & Puzzle Container")]
    public GameObject StartAnimator;   // ���� �ִϸ����� ������Ʈ
    public GameObject puzzle;          // ���� ��ü �����̳�
    #endregion

    #region State
    private bool isTriggered;          // �� ���� ����ǵ��� �÷���
    #endregion

    #region Unity Callbacks
    private void Update() => CheckClear(ToggleClearState);
    #endregion

    #region Clear Logic
    /// <summary>
    /// ��� ���� ������ Ŭ����Ǿ����� �˻��� ��, 
    /// Ŭ����Ǿ����� ������ �׼��� �����մϴ�.
    /// </summary>
    private void CheckClear(Action action)
    {
        if (IsClear())
            action.Invoke();
    }

    /// <summary>
    /// �迭 �� ��� ���� ������ isClear �÷��װ� true���� Ȯ��
    /// </summary>
    private bool IsClear()
    {
        foreach (var piece in puzzles)
        {
            if (!piece.isClear)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Ŭ���� ���¸� ����Ͽ� ����Ʈ, UI ����, �������� Ŭ��� ����
    /// </summary>
    private void ToggleClearState()
    {
        if (isTriggered) return;

        StartAnimator.SetActive(false);
        puzzle.SetActive(false);

        GameManager.Instance.clearAudio.Play();
        StageClearManager.Instance.GameEnd();

        isTriggered = true;
    }
    #endregion
}
#endregion
