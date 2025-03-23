using UnityEngine;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }

    [SerializeField] private string loginEndpoint = "http://localhost:8080/auth/login";
    [SerializeField] private LoginUIHandler loginUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém entre cenas importante
        }
        else
        {
            Destroy(gameObject); // destruir para evitar dubplicatas
        }
    }

    public async void HandleLogin(string username, string password)
    {
        if (!IsValidEmail(username))
        {
         //   loginUI.ShowError("Formato de e-mail inválido!");
            return;
        }

        using (HttpClient client = new HttpClient())
        {
            try
            {
                LoginData loginData = new LoginData { Email = username, Password = password };
                string json = JsonUtility.ToJson(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(loginEndpoint, content);
                string responseJson = await response.Content.ReadAsStringAsync();

                APILoginResponse apiResponse = JsonUtility.FromJson<APILoginResponse>(responseJson);

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(apiResponse.token))
                {
                    Debug.Log("Token: " + apiResponse.token);
                    SaveTokens(apiResponse.token, apiResponse.refreshToken);
                    loginUI.ClearFields();
                }
                else
                {
                //    loginUI.ShowError(apiResponse.error ?? "Erro desconhecido");
                }
            }
            catch (HttpRequestException ex)
            {
             //   loginUI.ShowError($"Erro de conexão: {ex.Message}");
            }
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
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
        public string Email;
        public string Password;
    }
}