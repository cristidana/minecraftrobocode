using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour,
    IDragHandler,
    IEndDragHandler,
    IPointerDownHandler,
    IPointerUpHandler

{
    public ItemData itemData;

    public void OnPointerDown(PointerEventData data) { }
    public void OnPointerUp(PointerEventData data) { }
    public void OnDrag(PointerEventData data) { }
    public void OnEndDrag(PointerEventData data) { }

}
