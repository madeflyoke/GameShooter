using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Image[] images;
    private Color disabledColor = new Color(0, 0, 0, 0.15f);
    private Color enabledColor = new Color(0, 0, 0, 1f);
    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        foreach (var item in images)
            item.color = disabledColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.CallOnInput(this, true);
        foreach (var item in images)
            item.color = enabledColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var item in images)
            item.color = disabledColor;
    }
}
