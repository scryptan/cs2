using UnityEngine;

public interface IActorMover
{
    void SetVelocityVector(Vector2 direction);
    void SetBound(Vector2 leftTop, Vector2 rightBottom);
}
