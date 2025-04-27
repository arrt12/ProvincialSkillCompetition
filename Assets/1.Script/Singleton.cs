using UnityEngine;

#region Class: Singleton
/// <summary>
/// 싱글톤 패턴을 적용하는 기본 클래스
/// </summary>
/// <typeparam name="T">MonoBehaviour를 상속받는 타입</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton Instance")]
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this as T;
        else if (Instance != this)
            Destroy(gameObject);
    }
}
#endregion
