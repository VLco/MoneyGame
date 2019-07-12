using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListScript : MonoBehaviour {

    [SerializeField] GameObject _basket;
    [SerializeField] GameObject _basketBut;
    [SerializeField] GameObject _content;
    [SerializeField] GameObject buttonOk;


    private int countProductInList = 0;
    public List<int> nameNoRend;
    public List<GameObject> objList;
    public List<Image> products;
    public List<Button> productsBut;
    void Start () {
        UpdateContent();
    }

    public void UpdateBasket()
    {
        nameNoRend.Add(0);
        products = new List<Image>(_basket.GetComponentsInChildren<Image>());
        productsBut = new List<Button>(_basketBut.GetComponentsInChildren<Button>());
        if (products.Count != countProductInList)
        {
             products.Remove(products[0]);

        }
        countProductInList = products.Count;
        ShowList();
    }


    public void ShowList()
    {
        Debug.Log(products.Count);
        GameObject backObj = new GameObject();
        if ((((int)((products.Count + 1) / 2)) * (buttonOk.GetComponent<RectTransform>().sizeDelta.y + 100f)) > _content.GetComponent<RectTransform>().sizeDelta.y)
            _content.GetComponent<RectTransform>().sizeDelta = new Vector2(_content.GetComponent<RectTransform>().sizeDelta.x, ((int)((products.Count + 1) / 2)) * (buttonOk.GetComponent<RectTransform>().sizeDelta.y + 100f));
        int sizeScreen = Screen.width;
        for (int i = 0; i < products.Count; i++)
        {
            GameObject em = new GameObject("" + i);
            buttonOk.GetComponent<Image>().sprite = products[i].sprite;
            GameObject obj = Instantiate(buttonOk, _content.transform, false);
            obj.name = "" + i;
            
            if (i % 2 == 1)
                obj.transform.localPosition = new Vector2(sizeScreen / 2 + 100f, backObj.transform.localPosition.y);
            else if (i == 0)
                obj.transform.localPosition = new Vector2(sizeScreen / 2 - 100f, -100f + ((int)((i + 2) / 2) - 1) * (-obj.GetComponent<RectTransform>().sizeDelta.y - 100f));
            else
                obj.transform.localPosition = new Vector2(sizeScreen / 2 - 100f, backObj.transform.localPosition.y - obj.GetComponent<RectTransform>().sizeDelta.y - 100f);
            objList.Add(obj);
            backObj = obj;



        }
    }

    // Update is called once per frame
    public void UpdateContent() {
        ClearContent();
        UpdateBasket();
    }

    public void ClearContent()
    {
        foreach (GameObject obj in objList)
        {
            Destroy(obj);
        }
        nameNoRend.Clear();
        objList.Clear();
    }

    public void ClickButtonOk(int n)
    {
        if (n != nameNoRend.Find(x => x == n))
            nameNoRend.Add(n);
        ClearContent();
        products.Remove(products[n]);
        productsBut.Remove(productsBut[n]);
        countProductInList = products.Count;
        ShowList();
    }


    public List<Button> GetFinalButtonList()
    { 
        return productsBut;
    }

    public int GetCountProduct()
    {
        List <Image> basket = new List<Image>(_basket.GetComponentsInChildren<Image>());
        return  basket.Count - productsBut.Count;
    }
}
