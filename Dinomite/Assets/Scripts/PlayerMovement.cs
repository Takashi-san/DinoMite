using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] [Min(0)] float _speed = 0;
	Rigidbody2D _rb;
	Controls _controls;
	Vector2 _input;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
		_controls = new Controls();
		_controls.Player.Enable();

		FindObjectOfType<PlayerLife>().playerDied += StopMoving;
	}

	void Update() {
		_input = _controls.Player.Move.ReadValue<Vector2>();
	}

	void FixedUpdate() {
		if (_input.x != 0) {
			Vector2 target = (Vector2)transform.position + Vector2.right * (_input.x * _speed * Time.fixedDeltaTime);
			_rb.MovePosition(target);
		}
	}

	void StopMoving() {
		_controls.Player.Disable();
	}

	void OnDisable() {
		_controls.Player.Disable();
	}
}
