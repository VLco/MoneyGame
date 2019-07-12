using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingLanguage : MonoBehaviour {

    [SerializeField] public Toggle Rus;
    [SerializeField] public Toggle Eng;
    [SerializeField] public GameObject TextRus;
    [SerializeField] public GameObject TextEng;
    public int Language = 0; //1 - русский 2 - английский
	void Start () {
		
	}
	
	void Update () {

        if (Rus.isOn)
        {
            Language = 1;
            TextRus.SetActive(true);
            TextEng.SetActive(false);
        }
        else
        {
            Language = 2;
            TextRus.SetActive(false);
            TextEng.SetActive(true);
        }
    }


}
