using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
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

    public bool isFirstTimePlay = true;
    //[SerializeField] GameObject TTFEController;
    // Start is called before the first frame update
    private void Awake()
    {
        //canvas = transform.Find("Canvas");
        //uiMainMenu = Instantiate(uiMainMenu,canvas);
        //uiComic = Instantiate(uiComic, canvas);
        ////uiPick = Instantiate(uiPick,canvas);
        //uiTutorial = Instantiate(uiTutorial, canvas);
        ////uiMainGame = Instantiate(uiMainGame,canvas);
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
        //Instantiate(TTFEController);
    }

    public void Update()
    {

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
        MyEvent.Post(gameObject);
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
        comic, none, game,setting
    }
}


