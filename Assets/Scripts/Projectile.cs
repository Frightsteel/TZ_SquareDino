using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerController>())
        {
            if (other.GetComponent<Enemy>())
            {
                other.GetComponent<Enemy>().Die();
            }

            gameObject.SetActive(false);
        }
    }
}
