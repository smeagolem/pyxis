using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    public GameObject original;

    void OnEnable()
    {
        ClickPointSystem.pointClicked += OnPointClicked;
    }

    void OnDisable()
    {
        ClickPointSystem.pointClicked -= OnPointClicked;
    }

    void OnPointClicked(Vector3 point)
    {
        SpawnExplosion(point);
    }

    void SpawnExplosion(Vector3 point)
    {
        // TODO: use object pooling
        var explosion = Instantiate(original, point, Quaternion.identity, transform);
        explosion.SetActive(true);
    }
}
