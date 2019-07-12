using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GetMoneyCombination : MonoBehaviour
{
    [SerializeField] GameObject _content; // content in listscreen
    List<float> _listMoney;
	static float _buySum;
	GameObject manager;
	GameObject wallet;
	List<float> bestComb;
	static float sum = 0f;
	Transform parentCombArea;
	Transform parentWalletArea;
	public float sumChange;
	static float summInWallet;  // сумма денег в кошельке пользователя

    public int emotionPers = 0;
    string sumChangeStr;
	[SerializeField] GameObject _emoticonImg;

	[SerializeField] GameObject _basket;    // корзина с экрана выбора товаров
	List <Button> products;  // товары, которые лежат в корзине
	[SerializeField] Text summText;

	void OnEnable()
	{
		bestComb = new List<float>();
		sumChangeStr = "";

		GetValueFromScript();   // получаем переменные
		CountProductsInBasket();

		summInWallet = 0;
		foreach (var item in _listMoney)
		{
			summInWallet += item;
		}

		if (summInWallet < _buySum) // если не хватает денег в кошельке
		{
			Debug.Log("Error");

		}
		else
		{
			manager.GetComponent<ManagerController>().countEnterCashbox++;  // +1 вход
			_listMoney.Sort();
			Combination comb = new Combination(_buySum);
			comb.MakeAllSets(_listMoney);
			bestComb = comb.GetBestSet();   // получаем комбинацию из денег для оплаты

			if (manager.GetComponent<ManagerController>().countEnterCashbox <= 1)
			{
				SetWalletMoney();
			}
			else
			{
				for (int i = 0; i < parentCombArea.childCount; i++)
				{
					Destroy(parentCombArea.GetChild(i).gameObject);
				}
			}
			SetMonCombination();
		}
	}

	/* Подсчет суммы покупки */
	void CountProductsInBasket()
	{
		_buySum = 0;
		List <Button> noSelectProducts = _content.GetComponent<ListScript>().GetFinalButtonList(); //list no select product
        products = new List<Button>(_basket.GetComponentsInChildren<Button>());
        foreach(var prod in noSelectProducts)
        {
            products.Remove(prod);
        }
		string price;
		Match match;
		Regex regex = new Regex(@"\d+(\.\d+)?");

		// Считываем строку с ценой и остаавляем только число с помощью регулярки
		foreach (var item in products)
		{
			price = item.GetComponentInChildren<Text>().text;   // строка с ценой
			match = regex.Match(price);
			_buySum += float.Parse(match.Value);
		}

		// Выгружаем сумму покупки на экран
		summText.text = _buySum.ToString() + " " + Authorization.currencyList.currency;
	}

	/* Подсчет сдачи от покупки */
	public void GetSumChange()
	{
		sum = 0;

		foreach (float item in bestComb)
		{
			sum += item;    // сумма денег в найденной комбинации
		}
		sumChange = sum - _buySum;  // сдача
	}

	/* Выгрузка подобранной комбинации денег */
	public void SetMonCombination()
	{
		parentCombArea = GameObject.FindGameObjectWithTag("moneyCombArea").transform;
		int len = CurrencyController.moneyPrefabs.Count;
		GameObject obj;

		if (bestComb != null)
		{   // выгружаем комбинации
			foreach (float item in bestComb)
			{
				for (int i = 0; i < len; i++)
				{
					if (CurrencyController.moneyPrefabs[i].GetComponent<Money>().value == item)
					{
						obj = Instantiate(CurrencyController.moneyPrefabs[i], CurrencyController.moneyPrefabs[i].transform.position, Quaternion.identity) as GameObject;
						obj.GetComponent<DragMoney>().enabled = false;
						obj.AddComponent<DropToPay>();
						obj.GetComponent<DropToPay>()._emoticonImg = _emoticonImg;
						obj.transform.parent = parentCombArea;
                        //emotionPers = obj.GetComponent<DropToPay>().emoticonPers;
                        break;
					}
				}
			}
		}

		GetSumChange();
	}

	/* Выгрузка всех денег из кошелька */
	public void SetWalletMoney()
	{
		parentWalletArea = GameObject.FindGameObjectWithTag("walletMoneyArea").transform;
		int len = CurrencyController.moneyPrefabs.Count;
		GameObject obj;

		if (_listMoney != null)
		{   // выгружаем комбинации из евро
			foreach (float item in _listMoney)
			{
				for (int i = 0; i < len; i++)
				{
					if (CurrencyController.moneyPrefabs[i].GetComponent<Money>().value == item)
					{
						obj = Instantiate(CurrencyController.moneyPrefabs[i], CurrencyController.moneyPrefabs[i].transform.position, Quaternion.identity) as GameObject;
						obj.GetComponent<DragMoney>().enabled = false;
						obj.AddComponent<Draggable>();
						obj.transform.parent = parentWalletArea;
						break;
					}
				}
			}
		}
	}

	/* Получение переменных из других скриптов */
	void GetValueFromScript()
	{
		// Получаем ссылку на кошелек из первого канваса
		manager = GameObject.FindGameObjectWithTag("manager");

		_listMoney = new List<float>(CountingItems.listMoney);

		//manager.GetComponent<ManagerController>().countEnterCashbox++;  // +1 вход
	}
}
