using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInputReader : InputReader
{
    [SerializeField] private float maxFloorRayDistance;
    [SerializeField] private LayerMask floorLayerMask;
    
    public override Vector3 GetMovementDirection()
    {
        float hMovement = Input.GetAxisRaw("Horizontal");
        float vMovement = Input.GetAxisRaw("Vertical");
        
        var direction = new Vector3(hMovement,0,vMovement);
        return direction.normalized;
    }

    public override Quaternion GetRotation(Vector3 rotationPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxFloorRayDistance, floorLayerMask))
        {
            Vector3 rotationVector = hitInfo.point - rotationPoint;
            rotationVector.y = 0;

            return Quaternion.LookRotation(rotationVector);
        }
        
        return Quaternion.identity;
    }
}
