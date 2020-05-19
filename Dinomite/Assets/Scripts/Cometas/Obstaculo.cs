using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {
	float _timer = 0;
	float _activeTime = 0;

	void Update() {
		_timer += Time.deltaTime;
		if (_timer > _activeTime) {
			Destroy();
		}
	}

	void Destroy() {
		Destroy(gameObject);
	}

	public void Setup(float activeTime) {
		_activeTime = activeTime;
	}
}
