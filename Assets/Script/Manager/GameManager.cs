using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    [Header("Camera")]
    public CinemachineCamera CAM_BaseModel;
    public CinemachineCamera CAM_PhotoHunt;

    [Header("UI")]
    [SerializeField] Canvas Canvas_GamePlay;
    [SerializeField] Button GamePlay_BT_SwapCamera;
    [SerializeField] Button GamePlay_BT_Pause;
    [SerializeField] TextMeshProUGUI TEXT_YouWin;

    [Header("Prefab")]
    [SerializeField] GameObject Bottle_Low;
    [SerializeField] GameObject Bottle_Low_Hunt;

    [SerializeField] GameObject Soda_Medium;
    [SerializeField] GameObject Soda_Medium_Hunt;

    [SerializeField] GameObject PropaneTank_High;
    [SerializeField] GameObject PropaneTank_High_Hunt;

    public enum GameMode { Easy, Normal, Hard };
    public enum CameraState { BaseModel , PhotoHunt}
    public enum RotateState { NotRotate, Rotating }
    [Header("Logic")]
    public GameMode gameMode;
    public CameraState cameraState;
    public RotateState rotateState;
    [SerializeField] Transform POS_BaseModelPos;
    [SerializeField] Transform POS_PhotoHuntPos;
    [SerializeField] Transform holder;
    GameObject spawnBaseModel;
    GameObject spawnPhotoHunt;
    [SerializeField] int wrongPointRemaining;
    Ray ray;
    RaycastHit raycastHit;
    private void Start()
    {
        inst = this;
        CAM_PhotoHunt.gameObject.SetActive(false);
        AddListenerToBT();
        HideUI();
    }

    void AddListenerToBT()
    {
        GamePlay_BT_SwapCamera.onClick.AddListener(OnClickGamePlay_BT_SwapCamera);
        GamePlay_BT_Pause.onClick.AddListener(OnClickGamePlay_BT_Pause);
    }

    void HideUI()
    {
        Canvas_GamePlay.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(rotateState == RotateState.NotRotate)
            OnTouch();
    }
    void OnTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                CheckTouchObject();
                //print("Touch");
            }
        }

        if (Input.GetMouseButton(0) && cameraState == CameraState.PhotoHunt && Input.touchCount <= 0)
        {
            CheckTouchObject();
        }
    }
    void CheckTouchObject()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 50f))
        {
            if (raycastHit.collider.CompareTag("WrongPoint"))
            {
                raycastHit.transform.gameObject.SetActive(false);
                wrongPointRemaining--;
                CheckEndGame();
            }
        }
    }
    void CheckEndGame()
    {
        if(wrongPointRemaining <= 0)
        {
            TEXT_YouWin.gameObject.SetActive(true);
            GamePlay_BT_SwapCamera.interactable = false;
            GamePlay_BT_Pause.interactable = false;
            StartCoroutine(WaitForEndGame(3.0f));
        }
    }
    IEnumerator WaitForEndGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ClearModel();
        MainMenuManager.inst.GoToMainMenu();
        TEXT_YouWin.gameObject.SetActive(false);
        GamePlay_BT_SwapCamera.interactable = true;
        GamePlay_BT_Pause.interactable = true;
    }

    public void StartGame(GameMode GameMode)
    {
        gameMode = GameMode;
        cameraState = CameraState.BaseModel;
        rotateState = RotateState.NotRotate;
        CAM_BaseModel.gameObject.SetActive(true);
        CAM_PhotoHunt.gameObject.SetActive(false);

        Canvas_GamePlay.gameObject.SetActive(true);
        SetLevelModel();
        TEXT_YouWin.gameObject.SetActive(false);
    }
    void SetLevelModel()
    {
        switch (gameMode)
        {
            case GameMode.Easy:
                spawnBaseModel = Instantiate(Bottle_Low, POS_BaseModelPos.position, Quaternion.identity, holder);
                spawnPhotoHunt = Instantiate(Bottle_Low_Hunt, POS_PhotoHuntPos.position, Quaternion.identity, holder);
                wrongPointRemaining = 2;
                break;
            case GameMode.Normal:
                spawnBaseModel = Instantiate(Soda_Medium, POS_BaseModelPos.position, Quaternion.identity, holder);
                spawnPhotoHunt = Instantiate(Soda_Medium_Hunt, POS_PhotoHuntPos.position, Quaternion.identity, holder);
                spawnPhotoHunt.GetComponent<ObjectMaterial>().RandomMaterial();
                wrongPointRemaining = 2;

                break;
            case GameMode.Hard:
                spawnBaseModel = Instantiate(PropaneTank_High, POS_BaseModelPos.position, Quaternion.identity, holder);
                spawnPhotoHunt = Instantiate(PropaneTank_High_Hunt, POS_PhotoHuntPos.position, Quaternion.identity, holder);
                wrongPointRemaining = 4;
                break;
        }
        spawnBaseModel.GetComponent<RotateObject>().SetReferenceCameraState(CameraState.BaseModel);
        spawnPhotoHunt.GetComponent<RotateObject>().SetReferenceCameraState(CameraState.PhotoHunt);
    }
    void ClearModel()
    {
        foreach (Transform child in holder)
        {
            Destroy(child.gameObject);
        }
    }
    void OnClickGamePlay_BT_SwapCamera()
    {
        if(cameraState == CameraState.BaseModel)
        {
            cameraState = CameraState.PhotoHunt;
            CAM_BaseModel.gameObject.SetActive(false);
            CAM_PhotoHunt.gameObject.SetActive(true);
        }
        else
        {
            cameraState = CameraState.BaseModel;
            CAM_BaseModel.gameObject.SetActive(true);
            CAM_PhotoHunt.gameObject.SetActive(false);
        }
    }
    void OnClickGamePlay_BT_Pause()
    {
        ClearModel();
        MainMenuManager.inst.GoToMainMenu();
    }
    public void SetRotateState(RotateState RotateState)
    {
        rotateState = RotateState;
    }
}
