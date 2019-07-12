using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersScene2 : MonoBehaviour {
    Animator anim;
    [SerializeField] GameObject Bag;

    public int fun = 0;

    void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("animTalk", true);
        Invoke("stand", 3);
    }
	
	void Update () {
        fun = Bag.GetComponent<DropMoney>().funPers;

        if (fun == 1)
        {
            anim.SetBool("animFun", true);
            anim.SetBool("animSad", false);

        }
        else if (fun == 2)
        {
            anim.SetBool("animFun", false);
            anim.SetBool("animSad", true);
        }
    }


    void stand()
    {
        anim.SetBool("animSad", false);
        anim.SetBool("animFun", false);
        anim.SetBool("animTalk", false);
    }
}
