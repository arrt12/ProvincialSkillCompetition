using UnityEngine;

#region Class: Obstacle
/// <summary>
/// �÷��̾ �浹���� �� Ʈ���� �ߵ���Ű�� �⺻ ��ֹ� Ŭ����
/// </summary>
public abstract class Obstacle : MonoBehaviour
{
    #region Components
    [Header("������Ʈ")]
    public Animator animator;
    #endregion

    #region Unity Callbacks
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (Player.Instance.isGhost)
            return;

        TriggerTrap();
    }
    #endregion

    #region Abstract Methods
    protected abstract void TriggerTrap();
    #endregion
}
#endregion
