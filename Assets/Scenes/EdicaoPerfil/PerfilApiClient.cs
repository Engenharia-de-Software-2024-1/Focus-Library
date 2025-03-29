using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class PerfilApiClient : MonoBehaviour
{
    private HttpClient httpClient;

    private void Awake()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://c7ff-177-73-205-176.ngrok-free.app/usuario"); //"http://localhost:8080/usuario"
    }

    public async Task<Perfil> ObterPerfil(string token)
    {
        try
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync("usuario");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonUtility.FromJson<Perfil>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao obter perfil: {ex.Message}");
            return null;
        }
    }

    public async Task<string> AtualizarPerfil(Perfil perfil, string token)
    {
        try
        {
            string json = JsonUtility.ToJson(perfil);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PutAsync("usuario", new StringContent(json));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar perfil: {ex.Message}");
            return null;
        }
    }
}