using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
    Top,
    Bot,
    Left,
    Right
}


public class movement : MonoBehaviour
{
    private Vector3 dir;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private Transform movePoint;

    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private ContactFilter2D Filter;

    public static event System.Action<float, float[], GameObject> OnUpdate;

    private GameObject FirstHit;

    private void Awake()
    {
        movePoint.parent = transform.parent;
        FirstHit = RaycastFront();
        trigger();
    }

    public void trigger()
    {
        OnUpdate?.Invoke(transform.rotation.eulerAngles.z, ArcDistance(), FirstHit);
        RaycastFront();
    }

    private GameObject RaycastFront()
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int counts = Physics2D.Raycast(transform.position, transform.up, Filter, hits);

        return hits[0].collider.gameObject;
    }

    private RaycastHit2D Raycast() => Raycast(transform.up);
    private RaycastHit2D Raycast(Vector2 direction) => Physics2D.Raycast(transform.position, direction, 99, WallLayer);

    public float FrontDistance => Raycast(transform.up).distance;
    public float LeftDistance => Raycast(transform.TransformDirection(Vector2.left)).distance;
    public float RightDistance => Raycast( transform.TransformDirection(Vector2.right)).distance;
    public float[] ArcDistance() => new float[] { LeftDistance, FrontDistance, RightDistance };

    public void OnMove(InputValue value)
    {
        dir = value.Get<Vector2>();
        
        if(dir.x < 0)
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

        FirstHit = RaycastFront();

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
