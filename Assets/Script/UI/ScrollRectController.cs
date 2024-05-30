using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollRectController : MonoBehaviour, IScrollHandler
{
    public ScrollRect scrollRect;
    public RectTransform content;

    private bool isDragging = false;

    public void OnScroll(PointerEventData eventData)
    {
        if (!isDragging)
            return;
        if (scrollRect.verticalNormalizedPosition <= 0 || scrollRect.verticalNormalizedPosition >= 1)
        {
            if (scrollRect.verticalNormalizedPosition <= 0)
            {
                scrollRect.verticalNormalizedPosition = 0;
            }
            else
            {
                scrollRect.verticalNormalizedPosition = 1;
            }
            isDragging = false;
        }
    }
    public void OnDrag()
    {
        isDragging = true;
    }
}
