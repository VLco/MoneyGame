using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMoney : MonoBehaviour, IDropHandler
{
	[SerializeField] GameObject _startPosition;
	[SerializeField] GameObject _emoticonImg;
    GameObject currentElement;
	GameObject addedElement;
	static int counter; // число денег в кошельке
    public int funPers = 0;

	void Start()
	{
		counter = 0;
	}

	public void OnDrop(PointerEventData eventData)
	{
		DragMoney d = eventData.pointerDrag.GetComponent<DragMoney>();
		currentElement = eventData.pointerDrag.gameObject;


		if (d != null && counter < 10)
		{
			d.parentToReturnTo = this.transform;

			if (d.parentToReturnTo.CompareTag("wallet"))    // если положили в кошелек ...
			{
				counter++;
                funPers = 1;//Анимация персонажа - радость
                _emoticonImg.GetComponent<EmoticonManager>().SetHappyEmoticon();

                foreach (var item in CurrencyController.moneyPrefabs) // создание такого же элемента
				{
					// Кэширование значений
					Money component = item.GetComponent<Money>();
					_startPosition = component.startPosition;

					if (component.value == currentElement.GetComponent<Money>().value)
					{
						addedElement = Instantiate(item) as GameObject;
						addedElement.transform.position = _startPosition.transform.position;
						addedElement.GetComponent<RectTransform>().SetParent(_startPosition.transform);
						addedElement.GetComponent<CanvasGroup>().blocksRaycasts = true;
						addedElement.name = "copy";
						break;
					}
				}
			}
			else /* переместили деньги обратно из кошелька */
			{
				Transform[] children = gameObject.GetComponentsInChildren<Transform>(); /* получение всех потомков области,
				в которую перемещаем */
                foreach (var element in children)   /* выбор среди потомков созданных копий нужного номинала */
				{
					if (element.name == "copy" && element.GetComponent<Money>().value == currentElement.GetComponent<Money>().value)
					{
						Destroy(element.gameObject);    // удаление потомка
                       
                    }
				}
			}
		}
	}
}
