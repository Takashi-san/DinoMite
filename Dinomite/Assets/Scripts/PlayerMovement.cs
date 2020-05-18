﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] [Min(0)] float _speed = 0;
	[SerializeField] [Min(0)] float _dashSpeed = 0;
	[SerializeField] [Min(0)] float _dashDuration = 0;
	[SerializeField] [Min(0)] float _dashCoolDown = 0;
	[Tooltip("Quantos % antes do cooldown terminar é possível ativar o dash.")]
	[SerializeField] [Range(0, 1)] float _dashTolerance = 0;
	Rigidbody2D _rb;
	Controls _controls;
	Vector2 _input;
	bool _dash = false;
	float _isRight = 1;
	float _dashTimer = 0;
	float _dashCDTimer = 0;

	public Action<float> dashCooldownUpdate;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
		_controls = new Controls();
		_controls.Player.Action.performed += Dash;
		_controls.Player.Enable();

		FindObjectOfType<PlayerLife>().playerDied += StopMoving;
		if (dashCooldownUpdate != null) {
			dashCooldownUpdate(1);
		}
	}

	void Update() {
		_input = _controls.Player.Move.ReadValue<Vector2>();
		_dashCDTimer += Time.deltaTime;

		float reset = _dashCoolDown - _dashCDTimer;
		reset = reset < 0 ? 0 : reset;
		dashCooldownUpdate(reset / _dashCoolDown);
	}

	void FixedUpdate() {
		if (_dash) {
			_dashTimer += Time.fixedDeltaTime;
			Vector2 target = (Vector2)transform.position + Vector2.right * (_isRight * _dashSpeed * Time.fixedDeltaTime);
			_rb.MovePosition(target);
			if (_dashTimer > _dashDuration) {
				_dashTimer = 0;
				_dashCDTimer = 0;
				_dash = false;
			}
		}
		else if (_input.x != 0) {
			if (_input.x > 0) {
				_isRight = 1;
			}
			else {
				_isRight = -1;
			}
			Vector2 target = (Vector2)transform.position + Vector2.right * (_input.x * _speed * Time.fixedDeltaTime);
			_rb.MovePosition(target);
		}
	}

	void Dash(InputAction.CallbackContext context) {
		if (_dashCDTimer > _dashCoolDown * (1 - _dashTolerance)) {
			_dash = true;
		}
	}

	void StopMoving() {
		_controls.Player.Disable();
	}

	void OnDisable() {
		_controls.Player.Disable();
	}
}
