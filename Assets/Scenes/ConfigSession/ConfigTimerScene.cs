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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseFocusTime()
    {
        focusSeconds += 1500;
        restSeconds += 300; 
        longRestSeconds += 1800;
        FormatTimeTexts();
    }

    public void DecreaseFocusTime()
    {
        if (focusSeconds <= 0) return;
        focusSeconds -= 1500;
        restSeconds -= 300;
        longRestSeconds -= 1800;
        FormatTimeTexts();
    }

    public void IncreaseSessionQtd()
    {
        sessionQtd++;
        sessionQtdText.text = (sessionQtd).ToString();
    }

    public void DecreaseSessionQtd()
    {
        if (sessionQtd <= 0) return;
        sessionQtd--;
        sessionQtdText.text = (sessionQtd).ToString();
    }

    public void OnNextButton()
    {
        TimerConfigs.FocusTime = focusSeconds;
        TimerConfigs.RestTime = restSeconds;
        TimerConfigs.LongRestTime = longRestSeconds;
        TimerConfigs.TotalSessions = sessionQtd;
        
        SceneManager.LoadScene("Timer");
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
