using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Mathematics;
using Unity.VisualScripting;

public class BasketsMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("basketMovementLine")]
    [SerializeField, Range(25.0f, 100.0f)] private float _edgeOffset = 30.0f;
    [SerializeField] private float _lineHeight = -1.0f;
    [SerializeField] private float _moveSpeed = 0.05f;

    [Space(10)]
    [Header("Components")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;
    [SerializeField] private Canvas canvas;

    private Vector2 cursorPosition;
    private Vector2 targetPosition;
    private float radius;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        SetStartPositionBasket();
        radius = MeasureCanvasRadiusByWidth() - _edgeOffset;
    }

    private void SetStartPositionBasket()
    {
        targetPosition = new Vector2(0.0f, _lineHeight);
    }

    private float MeasureCanvasRadiusByWidth()
    {
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float canvasRadius = canvasWidth / 2.0f;

        return canvasRadius;
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, _moveSpeed);
    }

    public void OnDrag(PointerEventData eventData)
    {
        cursorPosition += eventData.delta;
        GetCursorPositionOnLine(cursorPosition);
    }

    private void GetCursorPositionOnLine(Vector2 cursor)
    {
        float clampPosition = MovementConstraints();
        targetPosition = new Vector2(clampPosition, _lineHeight);
    }

    private float MovementConstraints()
    {
        return Mathf.Clamp(cursorPosition.x, -radius, radius);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
    }

    #region VisualizationOfDebugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        float width = MeasureCanvasRadiusByWidth() - _edgeOffset;
        Gizmos.DrawLine(GetStartLine(width), GetEndLine(width));
    }

    private Vector2 GetStartLine(float width)
    {
        return canvas.transform.TransformPoint(new Vector2(-width, _lineHeight));
    }

    private Vector2 GetEndLine(float width)
    {
        return canvas.transform.TransformPoint(new Vector2(width, _lineHeight));
    }
    #endregion
}
