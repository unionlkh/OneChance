using UnityEngine;
using System.Collections;

public class ControlBoxControl : MonoBehaviour {

    private InteractiveObjectControl _interactiveObjectControl;

    private float _useTime;
    private float _useTimeMax = 3.0f;//15초여야 한다

	// Use this for initialization
	void Awake () {
        this._interactiveObjectControl = this.GetComponent<InteractiveObjectControl>();
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetMouseButtonDown(0)) {
            if (this._interactiveObjectControl.getIsSelected()) {
                this.gameObject.SetActive(false);
            }
        }

        this._useTime += Time.deltaTime;

        if(this._useTime >= this._useTimeMax) {
            this._useTime = 0.0f;
            this.gameObject.SetActive(true);
        }*/
	}
}
