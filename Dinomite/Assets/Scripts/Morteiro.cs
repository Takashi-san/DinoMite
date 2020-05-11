﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morteiro : MonoBehaviour {
	[SerializeField] [Min(0)] float _speed = 0;
	[SerializeField] [Min(0)] int _damage = 0;
	[SerializeField] GameObject _miniMorteiro = null;
	[SerializeField] [Range(0, 90)] float _miniAngle = 0;
	//[SerializeField] [Min(0)] int _miniNum = 0;
	Rigidbody2D _rb;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		Vector2 target = (Vector2)transform.position + Vector2.down * (_speed * Time.fixedDeltaTime);
		_rb.MovePosition(target);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.GetComponent<PlayerLife>().DealDamage(_damage);
			Explode();
		}
		if (other.gameObject.tag == "Ground") {
			Explode();
		}
	}

	void Explode() {
		SpawnMini();
		Destroy(gameObject);
	}

	void SpawnMini() {
		GameObject mini;
		mini = Instantiate(_miniMorteiro, transform.position, _miniMorteiro.transform.rotation);
		mini.GetComponent<MiniMorteiro>().Setup(Quaternion.AngleAxis(_miniAngle, Vector3.forward) * Vector2.up);

		mini = Instantiate(_miniMorteiro, transform.position, _miniMorteiro.transform.rotation);
		mini.GetComponent<MiniMorteiro>().Setup(Quaternion.AngleAxis(-_miniAngle, Vector3.forward) * Vector2.up);
	}
}
