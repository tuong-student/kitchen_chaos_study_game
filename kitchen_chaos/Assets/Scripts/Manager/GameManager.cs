using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;

    public static GameManager Instance {get; private set;}

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;
    private bool isGamePause = false;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    ChangeState(State.CountdownToStart);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer < 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMax;
                    ChangeState(State.GamePlaying);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer < 0f)
                {
                    ChangeState(State.GameOver);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    private void GameInput_OnPauseAction(object sender, System.EventArgs e)
    {
        PauseGame();
    }

    private void ChangeState(State state)
    {
        this.state = state;
        this.OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer/gamePlayingTimerMax);
    }

    public void PauseGame()
    {
        isGamePause = !isGamePause;
        if(isGamePause)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPause?.Invoke(this, EventArgs.Empty);
        }
    }
}
