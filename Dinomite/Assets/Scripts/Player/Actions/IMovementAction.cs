using UnityEngine;

public abstract class IMovementAction : ScriptableObject {
	[SerializeField] [Min(0)] float _coolDown = 0;
	[Tooltip("Quantos % antes do cooldown terminar é possível ativar a ação.")]
	[SerializeField] [Range(0, 1)] float _coolDownTolerance = 0;

	public float CoolDown => _coolDown;
	public float CoolDownTolerance => _coolDownTolerance;

	protected Rigidbody2D _rb;

	public virtual void Setup(Rigidbody2D rb) {
		_rb = rb;
	}

	public abstract bool Action(float isRight);
}