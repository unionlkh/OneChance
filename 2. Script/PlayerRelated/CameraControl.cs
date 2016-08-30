using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    private float _CAMERAROTATEVERTICALSPEED_NORMAL = 50.0f;

    private Transform _cameraTransform;

    private GameObject _cameraRotationPivot;
    private Transform _cameraRotationPivotTransform;
    private GameObject _cameraPositionShadow;
    private Transform _cameraPositionShadowTransform;
    private GameObject _cameraPositionShadowOpposite;
    private Transform _cameraPositionShadowOppositeTransform;

    private float _cameraRotateHorizentalSpeed = 100.0f;

    private float _sightMaxHeight;
    private float _sightMinHeight;

    // Use this for initialization
    void Awake () {
        this._cameraTransform = this.GetComponent<Transform>();

        this._cameraRotationPivot = this.transform.parent.parent.gameObject;//GameObject.Find("PlayerCameraRotationPivot");
        this._cameraRotationPivotTransform = this._cameraRotationPivot.GetComponent<Transform>();
        this._cameraPositionShadow = this._cameraRotationPivot.transform.GetChild(0).gameObject;
        this._cameraPositionShadowTransform = this._cameraPositionShadow.GetComponent<Transform>();
        this._cameraPositionShadowOpposite = this._cameraRotationPivot.transform.GetChild(1).gameObject;
        this._cameraPositionShadowOppositeTransform = this._cameraPositionShadowOpposite.GetComponent<Transform>();

        this._cameraRotateHorizentalSpeed = this._CAMERAROTATEVERTICALSPEED_NORMAL;
    }

    void Update() {
        this.process_valueSetting();
    }
    private void process_valueSetting() {
        //플레이어의 눈높이의 약간 위/아래를 상/하한값으로 지정. 이 높이를 넘어가거나 낮으면 회전불가
        this._sightMaxHeight = this._cameraRotationPivotTransform.position.y + 0.35f;
        this._sightMinHeight = this._cameraRotationPivotTransform.position.y - 0.35f;
    }

    void LateUpdate() {
        this.cameraRotationControl_FULL3D_SIGHT();
    }
    private void cameraRotationControl_FULL3D_SIGHT() {
        if(this._cameraPositionShadowOppositeTransform.position.y < this._sightMinHeight) {//더 아래를 볼 수 없는데
            if (Input.GetAxis("Mouse Y") < 0) {//더 아래를 보려고 하면
                //do nothing
            } else {
                this._cameraRotationPivotTransform.Rotate(Vector3.right * Time.deltaTime * this._cameraRotateHorizentalSpeed * -Input.GetAxis("Mouse Y"));
            }
        } else if (this._cameraPositionShadowOppositeTransform.position.y > this._sightMaxHeight) {//더 위를 볼 수 없는데
            if (Input.GetAxis("Mouse Y") > 0) {//더 위로 올리려고 하면
                //do nothing
            } else {
                this._cameraRotationPivotTransform.Rotate(Vector3.right * Time.deltaTime * this._cameraRotateHorizentalSpeed * -Input.GetAxis("Mouse Y"));
            }
        } else {
            this._cameraRotationPivotTransform.Rotate(Vector3.right * Time.deltaTime * this._cameraRotateHorizentalSpeed * -Input.GetAxis("Mouse Y"));
        }

        this._cameraTransform.LookAt(this._cameraPositionShadowOppositeTransform);
    }
}
