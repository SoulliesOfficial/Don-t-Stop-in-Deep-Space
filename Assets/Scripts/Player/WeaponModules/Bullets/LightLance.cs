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
    public float existTime, lanceSize;
    public float damage;
    public List<Enemy> hits;

    public void Initialize(Vector2 playerPosition, Vector2 direction, float damage)
    {
        lance.type = Spline.Type.Linear;
        this.damage = damage;
        lanceSize = 1f;
        if(damage <= 5f)
        {
            lanceSize = 1f;
        }
        else
        {
            lanceSize = 2f;
        }
        lance.SetPoint(0, new SplinePoint() { position = playerPosition, size = lanceSize, color = Color.white });
        lance.SetPoint(1, new SplinePoint() { position = playerPosition + 100 * direction, size = 0, color = Color.white });
        lanceCollider.spline = lance;
        existTime = 0f;
        hits.Clear();
        Observable.Timer(System.TimeSpan.FromSeconds(0.5)).First().Subscribe(_ => { LeanPool.Despawn(gameObject); }).AddTo(this);
    }

    private void FixedUpdate()
    {
        existTime += Time.fixedDeltaTime;
        lance.SetPointSize(0, lanceSize - (2 * lanceSize * existTime));
        lance.SetPointSize(1,  0);
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
