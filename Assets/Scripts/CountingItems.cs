using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CountingItems : MonoBehaviour
{
	[SerializeField] GameObject bag;    // кошелек
	Transform[] money;  // префабы денег, которые лежат в кошельке
	public static List<float> listMoney;    // список для хранения сложенных денег номиналам в евро в кошелек


	Button[] products;

	void Start()
	{
		listMoney = new List<float>();
	}
	public void CountMoneyInWallet()
	{
		listMoney.Clear();
		money = bag.GetComponentsInChildren<Transform>();

		int len = money.Length;

		for (int i = 1; i < len; i++)
		{
			listMoney.Add(money[i].GetComponent<Money>().value);
		}
	}
}
