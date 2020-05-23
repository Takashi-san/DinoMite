using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetAudioLvl : MonoBehaviour {
	[SerializeField] AudioMixer _mixer = null;
	[SerializeField] string _variable = null;

	public void SetLvl(float level) {
		_mixer.SetFloat(_variable, level);
	}
}
