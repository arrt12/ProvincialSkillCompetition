using System.Collections;
using UnityEngine;

#region Class: ShakeCamera
/// <summary>
/// 카메라에 흔들림 효과를 적용하는 클래스
/// SetUp(time, power) 호출 시 지정된 시간 동안 흔들린 후 원위치로 복귀
/// </summary>
public class ShakeCamera : MonoBehaviour
{
    #region State
    private Vector3 originalPosition;  // 초기 카메라 위치 저장
    private float shakePower;          // 흔들림 세기
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        // 시작 시 원위치 저장
        originalPosition = transform.position;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// 흔들림 효과를 설정하고 코루틴을 시작
    /// </summary>
    /// <param name="time">흔들릴 시간(초)</param>
    /// <param name="power">흔들림 세기</param>
    public void SetUp(float time, float power)
    {
        shakePower = power;
        originalPosition = transform.position;
        StopCoroutine(nameof(Shake));
        StartCoroutine(Shake(time));
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// 지정된 시간 동안 카메라를 무작위로 이동시켜 흔들림을 연출하고,
    /// 완료 후 부드럽게 원위치로 복귀
    /// </summary>
    private IEnumerator Shake(float time)
    {
        float elapsed = 0f;

        // 흔들림 효과
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            Vector3 randomOffset = Random.insideUnitSphere * shakePower;
            transform.position = new Vector3(
                originalPosition.x + randomOffset.x,
                originalPosition.y,
                originalPosition.z + randomOffset.z
            );
            yield return null;
        }

        // 빠른 복귀: 0.1초 동안 부드럽게 원위치로 이동
        float returnDuration = 0.1f;
        float t = 0f;
        Vector3 startPos = transform.position;
        while (t < returnDuration)
        {
            t += Time.deltaTime;
            float lerpFactor = t / returnDuration;
            transform.position = Vector3.Lerp(startPos, originalPosition, lerpFactor);
            yield return null;
        }

        // 최종 원위치 설정
        transform.position = originalPosition;
    }
    #endregion
}
#endregion
