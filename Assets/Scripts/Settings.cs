using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	[SerializeField] GameObject wallet;
	[SerializeField] GameObject nextBtn;
	[SerializeField] GameObject backBtn;
	[SerializeField] GameObject basket;
	[SerializeField] GameObject restartBtn;
	[SerializeField] GameObject progressBar;

	void Start()
	{
		progressBar.SetActive(Authorization.settings.progressBar);
		if (wallet != null)
		{
			wallet.SetActive(false);
			if (ManagerController.images["wallet"] == null)
			{
				StartCoroutine(DownLoadImg(Authorization.settings.wallet, "wallet", wallet));   // настройка картинки для кошелька
			}
			else
			{
				SetImage(wallet, "wallet");
			}
		}
		if (nextBtn != null)
		{
			nextBtn.SetActive(false);
			if (ManagerController.images["nextBtn"] == null)
			{
				StartCoroutine(DownLoadImg(Authorization.settings.nextBtn, "nextBtn", nextBtn));    // настройка картинки для стрелки перехода на следующий экран
			}
			else
			{
				SetImage(nextBtn, "nextBtn");
			}
		}
		if (backBtn != null)
		{
			backBtn.SetActive(false);

			if (ManagerController.images["backBtn"] == null)
			{
				StartCoroutine(DownLoadImg(Authorization.settings.backBtn, "backBtn", backBtn));    // настройка картинки для стрелки перехода на предыдущий экран

			}
			else
			{
				SetImage(backBtn, "backBtn");
			}
		}
		if (basket != null)
		{
			basket.SetActive(false);
			if (ManagerController.images["basket"] == null)
			{
				StartCoroutine(DownLoadImg(Authorization.settings.basket, "basket", basket));   // настройка картинки для корзины покупок
			}
			else
			{
				SetImage(basket, "basket");
			}
		}
		if (restartBtn != null)
		{
			restartBtn.SetActive(false);
			if (ManagerController.images["restartBtn"] == null)
			{
				StartCoroutine(DownLoadImg(Authorization.settings.againBtn, "restartBtn", restartBtn)); // настройка картинки для кнопки перезагрузки игры
			}
			else
			{
				SetImage(restartBtn, "restartBtn");
			}
		}
	}

	// Скачиваем картинку по указанной ссылке и устанавливаем ее фоном для gameobject
	public IEnumerator DownLoadImg(string field, string objNameInDictionary, GameObject obj)
	{
		UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://" + field);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{
			Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
			ManagerController.images[objNameInDictionary] = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);   // кэшируем картинку
			obj.SetActive(true);
			obj.GetComponent<Image>().sprite = ManagerController.images[objNameInDictionary];
		}
	}

	// Устанавливаем кэшированную картинку фоном
	public void SetImage(GameObject obj, string objNameInDictionary)
	{
		obj.SetActive(true);
		obj.GetComponent<Image>().sprite = ManagerController.images[objNameInDictionary];
	}
}
