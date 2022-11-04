using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Jogando, Morreu, Pausa }

    private GameState currentState;

    public GameObject obstacle;
    public GameObject enemy;

            void Awake()
            {
                Instance = this;
            }

            void Start()
            {
                currentState = GameState.Jogando;
                // o Invoke Repeating abaixo chama o método SummonObstacles() depois de 1 segundo
                // e o chama de novo a cada 3 segundos 
                InvokeRepeating("SummonObstacles", 1, 3);
                InvokeRepeating("SummonEnemy", 2.5f, 5);
            }

            public void ChangeGameState(GameState state)
            {
                currentState = state;
            }

            public GameState CurrentGameState()
            {
                return currentState;
            }

            void SummonObstacles()
            {
                Vector3 pos = new Vector3(Random.Range(-1, 2), 1, 15);
                Instantiate(obstacle, pos, transform.rotation);
            }

            public void SummonEnemy()
            {
                Vector3 pos = new Vector3(Random.Range(-1, 2), Random.Range(1, 3), 15);
                Instantiate(enemy, pos, transform.rotation);
            }

            public void StopGame()
            {
                CancelInvoke();
                ChangeGameState(GameState.Morreu);
            }

        }
    
