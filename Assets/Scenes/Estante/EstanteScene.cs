using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EstanteScene : MonoBehaviour
{
    public void OnTimerlicked() => SceneManager.LoadScene("ConfigTimerScene");
    public void OnSettingsClicked() => SceneManager.LoadScene("Configuracoes Scene");
}
