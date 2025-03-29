using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToRegister : MonoBehaviour
{

    public void ChangeSceneToRegister(){
        SceneManager.LoadScene("TelaCadastro");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
