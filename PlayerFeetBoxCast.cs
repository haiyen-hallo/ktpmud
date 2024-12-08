using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetBoxCast : MonoBehaviour
{
    public PlayerController PlayerControllerScrips;
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerControllerScrips.OnGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerControllerScrips.OnGround = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControllerScrips.ResetJumpCount();
    }
}
