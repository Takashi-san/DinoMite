using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour {
	void OnEnable() {
		Cursor.visible = false;
	}

	void OnDisable() {
		Cursor.visible = true;
	}
}
