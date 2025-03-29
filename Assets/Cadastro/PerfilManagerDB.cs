using System;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

[System.Serializable]
public class PerfilDTO
{
    public string id;
    public string username;
    public string email;
    public DateTime dataNascimento;
    public string senha;
}

public class PerfilManagerDB
{
    /// <summary>
    /// Converte o objeto Perfil em JSON utilizando o JsonUtility e envia uma requisição POST.
    /// </summary>
    public async Task<string> EnviarPerfilParaRegistro(Perfil perfil)
    {
        // Cria o DTO e formata a data para dd-MM-yyyy
        PerfilDTO perfilData = new PerfilDTO
        {
            username = perfil.Username,
            email = perfil.Email,
            dataNascimento = perfil.DataNascimento,
            senha = perfil.Senha
        };

        Debug.Log("----@----");
        Debug.Log(perfilData.username);
        Debug.Log(perfilData.email);
        Debug.Log(perfilData.dataNascimento.ToString("dd-MM-yyyy"));
        Debug.Log(perfilData.senha);

        // Serializa usando JsonUtility
        string json = JsonUtility.ToJson(perfilData);

        using (HttpClient client = new HttpClient())
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // colocar link do ngrok.
            HttpResponseMessage response = await client.PostAsync(Constants.BACKEND_URL + "/auth/registrar", content);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.Created)
                throw new Exception($"Falha no registro. Status: {response.StatusCode}. Resposta: {responseBody}");

            return responseBody;
        }
    }

    public async Task<Perfil> ObterPerfil(string token)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            HttpResponseMessage response = 
                await client.GetAsync(Constants.BACKEND_URL + "/usuario");
            
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao buscar perfil: {responseBody}");

            PerfilDTO perfilData = JsonUtility.FromJson<PerfilDTO>(responseBody);
            
            Perfil perfil = new Perfil(perfilData.username, perfilData.email, "senha_temporaria")
            {
                Id = perfilData.id,
                DataNascimento = perfilData.dataNascimento
            };
            
            return perfil;
        }
    }
    
    public async Task<string> AtualizarPerfil(Perfil perfil, string token)
    {
        PerfilDTO perfilData = new PerfilDTO
        {
            id = perfil.Id,
            username = perfil.Username,
            email = perfil.Email,
            dataNascimento = perfil.DataNascimento
        };

        string json = JsonUtility.ToJson(perfilData);

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(
                $"{Constants.BACKEND_URL}/usuario/{perfil.Id}", content);
            
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao atualizar perfil: {responseBody}");

            return responseBody;
        }
    }
}