using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public LayerMask mask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == mask)
            GameManager.Instance.SetGameStage(GameStage.EndGame);

        Debug.Log("Trigger" + collision.gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == mask)
            GameManager.Instance.SetGameStage(GameStage.EndGame);

        Debug.Log("Collision" + collision.gameObject.name);
    }
}
