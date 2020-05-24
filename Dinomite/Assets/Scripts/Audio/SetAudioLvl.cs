using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLvl : MonoBehaviour {
	[SerializeField] AudioMixer _mixer = null;
	[SerializeField] string _variable = null;
	Slider _slider = null;

	void OnEnable() {
		_slider = GetComponent<Slider>();
		float value = 0;
		_mixer.GetFloat(_variable, out value);
		_slider.value = value;
	}

	public void SetLvl(float level) {
		_mixer.SetFloat(_variable, level);
	}
}
