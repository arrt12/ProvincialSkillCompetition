using UnityEngine;

#region Class: ReturyManager
public class ReturyManager : MonoBehaviour
{
    #region Unity Callbacks
    private void Awake()
    {
        Initialize();
    }
    #endregion

    #region Initialization
    /// <summary>
    /// PlayGameManger가 존재할 경우 이 매니저를 등록한다
    /// </summary>
    private void Initialize()
    {
        if (PlayGameManger.Instance == null)
            return;

        PlayGameManger.Instance.SetReturyManager(this);
    }
    #endregion
}
#endregion
