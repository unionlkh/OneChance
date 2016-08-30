using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {

    private Animator _animator;
    private InteractiveObjectControl _interactiveObjectControl;

    //문장뒤에 "//"표시를 한게 도움말 삭제를 위해 추가된 내용
    private GameObject _switchButton;//
    private GameObject _switchTip;//

	// Use this for initialization
	void Awake () {
        this._animator = this.GetComponent<Animator>();
        this._interactiveObjectControl = this.transform.parent.GetChild(1).GetChild(1).GetComponent<InteractiveObjectControl>();

        this._switchButton = this.transform.parent.GetChild(1).GetChild(1).gameObject;//
        this._switchTip = this.transform.parent.GetChild(1).GetChild(1).GetChild(0).gameObject;//
	}
	
	// Update is called once per frame
	void Update () {
        if (this._interactiveObjectControl.getIsSelected()) {
            Debug.Log("버튼을 선택한 상태!");
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("버튼 클릭!");
                this._animator.SetTrigger("DoorOpen");
                this._switchTip.SetActive(false);//
            }
        }
	}
}
