using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject sound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sound.SetActive(true);
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<GameManager>().Explode();
        }
    }

}
