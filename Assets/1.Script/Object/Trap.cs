using UnityEngine;

#region Class: Trap
/// <summary>
/// Obstacle을 상속하여 플레이어 충돌 시 Hit 애니메이션과 데미지를 적용하는 트랩 클래스
/// </summary>
public class Trap : Obstacle
{
    #region Trap Logic
    protected override void TriggerTrap()
    {
        animator.SetTrigger("Hit");
        Player.Instance.Hit();
    }
    #endregion
}
#endregion
