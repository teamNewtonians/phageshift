using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMaker : MonoBehaviour
{
    public int index;
    public int maxIndex;
    bool keyDown;
    public bool start;
    public bool paused;
    public bool hud;
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject hudPanel;
    public Animator startButton;
    public Animator scoresButton;
    public Animator quitButton;
    public Animator resumeButton;
    public Animator quit2Button;

    // Start is called before the first frame update
    void Start()
    {
        start = true;
        paused = false;
        hud = false;
        index = 0;
        maxIndex = 2;

        startMenu = GameObject.Find("StartMenu");
        pauseMenu = GameObject.Find("PauseMenu");
        hudPanel = GameObject.Find("HUD");
        startButton = GameObject.Find("StartMenu/StartButton").GetComponent<Animator>();
        scoresButton = GameObject.Find("StartMenu/ScoresButton").GetComponent<Animator>();
        quitButton = GameObject.Find("StartMenu/QuitButton").GetComponent<Animator>();
        resumeButton = GameObject.Find("PauseMenu/ResumeButton").GetComponent<Animator>();
        quit2Button = GameObject.Find("PauseMenu/Quit2Button").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Reset all button states
        startButton.SetBool("isSelected", false);
        scoresButton.SetBool("isSelected", false);
        quitButton.SetBool("isSelected", false);
        resumeButton.SetBool("isSelected", false);
        quit2Button.SetBool("isSelected", false);
        startButton.SetBool("isPressed", false);
        scoresButton.SetBool("isPressed", false);
        quitButton.SetBool("isPressed", false);
        resumeButton.SetBool("isPressed", false);
        quit2Button.SetBool("isPressed", false);

        if (!paused && !start)
        {
            Time.timeScale = 1;
        }
        //Pause
        if (Input.GetKeyDown(KeyCode.P) && !start)
        {
            paused = !paused;
        }
        if (paused || start)
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

        if (paused)
        {
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
                resumeButton.SetBool("isSelected", true);
                quit2Button.SetBool("isSelected", false);
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
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    quit2Button.SetBool("isPressed", true);
                    quit2Button.SetBool("isSelected", false);
                    start = true;
                }
            }
        }

        if (start)
        {
            maxIndex = 2;
            paused = false;
            hud = false;
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

        //Turn on whichever components
        startMenu.SetActive(start);
        pauseMenu.SetActive(paused);
        hudPanel.SetActive(hud);
    }
}
