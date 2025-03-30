using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfiguracoesScene : MonoBehaviour
{
    public void OnBackClicked() => SceneManager.LoadScene("Estante Scene");
    public void OnEditProfileClicked() => SceneManager.LoadScene("Edicao Perfil Scene");
}
