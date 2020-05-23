using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour {
	[SerializeField] GameObject _lifeGood = null;
	[SerializeField] GameObject _lifeExplode = null;
	[SerializeField] GameObject _lifeBad = null;
	[SerializeField] float _initialPosX = 0;
	[SerializeField] float _initialPosY = 0;
	[SerializeField] float _posStepX = 0;
	[SerializeField] float _posStepY = 0;
	GameObject[] _lifesGood = new GameObject[0];
	GameObject[] _lifesBad = new GameObject[0];

	void Awake() {
		FindObjectOfType<PlayerLife>().lifeUpdate += LifeUpdate;
	}

	void LifeUpdate(int life) {
		// Cria os containers caso não tenha containers suficientes para a vida do jogador.
		if (_lifesGood.Length < life) {
			_lifesGood = new GameObject[life];
			_lifesBad = new GameObject[life];
			for (int i = 0; i < life; i++) {
				_lifesBad[i] = Instantiate(_lifeBad) as GameObject;
				_lifesBad[i].transform.SetParent(transform, false);
				_lifesBad[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(_initialPosX + _posStepX * i, _initialPosY + _posStepY * i);

				_lifesGood[i] = Instantiate(_lifeGood) as GameObject;
				_lifesGood[i].transform.SetParent(transform, false);
				_lifesGood[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(_initialPosX + _posStepX * i, _initialPosY + _posStepY * i);
			}
		}

		for (int i = 0; i < _lifesGood.Length; i++) {
			if (i + 1 > life) {
				if (_lifesGood[i].activeInHierarchy) {
					Vector3 position = Camera.main.ScreenToWorldPoint(_lifesGood[i].GetComponent<RectTransform>().transform.position);
					Instantiate(_lifeExplode, position, _lifeExplode.transform.rotation);
					_lifesGood[i].SetActive(false);
				}
			}
			else {
				_lifesGood[i].SetActive(true);
			}
		}
	}
}
