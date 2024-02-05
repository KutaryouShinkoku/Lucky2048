using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    [Header("Game")]
    [SerializeField] TTFEController ttfeController;
    [SerializeField] CombatManager combatManager;

    [Header("UI")]
    [SerializeField] Transform canvas;
    [SerializeField] GameObject uiMainMenu;
    [SerializeField] GameObject uiComic;
    [SerializeField] GameObject uiPick;
    [SerializeField] GameObject uiTutorial;
    [SerializeField] GameObject uiMainGame;
    [SerializeField] GameObject uiGameOver;
    [SerializeField] GameObject uiGameWin;

    public AK.Wwise.Event MyEvent;
    public AK.Wwise.RTPC MyRtpc;
    public AK.Wwise.Event MyEventBegin;

    public bool isFirstTimePlay = true;
    //[SerializeField] GameObject TTFEController;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        uiMainMenu.SetActive(true);
        uiComic.SetActive(false);
        uiPick.SetActive(false);
        uiTutorial.SetActive(false);
        uiMainGame.SetActive(false);

        gameState = GameState.none;
        uiMainMenu.SetActive(true);
        uiGameOver.SetActive(false);
        uiGameWin.SetActive(false);
        //Instantiate(TTFEController);
        
    }

    public void Update()
    {
        if (combatManager.state == CombatState.selectR || combatManager.state == CombatState.selectC)
        {
            uiPick.SetActive(true);
        }
        if (combatManager.state == CombatState.over)
        {
            uiGameOver.SetActive(true);
        }
        if (combatManager.state == CombatState.win)
        {
            uiGameWin.SetActive(true);
        }
    }
    //-------------------------------------�����л�����---------------------------
    public void BtnStart() //��ʼ��Ϸ
    {
        if (isFirstTimePlay)
        {
            uiComic.SetActive(true);
        }

        uiPick.SetActive(true);
        gameState = GameState.game;
        uiMainGame.SetActive(true);
        uiMainMenu.SetActive(false);
        MyEventBegin.Post(gameObject);
        MyEvent.Post(gameObject);
        MyRtpc.SetGlobalValue(15);
    }
    public void BtnTutorial() //��ѧ
    {
        uiTutorial.SetActive(true);
    }
    public void BtnTutorialClose() //��ѧ
    {
        uiTutorial.SetActive(false);
    }

    //��������
    public void NextComic()
    {
        //��������������ж��ŵĻ��������
    }
    public void SkipComic()
    {
        //ֱ��������ֱ����Ϸ��ʼ
        uiPick.SetActive(true);
        uiComic.SetActive(false);
        Debug.Log($"SkipComics");
    }

    public enum GameState
    {
        comic, none, game, setting
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}


