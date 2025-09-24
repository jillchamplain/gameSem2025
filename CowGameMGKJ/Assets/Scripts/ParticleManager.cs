using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParticleManager : Manager
{
    [Header("Refs")]
    [SerializeField] List<Particle> particles;
    
    public void SpawnParticleAt(string index, Vector3 spawnPos)
    {
        for(int i = 0; i < particles.Count; i++)
        {
            if (particles[i].getName() == index)
            {
                GameObject theParticle = GameObject.Instantiate(particles[i].getGameObjectRef(), spawnPos, Quaternion.identity);
                
            }
        }
    }

    public void SpawnTextParticleAt(string index, string text, Vector3 spawnPos)
    {
        for (int i = 0; i < particles.Count; i++)
        {
            if (particles[i].getName() == index)
            {
                GameObject theParticle = GameObject.Instantiate(particles[i].getGameObjectRef(), spawnPos, Quaternion.identity);
                theParticle.GetComponentInChildren<TextMeshProUGUI>().text = text;
            }
        }
    }

    [Serializable]
    public struct Particle
    {
        [SerializeField] string name;
        public string getName() { return name; }
        [SerializeField] GameObject gameObjectRef;
        public GameObject getGameObjectRef() { return gameObjectRef; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
