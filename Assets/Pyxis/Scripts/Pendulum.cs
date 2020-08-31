using UnityEngine;

// https://natureofcode.com/book/chapter-3-oscillation/#chapter03_section9


[System.Serializable]
public class Pendulum
{
    [Min(0.1f)]
    public float length = 1f;

    [Range(0f, 1f)]
    public float damping = 0.25f;

    [Min(0f)]
    public float gravity = 9.81f;

    [HideInInspector]
    public float angle;

    [HideInInspector]
    public float velocity;

    [HideInInspector]
    public float acceleration;

    float torque;
    float torqueMultiplier = 60f;

    public void FixedUpdate()
    {
        var gravitationalTorque = gravity / length * Mathf.Sin(angle);
        torque += gravitationalTorque;

        acceleration = torque;
        velocity += acceleration;
        angle += velocity * Time.deltaTime;
        velocity *= 1 - damping;

        if (Mathf.Abs(acceleration) < 0.000001f &&
            Mathf.Abs(velocity) < 0.000001f)
        {
            angle = Mathf.PI;
            acceleration = 0f;
            velocity = 0f;
        }

        torque = 0f;
    }

    public void addTorque(float torque)
    {
        this.torque += torque * torqueMultiplier;
    }

    public Vector2 bobPosition()
    {
        return new Vector2(length * Mathf.Sin(angle), length * Mathf.Cos(angle));
    }

    public void DrawGizmos(Vector3 origin)
    {
        var bob = origin + (Vector3)bobPosition();
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(origin, 0.2f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(bob, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(origin, bob);
    }
}
