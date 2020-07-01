using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePF : MonoBehaviour {
	string _key = "testPF";

	void Start() {
		if (PlayerPrefs.HasKey(_key)) {
			int value = PlayerPrefs.GetInt(_key);
			value++;
			PlayerPrefs.SetInt(_key, value);
		}
		else {
			PlayerPrefs.SetInt(_key, 0);
		}

		GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(_key).ToString();
	}
}
