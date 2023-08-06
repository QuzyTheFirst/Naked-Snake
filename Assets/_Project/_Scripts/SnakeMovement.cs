using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    Vector3 PositionINeed = new Vector3(0f, 0f, 1.75f);
    Vector3 CharPos;

    [SerializeField]
    Text ApplesCountText;
    [SerializeField]
    ParticleSystem BOOM;
    [SerializeField]
    ParticleSystem WIN;
    [SerializeField]
    AudioClip BOOMSound;
    [SerializeField]
    AudioClip WINSound;
    [SerializeField]
    AudioClip AppleEatSound;


    [SerializeField]
    GameObject Panel;

    [SerializeField]
    AudioSource aud;
    enum where { WS, AD }
    where Where = new where();
    public enum state { alive,dead,win,menu}
    public state State = new state();

    int freePos;
    int LevelNow;
    int NextLevel;

    [SerializeField]
    int Apples;
    int ApplesToWin;

    [SerializeField]
    GameObject body;
    GameObject bodyPos;

    [SerializeField]
    float time;
    float timeSnake;

    [SerializeField]
    GameObject ShiftText;

    public GameObject[] Bodies;
    public Vector3[] PreviousPositions;
    public Quaternion[] PreviousRotations;

    bool turn;
    bool SpawnsBody;

    void Start()
    {
        LevelNow = SceneManager.GetActiveScene().buildIndex;
        NextLevel = LevelNow + 1;

        Where = where.WS;

        ApplesToWin = 0;
        SpawnsBody = true;
        WhatAboutApples();

        ShiftText.SetActive(false);

        timeSnake = 0.75f;
        Apples = 0;
        turn = true;
        State = state.alive;
        CharPos = gameObject.transform.position;
        PreviousPositions = new Vector3[ApplesToWin];
        PreviousRotations = new Quaternion[ApplesToWin];
        Bodies = new GameObject[ApplesToWin];
    }
    void WhatAboutApples()
    {
        switch (LevelNow)
        {
            case 1:
            case 2:
            case 3:
                ApplesToWin = 20;
                break;
              
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(State == state.alive)
        {
            ApplesCountText.text = Apples + " / " + ApplesToWin;
            KeyForMovement();
            time += Time.deltaTime;
            if (time >= timeSnake)
            {
                MoveOneWay();
                time = 0;
                turn = true;
            }
            if(Apples == ApplesToWin)
            {
                State = state.win;
                aud.PlayOneShot(WINSound);
                WIN.Play();
                Invoke("LoadNextLevel", 2f);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (State == state.alive)
            {
                State = state.menu;
            }
            else if (State == state.menu)
            {
                State = state.alive;
            }
            Panel.SetActive(!Panel.active);
        }

    }

    private void KeyForMovement()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveOneWay();
        }*/
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            timeSnake = 0.25f;
            ShiftText.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            timeSnake = 0.75f;
            ShiftText.SetActive(false);
        }
        
        if (turn == true)
        {
            if(Where == where.WS)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    turn = false;
                    Where = where.AD;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                    turn = false;
                    Where = where.AD;
                }
            }
            if(Where == where.AD)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    turn = false;
                    Where = where.WS;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    turn = false;
                    Where = where.WS;
                }
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                for(int i = 0; i < PreviousPositions.Length; i++)
                {
                    print(PreviousPositions[i]);
                    print(Bodies[i]);
                }
            }
        }
        
    }

    void MoveOneWay()
    {
        MovePrevPos();
        MovePrevQut();
        PreviousPositions[0] = transform.position;
        PreviousRotations[0] = transform.rotation;
        transform.Translate(Vector3.forward + PositionINeed,Space.Self);  
    }

    private void MovePrevQut()
    {
        for (int i = ApplesToWin - 1; i > 0; i--)
        {
            PreviousRotations[i] = PreviousRotations[i - 1];
        }
    }

    private void MovePrevPos()
    {
        for(int i = ApplesToWin - 1; i > 0; i--)
        {
            PreviousPositions[i] = PreviousPositions[i-1];
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "SnakeBody")
        {
            if(State == state.alive)
            {
                BOOM.Play();
                aud.PlayOneShot(BOOMSound);
            }
            State = state.dead;
            Invoke("RestartLevel",2f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Apple")
        {
            Destroy(other.gameObject);
            aud.PlayOneShot(AppleEatSound);
            Apples += 1;
            if(State == state.alive && SpawnsBody)
            {
                SpawnBody();
            }
        }
        
    }

    private void SpawnBody()
    {
        Instantiate(body);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LoadNextLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }
    public void ContinueButton()
    {
        Panel.SetActive(false);
        State = state.alive;
    }
}
