using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EmoticonManager : MonoBehaviour
{
	[SerializeField] GameObject _shopsScreen;
    public int emoticonPers = 0;
    void Start()
	{
		gameObject.SetActive(false);
	}

	public void SetHappyEmoticon()
	{
		gameObject.SetActive(true);
		_shopsScreen.GetComponent<ShopsController>().SetSprite(gameObject, "good");
        emoticonPers = 2;
}
	public void SetSadEmoticon()
	{
		gameObject.SetActive(true);
		_shopsScreen.GetComponent<ShopsController>().SetSprite(gameObject, "bad");
        emoticonPers = 1;
    }
}
