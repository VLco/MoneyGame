using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersScene1 : MonoBehaviour {
    Animator anim;
    [SerializeField] GameObject Manager;
    public bool autorizationNum = true;

    void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("animSad", false);
        anim.SetBool("animFun", false);
        anim.SetBool("animTalk", true);

        Invoke("stand", 3);
    }
	

	void Update () {
        autorizationNum = Manager.GetComponent<Authorization>().autorization;



        if (autorizationNum==false)
        {
            anim.SetBool("animSad", true);
            anim.SetBool("animFun", false);
            anim.SetBool("animTalk", false);
        }       
	}


    void stand()
    {
        anim.SetBool("animSad", false);
        anim.SetBool("animFun", false);
        anim.SetBool("animTalk", false);
    }
}
