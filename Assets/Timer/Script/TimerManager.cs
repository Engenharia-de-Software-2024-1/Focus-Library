using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimerState { Focus, Rest, Idle }

public class TimerManager : MonoBehaviour {
    // Valores default em segundos -> (Deve receber da tela de configuração de sessão).
    [SerializeField] private float focusTime = 1500f;       
    [SerializeField] private float restTime = 300f;           
    [SerializeField] private float longRestTime = 1800f;     
    [SerializeField] private int totalSessions = 1;           

    public TimerState CurrentState { get; private set; } = TimerState.Focus;
    public float CurrentTime { get; private set; }

    private int focusSessionsCount = 0;       // Conta as sessões de foco consecutivas
    private int completedSessions = 0;        // Conta os ciclos completos (foco + descanso)

    // Variáveis para armazenar os dados do ciclo atual
    private float lastFocusDuration = 0f;
    private float lastRestDuration = 0f;
    private float currentRestDuration = 0f;   // Guarda o tempo de descanso corrente (curto ou longo)

    public void StartTimer() {
        CurrentState = TimerState.Focus;
        CurrentTime = focusTime;
    }

    public void UpdateTimer() {
        if (CurrentState == TimerState.Idle)
            return;

        CurrentTime -= Time.deltaTime;

        if (CurrentTime <= 0) {
            if (CurrentState == TimerState.Focus) {
                lastFocusDuration = (int)focusTime;
                focusSessionsCount++;

                if (focusSessionsCount % 4 == 0) {
                    StartLongRest();
                } else {
                    StartRest();
                }

            } else if (CurrentState == TimerState.Rest) {
                lastRestDuration = (int)currentRestDuration;

                if (DataManager.Instance != null) {
                    DataManager.Instance.AddSession((int)lastFocusDuration, (int)lastRestDuration);
                } else {
                    Debug.LogWarning("DataManager não foi encontrado na cena!");
                }

                completedSessions++;

                // Verifica se completou o número total de sessões configuradas
                if (completedSessions >= totalSessions) {
                    EndSession();
                } else {
                    StartFocus();
                }
            }
        }
    }

    public void Quit() {
        if (CurrentState == TimerState.Focus) {
            lastFocusDuration = (int)(focusTime - CurrentTime);
        } else if (CurrentState == TimerState.Rest) {
            lastRestDuration = (int)(currentRestDuration - CurrentTime);
        }

        if (DataManager.Instance != null) {
            DataManager.Instance.AddSession((int)lastFocusDuration, (int)lastRestDuration);
        }
        EndSession();
    }

    private void StartFocus() {
        CurrentState = TimerState.Focus;
        CurrentTime = focusTime;
    }

    private void StartRest() {
        CurrentState = TimerState.Rest;
        currentRestDuration = restTime;
        CurrentTime = restTime;
    }

    private void StartLongRest() {
        CurrentState = TimerState.Rest;
        currentRestDuration = longRestTime;
        CurrentTime = longRestTime;
    }

    private void EndSession() {
        CurrentState = TimerState.Idle;
        CurrentTime = 0;
        Debug.Log("Sessão encerrada. Total de ciclos completados: " + completedSessions);
        if (DataManager.Instance != null) {
            DataManager.Instance.SendActivityData();
        }
    }
}
