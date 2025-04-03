using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs.Timer;
using Notifications;

public enum TimerState { Focus, Rest, Idle }

public class TimerManager : MonoBehaviour {
    public float focusTime;
    public float restTime;
    public float longRestTime;
    public int totalSessions;
    public TimerState CurrentState { get; private set; } = TimerState.Focus;
    public float CurrentTime { get; private set; }
    private int focusSessionsCount = 0;               
    private float currentRestDuration = 0f;   

    //As próximas váriaveis seram enviadas para o DataManager.(São os valores totais do tempo de foco, descanso e sessões de foco completadas).
    private int completedSessions = 0;
    private float totalFocusTime = 0f;
    private float totalRestTime = 0f;

    ISupressNotifications supressNotifications;

    public void StartTimer() {
        focusTime = TimerConfigs.FocusTime;
        restTime = TimerConfigs.RestTime; 
        longRestTime = TimerConfigs.LongRestTime;
        totalSessions = TimerConfigs.TotalSessions;
        
        CurrentState = TimerState.Focus;
        CurrentTime = focusTime;
        supressNotifications = new SupressNotifications();
        supressNotifications.SupressAllNotifications();
    }

    public void UpdateTimer() {
        if (CurrentState == TimerState.Idle) return;
            CurrentTime -= Time.deltaTime;
        if (CurrentTime <= 0) {
            if (CurrentState == TimerState.Focus) {
                totalFocusTime += focusTime;
                focusSessionsCount++;
                if (focusSessionsCount % 4 == 0) {
                    StartLongRest();
                } else {
                    StartRest();
                }
            } else {
                totalRestTime += currentRestDuration;
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
        }else if (CurrentState == TimerState.Rest){
            totalRestTime += currentRestDuration - CurrentTime;
        }
        EndSession();
    }

    private void StartFocus() {
        supressNotifications.SupressAllNotifications();
        CurrentState = TimerState.Focus;
        CurrentTime = focusTime;
    }
    private void StartRest() {
        supressNotifications.StartAllNotifications();
        CurrentState = TimerState.Rest;
        currentRestDuration = restTime;
        CurrentTime = restTime;
    }

    private void StartLongRest(){
        supressNotifications.StartAllNotifications();
        CurrentState = TimerState.Rest;
        currentRestDuration = longRestTime;
        CurrentTime = longRestTime;
    }
    private void EndSession() {
        supressNotifications.StartAllNotifications();
        CurrentState = TimerState.Idle;
        CurrentTime = 0;
    }
}
