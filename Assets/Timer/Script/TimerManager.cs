using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimerState { Focus, Rest, Idle }

public class TimerManager : MonoBehaviour {
    //Valores Default em segundos.
    [SerializeField] private float focusTime = 1500f;
    [SerializeField] private float restTime = 300f; //Pensando agora, falta implementar como será a questão do descanso longo.
    [SerializeField] private int totalSessions = 1; // Receberá o número de sessões da configuração de sessão.(como será?)
    public TimerState CurrentState { get; private set; } = TimerState.Focus;
    public float CurrentTime { get; private set; }

    //As próximas váriaveis seram enviadas para o DataManager.(São os valores totais do tempo de foco, descanso e sessões de foco completadas).
    private int completedSessions = 0;
    private float totalFocusTime = 0f;
    private float totalRestTime = 0f;

    public void StartTimer() {
        CurrentState = TimerState.Focus;
        CurrentTime = focusTime;
    }

    public void UpdateTimer() {
        if (CurrentState == TimerState.Idle) return;
        CurrentTime -= Time.deltaTime;
        if (CurrentTime <= 0) {
            if (CurrentState == TimerState.Focus) {
                totalFocusTime += focusTime;
                StartRest();
            } else {
                totalRestTime += restTime;
                completedSessions++;

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
            totalFocusTime += focusTime - CurrentTime;
         } else if (CurrentState == TimerState.Rest) {
            totalRestTime += restTime - CurrentTime;
         }
        EndSession();
    }

    private void StartFocus() {
        CurrentState = TimerState.Focus;
        CurrentTime = focusTime;
    }

    private void StartRest() {
        CurrentState = TimerState.Rest;
        CurrentTime = restTime;
    }

    private void EndSession() {
        CurrentState = TimerState.Idle;
        CurrentTime = 0;
        
        if (DataManager.Instance != null) {
            DataManager.Instance.SetStatistics(completedSessions, totalFocusTime, totalRestTime);
        } else {
            Debug.LogWarning("DataManager não foi encontrado na cena!");
        }
    }
}
