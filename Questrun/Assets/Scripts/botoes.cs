using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botoes : MonoBehaviour
{
    public Button botão;
    public string cena;
    // Start is called before the first frame update
    void Start()
    {
        botão.onClick.AddListener(Executar);
    }

    // Update is called once per frame
    private void Executar()
    {
        SceneManager.LoadScene(cena);
    }
}
