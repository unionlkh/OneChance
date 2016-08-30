using UnityEngine;
using System.Collections;

public class RotationDoorControl : MonoBehaviour {

    private GameObject _pivot;
    private Transform _pivotTransform;
    private float _rotationSpeed;
    private int _rotationWay;

	// Use this for initialization
	void Awake () {
        this._pivot = this.transform.GetChild(0).gameObject;
        this._pivotTransform = this._pivot.GetComponent<Transform>();
        this._rotationSpeed = Random.Range(100.0f, 150.0f);
        this._rotationWay = Random.Range(-1, 1);

        if(this._rotationWay == 0) {
            this._rotationWay = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        this._pivotTransform.Rotate(Vector3.up * this._rotationSpeed * this._rotationWay * Time.deltaTime);
	}
}
