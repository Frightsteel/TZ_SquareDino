using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}