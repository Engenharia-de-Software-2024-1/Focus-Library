using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TestePerfilManager : MonoBehaviour
{
    public TMP_InputField inputUsername;
    public TMP_InputField inputEmail;
    public TMP_InputField inputSenha;
    public TMP_InputField inputNovoUsername;
    public TMP_InputField inputNovaSenha;
    public TextMeshProUGUI txtLog;

    private GerenciadorPerfis gerenciador;
    private Guid perfilIdAtual; // Armazena o ID do último perfil cadastrado

    void Start()
    {
        gerenciador = new GerenciadorPerfis();
        txtLog.text = "Sistema pronto.\n";
    }

    // Cadastrar novo perfil
    public void CadastrarPerfil()
    {
        try
        {
            string username = inputUsername.text;
            string email = inputEmail.text;
            string senha = inputSenha.text;

            Perfil novoPerfil = gerenciador.CadastrarPerfil(username, email, senha);
            perfilIdAtual = novoPerfil.UserId; // Salva o ID para uso futuro

            txtLog.text += $"Perfil criado! ID: {novoPerfil.UserId}\n";
        }
        catch (ArgumentException ex)
        {
            txtLog.text += $"Erro: {ex.Message}\n";
        }
    }

    // Atualizar username
    public void AtualizarUsername()
    {
        if (perfilIdAtual == Guid.Empty)
        {
            txtLog.text += "Nenhum perfil cadastrado.\n";
            return;
        }

        try
        {
            Perfil perfil = gerenciador.BuscarPerfil(perfilIdAtual);
            string novoUsername = inputNovoUsername.text;

            perfil.AtualizarPerfil(
                perfil.NomeCompleto,
                novoUsername,
                perfil.Email,
                perfil.DataNascimento,
                perfil.FotoPerfil
            );

            txtLog.text += $"Username atualizado para: {novoUsername}\n";
        }
        catch (ArgumentException ex)
        {
            txtLog.text += $"Erro: {ex.Message}\n";
        }
    }

    // Alterar senha
    public void AlterarSenha()
    {
        if (perfilIdAtual == Guid.Empty)
        {
            txtLog.text += "Nenhum perfil cadastrado.\n";
            return;
        }

        try
        {
            Perfil perfil = gerenciador.BuscarPerfil(perfilIdAtual);
            string novaSenha = inputNovaSenha.text;

            perfil.AlterarSenha(novaSenha);
            txtLog.text += "Senha alterada com sucesso!\n";
        }
        catch (ArgumentException ex)
        {
            txtLog.text += $"Erro: {ex.Message}\n";
        }
    }
}