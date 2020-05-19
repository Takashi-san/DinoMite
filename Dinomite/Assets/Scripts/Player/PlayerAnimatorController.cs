using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimatorController : MonoBehaviour {
	[SerializeField] Animator _animator = null;
	bool _isRight = true;
	PlayerMovement.MovingState _state = PlayerMovement.MovingState.Stop;
	bool _lock = false;

	void Awake() {
		FindObjectOfType<PlayerMovement>().isRight += DirectionUpdate;
		FindObjectOfType<PlayerMovement>().movingState += StateUpdate;
		FindObjectOfType<PlayerLife>().hurt += GotHurt;
	}

	void DirectionUpdate(bool isRight) {
		if (_isRight != isRight) {
			_isRight = isRight;
			_animator.SetBool("isRight", isRight);
			if (!_lock) {
				_animator.SetTrigger("Update");
			}
		}
	}

	void StateUpdate(PlayerMovement.MovingState state) {
		if (_state != state) {
			_state = state;
			_animator.SetInteger("State", (int)state);
			if (!_lock) {
				_animator.SetTrigger("Update");
			}
		}
	}

	void GotHurt(float time) {
		_animator.SetInteger("State", 3);
		_animator.SetTrigger("Update");
		StartCoroutine(LockTime(time));
	}

	IEnumerator LockTime(float time) {
		_lock = true;
		yield return new WaitForSeconds(time);
		_lock = false;
		yield break;
	}
}
