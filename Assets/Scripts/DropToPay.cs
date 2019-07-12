using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DropToPay : MonoBehaviour, IDropHandler
{
    
    Vector3 newPosition;    // позиция только что положенных денег, которые совпали по номиналу
	public GameObject _emoticonImg;

	// void OnAwake()
	// {
	// 	_emoticonImg = GameObject.Find("EmoticonImg");
	// 	_emoticonImg.SetActive(false);
	// }

	// Перенос денег при оплате покупки
	public void OnDrop(PointerEventData eventData)
	{
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null)
		{
			d.parentToReturnTo = this.transform;
			if (d.parentToReturnTo.GetComponent<Money>().value == d.gameObject.GetComponent<Money>().value && newPosition != d.parentToReturnTo.gameObject.transform.position)
			{
				eventData.pointerDrag.gameObject.transform.position = d.parentToReturnTo.gameObject.transform.position;
				newPosition = d.parentToReturnTo.gameObject.transform.position;
				d.enabled = false;  // отключение возможности перетаскивать текущий элемент 
				d.gameObject.GetComponent<Image>().color = new Color(79 / 255.0f, 165 / 255.0f, 63 / 255.0f);

               // emoticonPers = 2;
				_emoticonImg.GetComponent<EmoticonManager>().SetHappyEmoticon();
			}
			else
			{
				d.parentToReturnTo = GameObject.FindGameObjectWithTag("walletMoneyArea").transform;
				_emoticonImg.GetComponent<EmoticonManager>().SetSadEmoticon();
			}
		}
	}
}
