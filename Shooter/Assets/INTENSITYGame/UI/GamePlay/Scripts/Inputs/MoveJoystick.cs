using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MoveJoystick : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform frontCircle;
    private RectTransform rtBackCircle;
    public Vector3 direct { get; private set; }   
    private Canvas canvas;
    private Vector2 backCircleCenterPoint, circleCenterPoint;
    private Image frontImage, backImage;
    
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        circleCenterPoint = frontCircle.anchoredPosition;
        backCircleCenterPoint = transform.position;
        rtBackCircle = GetComponent<RectTransform>();
        Cursor.lockState = CursorLockMode.None;

        frontImage = frontCircle.GetComponent<Image>();
        backImage = GetComponent<Image>();
        frontImage.color = new Color(0, 0, 0, 0.15f);
        backImage.color = new Color(0, 0, 0, 0.15f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = (eventData.position - backCircleCenterPoint) / canvas.scaleFactor;
        frontCircle.anchoredPosition = circleCenterPoint + Vector2.ClampMagnitude(direction, rtBackCircle.sizeDelta.x / 2);
        direct = direction / (rtBackCircle.sizeDelta.x / 2);
        if (direct.magnitude>1)
            direct = direct.normalized;
        EventManager.CallOnInput(this, true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EventManager.CallOnInput(this, false);
        frontCircle.anchoredPosition = circleCenterPoint;
        direct = Vector2.zero;
        frontImage.color = new Color(0, 0, 0, 0.15f);
        backImage.color = new Color(0, 0, 0, 0.15f);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        frontImage.color = new Color(0, 0, 0, 1f);
        backImage.color = new Color(0, 0, 0, 1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        frontImage.color = new Color(0, 0, 0, 0.15f);
        backImage.color = new Color(0, 0, 0, 0.15f);
    }
}

