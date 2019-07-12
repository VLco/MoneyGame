using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropPicrograms : MonoBehaviour, IDropHandler
{
	public string value;
	GameObject addedProduct;
	[SerializeField] GameObject _pictogramsArea;

	public void OnDrop(PointerEventData eventData)
	{
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null)
		{
			d.parentToReturnTo = this.transform;
			value = d.name;

			if (d.parentToReturnTo.CompareTag("auth"))
			{
				/* Добавление пиктограмы в область pictogramsArea 
				вместо того объекта, который переместили в поле логина/пароля */
				foreach (GameObject pictogram in Authorization.pictogramsPrefabs)
				{

					if (pictogram.name == value)
					{
						GameObject place = GameObject.Find(value + "place");

						addedProduct = Instantiate(d.gameObject) as GameObject;
						addedProduct.transform.position = place.transform.position;
						addedProduct.GetComponent<RectTransform>().SetParent(place.transform);
						addedProduct.GetComponent<CanvasGroup>().blocksRaycasts = true;
						addedProduct.name = value;
						break;
					}
				}
			}
			else
			{
				Image[] children = _pictogramsArea.GetComponentsInChildren<Image>(); /* получение всех потомков области,
				в которую перемещаем */
				foreach (var element in children)   /* выбор среди потомков созданных копий нужного */
				{
					if (element.name == value)
					{
						Destroy(element.gameObject);    // удаление потомка
					}
				}
			}
		}
	}
}
