using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMorteiro : MonoBehaviour {
	[SerializeField] [Min(0)] float _force = 0;
	[SerializeField] [Min(0)] int _damage = 0;
	[SerializeField] GameObject _destroyEffect = null;
	Vector2 _direction;
	Rigidbody2D _rb;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
		_rb.AddForce(_force * _direction, ForceMode2D.Impulse);
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
		if (_destroyEffect != null) {
			Instantiate(_destroyEffect, transform.position, _destroyEffect.transform.rotation);
		}
		Destroy(gameObject);
	}

	public void Setup(Vector2 direction) {
		_direction = direction;
	}
}
