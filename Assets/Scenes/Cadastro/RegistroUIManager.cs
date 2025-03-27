using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RegistroUIManager : MonoBehaviour
{
    public TMP_InputField inputUsername;
    public TMP_InputField inputEmail;
    public TMP_InputField inputSenha;
    public TMP_InputField inputConfirmarSenha;
    public Button botaoRegistrar;

    private PerfilManager perfilManager;
    private CadastroCheckFields checker;
    private void Start()
    {
        checker = new CadastroCheckFields();
        perfilManager = new PerfilManager();
        // botaoRegistrar.onClick.AddListener(() => StartCoroutine(TentarRegistrar()));
    }

    public async void TentarRegistrar()
    {
        string username = inputUsername.text.Trim();
        string email = inputEmail.text.Trim();
        string senha = inputSenha.text;
        string confirmarSenha = inputConfirmarSenha.text;


        Debug.Log(username);
        Debug.Log(email);
        Debug.Log(senha);
        Debug.Log(confirmarSenha);

        if(!checker.Check(username, email, senha, confirmarSenha)) return;

        try
        {
            Perfil perfil = await perfilManager.CriarPerfil(username, email, senha);
            Debug.Log("Registro concluído com sucesso!");
            SceneManager.LoadScene("Login Scene");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao registrar: {ex.Message}");
        }
    }
}

