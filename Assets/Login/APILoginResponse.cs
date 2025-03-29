[System.Serializable]
public class APILoginResponse
{
    public string acessToken { get; }
    public string refreshToken;
    public string error;
}