using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreGenerator : MonoBehaviour {
	int _score = 0;
	float _timer = 0;
	bool _stop = false;
	public Action<int> scoreUpdate;

	void Start() {
		if (scoreUpdate != null) {
			scoreUpdate(_score);
		}

		FindObjectOfType<PlayerLife>().playerDied += StopScore;
	}

	void Update() {
		if (!_stop) {
			_timer += Time.deltaTime;
			if (_timer > 1f) {
				_score += 1;
				scoreUpdate(_score);
				_timer -= 1f;
			}
		}
	}

	public void AddScore(int points) {
		if ((points > 0) && (!_stop)) {
			_score += points;
			scoreUpdate(_score);
		}
	}

	public void SubScore(int penalty) {
		if ((penalty > 0) && (!_stop)) {
			_score -= penalty;
			scoreUpdate(_score);
		}
	}

	public int GetScore() {
		return _score;
	}

	void StopScore() {
		_stop = true;
	}
}
