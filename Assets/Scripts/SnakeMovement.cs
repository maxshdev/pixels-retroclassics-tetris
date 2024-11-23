using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float moveDelay = 0.3f; // Tiempo entre cada movimiento
    private Vector2 direction = Vector2.right; // Dirección inicial
    private List<Transform> snakeSegments; // Partes del cuerpo de la serpiente
    public Transform segmentPrefab; // Prefab para los segmentos de la serpiente

    private void Start()
    {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform); // Agrega la cabeza como primer segmento
        InvokeRepeating(nameof(MoveSnake), moveDelay, moveDelay);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
            direction = Vector2.right;
    }

    private void MoveSnake()
    {
        Vector3 previousPosition = transform.position;

        // Mueve la cabeza
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0.0f
        );

        // Mueve los segmentos
        for (int i = snakeSegments.Count - 1; i > 0; i--)
        {
            snakeSegments[i].position = snakeSegments[i - 1].position;
        }

        if (snakeSegments.Count > 1)
            snakeSegments[1].position = previousPosition;
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Obstacle")
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0; // Pausa el juego
        }
    }
}