using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIColorManager : MonoBehaviour {
    [SerializeField] private TMP_Text timerDisplay;
    [SerializeField] private TMP_Text quitButtonText;
    [SerializeField] private TMP_Text modeDisplay;
    [SerializeField] private TMP_Text extraText;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image backgroundImage;

    public void UpdateColors(TimerState state) {
        Color corTexto, corBotao, corFundo;
        string modo = "";

        switch (state) {
            case TimerState.Focus:
                corTexto = new Color32(0x81, 0x4E, 0x37, 0xFF); 
                corBotao = new Color32(0x50, 0x21, 0x07, 0xFF); 
                corFundo = new Color32(0x50, 0x21, 0x07, 0xFF); 
                modo = "Modo Foco";
                break;
            case TimerState.Rest:
                corTexto = new Color32(0xA1, 0x33, 0x11, 0xFF); 
                corBotao = new Color32(0xC5, 0x7E, 0x5E, 0xFF); 
                corFundo = new Color32(0xC5, 0x7E, 0x5E, 0xFF); 
                modo = "Modo Descanso";
                break;
            default: //Só estou usando para saber como o looping está funcionando.
                corTexto = Color.white;
                corBotao = Color.gray;
                corFundo = Color.gray;
                modo = "Sessão Encerrada";
                break;
        }

        if (timerDisplay) timerDisplay.color = corTexto;
        if (quitButtonText) quitButtonText.color = corTexto;
        if (modeDisplay) modeDisplay.color = corTexto;
        if (extraText) extraText.color = corTexto;
        if (buttonImage) buttonImage.color = corBotao;
        if (backgroundImage) backgroundImage.color = corFundo;
        if (modeDisplay) modeDisplay.text = modo;
    }
}
