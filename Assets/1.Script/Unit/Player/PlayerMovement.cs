using UnityEngine;

#region Player Movement Class
public class PlayerMovement
{
    #region Fields
    private readonly Transform playerTransform;
    private readonly Transform axisTransform;
    private readonly Animator animator;
    private readonly Rigidbody rigidbody;
    private readonly float rotationSpeed;
    private float moveSpeed;
    private Vector3 moveDirection;
    #endregion

    #region Properties
    public Vector3 MoveDirection => moveDirection;
    #endregion

    #region Constructor
    public PlayerMovement(Transform playerTransform, Transform axisTransform, Animator animator, Rigidbody rigidbody, float moveSpeed, float rotationSpeed)
    {
        this.playerTransform = playerTransform;
        this.axisTransform = axisTransform;
        this.animator = animator;
        this.rigidbody = rigidbody;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }
    #endregion

    #region Public Methods
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void Move(Vector2 inputAxis)
    {
        moveDirection = new Vector3(inputAxis.y, 0, -inputAxis.x).normalized;
        Vector3 movePos = playerTransform.position + (moveDirection * moveSpeed);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            axisTransform.rotation = Quaternion.RotateTowards(axisTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        rigidbody.MovePosition(movePos);

        animator.SetBool("isWalk", moveDirection != Vector3.zero);
    }
    #endregion
}
#endregion
