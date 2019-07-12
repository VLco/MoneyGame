using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextToSpeech : MonoBehaviour
{
	[SerializeField] AudioSource _audio;

	void Start()
	{
		_audio = gameObject.GetComponent<AudioSource>();

		foreach (KeyValuePair<string, string> keyValue in ManagerController.text)
		{
			int compareResult = string.Compare(keyValue.Key, gameObject.name);  // сравнение названия экрана из списка и текущего экрана 
			if (compareResult == 0)
			{
				StartDownloadAudioDown(keyValue.Value);
				break;
			}
		}
	}

	IEnumerator DownloadAudio(string text)
	{
		Regex reg = new Regex("\\s+");
		string textToSpeech = reg.Replace(text, "+");
		string url = "http://api.voicerss.org/?key=5f5d20648db8493b80d8d149825ce0ae&hl=nl-nl&f=44khz_16bit_stereo&c=MP3&r=-3&src=" + textToSpeech;

		using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
		{
			yield return www.SendWebRequest();

			if (www.isNetworkError)
			{
				Debug.Log(www.error);
			}
			else
			{
				_audio.clip = DownloadHandlerAudioClip.GetContent(www);
				_audio.Play();
			}
		}
	}

	public void StartDownloadAudioDown(string text)
	{
		StartCoroutine(DownloadAudio(text));
	}
}
