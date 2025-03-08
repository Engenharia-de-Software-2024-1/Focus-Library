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
    [Tooltip("Texto do botão de desistir")]
    public TMP_Text quitButtonText; 

    [Tooltip("Texto para exibir o modo (ex.: 'Modo Foco' ou 'Modo Descanso')")]
    public TMP_Text modeDisplay;

    [Header("Texto Extra (opcional)")]
    [Tooltip("Outro texto qualquer na cena, se quiser mudar a cor")]
    public TMP_Text extraText;

    [Header("Imagem do Botão (ou outra Image)")]
    [Tooltip("A Imagem que representa o fundo do botão ou um elemento de UI a ser recolorido")]
    public Image buttonImage;

    [Header("Background (opcional)")]
    [Tooltip("Imagem de fundo que deve mudar de cor em cada estado")]
    public Image backgroundImage;

    [Header("Focus Colors")]
    public Color focusTextColor       = new Color32(0x81, 0x4E, 0x37, 0xFF); // #814E37
    public Color focusButtonColor     = new Color32(0x50, 0x21, 0x07, 0xFF); // #502107
    public Color focusBackgroundColor = new Color32(0x50, 0x21, 0x07, 0xFF); // #502107

    [Header("Rest Colors")]
    public Color restTextColor       = new Color32(0xD1, 0x6F, 0x3E, 0xFF);  // #D16F3E
    public Color restButtonColor     = new Color32(0xC5, 0x7E, 0x5E, 0xFF);  // #C57E5E
    public Color restBackgroundColor = new Color32(0xC5, 0x7E, 0x5E, 0xFF);  // #C57E5E

    [Header("Idle Colors")] //Por enquanto só coloquei para saber como o looping ta funcionando.
    public Color idleTextColor       = Color.white;
    public Color idleButtonColor     = Color.gray;
    public Color idleBackgroundColor = Color.gray;

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
        {
            modeDisplay.text = "Modo Foco";
            modeDisplay.color = focusTextColor;
        }
        if (timerDisplay != null)
            timerDisplay.color = focusTextColor;
        if (extraText != null)
            extraText.color = focusTextColor;
        if (buttonImage != null)
            buttonImage.color = focusButtonColor;
            quitButtonText.color = focusTextColor;
        if (backgroundImage != null)
            backgroundImage.color = focusBackgroundColor;
    }

    void StartRestSession()
    {
        currentState = TimerState.Rest;
        currentTime = restTimeInSeconds;

        if (modeDisplay != null)
        {
            modeDisplay.text = "Modo Descanso";
            modeDisplay.color = restTextColor;
        }
        if (timerDisplay != null)
            timerDisplay.color = restTextColor;
        if (extraText != null)
            extraText.color = restTextColor;
        if (buttonImage != null)
            buttonImage.color = restButtonColor;
            quitButtonText.color = restTextColor;
        if (backgroundImage != null)
            backgroundImage.color = restBackgroundColor;
    }

    public void QuitSession()
    {
        if (currentState == TimerState.Focus)
            totalFocusTimeSpent += (focusTimeInSeconds - currentTime);
        else if (currentState == TimerState.Rest)
            totalRestTimeSpent += (restTimeInSeconds - currentTime);

        currentState = TimerState.Idle;
        currentTime = 0;
        EndAllSessions();
    }

    void EndAllSessions()
    {
        currentState = TimerState.Idle;
        currentTime = 0;

        if (modeDisplay != null)
        {
            modeDisplay.text = "Sessão Encerrada";
            modeDisplay.color = idleTextColor;
        }
        if (timerDisplay != null)
            timerDisplay.color = idleTextColor;
        if (extraText != null)
            extraText.color = idleTextColor;
        if (buttonImage != null)
            buttonImage.color = idleButtonColor;
            quitButtonText.color = idleTextColor;
        if (backgroundImage != null)
            backgroundImage.color = idleBackgroundColor;

        if (DataManager.Instance != null)
            DataManager.Instance.SaveStatistics(completedFocusSessions, totalFocusTimeSpent, totalRestTimeSpent);

        Debug.Log("Sessões finalizadas.");
        Debug.Log("Sessões concluídas: " + completedFocusSessions);
        Debug.Log("Tempo total de foco: " + totalFocusTimeSpent + "s");
        Debug.Log("Tempo total de descanso: " + totalRestTimeSpent + "s");
    }
}
