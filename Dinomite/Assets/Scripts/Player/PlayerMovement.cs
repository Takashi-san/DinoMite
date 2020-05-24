using System.Collections;
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

	public Action dash;
	public Action<float> dashCooldownUpdate;
	public Action<bool> isRight;
	public Action<MovingState> movingState;

	public enum MovingState {
		Stop, Move, Dash
	}

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
		_controls = new Controls();
		_controls.Player.Action.performed += Dash;
		_controls.Player.Enable();

		FindObjectOfType<PlayerLife>().playerDied += DisableMoving;
		FindObjectOfType<PlayerLife>().hurt += Stunned;

		if (dashCooldownUpdate != null) {
			dashCooldownUpdate(1);
		}
		if (isRight != null) {
			isRight(true);
		}
		if (movingState != null) {
			movingState(MovingState.Stop);
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
				isRight(true);
			}
			else {
				_isRight = -1;
				isRight(false);
			}
			Vector2 target = (Vector2)transform.position + Vector2.right * (_input.x * _speed * Time.fixedDeltaTime);
			_rb.MovePosition(target);
			movingState(MovingState.Move);
		}
		else {
			movingState(MovingState.Stop);
		}
	}

	void Dash(InputAction.CallbackContext context) {
		if (_dashCDTimer > _dashCoolDown * (1 - _dashTolerance)) {
			_dash = true;
			movingState(MovingState.Dash);
			dash();
		}
	}

	void Stunned(float time) {
		StartCoroutine(StunnedTimer(time));
	}

	IEnumerator StunnedTimer(float time) {
		DisableMoving();
		yield return new WaitForSeconds(time);
		EnableMoving();
		yield break;
	}

	public void DisableMoving() {
		_controls.Player.Disable();
	}

	public void EnableMoving() {
		_controls.Player.Enable();
	}

	void OnDisable() {
		_controls.Player.Disable();
	}
}
