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


    //-------------------------------------�����л�����---------------------------
    public void BtnStart() //��ʼ��Ϸ
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


