using UnityEngine;

#region Class: EAnimator
/// <summary>
/// "GetPut" �±׸� ���� ������Ʈ�� Ʈ���ſ� �����ų� ���� �� �ִϸ��̼� Ʈ���Ÿ� ����
/// </summary>
public class EAnimator : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator ani;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GetPut"))
            ani.SetTrigger("E");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GetPut"))
            ani.SetTrigger("Exit");
    }
}
#endregion
