using UnityEngine;

#region Class: EAnimator
/// <summary>
/// "GetPut" 태그를 가진 오브젝트가 트리거에 들어오거나 나갈 때 애니메이션 트리거를 설정
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
