using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Liste von Pflanzen, die im Inventar gespeichert sind
    [SerializeField] private List<Plant> plants = new List<Plant>();
    
    // Singleton-Instanz: Statische Referenz auf das einzige Inventory-Objekt
    // Diese Variable erlaubt anderen Skripten, direkt auf das Inventar zuzugreifen, z.B. über: Inventory.Instance.Add(plant);
    public static Inventory Instance { get; private set; }

    
    // Awake wird aufgerufen, sobald das GameObject aktiviert wird.
    // Stellt sicher, dass nur eine einzige Instanz des Inventars existiert (Singleton-Pattern).
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Wenn bereits eine andere Instanz von Inventory existiert, zerstören wir dieses GameObject,
            // um doppelte Instanzen zu vermeiden und Fehler zu verhindern.
            Destroy(gameObject);
        }
        else 
        {
            // Wenn noch keine Instanz existiert, speichern wir die aktuelle Instanz in der statischen Variable.
            Instance = this;
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

