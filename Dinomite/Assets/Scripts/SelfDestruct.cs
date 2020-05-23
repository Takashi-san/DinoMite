using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
	[SerializeField] [Min(0)] float _time = 1;
	float _timer = 0;

	void Update() {
		_timer += Time.deltaTime;
		if (_timer > _time) {
			Destroy(gameObject);
		}
	}
}
