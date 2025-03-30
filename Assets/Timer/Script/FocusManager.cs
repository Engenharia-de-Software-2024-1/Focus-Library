using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FocusTimerScript : MonoBehaviour {
    //SerializeField serve para mesmo em privado aparecer no inspector do Unity
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private UIColorManager colorManager;
    [SerializeField] private TMP_Text timerDisplay;
    private TimerState lastState;

    private void Start() {
        timerManager.StartTimer();
        lastState = timerManager.CurrentState;
        colorManager.UpdateColors(timerManager.CurrentState);
    }

    private void Update() {
        timerManager.UpdateTimer();
        UpdateTimerDisplay();
        if (timerManager.CurrentState != lastState) {
            colorManager.UpdateColors(timerManager.CurrentState);
            lastState = timerManager.CurrentState;
        }
    }

    private void UpdateTimerDisplay() {
        float tempoAtual = timerManager.CurrentTime;
        int horas = Mathf.FloorToInt(tempoAtual / 3600);
        int minutos = Mathf.FloorToInt((tempoAtual % 3600) / 60);
        int segundos = Mathf.FloorToInt(tempoAtual % 60);

        if (timerDisplay){
            timerDisplay.text = $"{horas:00}:{minutos:00}:{segundos:00}";
        }
    }

    public void OnQuitClicked() {
        timerManager.Quit();
        colorManager.UpdateColors(TimerState.Idle);
        StartCoroutine(waiter(2f));
    }
    IEnumerator waiter(float x){
        yield return new WaitForSeconds(x);
        SceneManager.LoadScene("Estante Scene");
    }

}