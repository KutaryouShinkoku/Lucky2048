using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState gameState;

    [Header("UI")]
    [SerializeField] GameObject uiMainMenu;
    [SerializeField] GameObject uiComic;
    [SerializeField] GameObject uiPick;
    [SerializeField] GameObject uiTutorial;

    public bool isFirstTimePlay = true;
    //[SerializeField] GameObject TTFEController;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.none;
        uiMainMenu.SetActive(true);
        //Instantiate(TTFEController);
    }


    //-------------------------------------界面切换部分---------------------------
    public void BtnStart() //开始游戏
    {
        if (isFirstTimePlay)
        {
            uiComic.SetActive(true);
        }
        else
        {
            uiPick.SetActive(true);
        }
        gameState = GameState.game;
        uiMainMenu.SetActive(false);
    }
    public void BtnTutorial() //教学
    {
        uiTutorial.SetActive(true);
    }
    public void BtnTutorialClose() //教学
    {
        uiTutorial.SetActive(false);
    }

    //漫画部分
    public void NextComic()
    {
        //下张漫画；如果有多张的话启用这个
    }
    public void SkipComic()
    {
        //直接跳过，直到游戏开始
        uiPick.SetActive(true);
        uiComic.SetActive(false);
        Debug.Log($"SkipComics");
    }

    public enum GameState
    {
        comic, none, game,setting
    }
}


