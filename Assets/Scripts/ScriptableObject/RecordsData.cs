using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "Test/RecordsData")]
public class RecordsData : ScriptableObject
{
    [SerializeField] private int score;
    public int Score
    {
        get { return score; }
        set { score = value > score ? value : score; }
    }
    public float Time;
}