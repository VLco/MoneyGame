using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShopsController : MonoBehaviour
{

	public ProductsList productsList = new ProductsList();
	[SerializeField] Button Shop1Button;
	[SerializeField] Button Shop2Button;
	[SerializeField] Button Shop3Button;
	[SerializeField] GameObject CategoriesScreen; // экран выбора категории продуктов
	[SerializeField] GameObject shopImgOnCategorScreen;   /* логотип выбранного магазина 
													на экране выбора категории */
	[SerializeField] GameObject shopImgOnProdScreen;  /* логотип выбранного магазина 
													на экране выбора товара */
	[SerializeField] GameObject categoryImage;    // картинка выбранной категории продуктов

	// Флаги для определения выбранного магазина
	private bool onShop1Cliked;
	private bool onShop2Cliked;
	private bool onShop3Cliked;

	// Категрии продуктов
	[SerializeField] Button CheeseCategoryButton;
	[SerializeField] Button FruitCategoryButton;
	[SerializeField] Button MilkCategoryButton;
	[SerializeField] Button MeatCategoryButton;

	[SerializeField] GameObject ProductsScreen;   // экран выбора продуктов
	[SerializeField] GameObject productsArea;
	[SerializeField] GameObject productPrefab;
	public List<GameObject> resultProductsPrefabs;  /* список с итоговыми префабами продуктов для 
														выгрузки на экране выбора товара после того как выбранный продукт
														будет помещен в корзину */
	private static int curruntPrefabIndex;  // номер перфаба для записи в имя

	[SerializeField] GameObject _backToWallet;    // объект-ссылка для измения элемента Progress Bar

	void Start()
	{
		onShop1Cliked = false;
		onShop2Cliked = false;
		onShop3Cliked = false;

		resultProductsPrefabs = new List<GameObject>();
		curruntPrefabIndex = 0;

		// Назначение обработчиков нажатия кнопок магазинов
		Shop1Button.onClick.AddListener(Shop1Clicked);
		Shop2Button.onClick.AddListener(Shop2Clicked);
		Shop3Button.onClick.AddListener(Shop3Clicked);
		CheeseCategoryButton.onClick.AddListener(CheeseCategoryClicked);
		FruitCategoryButton.onClick.AddListener(FuitCategoryClicked);
		MilkCategoryButton.onClick.AddListener(MilkCategoryClicked);
		MeatCategoryButton.onClick.AddListener(MeatCategoryClicked);

		TextAsset asset = Resources.Load("DB") as TextAsset;    // загрузка JSON файла

		if (asset != null)
		{
			productsList = JsonUtility.FromJson<ProductsList>(asset.text);  // считывание полей JSON файла
		}
	}

	// Установка картинки
	public void SetSprite(GameObject obj, string spriteName)
	{
		Sprite spr = Resources.Load<Sprite>(spriteName);
		obj.GetComponent<Image>().sprite = spr;
	}

	public void Shop1Clicked()
	{
		onShop1Cliked = true;
		onShop2Cliked = false;
		onShop3Cliked = false;

		// Скрытие экрана выбора магазина, открытие экрана выбора категории
		ShowCategoriesScreen();

		// Установка логотипа выбранного магазина
		SetSprite(shopImgOnCategorScreen, "Logo1");
	}

	public void Shop2Clicked()
	{
		onShop1Cliked = false;
		onShop2Cliked = true;
		onShop3Cliked = false;

		ShowCategoriesScreen();

		// Установка логотипа выбранного магазина
		SetSprite(shopImgOnCategorScreen, "Logo2");
	}

	public void Shop3Clicked()
	{
		onShop1Cliked = false;
		onShop2Cliked = false;
		onShop3Cliked = true;

		ShowCategoriesScreen();

		// Установка логотипа выбранного магазина
		SetSprite(shopImgOnCategorScreen, "Logo3");
	}

	public void CheeseCategoryClicked()
	{
		ChooseCategory("cheese");
	}

	public void FuitCategoryClicked()
	{
		ChooseCategory("fruit");
	}

	public void MilkCategoryClicked()
	{
		ChooseCategory("milk");
	}

	public void MeatCategoryClicked()
	{
		ChooseCategory("meat");
	}

	// Выбор какой-либо категории продуктов
	private void ChooseCategory(string categoryName)
	{
		ShowProductsScreen();

		// Установка картинки выбранной категории продуктов
		SetSprite(categoryImage, categoryName);

		if (onShop1Cliked)
		{
			SetSprite(shopImgOnProdScreen, "Logo1");
			UplodProducts("Market", categoryName);
		}
		else if (onShop2Cliked)
		{
			SetSprite(shopImgOnProdScreen, "Logo2");
			UplodProducts("Market1", categoryName);
		}
		else
		{
			SetSprite(shopImgOnProdScreen, "Logo3");
			UplodProducts("Market2", categoryName);
		}
	}

	// Выгрузка продуктов из БД и отображение на экране
	private void UplodProducts(string shopName, string categoryName)
	{
		// Удаление продуктов из области выгрузки продуктов (необходимо при смене категории)
		Button[] children = productsArea.GetComponentsInChildren<Button>(); /* получение всех потомков области,
			 																в которую перемещаем */

		foreach (Button child in children)
		{
			Destroy(child.gameObject);
			resultProductsPrefabs.Clear();
		}

		Products[] productsInShop =
			(from products in productsList.Products
			 where products.shopName == shopName && products.category == categoryName
			 select products).ToArray();

		foreach (var product in productsInShop)
		{
			string poductName = product.productName;

			// Создание продукты
			GameObject currentProduct = Instantiate(productPrefab) as GameObject;

			currentProduct.transform.position = productsArea.transform.position;
			currentProduct.GetComponent<RectTransform>().SetParent(productsArea.transform);

			Sprite spr = Resources.Load<Sprite>(poductName);
			currentProduct.GetComponent<Image>().sprite = spr;
            currentProduct.GetComponent<CategoryScript>().setCategory(product.category);
            currentProduct.GetComponent<CategoryScript>().setShop(product.shopName);

            string currency = Authorization.currencyList.currency;
			if (currency == "euro")
			{
				currentProduct.GetComponentInChildren<Text>().text = product.euro_price;
			}
			else if (currency == "us_dollar")
			{
				currentProduct.GetComponentInChildren<Text>().text = product.us_dollar_price;
			}
			else
			{
				currentProduct.GetComponentInChildren<Text>().text = product.russian_ruble_price;
			}

			curruntPrefabIndex++;
			currentProduct.name = curruntPrefabIndex.ToString();

			resultProductsPrefabs.Add(currentProduct);  // сохранение итоговых префабов продуктов
		}
	}

	private void ShowProductsScreen()
	{
		CategoriesScreen.SetActive(false);
		ProductsScreen.SetActive(true);

		_backToWallet.GetComponent<MovingController>().LoadProgress(0.5f);
	}

	private void ShowCategoriesScreen()
	{
		gameObject.SetActive(false);
		CategoriesScreen.SetActive(true);

		_backToWallet.GetComponent<MovingController>().LoadProgress(0.4f);
	}
}

[System.Serializable]
/* Класс, описывающий продукты в магазинах для считывания из JSON файла*/
public class Products
{
	public int ID;  // id продукта
	public string shopName; // название магазина
	public string productName;  // название продукта
	public int count;   // кол-во данного продукта в текущем магазине
	public string euro_price; // цена продукта
	public string russian_ruble_price; // цена продукта
	public string us_dollar_price; // цена продукта
	public string category; // категория продукта
}

[System.Serializable]
public class ProductsList
{
	public List<Products> Products = new List<Products>();
}

