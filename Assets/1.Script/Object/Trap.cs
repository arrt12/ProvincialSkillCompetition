using UnityEngine;

#region Class: Trap
/// <summary>
/// Obstacle�� ����Ͽ� �÷��̾� �浹 �� Hit �ִϸ��̼ǰ� �������� �����ϴ� Ʈ�� Ŭ����
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
