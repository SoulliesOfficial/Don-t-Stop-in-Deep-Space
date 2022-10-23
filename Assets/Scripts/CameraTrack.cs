using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public Transform m_playerTransform;

    //设定一个角色能看到的最远值
    public float Ahead;

    //设置一个摄像机要移动到的点
    public Vector3 targetPos;

    //设置一个缓动速度插值
    public float smooth;

    void FixedUpdate()
    {


        //this.transform.position = new Vector3(m_playerTransform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        smooth = 10;

        targetPos = new Vector3(m_playerTransform.position.x, m_playerTransform.transform.position.y, gameObject.transform.position.z);


        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.deltaTime);

    }
}

