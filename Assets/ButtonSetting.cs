using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetting : MonoBehaviour {

    [SerializeField] GameObject setting;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowSetting() {
        setting.SetActive(true);
    }
}
