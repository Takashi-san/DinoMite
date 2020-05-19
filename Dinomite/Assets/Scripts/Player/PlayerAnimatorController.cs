using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {
	[SerializeField] Animator _animator = null;
	bool _isRight = true;
	PlayerMovement.MovingState _state = PlayerMovement.MovingState.Stop;

	void Awake() {
		FindObjectOfType<PlayerMovement>().isRight += DirectionUpdate;
		FindObjectOfType<PlayerMovement>().movingState += StateUpdate;
	}

	void DirectionUpdate(bool isRight) {
		if (_isRight != isRight) {
			_isRight = isRight;
			_animator.SetBool("isRight", isRight);
			_animator.SetTrigger("Update");
		}
	}

	void StateUpdate(PlayerMovement.MovingState state) {
		if (_state != state) {
			_state = state;
			_animator.SetInteger("State", (int)state);
			_animator.SetTrigger("Update");
		}
	}
}
