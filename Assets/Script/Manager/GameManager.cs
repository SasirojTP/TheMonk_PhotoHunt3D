using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using TMPro;
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

    [Header("Prefab")]
    [SerializeField] GameObject Bottle_Low;
    [SerializeField] GameObject Soda_Medium;
    [SerializeField] GameObject PropaneTank_High;

    public enum GameMode { Easy, Normal, Hard };
    public enum CameraState { BaseModel , PhotoHunt}
    [Header("Logic")]
    public GameMode gameMode;
    public CameraState cameraState;
    [SerializeField] Transform POS_BaseModelPos;
    [SerializeField] Transform POS_PhotoHuntPos;
    [SerializeField] Transform holder;
    GameObject spawnBaseModel;
    GameObject spawnPhotoHunt;
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

    Ray ray;
    RaycastHit raycastHit;
    private void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        print("Touch");
        //    }
        //}
        //}
        //if (Input.GetMouseButton(0) && cameraState == CameraState.PhotoHunt)
        //{
        //    ray = new Ray(Camera.main.transform.position, Vector3.forward);
        //    if(Physics.Raycast(ray, out raycastHit, 100f))
        //    {
        //        Debug.Log(raycastHit.transform.gameObject.name);
        //    }
        //}
    }

    public void StartGame(GameMode GameMode)
    {
        gameMode = GameMode;
        Canvas_GamePlay.gameObject.SetActive(true);
        SetLevelModel();
    }
    void SetLevelModel()
    {
        switch (gameMode)
        {
            case GameMode.Easy:
                spawnBaseModel = Instantiate(Bottle_Low, POS_BaseModelPos.position, Quaternion.identity, holder);
                spawnPhotoHunt = Instantiate(Bottle_Low, POS_PhotoHuntPos.position, Quaternion.identity, holder);
                break;
            case GameMode.Normal:
                spawnBaseModel = Instantiate(Soda_Medium, POS_BaseModelPos.position, Quaternion.identity, holder);
                spawnPhotoHunt = Instantiate(Soda_Medium, POS_PhotoHuntPos.position, Quaternion.identity, holder);
                
                break;
            case GameMode.Hard:
                spawnBaseModel = Instantiate(PropaneTank_High, POS_BaseModelPos.position, Quaternion.identity, holder);
                spawnPhotoHunt = Instantiate(PropaneTank_High, POS_PhotoHuntPos.position, Quaternion.identity, holder);
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

}
