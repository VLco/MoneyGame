using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropProducts : MonoBehaviour, IDropHandler
{
	List<GameObject> _resultProductsPrefabs;
	[SerializeField] GameObject shopsScreen;
	GameObject currentProduct;  // теткущий перетаскиваемый продукт
	[SerializeField] GameObject productsArea; // область выгрузки продуктов
	[SerializeField] GameObject _emoticonImg;
    public int emoticonPers = 0;
    void Start()
	{
		_resultProductsPrefabs = shopsScreen.GetComponent<ShopsController>().resultProductsPrefabs; // получение списка префабов продуктов
	}
	public void OnDrop(PointerEventData eventData)
	{
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null)
		{
			d.parentToReturnTo = this.transform;
			// Перемещаемый товар
			currentProduct = eventData.pointerDrag.gameObject;
			// Проверка области, в которую попал товар, если это корзина...
			if (d.parentToReturnTo.CompareTag("basket"))
			{
                emoticonPers = 2;
				_emoticonImg.GetComponent<EmoticonManager>().SetHappyEmoticon();
				/* Добавление экземпляра товара в область productsArea 
				вместо того объекта, который переместили в корзину */
				foreach (GameObject product in _resultProductsPrefabs)
				{

					if (product.name == currentProduct.name)
					{
						GameObject addedProduct = Instantiate(currentProduct) as GameObject;
						addedProduct.transform.position = productsArea.transform.position;
						addedProduct.GetComponent<RectTransform>().SetParent(productsArea.transform);
						addedProduct.GetComponent<CanvasGroup>().blocksRaycasts = true;
						addedProduct.name = currentProduct.name;
						break;
					}
				}
			}
			else
			{
				Button[] children = productsArea.GetComponentsInChildren<Button>(); /* получение всех потомков области,
				в которую перемещаем */

				foreach (var element in children)   /* выбор среди потомков созданных копий нужного */
				{
					if (element.name == currentProduct.name && element.GetComponentInChildren<Text>().text == currentProduct.GetComponentInChildren<Text>().text)
					{
						Destroy(element.gameObject);    // удаление потомка
					}
				}
			}
		}
	}

}
