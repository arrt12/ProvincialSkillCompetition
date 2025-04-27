using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#region Class: Puzzle
/// <summary>
/// ���� ���� �巡�� �� ��� ó���� ����ϴ� Ŭ����
/// ������ ���� ��ġ(puzzlePos)�� ����� �����ϸ� �ڵ����� �����ǰ� Ŭ���� ���·� �����ȴ�
/// </summary>
public class Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Components & State
    [Header("���� ���� ����")]
    public GameObject puzzlePos;           // ������ ��ġ
    private Image image;                   // ��ü �̹��� (Raycast �����)
    private RectTransform rectTransform;   // �巡�� �� ��ġ ����
    private Canvas canvas;                 // ���� ĵ����
    public bool isClear;                   // Ŭ���� ����
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start() => SetRayCastTarget(true);
    #endregion

    #region Drag Handlers
    /// <summary>
    /// �巡�� ���� �� ������ ĵ���� �ֻ����� �̵��Ѵ�
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
    }

    /// <summary>
    /// �巡�� �߿��� ��Ÿ��ŭ �̵���Ű��, Ŭ���� ���¸� �����Ѵ�
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null)
            return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        isClear = false;
    }

    /// <summary>
    /// �巡�� ���� �� ���� �Ÿ��� 30 �̳��̸� ��ġ ���� �� Ŭ���� ó���Ѵ�
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector2.Distance(puzzlePos.transform.position, transform.position) < 30f)
        {
            transform.SetParent(puzzlePos.transform);
            transform.localPosition = Vector3.zero;
            isClear = true;
        }
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// �ڽ� Image ������Ʈ�� RaycastTarget�� �����Ͽ�
    /// ���� ������ �巡�� �����ϵ��� �����Ѵ�
    /// </summary>
    public void SetRayCastTarget(bool isActive)
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (var img in images)
        {
            img.raycastTarget = (img == image) ? isActive : !isActive;
        }
    }
    #endregion
}
#endregion
