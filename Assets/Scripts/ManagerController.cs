using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerController : MonoBehaviour
{

	public GameObject _bag; // кошелек
	public int countEnterCashbox;   // счетчик входов на экран оплаты покупки
	public int countEnterChange;    // счетчик входов на экран выдачи сдачи
	public static Dictionary<string, Sprite> images = new Dictionary<string, Sprite>(); // словарь для кэширования скаченных картинок
	public static Dictionary<string, string> text = new Dictionary<string, string>();   // текст для озвучки для всех экранов

	void Awake()
	{
		countEnterCashbox = 0;
		countEnterChange = 0;

		text.Clear();
		text.Add("AuthorizationScreen", "Hallo ");
		text.Add("WalletScreen", "Plaats geld in de portemonnee");
		text.Add("ShopsScreen", "Druk op de afbeelding van een winkel");
		text.Add("CategoriesScreen", "Druk op de foto van een categorie");
		text.Add("ProductsScreen", "Laat producten in de winkelmand vallen");
		text.Add("CashboxScreen", "Plaats geld aan de bovenkant van het scherm");
		text.Add("ChangeScreen", "Dit is jouw wisselgeld. Plaats het in de portemonnee");
	}

	void Start()
	{
		images.Clear();
		images.Add("wallet", null);
		images.Add("nextBtn", null);
		images.Add("backBtn", null);
		images.Add("basket", null);
		images.Add("restartBtn", null);
	}

	// Use this for initialization
	void Update()
	{
		if (GameObject.FindGameObjectWithTag("wallet") != null)
		{ // получаем ссыдку на кошелек
			_bag = GameObject.FindGameObjectWithTag("wallet");
		}

		// 	// 	if (GameObject.FindGameObjectWithTag ("inputField") != null) {	// получаем ссылку на поле ввода
		// 	// 		_inputField = GameObject.FindGameObjectWithTag ("inputField");
		// 	// 	}
	}
}
