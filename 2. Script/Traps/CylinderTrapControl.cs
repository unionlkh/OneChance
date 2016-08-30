using UnityEngine;
using System.Collections;

public class CylinderTrapControl : MonoBehaviour {

    private Animator _animator;
    private float _randNum;
    private float _stay = 0.0f;

	// Use this for initialization
	void Awake () {
        //처음에는, 일단 애니메이터를 켠다. 랜덤한 시간동안 기다린 후 함정이 동작하도록 하기 위해서다.
        this._animator = this.GetComponent<Transform>().GetChild(0).GetComponent<Animator>();
        this._animator.enabled = false;
        this._randNum = Random.Range(0.0f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //이 스크립트가 활성화 되어있는 동안에는, 랜덤한 시간만큼 기다린다. 다 기다리면 이 스크립트를 디스에이블 한다.
        if (this._stay > this._randNum)
            this.enabled = false;

        this._stay += Time.deltaTime;
	}

    void OnDisable() {//이 스크립트를 디스에이블 하면서, 애니메이터는 켠다.
        this._animator.enabled = true;
    }
}
