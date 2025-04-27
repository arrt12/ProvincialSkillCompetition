using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#region Class: Puzzle
/// <summary>
/// 퍼즐 조각 드래그 앤 드롭 처리를 담당하는 클래스
/// 지정된 퍼즐 위치(puzzlePos)로 충분히 근접하면 자동으로 스냅되고 클리어 상태로 설정된다
/// </summary>
public class Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Components & State
    [Header("퍼즐 조각 설정")]
    public GameObject puzzlePos;           // 스냅될 위치
    private Image image;                   // 자체 이미지 (Raycast 제어용)
    private RectTransform rectTransform;   // 드래그 시 위치 조정
    private Canvas canvas;                 // 상위 캔버스
    public bool isClear;                   // 클리어 여부
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
    /// 드래그 시작 시 퍼즐을 캔버스 최상위로 이동한다
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
    }

    /// <summary>
    /// 드래그 중에는 델타만큼 이동시키고, 클리어 상태를 해제한다
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null)
            return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        isClear = false;
    }

    /// <summary>
    /// 드래그 종료 시 스냅 거리가 30 이내이면 위치 고정 및 클리어 처리한다
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
    /// 자식 Image 컴포넌트의 RaycastTarget을 제어하여
    /// 현재 조각만 드래그 가능하도록 설정한다
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
