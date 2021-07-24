using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private static Vector2 m_defaultPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_defaultPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData) // 요기 수정
    {
        Vector2 currPos = Input.mousePosition;
        transform.position = currPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = m_defaultPos;
    }
}