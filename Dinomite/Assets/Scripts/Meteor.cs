using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
	[SerializeField] [Min(0)] float _speed = 0;
	[SerializeField] [Min(0)] int _damage = 0;
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
		Destroy(gameObject);
	}
}
