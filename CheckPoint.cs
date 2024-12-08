using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator Checkpoints;
    PlayerRespawn PlayerRespawn;
    public Transform Pointcheckpoint;
    private void Awake()
    {
        PlayerRespawn = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRespawn>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerRespawn.UpdateCheckPoint(Pointcheckpoint.position);
        }
        else
        {
            Checkpoints.Play("flagon");
        }
    }
}
