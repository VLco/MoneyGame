using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropChange : MonoBehaviour, IDropHandler
{
    public int emotionPers = 0;
	[SerializeField] GameObject _emoticonImg;

    public void OnDrop(PointerEventData eventData)
	{
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		if (d != null)
		{
			d.parentToReturnTo = this.transform;
            emotionPers = 2;
			_emoticonImg.GetComponent<EmoticonManager>().SetHappyEmoticon();
        }
	}
}
