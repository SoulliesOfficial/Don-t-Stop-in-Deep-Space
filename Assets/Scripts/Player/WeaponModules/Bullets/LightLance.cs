using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UniRx;
using UnityEngine;
using Dreamteck.Splines;

public class LightLance : MonoBehaviour
{
    public SplineComputer lance;
    public SplineRenderer lanceRenderer;
    public PolygonColliderGenerator lanceCollider;
    public float existTime;

    public void Initialize(Vector2 playerPosition, Vector2 direction)
    {
        lance.type = Spline.Type.Linear;
        lance.SetPoint(0, new SplinePoint() { position = playerPosition, size = 0.5f, color = Color.white });
        lance.SetPoint(1, new SplinePoint() { position = playerPosition + 100 * direction, size = 0f, color = Color.white });
        lanceCollider.spline = lance;
        existTime = 0f;
        Observable.Timer(System.TimeSpan.FromSeconds(0.5)).First().Subscribe(_ => { LeanPool.Despawn(gameObject); }).AddTo(this);
    }

    private void FixedUpdate()
    {
        existTime += Time.fixedDeltaTime;
        lance.SetPointSize(0, 0.5f - existTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hurt(5);
        }
    }
}
