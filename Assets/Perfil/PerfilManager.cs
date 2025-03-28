using System;
using System.Net.Mail;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Gerencia a lógica de criação e edição de perfis, incluindo validação de e-mail.
/// </summary>
public class PerfilManager
{
    /// <summary>
    /// Cria um novo perfil, valida o e-mail e o envia para registro.
    /// </summary>
    /// <param name="username">Nome de usuário válido.</param>
    /// <param name="email">E-mail válido.</param>
    /// <param name="senha">Senha válida.</param>
    /// <returns>Objeto Perfil criado.</returns>
    /// <exception cref="ArgumentException">Lançada se o e-mail for inválido.</exception>
    public async Task<Perfil> CriarPerfil(string username, string email, string senha)
    {
        if (!ValidarEmail(email))
            throw new ArgumentException("Formato de e-mail inválido.");

        Perfil perfil = new Perfil(username, email, senha);
        await EnviarPerfil(perfil);
        return perfil;
    }

    /// <summary>
    /// Edita um perfil existente, valida o novo e-mail e envia as atualizações.
    /// </summary>
    /// <param name="perfil">Perfil a ser editado.</param>
    /// <param name="novoUsername">Novo nome de usuário válido.</param>
    /// <param name="novoEmail">Novo e-mail válido.</param>
    /// <param name="dataNascimento">Nova data de nascimento.</param>
    /// <returns>Perfil atualizado.</returns>
    /// <exception cref="ArgumentNullException">Lançada se o perfil for nulo.</exception>
    /// <exception cref="ArgumentException">Lançada se o novo e-mail for inválido.</exception>
<<<<<<< HEAD:Assets/Perfil/PerfilManager.cs
    public async Task<Perfil> EditarPerfil(Perfil perfil, string novoUsername, string novoEmail, DateTime? dataNascimento)
=======
    public async Task<Perfil> EditarPerfil(Perfil perfil, string novoUsername, string novoEmail, DateTime dataNascimento)
>>>>>>> 15b7abe (falta a logica de ober o perfil):Assets/Cadastro/PerfilManager.cs
    {
        if (perfil == null)
            throw new ArgumentNullException(nameof(perfil));

        if (!ValidarEmail(novoEmail))
            throw new ArgumentException("Formato de e-mail inválido.");

        perfil.AtualizarPerfil(novoUsername, novoEmail, dataNascimento);
        await EnviarPerfil(perfil);
        return perfil;
    }

    /// <summary>
    /// Valida se um e-mail está em formato correto.
    /// </summary>
    /// <param name="email">E-mail a ser validado.</param>
    /// <returns>True se o e-mail for válido, False caso contrário.</returns>
    private bool ValidarEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Encaminha o perfil para o PerfilManagerDB para registro ou atualização.
    /// </summary>
    /// <param name="perfil">Perfil a ser enviado.</param>
    private async Task EnviarPerfil(Perfil perfil)
    {
        PerfilManagerDB managerDB = new PerfilManagerDB();
        
        if (!string.IsNullOrEmpty(perfil.Id))
        {
            // Se tem ID, atualiza o perfil existente
            string token = PlayerPrefs.GetString("JWT_TOKEN"); // Corrigido para usar JWT_TOKEN em vez de authToken
            try
            {
                // Usa o método AtualizarPerfilComRefresh para atualizar o perfil e renovar o token
                string novoToken = await managerDB.AtualizarPerfilComRefresh(perfil, token);
                Debug.Log("Perfil atualizado e token renovado com sucesso: " + novoToken);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Erro ao atualizar perfil: {ex.Message}");
                throw; // Propaga a exceção para tratamento na camada superior
            }
        }
        else
        {
            // Se não tem ID, faz o registro
            try
            {
                string response = await managerDB.EnviarPerfilParaRegistro(perfil);
                Debug.Log("Resposta do registro: " + response);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Erro ao registrar perfil: {ex.Message}");
                throw; // Propaga a exceção para tratamento na camada superior
            }
        }
    }
}