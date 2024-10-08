using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManger : MonoBehaviour
{
    public static SkillManger instance;

    public Dash_Skill dash { get; private set; }
    public Clone_Skill clone { get; private set; }
    public Sword_Skill sword { get; private set; }

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        dash = GetComponent<Dash_Skill>();
        clone = GetComponent<Clone_Skill>();
        sword = GetComponent<Sword_Skill>();
    }
}
