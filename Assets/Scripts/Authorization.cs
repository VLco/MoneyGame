using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Authorization : MonoBehaviour
{
	[SerializeField] Button _transitionToWallet;   // переходим к первому игровому экрану (сбор денег)
	string password = System.String.Empty;  // введенный пароль
	string login = System.String.Empty;     // введенный логин
	[SerializeField] GameObject _walletScreen;
	[SerializeField] GameObject _authorizationScreen;
	static int restartCount = 0;        // сколько раз перегрузили игру
	JSONAuthData jsonData = new JSONAuthData(); // данные для чтения из json файла с сервера
	public static JSONSettings settings = new JSONSettings();   // настройки экранов 
	[SerializeField] Text _errorText;   // текстовое поле для вывода ошибок
	string protocol = "http://test-ait.herokuapp.com";
	public static Currency currencyList = new Currency();

	PictogramsList pictogramsList = new PictogramsList();   // список для чтения данных с сервера
	[SerializeField] GameObject _pictogramPrefab;
	[SerializeField] GameObject _pictogramsArea;    // область выгрузки пиктограм
	[SerializeField] GameObject _pictogramsAreaGO;
	Vector2 pictogramsParentPosition;
	Transform pictogramsParent;
	[SerializeField] GameObject[] _pictsLogin;  // строка логина
	[SerializeField] GameObject[] _pictsPassword;   // строка пароля
	public static List<GameObject> pictogramsPrefabs;   // массив для кэширования пиктограм
    public bool autorization = true;

	// Use this for initialization
	void Start()
	{
		// Кэширование значений
		pictogramsParentPosition = _pictogramsArea.transform.position;
		pictogramsParent = _pictogramsArea.transform;
		pictogramsPrefabs = new List<GameObject>();

		if (restartCount != 0)  // игра была перезапущена
		{
			_walletScreen.SetActive(true);
			_authorizationScreen.SetActive(false);   // скрыли экран авторизации
		}
		else /* первый вход в игру */
		{
			StartCoroutine(GetPictograms());
		}

		// Подключение кнопки перехода на экран сбора денег в кошелек
		_transitionToWallet.onClick.AddListener(TransitToWallet);

		restartCount++;
	}

	/* Считывание логина и пароля из соответсвующих областей */
	void GetAuthData()
	{
		login = GetString(_pictsLogin);
		password = GetString(_pictsPassword);
	}

	/* Получение логина/пароля в строку */
	string GetString(GameObject[] array)
	{
		int len = array.Length;
		string str = System.String.Empty;
		for (int i = 0; i < len; i++)
		{
			Image[] img = array[i].GetComponentsInChildren<Image>();
			if (img.Length == 2)
			{
				str += img[1].name;
			}
		}
		return str;
	}

	public void TransitToWallet()
	{
		GetAuthData();
		if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
		{
			StartCoroutine(POST());
		}
	}

	/* Авторизация пользователя */
	public IEnumerator POST()
	{
		var data = new WWWForm();
		data.AddField("login", login);
		data.AddField("password", password);

		using (UnityWebRequest www = UnityWebRequest.Post(protocol + "/api/v1/login/pid", data))
		{
			yield return www.SendWebRequest();
			if (www.isNetworkError || www.isHttpError)
			{
				_errorText.text = "Connection error";
			}
			else
			{
				string jsonString = www.downloadHandler.text;
				jsonData = JsonUtility.FromJson<JSONAuthData>(jsonString);
			}
		}

		if (jsonData.token.Length > 0)  // прошли авторизацию
		{
			StartCoroutine(PostTokenForMoney());
			StartCoroutine(PostTokenForSettings());
		}
		else
		{
            autorization = false;
            _errorText.text = "Wrong login or password";
		}
	}

	/* Получение настроек пользователя */
	public IEnumerator PostTokenForSettings()
	{
		using (UnityWebRequest www = UnityWebRequest.Post(protocol + "/api/v1/applications/moneygame/get/settings", ""))
		{
			www.SetRequestHeader("x-access-token", jsonData.token);
			yield return www.SendWebRequest();
			if (www.isNetworkError || www.isHttpError)
			{
				_errorText.text = "Connection error";
			}
			else
			{
				string jsonString = www.downloadHandler.text;
				settings = JsonUtility.FromJson<JSONSettings>(jsonString);

				_authorizationScreen.SetActive(false);  // скрыли экран авторизации
				_walletScreen.SetActive(true);  // открыли экран сбора денег в кошелек
			}
		}
	}

	/* Получение набора денег пользователя */
	public IEnumerator PostTokenForMoney()
	{
		using (UnityWebRequest www = UnityWebRequest.Post(protocol + "/api/v1/applications/moneygame/get/currency", ""))
		{
			www.SetRequestHeader("x-access-token", jsonData.token);
			yield return www.SendWebRequest();
			if (www.isNetworkError || www.isHttpError)
			{
				_errorText.text = "Can't post token";
			}
			else
			{
				string jsonString = www.downloadHandler.text;
				currencyList = JsonUtility.FromJson<Currency>(jsonString);
			}
		}
	}

	/* Получение пиктограм для авторизации */
	public IEnumerator GetPictograms()
	{
		UnityWebRequest www = UnityWebRequest.Get(protocol + "/api/v1/login/get-pictograms");
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			_errorText.text = "Can't get pictograms";
		}
		else
		{
			string jsonString = www.downloadHandler.text;
			pictogramsList = JsonUtility.FromJson<PictogramsList>(jsonString);
			StartCoroutine(DownLoadPictograms());
		}
	}

	/* Выгрузка пиктограм в соотвествующую область экрана */
	public IEnumerator DownLoadPictograms()
	{
		foreach (var item in pictogramsList.pictograms)
		{
			UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://" + item.image);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				_errorText.text = "Can't get pictograms";
			}
			else
			{
				GameObject currentPlace = Instantiate(_pictogramsArea) as GameObject;
				currentPlace.name = item.value + "place";
				currentPlace.transform.position = _pictogramsAreaGO.transform.position;
				currentPlace.GetComponent<RectTransform>().SetParent(_pictogramsAreaGO.transform);

				Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
				GameObject currentPrefab = Instantiate(_pictogramPrefab) as GameObject;
				currentPrefab.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);


				currentPrefab.name = item.value;

				pictogramsPrefabs.Add(currentPrefab);
				// currentPrefab.transform.position = pictogramsParentPosition;
				// currentPrefab.GetComponent<RectTransform>().SetParent(pictogramsParent);

				currentPrefab.transform.position = currentPlace.transform.position;
				currentPrefab.GetComponent<RectTransform>().SetParent(currentPlace.transform);
			}
		}
	}
}