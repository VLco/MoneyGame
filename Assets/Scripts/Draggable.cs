using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	public Transform parentToReturnTo = null;

	public void OnBeginDrag(PointerEventData eventDat)
	{
		parentToReturnTo = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent.parent);

		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventDat)
	{
		this.transform.SetParent(parentToReturnTo);

		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}
}