using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOnOffUI : MonoBehaviour {
	[SerializeField] Sprite _spriteOn = null;
	[SerializeField] Sprite _spriteOff = null;
	[SerializeField] [Range(-80, 20)] float _offValue = -80;
	Image _image;

	void Awake() {
		_image = GetComponent<Image>();
	}

	public void Check(float value) {
		if (value > _offValue) {
			_image.sprite = _spriteOn;
		}
		else {
			_image.sprite = _spriteOff;
		}
	}
}
