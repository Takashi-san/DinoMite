using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour {
	Slider _slider;

	void Awake() {
		_slider = GetComponent<Slider>();
		FindObjectOfType<PlayerMovement>().actionCooldownUpdate += BarUpdate;
	}

	void BarUpdate(float reset) {
		_slider.value = reset;
	}
}
