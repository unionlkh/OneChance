using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private float _MOVINGBACKWARDSPEEDHANDICAP = 0.7f;//WASD무빙에서, 뒤로갈때는 앞으로 갈때의 속도의 70퍼센트만 가능하다

    private Transform _playerTransform;
    private GameObject _playerStatusInstacne;
    private PlayerStatus _playerStatus;
    private GameObject _cameraInstance;
    private Camera _camera;
    private Animator _animator;

    public float _playerMovingSpeed = 3.0f; //플레이어의 이동속도
    private float _horizontalKeyboardMoving = 0.0f;//플레이어의 wasd이동에 쓰이는 변수
    private float _verticalKeyboardMoving = 0.0f;//플레이어의 wasd이동에 쓰이는 변수. 또, 이 값들을 체크하면 '이동중인지'아닌지 파악할수 있다.
    private float _playerRotateVerticalSpeed_FULL3D_SIGHT = 100.0f;//플레이어 좌우 회전에 쓰이는 변수
    private bool _isMoving = false;
    private bool _isMovable = true;

    private GameObject _nextControlRobot;
    private PlayerControl _nextControlRobotPlayerControl;
    private GameObject _beforeControlRobot;
    private PlayerControl _beforeControlRobotPlayerControl;
    private GameObject _nextRobotControllor;
    private GameObject _beforeRobotControllor;

    private float _runTime = 0.0f;
    private float _runTimeMax = 150.0f;
    private bool _doesRunning = false;

    // Use this for initialization
    void Awake() {
        this._playerTransform = this.GetComponent<Transform>();
        this._cameraInstance = this.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        //Debug.Log("CameraInstance : " + this._cameraInstance.name);
        this._camera = this._cameraInstance.GetComponent<Camera>();
        this._playerStatusInstacne = this.transform.GetChild(2).gameObject;
        this._playerStatus = this._playerStatusInstacne.GetComponent<PlayerStatus>();
        this._animator = this.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        this.process_playerMoving();
        this.process_controlChange();
        this.process_timer();
    }

    private void process_playerMoving() {
        this.process_playerMoving_FULL3D_SIGHT();
        this.process_playerRotate_FULL3D_SIGHT();
    }
    private void process_playerMoving_FULL3D_SIGHT() {//그냥 WASD이동이다.
        if (this._isMovable) {
            this._horizontalKeyboardMoving = Input.GetAxis("Horizontal");
            this._verticalKeyboardMoving = Input.GetAxis("Vertical");

            Vector3 moveDir = (Vector3.forward * this._verticalKeyboardMoving) + (Vector3.right * this._horizontalKeyboardMoving);

            if (this._verticalKeyboardMoving < -0.1f) {//뒤로 달리기하면 속도가 70퍼센트가 된다.
                this._playerTransform.Translate(moveDir.normalized * Time.deltaTime * this._playerMovingSpeed * this._MOVINGBACKWARDSPEEDHANDICAP, Space.Self);
            } else {
                this._playerTransform.Translate(moveDir.normalized * Time.deltaTime * this._playerMovingSpeed, Space.Self);
            }

            if (this._horizontalKeyboardMoving < 0.1 && this._horizontalKeyboardMoving > -0.1 &&
                this._verticalKeyboardMoving < 0.1 && this._verticalKeyboardMoving > -0.1) {
                this._isMoving = true;
            } else {
                this._isMoving = false;
            }
        } else {
            this._isMoving = false;
        }
    }
    private void process_playerRotate_FULL3D_SIGHT() {//플레이어 좌우 회전(마우스에 따라)
        this._playerTransform.Rotate(Vector3.up * Time.deltaTime * this._playerRotateVerticalSpeed_FULL3D_SIGHT * Input.GetAxis("Mouse X"));
    }

    private void process_controlChange() {
        if (Input.GetMouseButtonDown(0)) {
            if(this._playerStatus.getSelectedObject() != null) {
                if (this._playerStatus.getSelectedObject().tag == "RobotControllor") {
                    this._nextControlRobot = this._playerStatus.getSelectedObject().transform.parent.parent.GetChild(0).gameObject;
                    //Debug.Log("next로봇 : " + this._nextControlRobot.name);
                    this._nextControlRobotPlayerControl = this._nextControlRobot.GetComponent<PlayerControl>();

                    this._nextControlRobotPlayerControl.setBeforeControlRobot(this.gameObject);
                    this._nextControlRobotPlayerControl.setBeforeControlRobotPlayerControl(this);

                    this._nextRobotControllor = this._nextControlRobot.transform.parent.GetChild(1).GetChild(0).gameObject;

                    this.setControlToNext();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) {//이게 가장 큰 엔터키를 말한다고 한다.
            if (this._beforeControlRobot != null) {
                this.setControlToBefore();
            }
        }
    }
    public void setControlToNext() {
        Debug.Log("setControlToNext 호출");
        if (this._nextControlRobot != null) {
            this._nextRobotControllor.SetActive(false);//게임상에서 VR기기(로봇 컨트롤러)를 보이지 않도록 Disable

            this._nextControlRobotPlayerControl.enabled = true;//'다음'로봇(이 컨트롤러로 조종되는 로봇)의 PlayerControl스크립트 활성화
            this._nextControlRobotPlayerControl.getCamera().SetActive(true);//다음로봇의 카메라 게임오브젝트 활성화
            this._nextControlRobotPlayerControl.getPlayerStatus().SetActive(true);//다음 로봇의 PlayerStatus활성화
            this._nextControlRobotPlayerControl.getAnimator().SetTrigger("HaveControl");//다음 로봇이 자리에서 일어나는 애니메이션을 수행하도록, 애니메이터의 트리거 설정
            this._animator.SetTrigger("LostControl");//이 로봇이 자리에 앉는 애니메이션을 수행하도록, 애니메이터의 트리거 설정
            //캐릭터도 경고메시지를 막기 위해 애니메이터를 추가하고, 이 트리거 변수들을 추가해 놓았으나, 캐릭터에는 애니메이션이 필요없다.

            this._cameraInstance.SetActive(false);//이 로봇(혹은 캐릭터)의 카메라를 비활성화
            this._playerStatusInstacne.active = false;//이 로봇의 PlayerStatus를 비활성화. 이것과 위 2가지는 그냥 컴포넌트만 disable해도 된다.
            this.enabled = false;
        }
    }
    public void setControlToBefore() {
        if (this._beforeControlRobot != null) {
            if (this._beforeRobotControllor != null) {
                //this._beforeRobotControllor.SetActive(true);//다시 보이도록 한다.
            }

            Debug.Log(this._beforeControlRobotPlayerControl);
            this._beforeControlRobotPlayerControl.enabled = true;
            this._beforeControlRobotPlayerControl.getCamera().SetActive(true);
            this._beforeControlRobotPlayerControl.getPlayerStatus().SetActive(true);
            this._beforeControlRobotPlayerControl.getAnimator().SetTrigger("HaveControl");
            this._animator.SetTrigger("LostControl");

            this._cameraInstance.SetActive(false);
            this._playerStatusInstacne.active = false;
            this.enabled = false;
        }
    }

    private void process_timer() {
        if (this._doesRunning) {
            this._runTime += Time.deltaTime;
        }

        if(this._runTime >= this._runTimeMax) {//시간이 초과되면,
            this.setControlToBefore();//'전'의 녀석에게 컨트롤을 넘긴다.
        }
    }

    void OnEnable() {
        this._doesRunning = true;
        this.transform.GetChild(0).GetChild(0).gameObject.active = false;//자신이 조종하는것의 모델의 outlook을 disable해서, 화면에 나타나지 않게 한다.
        this._runTime = 0.0f;
    }
    void OnDisable() {
        this._doesRunning = false;
        this.transform.GetChild(0).GetChild(0).gameObject.active = true;//자신이 조종하지 않게 되는 경우, 다시 inable한다.
    }









    public GameObject getCamera() {
        return this._cameraInstance;
    }
    public GameObject getPlayerStatus() {
        return this._playerStatusInstacne;
    }
    public float getRuntime() {
        return (this._runTimeMax - this._runTime);
    }
    public Animator getAnimator() {
        return this._animator;
    }

    public void setBeforeControlRobot(GameObject beforeControlRobot) {
        this._beforeControlRobot = beforeControlRobot;
    }
    public void setBeforeControlRobotPlayerControl(PlayerControl beforeControlRobotPlayerControl) {
        this._beforeControlRobotPlayerControl = beforeControlRobotPlayerControl;
    }
}
