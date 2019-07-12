using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	[SerializeField] GameObject _hand;
	[SerializeField] GameObject _button;
	[SerializeField] AnimationClip _clip;

	void SetHandInvisiable()
	{
		_hand.SetActive(false);
	}

	public void StarButtontAnimation()
	{
		Animation anim = _button.GetComponent<Animation>();
		anim.Play(_clip.name);
	}
}
