using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    void Awake()
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

    public void SaveStatistics(int completedSessions, float totalFocusTime, float totalRestTime)
    {
        Debug.Log("Salvando estatísticas...");
        Debug.Log("Sessões concluídas: " + completedSessions);
        Debug.Log("Tempo total de foco: " + totalFocusTime + "s");
        Debug.Log("Tempo total de descanso: " + totalRestTime + "s");

        StartCoroutine(PostStatistics(completedSessions, totalFocusTime, totalRestTime));
    }

    IEnumerator PostStatistics(int completedSessions, float totalFocusTime, float totalRestTime)
    {
        StatisticsData data = new StatisticsData(completedSessions, totalFocusTime, totalRestTime);
        string jsonData = JsonUtility.ToJson(data);
        string url = "http://yourserver/api/statistics"; // Substitua pela URL do seu backend
        UnityWebRequest request = UnityWebRequest.Put(url, jsonData);
        request.method = UnityWebRequest.kHttpVerbPOST;
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.Log("Erro ao enviar estatísticas: " + request.error);
        else
            Debug.Log("Estatísticas enviadas com sucesso!");
    }

    [System.Serializable]
    public class StatisticsData
    {
        public int completedSessions;
        public float totalFocusTime;
        public float totalRestTime;

        public StatisticsData(int sessions, float focus, float rest)
        {
            completedSessions = sessions;
            totalFocusTime = focus;
            totalRestTime = rest;
        }
    }
}
