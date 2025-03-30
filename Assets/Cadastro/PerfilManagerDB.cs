using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Constants;
using Networking;

[System.Serializable]
public class PerfilDTO
{
    public string id;
    public string username;
    public string email;
    public string dataNascimento; // Mantido como string para controlar o formato
    public string senha;
}

[System.Serializable]
public class PerfilAtualizacaoDTO
{
    public string username;
    public string email;
    public string dataNascimento;
    // Não inclui o ID pois será enviado apenas na URL
}

public class PerfilManagerDB
{
    /// <summary>
    /// Converte o objeto Perfil em JSON utilizando o JsonUtility e envia uma requisição POST para registrar o perfil.
    /// </summary>
    public async Task<string> EnviarPerfilParaRegistro(Perfil perfil)
    {
        // Cria o DTO e formata a data para yyyy/MM/dd para o servidor se existir
        PerfilDTO perfilData = new PerfilDTO
        {
            username = perfil.Username,
            email = perfil.Email,
            dataNascimento = perfil.DataNascimento.HasValue ? perfil.DataNascimento.Value.ToString("yyyy/MM/dd") : null,
            senha = perfil.Senha
        };

        Debug.Log("----@----");
        Debug.Log(perfilData.username);
        Debug.Log(perfilData.email);
        Debug.Log(perfilData.dataNascimento ?? "Data não informada");
        Debug.Log(perfilData.senha);

        // Serializa usando JsonUtility
        string json = JsonUtility.ToJson(perfilData);

        var networkingClient = new NetworkingClient();
        var client = networkingClient.Client;

        var content = new StringContent(json, Encoding.UTF8, "application/json"); // colocar link do ngrok.
        HttpResponseMessage response = await client.PostAsync(NetworkingConstants.BACKEND_URL + "/auth/registrar", content);
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.StatusCode != HttpStatusCode.Created)
            throw new Exception($"Falha no registro. Status: {response.StatusCode}. Resposta: {responseBody}");

