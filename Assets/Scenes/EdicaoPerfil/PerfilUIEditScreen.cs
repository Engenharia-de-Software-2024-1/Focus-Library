using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PerfilEditScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField dataNascimentoInput;
    [SerializeField] private Button salvarButton;

    private PerfilManager perfilManager;
    private Perfil perfilAtual;

    private void Start()
    {
        perfilManager = new PerfilManager();
        salvarButton.onClick.AddListener(SalvarPerfil);
        CarregarPerfilAtual(); 
    }

    private async void CarregarPerfilAtual()
    {
        string token = PlayerPrefs.GetString("JWT_TOKEN");
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("Token de autenticação não encontrado!");
            return;
        }

        PerfilManagerDB managerDB = new PerfilManagerDB();
        try
        {
            perfilAtual = await managerDB.ObterPerfil(token);
            PreencherCampos();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Falha ao carregar perfil: {ex.Message}");
        }
    }

    private void PreencherCampos()
    {
        usernameInput.text = perfilAtual.Username;
        emailInput.text = perfilAtual.Email;
        dataNascimentoInput.text = perfilAtual.DataNascimento.ToString("dd/MM/yyyy");
    }

    private async void SalvarPerfil()
    {
        try
        {
            DateTime dataNascimento = DateTime.Parse(dataNascimentoInput.text);
            await perfilManager.EditarPerfil(
                perfilAtual, 
                usernameInput.text, 
                emailInput.text, 
                dataNascimento
            );
            Debug.Log("Perfil atualizado com sucesso!");
            gameObject.SetActive(false);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao salvar perfil: {ex.Message}");
        }
    }
}