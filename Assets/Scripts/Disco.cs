using UnityEngine;

public class Disco : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject whole;
    public GameObject sliced;
    private ParticleSystem juiceEffect;
    private Collider fruitCollider;
    private Rigidbody fruitRigidbody;
    private Vector3 direction;
    private Vector3 position;
    private float force;
    public int points = 1;
    public GameObject sound;
    
    public bool isStartingGame;
    public GameObject Spawner;
    public GameObject UI;

    private void Awake()
    {
        mainCamera = Camera.main;
        juiceEffect = GetComponentInChildren<ParticleSystem>();
        fruitCollider = GetComponent<Collider>();
        fruitRigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        BreakObject();
    }

    private void OnTouchDown()
    {
        BreakObject();
    }

    private void BreakObject()
    {
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.5f, 2.3f);
        sound.SetActive(true);
        if (isStartingGame)
        {
            whole.SetActive(false);
            sliced.SetActive(true);
            juiceEffect.Play();
            Spawner.SetActive(true);
            UI.SetActive(false);
            
            
        }
        else
        {
            fruitCollider.enabled = false;
            FindObjectOfType<GameManager>().IncreaseScore(points);
            whole.SetActive(false);
            sliced.SetActive(true);
            juiceEffect.Play();

            Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

            direction = (slices[0].transform.position - fruitRigidbody.transform.position).normalized;
            position = fruitRigidbody.transform.position;
            force = fruitRigidbody.velocity.magnitude;

            // Add a force to each slice based on the blade direction
            foreach (Rigidbody slice in slices)
            {
                slice.velocity = fruitRigidbody.velocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            }
        }
    }
}