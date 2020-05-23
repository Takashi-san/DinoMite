using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour {
	[SerializeField] SpriteRenderer _bgRenderer = null;
	[SerializeField] SpriteRenderer _limitRenderer = null;
	[SerializeField] Sprite[] _bgSprites = null;
	[SerializeField] Sprite[] _limitSprites = null;

	void Start() {
		int index = Random.Range(0, _bgSprites.Length);
		_bgRenderer.sprite = _bgSprites[index];
		_limitRenderer.sprite = _limitSprites[index];
	}
}
