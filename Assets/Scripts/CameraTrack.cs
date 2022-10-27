using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public Transform m_playerTransform;
    public Camera playerCamera;

    //设定一个角色能看到的最远值
    public float Ahead;

    //设置一个摄像机要移动到的点
    public Vector3 targetPos;

    public float targetFOV;
    //设置一个缓动速度插值
    public float smoothPos, smoothFOV;

    void FixedUpdate()
    {
        //this.transform.position = new Vector3(m_playerTransform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        smoothPos = 2.5f;
        smoothFOV = 0.5f;

        targetPos = new Vector3(m_playerTransform.position.x, m_playerTransform.transform.position.y, gameObject.transform.position.z);
        targetFOV = 10 + (m_playerTransform.GetComponent<Player>().instantSpeed / Time.fixedDeltaTime / 4);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothPos * Time.fixedDeltaTime);

        playerCamera.orthographicSize = Mathf.Lerp(10, targetFOV, smoothFOV * Time.fixedDeltaTime);
    }
}

