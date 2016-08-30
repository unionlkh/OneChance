using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerStatus : MonoBehaviour {

    private GameObject _selectedObject;
    private GameObject _lastSelectedObject;
    private Vector3 _mousePosition;
    private GameObject _camera;
    private Camera _cameraComponent;

    // Use this for initialization
    void Awake () {
        this._selectedObject = null;
        this._lastSelectedObject = null;
        this._camera = this.transform.parent.GetChild(1).GetChild(0).GetChild(0).gameObject;
        this._cameraComponent = this._camera.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        this.process_selectObjectWithMousePointer();
	}

    private void process_selectObjectWithMousePointer() {
        this._mousePosition = Input.mousePosition;

        if (!EventSystem.current.IsPointerOverGameObject()) {//마우스가 UI위에 있지 않을때
            Ray ray = this._cameraComponent.ScreenPointToRay(this._mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

            this._lastSelectedObject = this._selectedObject;//저번 프레임에서의 selectedObject를 lastSelectedObject로 옮기고

            if (Physics.Raycast(ray, out hit, 3.0f)) {
                this._selectedObject = hit.transform.gameObject;//새로 선택한게 있으면, selectedObject로 설정
                Debug.Log("선택한 게임오브젝트 : " + this._selectedObject.name);// ----> 프레임드랍
            } else {
                this._selectedObject = null;//빈 공간을 가리키더라도, Raycast특성상 null을 반환하지는 않는듯 하다. 따라서 이렇게 해준다.
            }

            if (this._selectedObject == null) {//마우스가 가리킨 오브젝트가 없는 경우
                if (this._lastSelectedObject == null) {
                    //do nothing
                } else {//이건 매건 콜하는 루틴이 아니므로, GetComponent를 써도 상관 없을듯 하다.
                    switch (this._lastSelectedObject.layer) {
                        case 9://9번 레이어가 InteractiveObject
                            this._lastSelectedObject.GetComponent<InteractiveObjectControl>().setIsSelected(false);
                            break;
                    }
                }
            } else {//마우스가 가리킨 오브젝트가 있는 경우
                if (this._selectedObject.layer == 9) {//이것도 매번 콜하는 루틴은 아니지만, GetComponent를 빼고싶긴 하지만 기술적으로 뺼 수가 없다.
                    this._selectedObject.GetComponent<InteractiveObjectControl>().setIsSelected(true);

                    if (this._lastSelectedObject == null) {
                        //do nothing
                    } else {
                        if (this._selectedObject.Equals(this._lastSelectedObject)) {
                            //do nothing
                        } else {
                            switch (this._lastSelectedObject.layer) {
                                case 9://9번 레이어가 InteractiveObject
                                    this._lastSelectedObject.GetComponent<InteractiveObjectControl>().setIsSelected(false);
                                    break;
                            }
                        }
                    }
                } else {
                    if (this._lastSelectedObject == null) {
                        //do nothing
                    } else {
                        if (this._selectedObject.Equals(this._lastSelectedObject)) {
                            //do nothing
                        } else {
                            switch (this._lastSelectedObject.layer) {
                                case 9://9번 레이어가 InteractiveObject
                                    this._lastSelectedObject.GetComponent<InteractiveObjectControl>().setIsSelected(false);
                                    break;
                            }
                        }
                    }
                    //이건 뭐냐면, 예를들어 버튼을 가리켰다가 'Floor'를 가리키는경우, 
                    //Floor는 태그가 없으므로 이리 들어오게 된다.
                    //그런데 여기를 do nothing으로 두면, lastSelectedObject가 그대로 남아
                    //버튼 사이즈가 초기화가 안된다. 따라서 마지막 else엔 이걸 붙여야 한다.
                }
            }
        } else {//마우스가 UI위에 있을떄
            //do nothing - 나중에, 마우스가 올라간 UI가 '뭐냐'하는식의 구체적인 케이스분류도 필요할듯 ->그럼 스위치로
            this._selectedObject = null;
        }
    }

    public GameObject getSelectedObject() {
        return this._selectedObject;
    }
}
