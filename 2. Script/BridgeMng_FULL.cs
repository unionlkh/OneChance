using UnityEngine;
using System.Collections;

public class BridgeMng_FULL : MonoBehaviour {

    private float _angleTo = 0;
    private Transform _bridgeBodyTransform;
    private GameObject _switch;
    private InteractiveObjectControl _switchInteractiveObjectControl;

    void Awake() {
        this._bridgeBodyTransform = this.gameObject.GetComponent<Transform>();
        this._switch = this.transform.parent.GetChild(1).GetChild(1).gameObject;
        this._switchInteractiveObjectControl = this._switch.GetComponent<InteractiveObjectControl>();
        this._angleTo = this._bridgeBodyTransform.eulerAngles.y;
    }

    void Update() {
        this.process_turnBridgeForAngle();

        if (Input.GetMouseButtonDown(0)) {
            if (this._switchInteractiveObjectControl.getIsSelected()) {
                trunBridge();
            }
        }
    }

    private void process_turnBridgeForAngle() {
        float nextRotation = Mathf.LerpAngle(transform.eulerAngles.y, this._angleTo, Time.deltaTime * 1.2f);
        transform.eulerAngles = new Vector3(0, nextRotation, 0);
    }

    public void trunBridge() {
        this._angleTo = Mathf.Repeat(this._angleTo + 90, 360);
    }
}
