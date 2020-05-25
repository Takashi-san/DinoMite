using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour {
	[SerializeField] GameObject _menu = null;
	Controls _controls;
	PlayerMovement _playerMovement;

	void Start() {
		if (!_menu) {
			Debug.LogWarning("No Menu specified to control");
		}
		else {
			_menu.SetActive(false);
		}
		_playerMovement = FindObjectOfType<PlayerMovement>();

		_controls = new Controls();
		_controls.Player.Pause.performed += Paused;
		_controls.Player.Enable();
	}

	void Paused(InputAction.CallbackContext context) {
		if (_menu.activeInHierarchy) {
			Deactivate();
		}
		else {
			Activate();
		}
	}

	void OnDestroy() {
		Time.timeScale = 1;
		_controls.Player.Pause.performed -= Paused;
	}

	public void Activate() {
		_menu.SetActive(true);
		_playerMovement.DisableMoving();
		Time.timeScale = 0;
	}

	public void Deactivate() {
		_menu.SetActive(false);
		_playerMovement.EnableMoving();
		Time.timeScale = 1;
	}
}
