using UnityEngine;

[CreateAssetMenu(fileName = "Blink Action", menuName = "ScriptableObjects/Movement Actions/Blink Action")]
public class BlinkAction : IMovementAction {
	[SerializeField] [Min(0)] float _distance = 0;
	[SerializeField] GameObject _effect = null;

	public override bool Action(float isRight) {
		if (_effect) {
			Instantiate(_effect, _rb.transform.position, Quaternion.identity);
		}
		RaycastHit2D[] hits = Physics2D.RaycastAll(_rb.transform.position, isRight > 0 ? Vector2.right : Vector2.left, _distance);
		foreach (var item in hits) {
			if (item.transform.tag == "Wall") {
				_rb.transform.Translate(Vector2.right * isRight * item.distance);
				if (_effect) {
					Instantiate(_effect, _rb.transform.position, Quaternion.identity);
				}
				return false;
			}
		}

		_rb.transform.Translate(Vector2.right * isRight * _distance);
		if (_effect) {
			Instantiate(_effect, _rb.transform.position, Quaternion.identity);
		}
		return false;
	}
}
