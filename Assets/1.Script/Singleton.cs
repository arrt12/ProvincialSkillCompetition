using UnityEngine;

#region Class: Singleton
/// <summary>
/// �̱��� ������ �����ϴ� �⺻ Ŭ����
/// </summary>
/// <typeparam name="T">MonoBehaviour�� ��ӹ޴� Ÿ��</typeparam>
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
