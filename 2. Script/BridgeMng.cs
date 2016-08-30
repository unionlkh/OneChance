using UnityEngine;
using System.Collections;

public class BridgeMng : MonoBehaviour 
{
	private float _angleTo = 0;
    private Transform _bridgeBodyTransform;
    private GameObject _verticalSwitch;
    private InteractiveObjectControl _verticalSwitchInteractiveObjectControl;
    private GameObject _horizontalSwitch;
    private InteractiveObjectControl _horizontalSwitchInteractiveObjectControl;

    void Awake() {
        this._bridgeBodyTransform = this.gameObject.GetComponent<Transform>();
        this._verticalSwitch = this.transform.parent.GetChild(1).GetChild(1).gameObject;
        this._horizontalSwitch = this.transform.parent.GetChild(2).GetChild(1).gameObject;

        this._verticalSwitchInteractiveObjectControl = this._verticalSwitch.GetComponent<InteractiveObjectControl>();
        this._horizontalSwitchInteractiveObjectControl = this._horizontalSwitch.GetComponent<InteractiveObjectControl>();
    }

	void Update() {
        this.process_turnBridgeForAngle();

        if (Input.GetMouseButtonDown(0)) {
            if (this._verticalSwitchInteractiveObjectControl.getIsSelected()) {
                trunBridge(true);
            }

            if (this._horizontalSwitchInteractiveObjectControl.getIsSelected()) {
                trunBridge(false);
            }
        }
	}


    private void process_turnBridgeForAngle() {
        float nextRotation = Mathf.LerpAngle(this._bridgeBodyTransform.eulerAngles.y, this._angleTo, Time.deltaTime * 1.2f);
        this._bridgeBodyTransform.eulerAngles = new Vector3(0, nextRotation, 0);
    }

    public void trunBridge(bool isVerticle) {//이걸 이용해서, private _angleTo의 값을 바꿔주면, 다음 프레임부터 update마다 회전
        this._angleTo = (isVerticle) ? 0.0f : 90.0f;
    }
}
