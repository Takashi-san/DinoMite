using UnityEngine;

public class ShowCursor : MonoBehaviour {
	bool _init;

	void OnEnable() {
		_init = Cursor.visible;
		Cursor.visible = true;
	}

	void OnDisable() {
		Cursor.visible = _init;
	}
}
