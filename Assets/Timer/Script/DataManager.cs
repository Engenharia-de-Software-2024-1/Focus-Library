using UnityEngine;

public class DataManager : MonoBehaviour {
    //Por enquanto só armazena.
    public static DataManager Instance { get; private set; }

    public int completedSessions { get; private set; }
    public float totalFocusTime { get; private set; }
    public float totalRestTime { get; private set; }

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
        totalFocusTime = focus;
        totalRestTime = rest;
    }
}
