using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerLife : MonoBehaviour {
	[SerializeField] [Min(1)] int _MaxLife = 1;
	int _life;
	public Action<int> lifeUpdate;
	public Action playerDied;

	void Start() {
		_life = _MaxLife;
		if (lifeUpdate != null) {
			lifeUpdate(_life);
		}
	}

	public void DealDamage(int damage) {
		if (damage > 0) {
			_life -= damage;
			_life = _life > 0 ? _life : 0;
			if (lifeUpdate != null) {
				lifeUpdate(_life);
			}
			if (_life == 0) {
				Die();
			}
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

	void Die() {
		playerDied();
		Destroy(gameObject);
	}
}
