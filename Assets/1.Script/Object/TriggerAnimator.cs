using System.Collections;
using UnityEngine;

#region Class: TriggerAnimator
/// <summary>
/// 벽이나 상자 벽을 움직이는 애니메이터 클래스
/// WallMove()와 BoxWallMove() 호출 시 흔들림, 사운드와 위치 보간을 통해 이동 효과를 연출
/// </summary>
public class TriggerAnimator : MonoBehaviour
{
    #region Serialized Fields
    [Header("Wall & Box Settings")]
    public bool isBoxTigger;     // 상자 벽 리셋 플래그
    public Vector3 pos;          // 이동 목표 위치
    public ClearObject box;      // BoxWall 리셋 시 사용할 ClearObject
    #endregion

    #region State
    private Vector3 resetPos;    // 원위치 저장
    #endregion

    #region Public Methods
    /// <summary>
    /// 일반 벽 이동 코루틴을 시작
    /// </summary>
    public void WallMove()
        => StartCoroutine(MoveWall());

    /// <summary>
    /// 박스 벽 이동 및 복귀 코루틴을 시작
    /// </summary>
    public void BoxWallMove()
        => StartCoroutine(MoveBoxWall());
    #endregion

    #region Coroutines
    /// <summary>
    /// Shake + 사운드 재생 후 벽을 pos 위치로 선형 보간 이동
    /// </summary>
    private IEnumerator MoveWall()
    {
        PlayWallAnimation();

        Vector3 startPos = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, pos, t);
            yield return null;
        }

        transform.position = pos;
    }

    /// <summary>
    /// Shake + 사운드 재생 후 박스 벽을 pos로 이동, 대기, 다시 원위치로 복귀시키는 코루틴
    /// </summary>
    private IEnumerator MoveBoxWall()
    {
        PlayWallAnimation();

        resetPos = transform.position;
        Vector3 startPos = resetPos;
        float t = 0f;

        // 이동
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, pos, t);
            yield return null;
        }

        transform.position = pos;
        yield return new WaitForSeconds(5f);

        // 복귀
        PlayWallAnimation();

        startPos = transform.position;
        float c = 0f;
        while (c < 1f)
        {
            c += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, resetPos, c);
            yield return null;
        }

        transform.position = resetPos;
        yield return new WaitForSeconds(1f);

        box.isClear = false;
        isBoxTigger = false;
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// 카메라 흔들림, 사운드 재생 후 짧은 대기 코루틴을 실행
    /// </summary>
    private void PlayWallAnimation()
    {
        if (Camera.main.GetComponent<ShakeCamera>() is ShakeCamera shake)
            shake.SetUp(1.2f, 0.02f);

        GameManager.Instance.OpenAudio.Play();
        StartCoroutine(WaitForAudio());
    }

    /// <summary>
    /// 사운드 재생 후 0.2초 대기
    /// </summary>
    private IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(0.2f);
    }
    #endregion
}
#endregion