        return responseBody;
    }

    /// <summary>
    /// Obtém o perfil do usuário utilizando o token JWT.
    /// </summary>
    public async Task<Perfil> ObterPerfil(string token)
    {
        var client = (new NetworkingClient()).Client;

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        HttpResponseMessage response = await client.GetAsync(NetworkingConstants.BACKEND_URL + "/usuario");
        
        string responseBody = await response.Content.ReadAsStringAsync();
        Debug.Log($"Resposta do servidor ao obter perfil: {responseBody}");

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao buscar perfil: {response.StatusCode}, {responseBody}");

        PerfilDTO perfilData = JsonUtility.FromJson<PerfilDTO>(responseBody);
        
        // Cria o perfil com dados básicos
        Perfil perfil = new Perfil(perfilData.username, perfilData.email, "senha_temporaria")
        {
            Id = perfilData.id
        };
        
        // Processa a data de nascimento apenas se existir no perfil DTO
        if (!string.IsNullOrEmpty(perfilData.dataNascimento))
        {
            DateTime dataNascimento;
            if (DateTime.TryParseExact(perfilData.dataNascimento, "yyyy/MM/dd", 
                System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.None, out dataNascimento))
            {
                perfil.DataNascimento = dataNascimento;
            }
            else if (DateTime.TryParse(perfilData.dataNascimento, out dataNascimento))
            {
                perfil.DataNascimento = dataNascimento;
            }
            else
            {
                Debug.LogWarning($"Não foi possível parsear a data de nascimento: {perfilData.dataNascimento}");
                perfil.DataNascimento = null;
            }
        }
        else
        {
            perfil.DataNascimento = null;
        }
        
        return perfil;
    }
    
    /// <summary>
    /// Atualiza o perfil do usuário no backend.
    /// </summary>
    public async Task AtualizarPerfil(Perfil perfil, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("O token está vazio ou nulo");
            throw new Exception("Token de autenticação inválido");
        }
        
        Debug.Log($"Token usado para autorização: {token.Substring(0, Math.Min(10, token.Length))}...");
        
        // Cria o objeto DTO específico para atualização (sem o ID)
        PerfilAtualizacaoDTO perfilData = new PerfilAtualizacaoDTO
        {
            username = perfil.Username,
            email = perfil.Email,
            // Formata a data no padrão "yyyy-MM-dd" conforme especificado
            dataNascimento = perfil.DataNascimento.HasValue ? 
                perfil.DataNascimento.Value.ToString("yyyy-MM-dd") : null
        };

        // Serializa o objeto para JSON
        string json = JsonUtility.ToJson(perfilData);
        Debug.Log($"Enviando dados para atualização: {json}");

        // Realiza o PUT para atualizar o perfil no endpoint /usuario/dadosgerais/{id}
        var client = (new NetworkingClient()).Client;

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        // Exibe os headers enviados para debug
        Debug.Log("Headers enviados:");
        foreach (var header in client.DefaultRequestHeaders)
        {
            Debug.Log($"{header.Key}: {string.Join(", ", header.Value)}");
        }
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Debug.Log($"Content-Type: {content.Headers.ContentType}");
        
        string putUrl = NetworkingConstants.BACKEND_URL + "/usuario/dadosgerais/" + perfil.Id;
        Debug.Log($"Chamando PUT: {putUrl}");
        
        HttpResponseMessage putResponse = await client.PutAsync(putUrl, content);
        string putResponseBody = await putResponse.Content.ReadAsStringAsync();
        Debug.Log($"Resposta da atualização: StatusCode={putResponse.StatusCode}, Body={putResponseBody}");

        if (!putResponse.IsSuccessStatusCode)
        {
            if (putResponse.StatusCode == HttpStatusCode.Forbidden)
            {
                Debug.LogError("Erro 403 Forbidden - Verifique se o token está válido e tem permissões corretas");
                Debug.LogError($"Token usado: {token.Substring(0, Math.Min(20, token.Length))}...");
            }
            
            throw new Exception($"Erro ao atualizar perfil: {putResponse.StatusCode}, {putResponseBody}");
        }
    }

    /// <summary>
    /// Realiza o refresh do token JWT utilizando o refresh token salvo nos PlayerPrefs.
    /// </summary>
    public async Task<string> RefreshToken()
    {
        // Recupera o refresh token salvo nos PlayerPrefs
        string refreshToken = PlayerPrefs.GetString("REFRESH_TOKEN");
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new Exception("Refresh token não encontrado nos PlayerPrefs!");
        }

        Debug.Log($"RefreshToken recuperado: {refreshToken.Substring(0, Math.Min(10, refreshToken.Length))}...");

        // Realiza o GET para renovar o token JWT usando o refresh token
        var client = (new NetworkingClient()).Client;
    
        string refreshUrl = $"{NetworkingConstants.BACKEND_URL}/auth/refresh?refreshToken={refreshToken}";
        Debug.Log($"Chamando GET: {refreshUrl}");
        
        HttpResponseMessage refreshResponse = await client.GetAsync(refreshUrl);
        string newToken = await refreshResponse.Content.ReadAsStringAsync();
        Debug.Log($"Resposta do refresh: StatusCode={refreshResponse.StatusCode}");

        if (!refreshResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Erro ao renovar token: {refreshResponse.StatusCode}, {newToken}");
        }

        // Atualiza o novo token no PlayerPrefs
        PlayerPrefs.SetString("JWT_TOKEN", newToken);
        PlayerPrefs.Save();
        Debug.Log("Novo token salvo no PlayerPrefs.");

        return newToken;
    }

    /// <summary>
    /// Atualiza o perfil e renova o token, combinando as duas operações.
    /// </summary>
    public async Task<string> AtualizarPerfilComRefresh(Perfil perfil, string token)
    {
        await AtualizarPerfil(perfil, token);
        return await RefreshToken();
    }
}