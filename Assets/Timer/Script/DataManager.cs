using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class DataManager : MonoBehaviour {
    public static DataManager Instance { get; private set; }
    public int completedSessions { get; private set; }
    public int totalFocusTime { get; private set; }
    public float totalRestTime { get; private set; }

    [SerializeField] private string apiBaseUrl = "https://seuservidor.com/api";

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetStatistics(int sessions, float focus, float rest) {
        completedSessions = sessions;
        totalFocusTime = (int)Math.Truncate(focus / 60); 
        totalRestTime = rest;
    }

    public void SendStatistics() {
        StartCoroutine(PostStatisticsCoroutine());
    }

        IEnumerator PostStatisticsCoroutine() {
            // Cria um objeto com os dados a serem enviados
            StatisticsData data = new StatisticsData() {
                sessions = completedSessions,
                focusTime = totalFocusTime,
                date = DateTime.Now.ToString("yyyy/MM/dd") 
            };
            
         // Serializa o objeto para JSON
        string jsonData = JsonUtility.ToJson(data);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        string url = apiBaseUrl + "/sessoes/id"; //Colocar a Url do endpoint da API rest.

        // Cria a requisição POST com o JSON no corpo
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Envia a requisição e aguarda a resposta
        yield return request.SendWebRequest();

        // Verifica o resultado 
        if (request.result == UnityWebRequest.Result.Success) {
            Debug.Log("Estatísticas enviadas com sucesso!");
            // Opcional: desserializar a resposta se necessário
            // StatisticsResponse responseData = JsonUtility.FromJson<StatisticsResponse>(request.downloadHandler.text);
        } else {
            Debug.LogError("Erro ao enviar estatísticas: " + request.error);
        }
    }
}


// Classe de dados para enviar as estatísticas para o servidor.
[Serializable]
public class StatisticsData {
    public int sessions;      // Número de sessões completadas
    public int focusTime;      // Tempo total de foco em minutos
    public string date;        // Data da operação, no formato "yyyy/MM/dd"
}


