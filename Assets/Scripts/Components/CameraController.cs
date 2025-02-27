using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    new public Camera camera;
    public float distancePower = 2;
    public float offset = 25;
    public Vector3 offsetDirection;

    void Update()
    {
        /*if(TimeManager.isPaused) 
            return;*/

        Vector3 min = Vector3.positiveInfinity;
        Vector3 max = Vector3.negativeInfinity;

        int playerCount = PlayerManager.PlayerCount;
        for (int i = 0; i < playerCount; i++)
        {
            Vector3 pos = PlayerManager.GetPosition(i);
            if(pos.x < min.x)
                min.x = pos.x;
            if(pos.y < min.y)
                min.y = pos.y;
            if(pos.z < min.z)
                min.z = pos.z;

            if(pos.x > max.x)
                max.x = pos.x;
            if(pos.y > max.y)
                max.y = pos.y;
            if(pos.z > max.z)
                max.z = pos.z;
        }
        Vector3 centre = Vector3.Lerp(min, max, 0.5f);

        min.y = min.y * 2f;
        max.y = max.y * 2f;
        float minDistance = Mathf.Pow(Vector3.Distance(min, max), distancePower) + offset;

        Vector3 targetPos = centre + offsetDirection * minDistance;

        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, Time.deltaTime * 5);
    }
}
