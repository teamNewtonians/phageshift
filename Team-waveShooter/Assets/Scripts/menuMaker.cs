using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuMaker : MonoBehaviour
{
    public int index;
    public int maxIndex;
    bool keyDown;
    public bool start;
    public bool paused;
    public bool hud;
    public bool gameOver;
    public bool restart;

    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject hudPanel;
    public GameObject gameOverPanel;
    public GameObject playerPhage;
    public Text scoreNtimer;
    public Text levelText;

    public Animator startButton;
    public Animator scoresButton;
    public Animator quitButton;
    public Animator resumeButton;
    public Animator quit2Button;
    public Animator resetButton;
    public Animator restartButton;
    public Animator quit3Button;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        start = true;
        paused = false;
        hud = false;
        gameOver = false;
        restart = false;

        index = 0;
        maxIndex = 2;
        timer = 0;

        startMenu = GameObject.Find("StartMenu");
        pauseMenu = GameObject.Find("PauseMenu");
        hudPanel = GameObject.Find("HUD");
        gameOverPanel = GameObject.Find("GameOver");
        scoreNtimer = GameObject.Find("scoreNtimer").GetComponent<Text>();
        levelText = GameObject.Find("levelText").GetComponent<Text>();

        //Button animators
        startButton = GameObject.Find("StartMenu/StartButton").GetComponent<Animator>();
        scoresButton = GameObject.Find("StartMenu/ScoresButton").GetComponent<Animator>();
        quitButton = GameObject.Find("StartMenu/QuitButton").GetComponent<Animator>();

        resumeButton = GameObject.Find("PauseMenu/ResumeButton").GetComponent<Animator>();
        quit2Button = GameObject.Find("PauseMenu/Quit2Button").GetComponent<Animator>();
        resetButton = GameObject.Find("PauseMenu/ResetField").GetComponent<Animator>();

        restartButton = GameObject.Find("GameOver/RestartButton").GetComponent<Animator>();
        quit3Button = GameObject.Find("GameOver/Quit3Button").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        playerPhage = GameObject.Find("playField").GetComponent<fieldGenerator>().playerPhage;

        timer += Time.deltaTime;

        //Reset all select states
        startButton.SetBool("isSelected", false);
        scoresButton.SetBool("isSelected", false);
        quitButton.SetBool("isSelected", false);
        resumeButton.SetBool("isSelected", false);
        quit2Button.SetBool("isSelected", false);
        resetButton.SetBool("isSelected", false);
        quit3Button.SetBool("isSelected", false);
        restartButton.SetBool("isSelected", false);

        //Reset all pressed states
        startButton.SetBool("isPressed", false);
        scoresButton.SetBool("isPressed", false);
        quitButton.SetBool("isPressed", false);
        resumeButton.SetBool("isPressed", false);
        quit2Button.SetBool("isPressed", false);
        resetButton.SetBool("isPressed", false);
        quit3Button.SetBool("isPressed", false);
        restartButton.SetBool("isPressed", false);

        if (hud)
        {
            scoreNtimer.text = "Health: " + playerPhage.GetComponent<CreatureController>().health + 
                " Timer: " + (int)timer + " Score: " + GameObject.Find("playField").GetComponent<fieldGenerator>().score + 
                " / " + GameObject.Find("playField").GetComponent<fieldGenerator>().vCount  + 
                " Total: " + GameObject.Find("playField").GetComponent<fieldGenerator>().totScore;
            levelText.text = "Level: " + GameObject.Find("playField").GetComponent<fieldGenerator>().level;
        }

        if (!paused && !start && !gameOver)
        {
            Time.timeScale = 1;
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.P) && !start)
        {
            paused = !paused;
        }
        if (paused || start || gameOver)
        {
            Time.timeScale = 0;
        }
        //Quit game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Toggle hud
        if (!start)
        {
            hud = true;    
        }

        if(playerPhage.GetComponent<CreatureController>().health <= 0)
        {
            gameOver = true;
        }

        //Turn on whichever components
        startMenu.SetActive(start);
        pauseMenu.SetActive(paused);
        hudPanel.SetActive(hud);
        gameOverPanel.SetActive(gameOver);

        if (gameOver)
        {
            paused = false;
            maxIndex = 1;
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (index < maxIndex)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index = maxIndex;
                }
            }

            if (index == 0)
            {
                restartButton.SetBool("isSelected", true);
                quit3Button.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    restartButton.SetBool("isPressed", true);
                    restartButton.SetBool("isSelected", false);
                    timer = 0;
                    restart = true;
                }
            }
            if (index == 1)
            {
                restartButton.SetBool("isSelected", false);
                quit3Button.SetBool("isSelected", true);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    quit3Button.SetBool("isPressed", true);
                    quit3Button.SetBool("isSelected", false);
                    gameOver = false;
                    start = true;
                }
            }
        }

        if (paused)
        {
            maxIndex = 2;
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (index < maxIndex)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index = maxIndex;
                }
            }

            if (index == 0)
            {
                resumeButton.SetBool("isSelected", true);
                quit2Button.SetBool("isSelected", false);
                resetButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    resumeButton.SetBool("isPressed", true);
                    resumeButton.SetBool("isSelected", false);
                    paused = false;
                }
            }
            if (index == 1)
            {
                resumeButton.SetBool("isSelected", false);
                quit2Button.SetBool("isSelected", true);
                resetButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    quit2Button.SetBool("isPressed", true);
                    quit2Button.SetBool("isSelected", false);
                    restart = true;
                    start = true;
                }
            }
            if (index == 2)
            {
                resumeButton.SetBool("isSelected", false);
                quit2Button.SetBool("isSelected", false);
                resetButton.SetBool("isSelected", true);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    resetButton.SetBool("isPressed", true);
                    GameObject.Find("playField").GetComponent<fieldGenerator>().resetField = true;
                    resetButton.SetBool("isPressed", false);
                }
            }
        }

        if (start)
        {
            maxIndex = 2;
            paused = false;
            hud = false;
            gameOver = false;
            
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (index < maxIndex)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index = maxIndex;
                }
            }

            if( index == 0)
            {
                startButton.SetBool("isSelected", true);
                scoresButton.SetBool("isSelected", false);
                quitButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    startButton.SetBool("isPressed", true);
                    startButton.SetBool("isSelected", false);
                    timer = 0;
                    start = false;
                }
            }
            if (index == 1)
            {
                startButton.SetBool("isSelected", false);
                scoresButton.SetBool("isSelected", true);
                quitButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    scoresButton.SetBool("isPressed", true);
                    scoresButton.SetBool("isSelected", false);
                    //start = false;
                }
            }
            if (index == 2)
            {
                startButton.SetBool("isSelected", false);
                scoresButton.SetBool("isSelected", false);
                quitButton.SetBool("isSelected", true);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    quitButton.SetBool("isPressed", true);
                    quitButton.SetBool("isSelected", false);
                    Application.Quit();
                }
            }
        }

    }
}
