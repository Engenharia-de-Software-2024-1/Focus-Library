using UnityEngine;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{
    public RectTransform telaAtual; 
    public RectTransform telaEsquerda;
    public RectTransform telaDireita;
    public float velocidade = 5f;

    private Vector2 posInicial;

    void Start()
    {
        posInicial = telaAtual.anchoredPosition;   
    }

    public void ArrastarParaEsquerda()
    {
        if (telaEsquerda != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoverTela(telaEsquerda.anchoredPosition));
        }
    }

    public void ArrastarParaDireita()
    {
        if (telaEsquerda != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoverTela(telaDireita.anchoredPosition));
        }
    }

    private System.Collections.IEnumerator MoverTela(Vector2 destino)
    {
        while (Vector2.Distance(telaAtual.anchoredPosition,destino) > 0.1f)
        {
            telaAtual.anchoredPosition = Vector2.Lerp(telaAtual.anchoredPosition, destino, Time.deltaTime * velocidade);
            yield return null;
        }

        telaAtual.anchoredPosition = destino;
    }
}