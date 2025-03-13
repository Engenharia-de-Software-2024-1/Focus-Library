using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class GoogleAuth : MonoBehaviour
{
    public Button googleLoginButton;
    private string clientId = "SEU_CLIENT_ID";
    private string clientSecret = "SEU_CLIENT_SECRET"; // AVISO NÂO ARMAZENAR isso no client-side!
    private string redirectUri = "http://localhost:8080";

    void Start()
    {
        googleLoginButton.onClick.AddListener(() => StartCoroutine(LoginWithGoogle()));
    }

    IEnumerator LoginWithGoogle()
    {
        // Abre a URL de login do Google
        string authUrl = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={clientId}&redirect_uri={redirectUri}&response_type=code&scope=openid%20email%20profile";
        Application.OpenURL(authUrl); // Usar WebView ou plugin para capturar o redirect

        // precisa capturar o código de autorização retornado (depende da plataforma)
        // Exemplo simplificado (não funcional sem capturar o redirect):
        // precisa de um WebViewer 
        string authorizationCode = "CODIGO_RECEBIDO_DO_GOOGLE";

        // Troca o código pelo ID Token
        string tokenUrl = "https://oauth2.googleapis.com/token";
        WWWForm form = new WWWForm();
        form.AddField("code", authorizationCode);
        form.AddField("client_id", clientId);
        form.AddField("client_secret", clientSecret);
        form.AddField("redirect_uri", redirectUri);
        form.AddField("grant_type", "authorization_code");

        using (UnityWebRequest request = UnityWebRequest.Post(tokenUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                GoogleAuthResponse response = JsonUtility.FromJson<GoogleAuthResponse>(jsonResponse);
                string idToken = response.id_token;

                // Valida o token e extrai o ID do usuário
                string userId = ValidateIdToken(idToken);
                Debug.Log("ID do usuário: " + userId);
            }
            else
            {
                Debug.LogError("Erro: " + request.error);
            }
        }
    }

    private string ValidateIdToken(string idToken)
    {
        // validar o JWT (ID Token) aqui.
        // isso deve ser feito no backend
        // Exemplo simplificado (não seguro para produção): // precisa alterar para colocar no backend
        string[] splitToken = idToken.Split('.');
        string payload = Base64Decode(splitToken[1]);
        GoogleJwtPayload data = JsonUtility.FromJson<GoogleJwtPayload>(payload);
        return data.sub; // "sub" é o ID único do usuário
    }

    private string Base64Decode(string base64)
    {
        byte[] bytes = System.Convert.FromBase64String(base64.PadRight(4 * ((base64.Length + 3) / 4), '='));
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}

[System.Serializable]
public class GoogleAuthResponse
{
    public string id_token;
}

[System.Serializable]
public class GoogleJwtPayload
{
    public string sub; // ID do usuário
    public string email;
    public string name;
}