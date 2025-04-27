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
    /// PlayGameManger�� ������ ��� �� �Ŵ����� ����Ѵ�
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
