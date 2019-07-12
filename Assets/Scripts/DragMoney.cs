using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMoney : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Transform parentToReturnTo = null;
	Transform startPosition = null; // стартовая область (для монет своя, для купюр своя)
	Transform start = null; // начальная позиция, откуда начали перемещение
	[SerializeField] GameObject _wallet;

	// void Start()
	// {
	// 	_wallet = GameObject.Find("Bag");
	// }

	public void OnBeginDrag(PointerEventData eventDat)
	{
		parentToReturnTo = this.transform.parent;
		start = parentToReturnTo;
		startPosition = this.GetComponent<Money>().startPosition.transform;
		this.transform.SetParent(this.transform.parent.parent);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventDat)
	{
		_wallet = GameObject.Find("Bag");
		Transform walletPosition = _wallet.transform;

		/* Проверка, если элемент лежал в кошельке, но не попал в стартовую облатсть,
		то не оставляем там, где лежал */
		if ((start == walletPosition && parentToReturnTo != startPosition))
		{
			parentToReturnTo = start;
		}
		this.transform.SetParent(parentToReturnTo);

		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}
}
