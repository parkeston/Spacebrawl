using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Stat))]
[RequireComponent(typeof(ResourceDisplayer))]
public class PhotonStatsObserver : MonoBehaviour
{
    private Stat[] displayedStats;
    private ResourceDisplayer[] resourceDisplayers;

    private Dictionary<Stat, int> statToDisplayerIndex;

    private PhotonView photonView;
    
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        resourceDisplayers = GetComponents<ResourceDisplayer>();

        displayedStats = new Stat[resourceDisplayers.Length];
        statToDisplayerIndex = new Dictionary<Stat, int>();

        for(int i=0;i<resourceDisplayers.Length;i++)
        {
            displayedStats[i] = resourceDisplayers[i].DisplayedStat;
            statToDisplayerIndex.Add(displayedStats[i],i);
        }
        
        foreach (var stat in displayedStats)
        {
            stat.OnValueChanged += ObserveStat;
        }
    }

    private void ObserveStat(Stat modifiedStat, float value, float maxValue)
    {
        int displayerIndex = statToDisplayerIndex[modifiedStat];
        photonView.RPC(nameof(ReflectStatChangesOnNetwork),RpcTarget.All,displayerIndex,value,maxValue);
    }

    [PunRPC]
    private void ReflectStatChangesOnNetwork(int displayerIndex, float value, float maxValue)
    {
        displayedStats[displayerIndex].SyncWithLocalValue(value);
        resourceDisplayers[displayerIndex].UpdateResourceDisplay(value,maxValue);
    }
}
