using UnityEngine;

#region Class: ObjectCoillder
/// <summary>
/// 플레이어가 상호작용 영역(GetPut)에 진입하거나 탈출할 때
/// 해당 아이템 오브젝트와 타입을 Player.Instance에 설정/해제합니다.
/// </summary>
public class ObjectCoillder : MonoBehaviour
{
    #region Serialized Fields
    [Header("획득 가능한 아이템 종류")]
    public GetItem item;
    #endregion

    #region Unity Callbacks
    private void OnTriggerEnter(Collider other)
    {
        // 이미 아이템을 들고 있다면 무시
        if (Player.Instance.isGetB)
            return;

        if (other.CompareTag("GetPut"))
        {
            Player.Instance.GetItemObject = gameObject;
            Player.Instance.getItem = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 이미 아이템을 들고 있다면 무시
        if (Player.Instance.isGetB)
            return;

        if (other.CompareTag("GetPut"))
        {
            Player.Instance.GetItemObject = null;
            Player.Instance.getItem = GetItem.None;
        }
    }
    #endregion
}
#endregion
