using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersScene7 : MonoBehaviour {

    Animator anim;
    [SerializeField] GameObject BagScene7;

    public int emotion = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("animTalk", true);
        Invoke("stand", 3);
    }

    void Update()
    {
        emotion = BagScene7.GetComponent<DropChange>().emotionPers;

        if (emotion == 1)
        {
            anim.SetBool("animFun", false);
            anim.SetBool("animSad", true);
        }
        else if (emotion == 2)
        {
            anim.SetBool("animFun", true);
            anim.SetBool("animSad", false);
        }
    }
    void stand()
    {
        anim.SetBool("animSad", false);
        anim.SetBool("animFun", false);
        anim.SetBool("animTalk", false);
    }
}
