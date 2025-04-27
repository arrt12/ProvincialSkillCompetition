using System.Collections;
using UnityEngine;

#region Class: TrapMonster
/// <summary>
/// 플레이어가 충돌 시 일정 시간 후 리셋 효과를 주는 트랩이다
/// Obstacle을 상속하여 트리거 시 코루틴을 실행한다
/// </summary>
public class TrapMonster : Obstacle
{
    #region Trigger
    /// <summary>
    /// Obstacle 충돌 시 호출됩니다. 플레이어 체크 코루틴을 시작한다
    /// </summary>
    protected override void TriggerTrap()
    {
        StartCoroutine(CheckPlayer());
    }
    #endregion

    #region Coroutine
    /// <summary>
    /// 히트 이펙트 재생 후 일정 시간 대기하고 플레이어를 리셋한다
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
    /// 플레이어 체력 감소 후 위치를 리셋하고 이동을 재활성화한다
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
