using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public Transform startPoint; // Onde o jogo deve começar
    public GroundSpawner tilesPrefab;
    public float moveSpeed = 12;
    public int tilesPreSpawn = 5; // Quantidade de tiles que eu quero ter sempre em cena
    public int tilesNoObstacle = 3; // Quantidade de tiles sem obstáculos no início do jogo


    List<GroundSpawner> spawnedTiles = new List<GroundSpawner>(); // Lista de tiles


    public static GroundGenerator Instance;

    private void Start()
    {
        Instance = this;
        Vector3 spawnPos = startPoint.position;
        int tilesNoObstacleTemp = tilesNoObstacle;

        for (int i = 0; i < tilesPreSpawn; i++)
        {
            spawnPos -= tilesPrefab.startPoint.position; // Calcula a diferença do offset do tile
            GroundSpawner currentTile = Instantiate(tilesPrefab, spawnPos, Quaternion.identity); // de onde sai o currentTile
            if (tilesNoObstacle > 0)
            {
                currentTile.DeactivateAllObstacles();
                tilesNoObstacleTemp--;
            }
            else
            {
                currentTile.ActivateRandomObstacle();
            }
            spawnPos = currentTile.endPoint.position; // Posição para criar o próximo tile
            currentTile.transform.SetParent(transform); // Tiles viram filhos do objeto
            spawnedTiles.Add(currentTile); // Adiciona o tile na lista
        }

    }

    private void Update()
    {
        transform.Translate(-spawnedTiles[0].transform.forward * Time.deltaTime * moveSpeed); // Move este objeto para frente

        // Esse if verifica se o objeto 0 da lista (o primeiro) passou da visão da câmera no eixo X negativo,
        // ou seja, se ele está à esquerda da câmera, fora da área de renderização
        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < -0.3f)
        {
            // Move o tile pra frente se ele tiver saído da câmera
            GroundSpawner tileTemp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0); // Remove o tile da lista
            // Reposiciona o tile fora da visão da câmera e o coloca no final
            tileTemp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTemp.startPoint.localPosition;
            tileTemp.ActivateRandomObstacle(); // Ativa um obstáculo aleatório
            spawnedTiles.Add(tileTemp); // Readiciona o tile à lista (no final)
        }
    }
}
