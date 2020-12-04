using UnityEngine;

public class ActorTransformMover : MonoBehaviour, IActorMover
{
    private float speed;
    private Vector3 velocity;
    private Vector2 leftTopBoundPosition;
    private Vector2 rightBottomBoundPosition;

    private void Start()
    {
        speed = 1f;
    }

    void FixedUpdate()
    {
        var nextPos = transform.localPosition + velocity * (speed * Time.deltaTime);
        if (IsInBound(nextPos))
            transform.localPosition = nextPos;
    }

    
    public void SetVelocityVector(Vector2 direction)
    {
        velocity = direction;
    }

    public void SetBound(Vector2 leftTop, Vector2 rightBottom)
    {
        leftTopBoundPosition = leftTop;
        rightBottomBoundPosition = rightBottom;
    }

    private bool IsInBound(Vector2 nextPos)
    {
        return nextPos.x >= leftTopBoundPosition.x &&
               nextPos.x <= rightBottomBoundPosition.x &&
               nextPos.y >= rightBottomBoundPosition.y &&
               nextPos.y <= leftTopBoundPosition.y;
    } 
}