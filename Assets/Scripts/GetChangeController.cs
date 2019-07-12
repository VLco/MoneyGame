using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChangeController : MonoBehaviour
{
	private static float _sumChange;
	private GameObject manager;
	private List<float> bestComb;
	private List<float> allMoney;
	private Transform parentChangeArea;
	private GameObject obj;
	private static float sumChangeForLoad;
	private static string sumChangeStr;

	public GameObject _cashboxScreen;
	[SerializeField] GameObject _bag;

	private void Start()
	{
		_sumChange = 0;
		sumChangeForLoad = 0;
		sumChangeStr = "";
		bestComb = new List<float>();
	}

	// Получение переменных из скрипта менеджера игры
	void GetValueFromScript()
	{
		// Получаем ссылку на кнопку из третьего канваса
		manager = GameObject.FindGameObjectWithTag("manager");

		// Получение суммы сдачи
		sumChangeForLoad = float.Parse(_cashboxScreen.GetComponent<GetMoneyCombination>().sumChange.ToString("0.00"));
		// sumChangeStr = _sumChange.ToString("0.00");
		// sumChangeForLoad = float.Parse(sumChangeStr);

		manager.GetComponent<ManagerController>().countEnterChange++;
	}

	// Выгрузка комбинации сдачи на экран
	public void SetChange()
	{
		if (!(_cashboxScreen.activeSelf))   // переход между экранами состоялся
		{
			GetValueFromScript();
			bestComb.Clear();
			if (sumChangeForLoad > 0)
			{
				//allMoney = new List<float>() { 200f, 100f, 50f, 20f, 10f, 5f, 2f, 1f, 0.5f, 0.2f, 0.1f, 0.05f, 0.01f };
				// TODO: 
				allMoney = new List<float>(CurrencyController.moneyPrefabs.Count);
				foreach (var item in CurrencyController.moneyPrefabs)
				{
					allMoney.Add(item.GetComponent<Money>().value);
				}
				allMoney.Sort();
				allMoney.Reverse();

				foreach (float coin in allMoney)
				{
					while (sumChangeForLoad >= coin)
					{
						sumChangeForLoad -= coin;

						if (sumChangeForLoad > 99f)
						{
							sumChangeStr = sumChangeForLoad.ToString("000.00");
						}
						else if (sumChangeForLoad > 9f)
						{
							sumChangeStr = sumChangeForLoad.ToString("00.00");
						}
						else
						{
							sumChangeStr = sumChangeForLoad.ToString("0.00");
							// Debug.Log(sumChangeForLoad);
						}
						sumChangeForLoad = float.Parse(sumChangeStr);

						bestComb.Add(coin);
					}
				}
			}

			if (manager.GetComponent<ManagerController>().countEnterChange > 1)
			{

				for (int i = 0; i < parentChangeArea.childCount; i++)
				{
					Destroy(parentChangeArea.GetChild(i).gameObject);
				}
			}

			for (int i = 0; i < _bag.transform.childCount; i++)
			{
				Destroy(_bag.transform.GetChild(i).gameObject);
			}

			parentChangeArea = GameObject.FindGameObjectWithTag("changeArea").transform;
			if (bestComb != null)
			{
				int len = CurrencyController.moneyPrefabs.Count;

				foreach (float item in bestComb)
				{
					for (int i = 0; i < len; i++)
					{
						if (CurrencyController.moneyPrefabs[i].GetComponent<Money>().value == item)
						{
							obj = Instantiate(CurrencyController.moneyPrefabs[i], CurrencyController.moneyPrefabs[i].transform.position, Quaternion.identity) as GameObject;
							obj.GetComponent<DragMoney>().enabled = false;
							obj.AddComponent<Draggable>();
							obj.transform.parent = parentChangeArea;
							break;
						}
					}
				}
			}
		}
	}
}
