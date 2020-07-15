using UnityEngine;

[CreateAssetMenu(fileName = "Dash Action", menuName = "ScriptableObjects/Movement Actions/Dash Action")]
public class DashAction : IMovementAction {
	[SerializeField] [Min(0)] float _speed = 0;
	[SerializeField] [Min(0)] float _duration = 0;

	float _timer = 0;

	public override bool Action(float isRight) {
		Vector2 target = (Vector2)_rb.transform.position + Vector2.right * (isRight * _speed * Time.fixedDeltaTime);
		_rb.MovePosition(target);

		_timer += Time.fixedDeltaTime;
		if (_timer > _duration) {
			_timer = 0;
			return false;
		}
		else {
			return true;
		}
	}
}
