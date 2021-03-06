﻿using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour {
	TextMeshProUGUI _text;

	void Awake() {
		_text = gameObject.GetComponent<TextMeshProUGUI>();
		if (_text == null) {
			Debug.LogWarning("no textmeshpro");
		}

		FindObjectOfType<ScoreGenerator>().scoreUpdate += UpdateText;
		_text.text = FindObjectOfType<ScoreGenerator>().GetScore().ToString();
	}

	void UpdateText(int score) {
		_text.text = "" + score;
	}
}
