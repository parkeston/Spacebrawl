using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private new Rigidbody rigidbody;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }
    
    // implement moveable interface?
    
    public void Teleport(Vector3 endPoint, float time, Action onComplete = null)
    {
        StartCoroutine(TeleportRoutine(endPoint, time,onComplete));
    }

    private IEnumerator TeleportRoutine(Vector3 endPoint, float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        rigidbody.position = endPoint;
        
        onComplete?.Invoke();
    }
    
    public void Move(Vector3 startPoint, Vector3 endPoint, float time, Action onComplete = null)
    {
        rigidbody.position = startPoint;
        Vector3 velocity = (endPoint - startPoint) / time;
        rigidbody.rotation = Quaternion.LookRotation(velocity);

        StartCoroutine(MoveRoutine(endPoint, velocity.magnitude, time, onComplete));
    }

    private IEnumerator MoveRoutine(Vector3 endPoint, float speed, float time, Action onComplete)
    {
        float maxDelta = speed* Time.fixedDeltaTime;
        while (time>0)
        {
            rigidbody.MovePosition( Vector3.MoveTowards(rigidbody.position, endPoint, maxDelta));
            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        onComplete?.Invoke();
    }
}
