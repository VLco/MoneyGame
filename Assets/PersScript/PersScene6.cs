using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersScene6 : MonoBehaviour {
    Animator anim;
    [SerializeField] GameObject imgEmoticon;
    public int emoticon = 0;

    void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetBool("animTalk", true);
        Invoke("stand", 3);

    }

    void Update()
    {
        emoticon = imgEmoticon.GetComponent<EmoticonManager>().emoticonPers;

        if (emoticon == 1)
        {
            anim.SetBool("animSad", true);
            anim.SetBool("animFun", false);
            anim.SetBool("animTalk", false);
        }
        else if (emoticon == 2)
        {
            anim.SetBool("animSad", false);
            anim.SetBool("animFun", true);
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
