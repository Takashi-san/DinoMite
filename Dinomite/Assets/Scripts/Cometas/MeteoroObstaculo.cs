using UnityEngine;

public class MeteoroObstaculo : MonoBehaviour {
	[SerializeField] [Min(0)] float _speed = 0;
	[SerializeField] [Min(0)] int _damage = 0;
	[SerializeField] GameObject _obstaculo = null;
	[SerializeField] float _obstaculoTime = 0;
	[SerializeField] GameObject _destroyEffect = null;
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
			Spawn();
			Explode();
		}
	}

	void Explode() {
		if (_destroyEffect != null) {
			Instantiate(_destroyEffect, transform.position, _destroyEffect.transform.rotation);
		}
		Destroy(gameObject);
	}

	void Spawn() {
		GameObject obstaculo;
		obstaculo = Instantiate(_obstaculo, transform.position, _obstaculo.transform.rotation);
		obstaculo.GetComponent<Obstaculo>().Setup(_obstaculoTime);
	}
}

