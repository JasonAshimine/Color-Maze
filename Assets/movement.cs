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

    public static event System.Action<float, float[]> OnUpdate;



    private void Awake()
    {
        movePoint.parent = transform.parent;
        trigger();
    }

    private void trigger()
    {
        OnUpdate?.Invoke(transform.rotation.eulerAngles.z, ArcDistance());
    }

    public float FrontDistance => Physics2D.Raycast(transform.position, transform.up,99,WallLayer).distance;

    public float LeftDistance => Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 99, WallLayer).distance;

    public float RightDistance => Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 99, WallLayer).distance;

    public float[] ArcDistance() => new float[] { LeftDistance, FrontDistance, RightDistance};

    public void OnMove(InputValue value)
    {
        dir = value.Get<Vector2>();
        
        if(dir.x < 0)
        {
            handleRotation(-turnSpeed * -1);
        }
        else if (dir.x > 0)
        {
            handleRotation(-turnSpeed * 1);
        }

/*        if (Mathf.Abs(dir.x) == 1)
        {
            handleRotation(-turnSpeed * dir.x);
        }*/
        
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
