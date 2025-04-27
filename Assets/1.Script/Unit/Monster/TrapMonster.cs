using System.Collections;
using UnityEngine;

#region Class: TrapMonster
/// <summary>
/// �÷��̾ �浹 �� ���� �ð� �� ���� ȿ���� �ִ� Ʈ���̴�
/// Obstacle�� ����Ͽ� Ʈ���� �� �ڷ�ƾ�� �����Ѵ�
/// </summary>
public class TrapMonster : Obstacle
{
    #region Trigger
    /// <summary>
    /// Obstacle �浹 �� ȣ��˴ϴ�. �÷��̾� üũ �ڷ�ƾ�� �����Ѵ�
    /// </summary>
    protected override void TriggerTrap()
    {
        StartCoroutine(CheckPlayer());
    }
    #endregion

    #region Coroutine
    /// <summary>
    /// ��Ʈ ����Ʈ ��� �� ���� �ð� ����ϰ� �÷��̾ �����Ѵ�
    /// </summary>
    private IEnumerator CheckPlayer()
    {
        PlayHitEffect();
        yield return new WaitForSeconds(1.3f);
        ResetPlayer();
    }
    #endregion

    #region Helpers
    private void PlayHitEffect()
    {
        animator.SetTrigger("CheckPlayer");
        GameManager.Instance.hitAudio.Play();
        Camera.main.GetComponent<ShakeCamera>().SetUp(1.3f, 0.01f);
        Player.Instance.isMove = false;
        GameManager.Instance.BlackOut.SetTrigger("BlackOut");
    }

    /// <summary>
    /// �÷��̾� ü�� ���� �� ��ġ�� �����ϰ� �̵��� ��Ȱ��ȭ�Ѵ�
    /// </summary>
    private void ResetPlayer()
    {
        Player.Instance.state.Hp -= 20;
        Player.Instance.transform.position = GameManager.Instance.resetPos.position;
        Player.Instance.isMove = true;
    }
    #endregion
}
#endregion
