using UnityEngine;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }
    // pegar o link do ngrok do comando ngrok http 8080
    [SerializeField] private string loginEndpoint = "https://1135-177-73-205-176.ngrok-free.app/auth/login"; //"http://localhost:8080/auth/login"; 
    [SerializeField] private LoginUIHandler loginUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void HandleLogin(string username, string password)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                LoginData loginData = new LoginData { 
                    username = username, 
                    senha = password 
                };

                string json = JsonUtility.ToJson(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(loginEndpoint, content);
                string responseJson = await response.Content.ReadAsStringAsync();

                APILoginResponse apiResponse = JsonUtility.FromJson<APILoginResponse>(responseJson);

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(apiResponse.acessToken))
                {
                    Debug.Log("Login bem-sucedido!");
                    SaveTokens(apiResponse.acessToken, apiResponse.refreshToken);
                    loginUI.ClearFields();
                }
                else
                {
                    // Tratamento específico para 403 Forbidden
                    if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        
                        Debug.LogError("Login Inválido");
                        loginUI.ShowErrorMessage("Login Invalido");
                        
                    }
                    else
                    {
                        Debug.LogError($"Erro: {response.StatusCode}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Erro de conexão: {ex.Message}");
                
            }
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