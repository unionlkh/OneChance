using UnityEngine;
using System.Collections;

public class GateControl : MonoBehaviour {

    private BoxCollider _collider;
    private Animation _animation;

	// Use this for initialization
	void Start () {
        this._collider = this.GetComponent<BoxCollider>();
        this._animation = this.GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll) {
        //Debug.Log("문에서 가까워짐");
        this._animation["gate@open"].speed = 1.0f;
        this._animation.Play("gate@open");
    }
    void OnTriggerExit(Collider coll) {
        //Debug.Log("문에서 멀어짐");
        this._animation["gate@open"].speed = -1.0f;
        this._animation.Play("gate@open");
    }
}
