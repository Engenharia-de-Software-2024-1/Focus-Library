using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Constants;
using Networking;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private string loginEndpoint = NetworkingConstants.BACKEND_URL + "/auth/login";

    public async void HandleLogin(string username, string password, Action onSuccess = null, Action<string> onFailure = null)
    {
        try
        {
            var client = new NetworkingClient().Client;

            LoginData loginData = new LoginData { 
                username = username, 
                senha = password 
            };
            string json = JsonUtility.ToJson(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(loginEndpoint, content);
            string responseJson = await response.Content.ReadAsStringAsync();
            APILoginResponse apiResponse = JsonUtility.FromJson<APILoginResponse>(responseJson);

            if (response.IsSuccessStatusCode)
            {
                Debug.Log("Login bem-sucedido!");
                SaveTokens(apiResponse.acessToken, apiResponse.refreshToken);
                onSuccess?.Invoke();
                SceneManager.LoadScene("Estante Scene");
                if(string.IsNullOrEmpty(apiResponse.acessToken)){
                    Debug.Log("O token não veio :(");
                }else{
                    Debug.Log("O token veio :)");
                }
            }
            else
            {
                // Tratamento específico para 403 Forbidden
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    
                    Debug.LogError("Login Inválido");
                    onFailure?.Invoke("Login Invalido");
                    
                }
                else
                {
                    Debug.LogError($"Erro: {response.StatusCode}");
                    onFailure?.Invoke($"Erro: {response.StatusCode}");
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Erro de conexão: {ex.Message}");
            onFailure?.Invoke($"Erro de conexão: {ex.Message}");
        }
    }

    private void SaveTokens(string token, string refreshToken)
    {
        PlayerPrefs.SetString("JWT_TOKEN", token);
        PlayerPrefs.SetString("REFRESH_TOKEN", refreshToken);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    private class LoginData
    {
        public string username;
        public string senha;
    }
}