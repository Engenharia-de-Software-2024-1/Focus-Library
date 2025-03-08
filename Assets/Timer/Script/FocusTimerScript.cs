using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TimerState { Focus, Rest, Idle }

public class FocusTimerScript : MonoBehaviour
{
    [Header("Sessão Config")]
    [Tooltip("Tempo de foco em segundos (Default: 1500 para 25 minutos)")]
    public float focusTimeInSeconds = 1500f;
    [Tooltip("Tempo de descanso em segundos (Default: 300 para 5 minutos)")]
    public float restTimeInSeconds = 300f;
    [Tooltip("Número total de sessões")]
    public int totalSessions = 1; // Irá receber da tela de configurações.

    [Header("UI Elementos")]
    [Tooltip("Texto que exibe o timer (00:00:00)")]
    public TMP_Text timerDisplay;
    [Tooltip("Botão para desistir")]
    public Button quitButton;
    [Tooltip("Texto para exibir o modo (ex.: 'Modo Foco' ou 'Modo Descanso')")]
    public TMP_Text modeDisplay;

    private float currentTime;
    private TimerState currentState = TimerState.Idle;
    private int completedFocusSessions = 0;
    private float totalFocusTimeSpent = 0f;
    private float totalRestTimeSpent = 0f;

    void Start()
    {
        StartFocusSession();
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitSession);
    }

    void Update()
    {
        if (currentState == TimerState.Idle)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            if (currentState == TimerState.Focus)
            {
                totalFocusTimeSpent += focusTimeInSeconds;
                StartRestSession();
            }
            else if (currentState == TimerState.Rest)
            {
                totalRestTimeSpent += restTimeInSeconds;
                completedFocusSessions++;
                if (completedFocusSessions >= totalSessions)
                    EndAllSessions();
                else
                    StartFocusSession();
            }
        }
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int hours = Mathf.FloorToInt(currentTime / 3600);
        int minutes = Mathf.FloorToInt((currentTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    void StartFocusSession()
    {
        currentState = TimerState.Focus;
        currentTime = focusTimeInSeconds;
        if (modeDisplay != null)
            modeDisplay.text = "Modo Foco";
    }

    void StartRestSession()
    {
        currentState = TimerState.Rest;
        currentTime = restTimeInSeconds;
        if (modeDisplay != null)
            modeDisplay.text = "Modo Descanso";
    }

    public void QuitSession() //Caso clique no botão de desistir.
    {
        if (currentState == TimerState.Focus)
            totalFocusTimeSpent += (focusTimeInSeconds - currentTime);
        else if (currentState == TimerState.Rest)
            totalRestTimeSpent += (restTimeInSeconds - currentTime);

        currentState = TimerState.Idle;
        currentTime = 0;
        EndAllSessions();
    }

    void EndAllSessions() // Envia os dados concluidos dessas sessões para DataManager.
    {
        if (DataManager.Instance != null)
            DataManager.Instance.SaveStatistics(completedFocusSessions, totalFocusTimeSpent, totalRestTimeSpent);
    }
}
