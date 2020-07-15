using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump Action", menuName = "ScriptableObjects/Movement Actions/Jump Action")]
public class JumpAction : IMovementAction {
	[SerializeField] [Min(0)] float _height = 0;
	[SerializeField] [Min(0)] float _distance = 0;
	[SerializeField] [Min(0.001f)] float _duration = 0;

	float _timer = 0;
	float _gravity;
	float _velocityX;
	float _velocityY;

	public override void Setup(Rigidbody2D rb) {
		_rb = rb;
		_velocityX = _distance / _duration;
		_gravity = (-8 * _height) / _duration;
		_velocityY = (_gravity * _duration) / -2;
	}

	public override bool Action(float isRight) {
		_timer += Time.fixedDeltaTime;

		float velocityY = _gravity * _timer + _velocityY;
		Vector2 target = (Vector2)_rb.transform.position;
		target += Vector2.right * (isRight * _velocityX * Time.fixedDeltaTime);
		target += Vector2.up * (velocityY * Time.fixedDeltaTime);
		_rb.MovePosition(target);

		if (_timer > _duration) {
			_timer = 0;
			return false;
		}
		else {
			return true;
		}
	}
}
