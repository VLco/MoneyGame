using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScript : MonoBehaviour {

    [SerializeField] GameObject _basket;
    [SerializeField] GameObject MapShop;
    private GameObject im = new GameObject();
    public List<string> category;
    public string shop;


    public void UpdateCategory()
    {
        category.Clear();
        List<Button> products = new List<Button>(_basket.GetComponentsInChildren<Button>());
        foreach (var prod in products)
        {
            string cat = prod.GetComponent<CategoryScript>().getCategory();
            if (category.Find(x=>x == cat) != cat)
            {
                category.Add(cat);
            }

            string sh = prod.GetComponent<CategoryScript>().getShop();
            if (shop != sh)
            {
                shop = sh;
            }
        }
        Debug.Log(category);
        Debug.Log(shop);
    }

    public void setImageShop()
    {
        string catl = "";
        MapShop.GetComponent<Image>().sprite = null;
        catl = editListCategory(category);
        Debug.Log(catl);
        Debug.Log(im);
        Debug.Log("/MarketImage/" + shop + "_" + catl);
        Debug.Log(Resources.Load<Sprite>("MarketImage/" + shop + "_" + catl));
        im = Instantiate(MapShop, MapShop.transform, false);
        im.name = shop + catl;
        im.GetComponent<Image>().sprite = Resources.Load<Sprite>("MarketImage/" + shop + "_" + catl);


    }

    public string editListCategory(List<string> list)
    {
        list.Sort();
        string catl = "";
        foreach (var cat in list)
        {
            if((shop != "Market" && cat != "meat") || (shop != "Market2" && cat != "meat"))
                catl += cat;
        }
        return catl;
    }


   public void clearMap()
    {
        Destroy(im);
    }
}
