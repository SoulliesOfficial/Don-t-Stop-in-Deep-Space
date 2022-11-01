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
    public float damage;
    public List<Enemy> hits;

    public void Initialize(Vector2 playerPosition, Vector2 direction, float damage)
    {
        lance.type = Spline.Type.Linear;
        this.damage = damage;
        if(damage <= 5f)
        {
            lanceRenderer.color = Color.blue;
        }
        else
        {
            lanceRenderer.color = Color.red;
        }
        lance.SetPoint(0, new SplinePoint() { position = playerPosition, size = 1f, color = Color.white });
        lance.SetPoint(1, new SplinePoint() { position = playerPosition + 100 * direction, size = 1f, color = Color.white });
        lanceCollider.spline = lance;
        existTime = 0f;
        hits.Clear();
        Observable.Timer(System.TimeSpan.FromSeconds(0.5)).First().Subscribe(_ => { LeanPool.Despawn(gameObject); }).AddTo(this);
    }

    private void FixedUpdate()
    {
        existTime += Time.fixedDeltaTime;
        lance.SetPointSize(0, 1f - 2 * existTime);
        lance.SetPointSize(1, 1f - 2 * existTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !hits.Contains(collision.GetComponent<Enemy>()))
        {
            hits.Add(collision.GetComponent<Enemy>());
            collision.GetComponent<Enemy>().Hurt(damage);
        }
    }
}
