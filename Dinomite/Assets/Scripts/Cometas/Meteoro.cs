﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteoro : MonoBehaviour {
	[SerializeField] [Min(0)] float _speed = 0;
	[SerializeField] [Min(0)] int _damage = 0;
	[SerializeField] GameObject _destroyEffect = null;
	Rigidbody2D _rb;

	private Shake shake;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();

		shake = GameObject.FindGameObjectWithTag("Screenshake").GetComponent<Shake>();
	}

	void FixedUpdate() {
		Vector2 target = (Vector2)transform.position + Vector2.down * (_speed * Time.fixedDeltaTime);
		_rb.MovePosition(target);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			shake.CamShake();
			other.GetComponent<PlayerLife>().DealDamage(_damage);
			Explode();
		}
		if (other.gameObject.tag == "Ground") {
			Explode();
		}
	}

	void Explode() {
		if (_destroyEffect != null) {
			Instantiate(_destroyEffect, transform.position, _destroyEffect.transform.rotation);
		}
		Destroy(gameObject);
	}
}
