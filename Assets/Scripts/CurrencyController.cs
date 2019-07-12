using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour
{
	[SerializeField] GameObject _coinsArea;
	[SerializeField] GameObject _banknotesArea;
	[SerializeField] GameObject _currencyPrefab;
	Vector2 coinsParentPosition;
	Vector2 banknotesParentPosition;
	Transform coinsParent;
	Transform banknotesParent;
	public static List<GameObject> moneyPrefabs;
	bool isCoin;
	[SerializeField] Text currencyText; // используемая валюта

	void Start()
	{
		moneyPrefabs = new List<GameObject>();
		coinsParentPosition = _coinsArea.transform.position;
		banknotesParentPosition = _banknotesArea.transform.position;
		coinsParent = _coinsArea.transform;
		banknotesParent = _banknotesArea.transform;

		isCoin = false;
		StartCoroutine(DownloadCurrrancyImage(Authorization.currencyList.banknotes, isCoin));

		isCoin = true;
		StartCoroutine(DownloadCurrrancyImage(Authorization.currencyList.coins, isCoin));

		currencyText.text = Authorization.currencyList.currency;    // выгрузка названия используемой валюты
	}

	IEnumerator DownloadCurrrancyImage(List<CurrencyFields> list, bool isCoin)
	{
		GameObject currentPrefab;
		Money component;

		foreach (var item in list)
		{
			UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://" + item.image);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;

				if (isCoin)
				{
					currentPrefab = Instantiate(_currencyPrefab) as GameObject;
					currentPrefab.transform.position = coinsParentPosition;
					currentPrefab.GetComponent<RectTransform>().SetParent(coinsParent);
					component = currentPrefab.GetComponent<Money>();
					component.startPosition = _coinsArea;
					component.value = item.count;
				}
				else
				{
					currentPrefab = Instantiate(_currencyPrefab) as GameObject;
					currentPrefab.transform.position = banknotesParentPosition;
					currentPrefab.GetComponent<RectTransform>().SetParent(banknotesParent);
					component = currentPrefab.GetComponent<Money>();
					component.startPosition = _banknotesArea;
					component.value = item.count;
				}

				currentPrefab.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
				currentPrefab.AddComponent<DragMoney>();

				moneyPrefabs.Add(currentPrefab);
			}
		}
	}
}
