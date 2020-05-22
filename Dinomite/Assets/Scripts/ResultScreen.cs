using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class ResultScreen : MonoBehaviour {
	[SerializeField] [Min(0)] float _showTime = 0;
	[SerializeField] GameObject _resultScreen = null;
	[SerializeField] TextMeshProUGUI _scoreText = null;
	[SerializeField] string _nextScene = "";
	bool _active = false;
	Controls _controls;

	void Awake() {
		_resultScreen.SetActive(false);
		FindObjectOfType<PlayerLife>().playerDied += ShowResult;

		_controls = new Controls();
		_controls.Player.Action.performed += Next;
		_controls.Player.Enable();
	}

	void ShowResult() {
		StartCoroutine(ShowTimer());
	}

	IEnumerator ShowTimer() {
		yield return new WaitForSeconds(_showTime);
		_resultScreen.SetActive(true);
		_active = true;
		yield break;
	}

	void Next(InputAction.CallbackContext context) {
		if (_active) {
			FindObjectOfType<SceneHandler>().LoadScene(_nextScene);
		}
	}

	void OnDisable() {
		_controls.Player.Action.performed -= Next;
	}
}
