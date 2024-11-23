using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public BoxCollider2D spawnArea;

    private void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

        Instantiate(foodPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
    }
}