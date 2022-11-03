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
    public float spring = 0.01f;

    public float targetFOV;
    //设置一个缓动速度插值
    public float smoothPos, smoothFOV, distance;
    public Vector3 offset;

    private void Start()
    {
        smoothPos = 20f;
        smoothFOV = 2f;
        spring = 0.1f;
        transform.position = new Vector3(m_playerTransform.position.x, m_playerTransform.position.y, -10);
        targetFOV = 10;
    }

    void FixedUpdate()
    {
        //this.transform.position = new Vector3(m_playerTransform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        targetPos = new Vector3(m_playerTransform.position.x, m_playerTransform.transform.position.y, gameObject.transform.position.z);
        targetFOV = 10 + (m_playerTransform.GetComponent<Player>().instantSpeed / Time.fixedDeltaTime * 0.01f);

        //transform.position = Vector3.MoveTowards(transform.position, targetPos, smoothPos * Time.fixedDeltaTime);


        distance = Vector2.Distance(this.transform.position, m_playerTransform.position);
        transform.position = Vector3.MoveTowards(this.transform.position, targetPos, distance * spring + smoothPos * Time.fixedDeltaTime);


        playerCamera.orthographicSize = Mathf.Lerp(playerCamera.orthographicSize, targetFOV, smoothFOV * Time.fixedDeltaTime);
    }

}

