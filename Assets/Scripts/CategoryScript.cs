using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryScript : MonoBehaviour {
    private string category;
    private string shop;
	public void setCategory(string c)
    {
        category = c;
    }
    public string getCategory()
    {
        return category;
    }

    public void setShop(string s)
    {
        shop = s;
    }
    public string getShop()
    {
        return shop;
    }

}
