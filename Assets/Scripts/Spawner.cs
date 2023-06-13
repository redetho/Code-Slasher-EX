using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;

    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;
    

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        float timer = 0f; // Timer to track the 60-second interval

        while (enabled)
        {
            GameObject prefab;

            if (timer >= 30f) // Check if 60 seconds have passed
            {
                //muda o prefab de fruta, pra uma que só pode ser acertada por um ataque de fogo.
                
                prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            }
            else
            {
                if (Random.value < bombChance)
                {
                    prefab = bombPrefab;
                }
                else
                {
                    prefab = fruitPrefabs[Random.Range(0, (fruitPrefabs.Length - 1))];
                }
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifetime);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            timer += maxSpawnDelay; // Increase the timer by the maximum spawn delay
        }
    }
}