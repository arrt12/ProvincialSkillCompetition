using System.Collections;
using UnityEngine;

#region Class: TriggerAnimator
/// <summary>
/// ���̳� ���� ���� �����̴� �ִϸ����� Ŭ����
/// WallMove()�� BoxWallMove() ȣ�� �� ��鸲, ����� ��ġ ������ ���� �̵� ȿ���� ����
/// </summary>
public class TriggerAnimator : MonoBehaviour
{
    #region Serialized Fields
    [Header("Wall & Box Settings")]
    public bool isBoxTigger;     // ���� �� ���� �÷���
    public Vector3 pos;          // �̵� ��ǥ ��ġ
    public ClearObject box;      // BoxWall ���� �� ����� ClearObject
    #endregion

    #region State
    private Vector3 resetPos;    // ����ġ ����
    #endregion

    #region Public Methods
    /// <summary>
    /// �Ϲ� �� �̵� �ڷ�ƾ�� ����
    /// </summary>
    public void WallMove()
        => StartCoroutine(MoveWall());

    /// <summary>
    /// �ڽ� �� �̵� �� ���� �ڷ�ƾ�� ����
    /// </summary>
    public void BoxWallMove()
        => StartCoroutine(MoveBoxWall());
    #endregion

    #region Coroutines
    /// <summary>
    /// Shake + ���� ��� �� ���� pos ��ġ�� ���� ���� �̵�
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
    /// Shake + ���� ��� �� �ڽ� ���� pos�� �̵�, ���, �ٽ� ����ġ�� ���ͽ�Ű�� �ڷ�ƾ
    /// </summary>
    private IEnumerator MoveBoxWall()
    {
        PlayWallAnimation();

        resetPos = transform.position;
        Vector3 startPos = resetPos;
        float t = 0f;

        // �̵�
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, pos, t);
            yield return null;
        }

        transform.position = pos;
        yield return new WaitForSeconds(5f);

        // ����
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
    /// ī�޶� ��鸲, ���� ��� �� ª�� ��� �ڷ�ƾ�� ����
    /// </summary>
    private void PlayWallAnimation()
    {
        if (Camera.main.GetComponent<ShakeCamera>() is ShakeCamera shake)
            shake.SetUp(1.2f, 0.02f);

        GameManager.Instance.OpenAudio.Play();
        StartCoroutine(WaitForAudio());
    }

    /// <summary>
    /// ���� ��� �� 0.2�� ���
    /// </summary>
    private IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(0.2f);
    }
    #endregion
}
#endregion
