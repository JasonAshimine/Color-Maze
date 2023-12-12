using Game.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{     
    [SerializeField] private GameEventData _forwardEvent;

    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform movePoint;

    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private LayerMask DetectLayer;

    //[SerializeField] private ContactFilter2D Filter;

    private Vector3 dir;
    private float turnSpeed = 90;

    private void Awake()
    {
        movePoint.parent = transform.parent;
        trigger();
    }

    private void trigger()
    {
        _forwardEvent.Raise(new Direction(transform.rotation.eulerAngles.z, DirLeft, DirFront, DirRight));
    }

    private RaycastHit2D Raycast(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, 99f, DetectLayer);
    }

    private RaycastHit2D DirFront => Raycast(transform.up);
    private RaycastHit2D DirLeft => Raycast(transform.TransformDirection(Vector2.left));
    private RaycastHit2D DirRight => Raycast( transform.TransformDirection(Vector2.right));

    public void OnMove(InputValue value)
    {
        if (Time.timeScale == 0)
            return;

        dir = value.Get<Vector2>();
        if (dir.x < 0)
        {
            handleRotation(turnSpeed);
        }
        else if (dir.x > 0)
        {
            handleRotation(-turnSpeed);
        }
    }

    private void handleRotation(float angle)
    {
        transform.Rotate(transform.forward, angle);
        trigger();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if(Mathf.Abs(dir.y) == 1)
            {
                Vector2 newPoint = movePoint.position + transform.up * dir.y;

                if (!Physics2D.OverlapArea(transform.position, newPoint, WallLayer))
                    movePoint.position = newPoint;
            }            
        }
        else
        {
            trigger();
        }        
    }

}


public struct Direction 
{
    public Direction(float direction, RaycastHit2D left, RaycastHit2D forward, RaycastHit2D right)
    {
        this.direction = direction;
        this.forward = forward;
        this.left = left;
        this.right = right;
    }

    public float direction;
    public RaycastHit2D forward;
    public RaycastHit2D left;
    public RaycastHit2D right;
}
