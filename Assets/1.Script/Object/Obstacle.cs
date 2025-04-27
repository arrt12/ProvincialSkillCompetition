using UnityEngine;

#region Class: Obstacle
/// <summary>
/// 플레이어가 충돌했을 때 트랩을 발동시키는 기본 장애물 클래스
/// </summary>
public abstract class Obstacle : MonoBehaviour
{
    #region Components
    [Header("컴포넌트")]
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
