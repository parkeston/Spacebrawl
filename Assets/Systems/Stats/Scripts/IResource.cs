using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource
{
   ResourceType ResourceType { get; }
   float ResourceValue { get; }
   void AlterResourceAmount (float alterAmount);
}
