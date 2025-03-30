using UnityEngine;

[System.Serializable]
public class ActivityData {
    public string atividadeId;   
    public string data;           // Ex.: "yyyy-MM-dd"
    public SessionData[] sessoes; // Array de sessões individuais
    
    public ActivityData() {
        atividadeId = System.Guid.NewGuid().ToString();
    }
}
