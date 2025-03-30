using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs.Timer;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScene : MonoBehaviour
{
    private float focusSeconds;
    private float restSeconds;
    private float longRestSeconds;
    private int sessionQtd;

    [SerializeField] private TMP_Text focusTimeText;
    [SerializeField] private TMP_Text restTimeText;
    [SerializeField] private TMP_Text longRestTimeText;
    
    [SerializeField] private TMP_Text sessionQtdText;

    void Start()
    {
        focusSeconds = 1500;
        restSeconds = 300; 
        longRestSeconds = 1800;

        sessionQtd = 1;
        sessionQtdText.text = $"{sessionQtd:00}";
        FormatTimeTexts();
    }

    public void IncreaseFocusTime()
    {
        if (longRestSeconds >= 356400) return;
        focusSeconds += 1500;
        restSeconds += 300; 
        longRestSeconds += 1800;
        FormatTimeTexts();
    }

    public void DecreaseFocusTime()
    {
        if (focusSeconds <= 1500) return;
        focusSeconds -= 1500;
        restSeconds -= 300;
        longRestSeconds -= 1800;
        FormatTimeTexts();
    }

    public void IncreaseSessionQtd()
    {
        if (sessionQtd >= 99) return;
        sessionQtd++;
        sessionQtdText.text = $"{sessionQtd:00}";
    }

    public void DecreaseSessionQtd()
    {
        if (sessionQtd <= 1) return;
        sessionQtd--;
        sessionQtdText.text = $"{sessionQtd:00}";
    }

    public void OnNextButton()
    {
        TimerConfigs.FocusTime = focusSeconds;
        TimerConfigs.RestTime = restSeconds;
        TimerConfigs.LongRestTime = longRestSeconds;
        TimerConfigs.TotalSessions = sessionQtd;
        
        SceneManager.LoadScene("AppRestriction");
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("Estante Scene");
    }

    void FormatTimeTexts()
    {
        var focusHoursText = (int)focusSeconds/3600;
        var focusMinutesText = (focusSeconds/60)%60;
        focusTimeText.text = $"{focusHoursText:00}:{focusMinutesText:00}";
        
        var restHoursText = (int)restSeconds/3600;
        var restMinutesText = (restSeconds/60)%60;
        restTimeText.text = $"{restHoursText:00}:{restMinutesText:00}";

        var longRestHoursText = (int)longRestSeconds/3600;
        var longRestMinutesText = (longRestSeconds/60)%60;
        longRestTimeText.text = $"{longRestHoursText:00}:{longRestMinutesText:00}";
    }
}
