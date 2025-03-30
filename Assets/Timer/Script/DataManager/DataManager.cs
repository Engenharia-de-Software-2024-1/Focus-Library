using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Constants;

public class DataManager : MonoBehaviour {
    public static DataManager Instance { get; private set; }
    private List<SessionData> sessions = new List<SessionData>(); // Agora será uma lista que armazena cada sessão individualmente.

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddSession(int focoEmSegundos, int descansoEmSegundos) {
        SessionData session = new SessionData() {
            segundosFoco = focoEmSegundos,
            segundosDescanso = descansoEmSegundos
        };
        sessions.Add(session);
        Debug.Log("Sessão adicionada: Foco = " + focoEmSegundos + " s, Descanso = " + descansoEmSegundos + " s");
    }

    public void SendActivityData() {
        StartCoroutine(PostActivityDataCoroutine());
    }

    IEnumerator PostActivityDataCoroutine() {
        ActivityData data = new ActivityData() {
            data = System.DateTime.Now.ToString("yyyy-MM-dd"),
            sessoes = sessions.ToArray()
        };

        string jsonData = JsonUtility.ToJson(data);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        string url = NetworkingConstants.BACKEND_URL + "/sessao"; 

        Debug.Log("URL da API: " + url);
        Debug.Log("JSON que será enviado: " + jsonData); 

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("JWT_TOKEN")); // Adiciona o token JWT no cabeçalho da requisição

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) {
            Debug.Log("Dados de atividade enviados com sucesso!");
        } else {
            Debug.LogError("Erro ao enviar dados de atividade: " + request.error);
        }
    }
}
