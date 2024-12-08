using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    Vector2 CheckPoint;
    Rigidbody2D playerRb;
    public void Start()
    {
        CheckPoint = transform.position;
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deadground"))
        {
            Die();
        }
    }
    public void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }
    public void UpdateCheckPoint(Vector2 Point)
    {
        CheckPoint = Point;
    }
    IEnumerator Respawn(float value)
    {
        playerRb.simulated = false;
        transform.localScale = new Vector2(0, 0);
        yield return new WaitForSeconds(value);
        transform.position = CheckPoint;
        transform.localScale = new Vector3(1.3f, 1.2f, 1.2f);
        playerRb.simulated = true;
    }
}
