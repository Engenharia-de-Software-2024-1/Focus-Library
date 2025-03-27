using System;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

[System.Serializable]
public class PerfilDTO
{
    public string username;
    public string email;
    public string dataNascimento;
    
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
            dataNascimento = perfil.DataNascimento.ToString("dd-MM-yyyy"),
            
        };

        // Serializa usando JsonUtility
        string json = JsonUtility.ToJson(perfilData);

        using (HttpClient client = new HttpClient())
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // colocar link do ngrok.
            HttpResponseMessage response = await client.PostAsync("http://localhost:8080/auth/registrar", content);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.Created)
                throw new Exception($"Falha no registro. Status: {response.StatusCode}. Resposta: {responseBody}");

            return responseBody;
        }
    }
}