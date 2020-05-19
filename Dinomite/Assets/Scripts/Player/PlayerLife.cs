using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerLife : MonoBehaviour {
	[SerializeField] [Min(1)] int _MaxLife = 1;
	[SerializeField] [Min(0)] float _invulnerableTime = 0;
	[SerializeField] [Range(0, 1)] float _stunRatio = 0;
	[SerializeField] SpriteRenderer _visual = null;
	[SerializeField] [Min(0)] float _blinkTime = 0;
	int _life;
	bool _isInvulnerable = false;
	float _timer = 0;
	Coroutine _blink;

	public Action<int> lifeUpdate;
	public Action<float> hurt;
	public Action playerDied;

	void Start() {
		_life = _MaxLife;
		if (lifeUpdate != null) {
			lifeUpdate(_life);
		}
	}

	void Update() {
		if (_isInvulnerable) {
			_timer += Time.deltaTime;
			if (_timer > _invulnerableTime) {
				_timer = 0;
				_isInvulnerable = false;
			}
		}
	}

	public void DealDamage(int damage) {
		if ((damage > 0) && !_isInvulnerable) {
			_life -= damage;
			_life = _life > 0 ? _life : 0;

			if (hurt != null) {
				hurt(_invulnerableTime * _stunRatio);
			}
			if (lifeUpdate != null) {
				lifeUpdate(_life);
			}

			if (_life == 0) {
				Die();
			}

			_isInvulnerable = true;
			_blink = StartCoroutine(Blink());
		}
	}

	public void Heal(int heal) {
		if (heal > 0) {
			_life += heal;
			_life = _life > _MaxLife ? _MaxLife : _life;
			if (lifeUpdate != null) {
				lifeUpdate(_life);
			}
		}
	}

	IEnumerator Blink() {
		while (_isInvulnerable) {
			if (_visual) {
				if (_visual.color.a > 0) {
					_visual.color = new Color(1, 1, 1, 0);
				}
				else {
					_visual.color = Color.white;
				}
			}

			yield return new WaitForSeconds(_blinkTime);
		}

		if (_visual) {
			_visual.color = Color.white;
		}
		yield break;
	}

	void Die() {
		playerDied();
		StopCoroutine(_blink);
		Destroy(gameObject);
	}
}
