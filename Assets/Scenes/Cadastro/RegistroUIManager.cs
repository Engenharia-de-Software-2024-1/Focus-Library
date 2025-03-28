using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        botaoRegistrar.onClick.AddListener(() => StartCoroutine(TentarRegistrar()));
    }

    private IEnumerator TentarRegistrar()
    {
        string username = inputUsername.text.Trim();
        string email = inputEmail.text.Trim();
        string senha = inputSenha.text;
        string confirmarSenha = inputConfirmarSenha.text;

        if(!checker.Check(username, email, senha, confirmarSenha)) yield return null;

        Task<Perfil> tarefa = perfilManager.CriarPerfil(username, email, senha);
        while (!tarefa.IsCompleted) yield return null;
        
        Debug.Log("Registro concluído com sucesso!");
    }
}