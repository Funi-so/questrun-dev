using UnityEngine;
using UnityEngine.UI;


public class Destroy : MonoBehaviour
{
    public float distancia;
    void Update()
    {
        if (transform.position.z < distancia && GameManager.Instance.CurrentGameState() == GameManager.GameState.Jogando)
        {

            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("Quebravel"))
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
