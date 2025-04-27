using UnityEngine;

#region Player Input Handler
public class PlayerInputHandler
{
    #region Properties
    public Vector2 MoveAxis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    public bool IsAttackPressed => Input.GetKeyDown(KeyCode.Mouse0);
    public bool IsUseItemPressed => Input.GetKeyDown(KeyCode.Q);
    public bool IsInteractPressed => Input.GetKeyDown(KeyCode.E);
    #endregion
}
#endregion
