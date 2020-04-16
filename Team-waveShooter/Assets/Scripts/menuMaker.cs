using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class menuMaker : MonoBehaviour
{
    public int index;
    public int maxIndex;
    public int fieldResetCount;

    public bool seeStart;
    public bool paused;
    public bool hud;
    public bool gameOver;
    public bool seeScores;
    public bool restart;

    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject hudPanel;
    public GameObject gameOverPanel;
    public GameObject scoresMenu;
    public GameObject playerPhage;
    public InputField inputField;

    public Text scoreNtimer;
    public Text levelText;
    public Text scoreBoard;
    public string playerName;

    public Animator startButton;
    public Animator scoresButton;
    public Animator quitButton;
    public Animator resumeButton;
    public Animator quit2Button;
    public Animator resetButton;
    public Animator restartButton;
    public Animator quit3Button;
    public Animator submitButton;
    public Animator backtitle;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        seeStart = true;
        paused = false;
        hud = false;
        gameOver = false;
        seeScores = false;
        restart = false;

        index = 0;
        maxIndex = 2;
        timer = 0;
        fieldResetCount = 0;

        startMenu = GameObject.Find("StartMenu");
        pauseMenu = GameObject.Find("PauseMenu");
        hudPanel = GameObject.Find("HUD");
        gameOverPanel = GameObject.Find("GameOver");
        scoresMenu = GameObject.Find("ScoreMenu");

        scoreNtimer = GameObject.Find("scoreNtimer").GetComponent<Text>();
        levelText = GameObject.Find("levelText").GetComponent<Text>();
        scoreBoard = GameObject.Find("ScoresPanel/Text").GetComponent<Text>();

        //Button animators
        startButton = GameObject.Find("StartMenu/StartButton").GetComponent<Animator>();
        scoresButton = GameObject.Find("StartMenu/ScoresButton").GetComponent<Animator>();
        quitButton = GameObject.Find("StartMenu/QuitButton").GetComponent<Animator>();

        resumeButton = GameObject.Find("PauseMenu/ResumeButton").GetComponent<Animator>();
        quit2Button = GameObject.Find("PauseMenu/Quit2Button").GetComponent<Animator>();
        resetButton = GameObject.Find("PauseMenu/ResetField").GetComponent<Animator>();

        restartButton = GameObject.Find("GameOver/RestartButton").GetComponent<Animator>();
        submitButton = GameObject.Find("GameOver/SubmitButton").GetComponent<Animator>();
        quit3Button = GameObject.Find("GameOver/Quit3Button").GetComponent<Animator>();

        backtitle = GameObject.Find("ScoreMenu/BackTitle").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Necessary for webgl builds?
        PlayerPrefs.Save();
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
        submitButton.SetBool("isSelected", false);
        restartButton.SetBool("isSelected", false);
        backtitle.SetBool("isSelected", false);

        //Reset all pressed states
        startButton.SetBool("isPressed", false);
        scoresButton.SetBool("isPressed", false);
        quitButton.SetBool("isPressed", false);
        resumeButton.SetBool("isPressed", false);
        quit2Button.SetBool("isPressed", false);
        resetButton.SetBool("isPressed", false);
        quit3Button.SetBool("isPressed", false);
        submitButton.SetBool("isPressed", false);
        restartButton.SetBool("isPressed", false);
        backtitle.SetBool("isPressed", false);

        if (hud)
        {
            scoreNtimer.text = "Health: " + playerPhage.GetComponent<CreatureController>().health + 
                " Timer: " + (int)timer + " Score: " + GameObject.Find("playField").GetComponent<fieldGenerator>().score + 
                " / " + GameObject.Find("playField").GetComponent<fieldGenerator>().vCount  + 
                " Total: " + GameObject.Find("playField").GetComponent<fieldGenerator>().totScore;
            levelText.text = "Level: " + GameObject.Find("playField").GetComponent<fieldGenerator>().level;
        }

        if (!paused && !seeStart && !gameOver && !seeScores)
        {
            Time.timeScale = 1;
        }

        if (paused || seeStart || gameOver || seeScores)
        {
            Time.timeScale = 0;
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.P) && !seeStart && !gameOver && !seeScores)
        {
            paused = !paused;
        }

        //Quit game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Toggle hud
        if( seeStart || seeScores)
        {
            hud = false;
        }
        else
        {
            hud = true;
        }

        if(playerPhage.GetComponent<CreatureController>().health <= 0)
        {
            gameOver = true;
        }

        //Turn on whichever components
        startMenu.SetActive(seeStart);
        pauseMenu.SetActive(paused);
        hudPanel.SetActive(hud);
        gameOverPanel.SetActive(gameOver);
        scoresMenu.SetActive(seeScores);

        ///////Menu functionality start

        //Game over menu functionality
        if (gameOver)
        {
            maxIndex = 2;
            if (Input.GetKeyDown(KeyCode.DownArrow))
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
            if (Input.GetKeyDown(KeyCode.UpArrow))
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
                submitButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    restartButton.SetBool("isPressed", true);
                    restartButton.SetBool("isSelected", false);
                    timer = 0;
                    restart = true;
                    index = 5;
                }
            }
            if (index == 1)
            {
                restartButton.SetBool("isSelected", false);
                quit3Button.SetBool("isSelected", false);
                submitButton.SetBool("isSelected", true);

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    submitButton.SetBool("isPressed", true);
                    submitButton.SetBool("isSelected", false);

                    //Take input and put it in PlayerPrefs with score values.
                    playerName = inputField.text.ToString();
                    AddScore(playerName, GameObject.Find("playField").GetComponent<fieldGenerator>().totScore, (int)timer, fieldResetCount);

                    index = 5;
                }
            }
            if (index == 2)
            {
                restartButton.SetBool("isSelected", false);
                quit3Button.SetBool("isSelected", true);
                submitButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    quit3Button.SetBool("isPressed", true);
                    quit3Button.SetBool("isSelected", false);
                    seeStart = true;
                    index = 5;
                }
            }
        }
        /////////////////////////////////////////////For some unknown reason, this function breaks the other menu functionality unless all of the button set the index to an unused index essentially.
        //Scores menu functionality
        if (seeScores)
        {
            seeStart = false;
            maxIndex = 2;

            //Display scores...
            scoreBoard.text = "| Phage:   | Score:   | Time:    | Resets:  |\n--------------------------------------------------\n";
            for (int i = 0; i < 50; i++)
            {
                if (PlayerPrefs.HasKey(i + "HScore"))
                {
                    if(PlayerPrefs.GetInt(i + "HScore") > 0)
                    {
                        scoreBoard.text += string.Format("| {0,10} | {1,10} | {2,10} | {3,10} |", PlayerPrefs.GetString(i + "HScoreName"),PlayerPrefs.GetInt(i + "HScore"), PlayerPrefs.GetInt(i + "HTime"), PlayerPrefs.GetInt(i + "HResets"));
                        scoreBoard.text += "\n --------------------------------------------------\n";
                    }
                    
                }
            }


            if (Input.GetKeyDown(KeyCode.DownArrow))
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
            if (Input.GetKeyDown(KeyCode.UpArrow))
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
            }
            if (index == 1)
            {
                backtitle.SetBool("isSelected", true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    backtitle.SetBool("isPressed", true);
                    backtitle.SetBool("isSelected", false);
                    seeStart = true;
                    index = 5;
                }
            }
        }

        //Pause menu functionality
        if (paused)
        {
            seeScores = false;
            maxIndex = 2;
            if (Input.GetKeyDown(KeyCode.DownArrow))
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
            if (Input.GetKeyDown(KeyCode.UpArrow))
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
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    resumeButton.SetBool("isPressed", true);
                    resumeButton.SetBool("isSelected", false);
                    paused = false;
                    index = 5;
                }
            }
            if (index == 1)
            {
                resumeButton.SetBool("isSelected", false);
                quit2Button.SetBool("isSelected", true);
                resetButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    quit2Button.SetBool("isPressed", true);
                    quit2Button.SetBool("isSelected", false);
                    seeStart = true;
                    index = 5;
                }
            }
            if (index == 2)
            {
                resumeButton.SetBool("isSelected", false);
                quit2Button.SetBool("isSelected", false);
                resetButton.SetBool("isSelected", true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    resetButton.SetBool("isPressed", true);
                    GameObject.Find("playField").GetComponent<fieldGenerator>().resetField = true;
                    fieldResetCount += 1;
                    resetButton.SetBool("isSelected", false);
                    index = 5;
                }
            }
        }

        //Start menu funtionality
        if (seeStart)
        {
            maxIndex = 2;
            paused = false;
            hud = false;
            gameOver = false;
            seeScores = false;
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
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
            if (Input.GetKeyDown(KeyCode.UpArrow))
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

            if( index == 0 )
            {
                startButton.SetBool("isSelected", true);
                scoresButton.SetBool("isSelected", false);
                quitButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    startButton.SetBool("isPressed", true);
                    startButton.SetBool("isSelected", false);
                    timer = 0;
                    seeStart = false;
                    index = 5;
                }
            }
            if (index == 1)
            {
                startButton.SetBool("isSelected", false);
                scoresButton.SetBool("isSelected", true);
                quitButton.SetBool("isSelected", false);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    scoresButton.SetBool("isPressed", true);
                    scoresButton.SetBool("isSelected", false);
                    seeScores = true;
                    index = 5;
                }
            }
            if (index == 2)
            {
                startButton.SetBool("isSelected", false);
                scoresButton.SetBool("isSelected", false);
                quitButton.SetBool("isSelected", true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    quitButton.SetBool("isPressed", true);
                    quitButton.SetBool("isSelected", false);
                    Application.Quit();
                    index = 5;
                }
            }
        }

        //////Menu functionality end
    }

    void AddScore(string name, int score, int time, int resets)
    {
        int newScore;
        string newName;
        int newTime;
        int newResets;

        int oldScore;
        string oldName;
        int oldTime;
        int oldResets;

        newScore = score;
        newName = name;
        newTime = time;
        newResets = resets;

        for (int i = 0; i < 50; i++)
        {
            if (PlayerPrefs.HasKey(i + "HScore"))
            {
                if (PlayerPrefs.GetInt(i + "HScore") < newScore)
                {
                    // new score is higher than the stored score
                    oldScore = PlayerPrefs.GetInt(i + "HScore");
                    oldName = PlayerPrefs.GetString(i + "HScoreName");
                    oldTime = PlayerPrefs.GetInt(i + "HTime");
                    oldResets = PlayerPrefs.GetInt(i + "HResets");

                    PlayerPrefs.SetInt(i + "HScore", newScore);
                    PlayerPrefs.SetString(i + "HScoreName", newName);
                    PlayerPrefs.SetInt(i + "HTime", newTime);
                    PlayerPrefs.SetInt(i + "HResets", newResets);

                    newScore = oldScore;
                    newName = oldName;
                    newTime = oldTime;
                    newResets = oldResets;
                }
            }
            else
            {
                PlayerPrefs.SetInt(i + "HScore", newScore);
                PlayerPrefs.SetString(i + "HScoreName", newName);
                PlayerPrefs.SetInt(i + "HTime", newTime);
                PlayerPrefs.SetInt(i + "HResets", newResets);

                newScore = 0;
                newName = "";
                newTime = 0;
                newResets = 0;

            }
        }
    }
}
