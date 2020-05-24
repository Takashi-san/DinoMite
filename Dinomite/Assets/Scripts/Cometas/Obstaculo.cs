using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {
	[SerializeField] GameObject _destroyEffect = null;
	float _timer = 0;
	float _activeTime = 0;

	void Update() {
		_timer += Time.deltaTime;
		if (_timer > _activeTime) {
			Destroy();
		}
	}

	void Destroy() {
		if (_destroyEffect != null) {
			Instantiate(_destroyEffect, transform.position, _destroyEffect.transform.rotation);
		}
		Destroy(gameObject);
	}

	public void Setup(float activeTime) {
		_activeTime = activeTime;
	}
}
