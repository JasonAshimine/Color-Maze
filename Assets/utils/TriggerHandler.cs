using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField]
    private StateDataSet _stateData;
    public LayerMask mask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject collision)
    {
        if ((mask & (1 << collision.layer)) != 0)
        {
            _stateData.Raise(GameStage.EndGame);
        }
    }
}
