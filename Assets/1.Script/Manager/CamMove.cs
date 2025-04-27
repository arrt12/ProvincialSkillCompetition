using UnityEngine;

#region Class: CamMove
/// <summary>
/// �÷��̾ ���󰡴� ī�޶� �̵��� ó��
/// </summary>
public class CamMove : MonoBehaviour
{
    [Header("Camera Settings")]
    public Vector3 offset;
    public float speed = 0.1f;

    private void FixedUpdate()
    {
        if (Player.Instance == null)
            return;

        Vector3 targetPosition = Player.Instance.transform.position + transform.InverseTransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }
}
#endregion
