
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    public Slider temporizador;
    void Start()
    {
        temporizador.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        temporizador.value = Time.time;
        if (temporizador.value== 600)
        {
            SceneManager.LoadScene("ganhou");


        }
    
    }
}
