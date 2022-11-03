using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShipUI : MonoBehaviour
{
    float shakeAmount = 0.05f;//振幅
    bool is_shake;
    Vector3 first_pos;

    private void Start()
    {
        shakeAmount = 5f;
        first_pos = this.transform.localPosition;
        StartShake();
    }

    public void StartShake()
    {
        if (is_shake) return;
        is_shake = true;
        Observable.Timer(System.TimeSpan.FromSeconds(0.5f)).First().Subscribe(_ => { is_shake = false; });
    }

    public void Update()
    {
        if (!is_shake) return;
        Vector3 pos = first_pos + Random.insideUnitSphere * shakeAmount;
        pos.y = transform.localPosition.y;
        transform.localPosition = pos;
    }

    public void EndShake()
    {
        is_shake = false;
        first_pos.y = transform.localPosition.y;
        transform.localPosition = first_pos;
    }
}