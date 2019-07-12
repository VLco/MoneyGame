using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersScene3 : MonoBehaviour {
    Animator anim;

    void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("animSad", false);
        anim.SetBool("animFun", false);
        anim.SetBool("animTalk", true);

        Invoke("stand", 3);

    }

    void stand() {
        anim.SetBool("animSad", false);
        anim.SetBool("animFun", false);
        anim.SetBool("animTalk", false);
    }
}
