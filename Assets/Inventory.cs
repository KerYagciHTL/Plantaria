using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Plant> plants = new List<Plant>();
    public static Inventory Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //damit keine Performance Probleme entstehen wird die neue Instance, die versucht wird erstellt zu werden, gelöscht
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Erhält die Instanz beim Szenenwechsel
        }
    }

    public void Add(Plant plant)
    {
        //plant wird zum Inventar hinzugefügt
        plants.Add(plant);
    }
    
    public void Remove(Plant plant)
    {
        //plant wird gelöscht (später vllt nützlich)
        if (plants.Contains(plant))
        {
            plants.Remove(plant);
            Debug.Log($"Plant '{plant.name}' removed from the inventory.");
        }
        else
        {
            Debug.LogWarning("Plant not found in the inventory.");
        }
    }
}

