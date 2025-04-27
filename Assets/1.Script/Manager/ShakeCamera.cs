using System.Collections;
using UnityEngine;

#region Class: ShakeCamera
/// <summary>
/// ī�޶� ��鸲 ȿ���� �����ϴ� Ŭ����
/// SetUp(time, power) ȣ�� �� ������ �ð� ���� ��鸰 �� ����ġ�� ����
/// </summary>
public class ShakeCamera : MonoBehaviour
{
    #region State
    private Vector3 originalPosition;  // �ʱ� ī�޶� ��ġ ����
    private float shakePower;          // ��鸲 ����
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        // ���� �� ����ġ ����
        originalPosition = transform.position;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// ��鸲 ȿ���� �����ϰ� �ڷ�ƾ�� ����
    /// </summary>
    /// <param name="time">��鸱 �ð�(��)</param>
    /// <param name="power">��鸲 ����</param>
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
    /// ������ �ð� ���� ī�޶� �������� �̵����� ��鸲�� �����ϰ�,
    /// �Ϸ� �� �ε巴�� ����ġ�� ����
    /// </summary>
    private IEnumerator Shake(float time)
    {
        float elapsed = 0f;

        // ��鸲 ȿ��
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

        // ���� ����: 0.1�� ���� �ε巴�� ����ġ�� �̵�
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

        // ���� ����ġ ����
        transform.position = originalPosition;
    }
    #endregion
}
#endregion
