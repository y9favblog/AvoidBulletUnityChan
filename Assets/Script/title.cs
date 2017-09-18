using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class title : MonoBehaviour {
	[SerializeField]GameObject nameobj;
	[SerializeField]InputField inpf;

	public void stbt(){
		if (PlayerPrefs.HasKey("Player Name")) {
			SceneManager.LoadScene ("GameMain");
		} else {
			nameobj.SetActive (true);
		}
	}

	public void okbt(){
		if (!inpf.text.Equals("")) {
			PlayerPrefs.SetString ("Player Name", inpf.text);
		} else {
			PlayerPrefs.SetString ("Player Name", "NoName");
		}
		nameobj.SetActive (false);
	}
}
