using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager inst;
    [Header("MainMenu")]
    [SerializeField] Canvas Canvas_MainMenu;
    [SerializeField] Button BT_Bottle_Low;
    [SerializeField] Button BT_Soda_Medium;
    [SerializeField] Button BT_PropaneTank_High;

    [Header("GamePlay")]
    [SerializeField] Canvas Canvas_GamePlay;

    private void Start()
    {
        inst = this;
        HideUI();
        AddListenerTOBT();
    }

    void HideUI()
    {

    }

    void AddListenerTOBT()
    {
        BT_Bottle_Low.onClick.AddListener(OnClickBT_Bottle_Low);
        BT_Soda_Medium.onClick.AddListener(OnClickBT_Soda_Medium);
        BT_PropaneTank_High.onClick.AddListener(OnClickBT_PropaneTank_High);
    }
    public void GoToMainMenu()
    {
        Canvas_MainMenu.gameObject.SetActive(true);
    }

    void OnClickBT_Bottle_Low()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        GameManager.inst.StartGame(GameManager.GameMode.Easy);
    }
    void OnClickBT_Soda_Medium()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        GameManager.inst.StartGame(GameManager.GameMode.Normal);
    }
    void OnClickBT_PropaneTank_High()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        GameManager.inst.StartGame(GameManager.GameMode.Hard);
    }
}
