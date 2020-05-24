using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SequenceLoopPlay : MonoBehaviour {
	[SerializeField] AudioClip[] _clips = null;
	AudioSource _audioSource;
	Coroutine _coroutine;

	void Start() {
		_audioSource = GetComponent<AudioSource>();
		_audioSource.loop = false;
		_coroutine = StartCoroutine(Play());
	}

	// only loops the last clip.
	IEnumerator Play() {
		for (int i = 0; i < _clips.Length; i++) {
			_audioSource.clip = _clips[i];
			_audioSource.Play();
			if (i == _clips.Length - 1) {
				_audioSource.loop = true;
				yield break;
			}

			//yield return new WaitForSeconds(_clips[i].length);
			while (_audioSource.isPlaying) {
				yield return null;
			}
		}
	}

	void OnDisable() {
		StopCoroutine(_coroutine);
	}
}
