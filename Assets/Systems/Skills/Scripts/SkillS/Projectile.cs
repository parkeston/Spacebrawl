using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private PhotonView photonView;

    private Action onComplete;
    
    private void Awake()
    {
        photonView = PhotonView.Get(this);
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }
    
    // implement moveable interface?
    
    public void Teleport(Vector3 endPoint, float time, Action onComplete = null)
    {
        this.onComplete = onComplete;
        photonView.RPC(nameof(TeleportNetwork),RpcTarget.All,endPoint,time);
    }

    [PunRPC]
    private void TeleportNetwork(Vector3 endPoint, float time)
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
        this.onComplete = onComplete;
        photonView.RPC(nameof(MoveNetwork),RpcTarget.All,startPoint,endPoint,time);
    }

    [PunRPC]
    private void MoveNetwork(Vector3 startPoint,Vector3 endPoint,float time)
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
