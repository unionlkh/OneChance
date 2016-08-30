using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {

    private bool _isMouseCursorEnabled = false;

    // Use this for initialization
    void Start () {
        this.cursorLock();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            if (this._isMouseCursorEnabled) {
                this.cursorLock();
            } else {
                this.cursorUnLock();
            }
        }
    }

    public void cursorLock() {
        this._isMouseCursorEnabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;//커서를 화면 중앙에 고정시킨다. -> 구석에 고정시키는거라도 있으면 좋을텐데
    }
    public void cursorUnLock() {
        this._isMouseCursorEnabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
