using UnityEngine;

#region Unit Base Class
public abstract class Unit : MonoBehaviour
{
    #region Fields
    public Rigidbody rb;
    public float speed;
    #endregion

    #region Abstract Methods
    protected abstract void Attack();
    #endregion

    #region Protected Methods
    protected virtual void Move(Vector3 movePos)
    {
        rb.MovePosition(movePos);
    }

    protected virtual void Rotate(Vector3 lookDirection, float rotateSpeed, Transform axis)
    {
        if (lookDirection == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        axis.rotation = Quaternion.RotateTowards(axis.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
    #endregion
}
#endregion
