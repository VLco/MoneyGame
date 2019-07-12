using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListButtonScript : MonoBehaviour {

    public void click()
    {
        //this.transform.localScale = new Vector2(0, 0);
        Debug.Log(this.transform.parent.gameObject.name);
        this.transform.parent.gameObject.GetComponent<ListScript>().ClickButtonOk(int.Parse(this.name));

    }
}
