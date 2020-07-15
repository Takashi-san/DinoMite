using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] [Min(0)] float _speed = 0;
	[SerializeField] IMovementAction _action = null;
	Rigidbody2D _rb;
	Controls _controls;
	Vector2 _input;
	bool _doAction = false;
	float _isRight = 1;
	float _actionTimer = 0;

	public Action action;
	public Action<float> actionCooldownUpdate;
	public Action<bool> isRight;
	public Action<MovingState> movingState;

	public enum MovingState {
		Stop, Move, Dash
	}

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
		_action.Setup(_rb);
		_controls = new Controls();
		_controls.Player.Action.performed += Action;
		_controls.Player.Enable();

		FindObjectOfType<PlayerLife>().playerDied += DisableMoving;
		FindObjectOfType<PlayerLife>().hurt += Stunned;

		if (actionCooldownUpdate != null) {
			actionCooldownUpdate(0);
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
		_actionTimer += Time.deltaTime;

		float reset = _action.CoolDown - _actionTimer;
		reset = reset < 0 ? 0 : reset;
		actionCooldownUpdate(1 - (reset / _action.CoolDown));
	}

	void FixedUpdate() {
		if (_doAction) {
			// Action
			if (!_action.Action(_isRight)) {
				_actionTimer = 0;
				_doAction = false;
			}
		}
		else if (_input.x != 0) {
			// Determine direction
			if (_input.x > 0) {
				_isRight = 1;
				isRight(true);
			}
			else {
				_isRight = -1;
				isRight(false);
			}

			// Walk
			Walk();
			movingState(MovingState.Move);
		}
		else {
			movingState(MovingState.Stop);
		}
	}

	void Walk() {
		Vector2 target = (Vector2)transform.position + Vector2.right * (_input.x * _speed * Time.fixedDeltaTime);
		_rb.MovePosition(target);
	}

	void Action(InputAction.CallbackContext context) {
		if (_actionTimer > _action.CoolDown * (1 - _action.CoolDownTolerance)) {
			_doAction = true;
			action();

			movingState(MovingState.Dash);
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
