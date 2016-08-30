using UnityEngine;
using System.Collections;

public class InteractiveObjectControl : MonoBehaviour {//MonoBeabiour를 상속하지 않으면 GetComponent로 불러올수 없다.

    private bool _isSelected = false;//이녀석은 오로지 이것만 가지고 있다
    
    public void setIsSelected(bool temp) {
        this._isSelected = temp;
    }
    public bool getIsSelected() {
        return this._isSelected;
    }
}
