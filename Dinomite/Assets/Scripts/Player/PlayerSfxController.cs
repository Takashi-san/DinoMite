using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfxController : MonoBehaviour {
	[SerializeField] AudioClip _hurt = null;
	[SerializeField] AudioClip _dash = null;
	AudioSource _audioSource;

	void Awake() {
		_audioSource = GetComponent<AudioSource>();
		GetComponent<PlayerLife>().hurt += Hurt;
		GetComponent<PlayerMovement>().dash += Dash;
	}

	void Hurt(float stunTime) {
		_audioSource.clip = _hurt;
		_audioSource.Play();
	}

	void Dash() {
		_audioSource.clip = _dash;
		_audioSource.Play();
	}

	void OnDisable() {
		GetComponent<PlayerLife>().hurt -= Hurt;
		GetComponent<PlayerMovement>().dash -= Dash;
	}
}
