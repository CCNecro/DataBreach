using UnityEngine;

public class GoblinArrow : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 5f;

    void Start()
    {
        // Auto-destruct after a few seconds so we don't leak memory
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add your player damage logic here!
            Debug.Log("Player hit for " + damage + " damage!");
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
