using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PackSystem : MonoBehaviour
{
    [SerializeField] private PlantList plantList;
    [SerializeField] private int numberOfPlantsToGive = 3;

    // Liste von erlaubten Raritäten
    [SerializeField] private List<Rarity> allowedRarities;

    private void Start()
    {
        GivePlants();
    }

    private void GivePlants()
    {
        List<Plant> eligiblePlants = new List<Plant>();

        // Pflanzen filtern basierend auf der erlaubten Rarität
        foreach (Plant plant in plantList.plants)
        {
            if (allowedRarities.Contains(plant.rarity))
            {
                float roll = Random.Range(0f, 100f);

                // Wahrscheinlichkeit basierend auf der Pflanze
                if (roll <= plant.probability)
                {
                    eligiblePlants.Add(plant);
                }
            }
        }

        // Fehlende Pflanzen auffüllen, falls nicht genug vorhanden
        while (eligiblePlants.Count < numberOfPlantsToGive)
        {
            Plant randomPlant = plantList.plants[Random.Range(0, plantList.plants.Count)];
            if (!eligiblePlants.Contains(randomPlant) && allowedRarities.Contains(randomPlant.rarity))
            {
                eligiblePlants.Add(randomPlant);
            }
        }

        // Sortieren nach Rating und Rarity
        var awardedPlants = eligiblePlants
            .OrderByDescending(x => x.rating)
            .ThenBy(x => x.rarity)
            .Take(numberOfPlantsToGive)
            .ToList();

        // Pflanzen ins Inventar hinzufügen und anzeigen
        foreach (Plant plant in awardedPlants)
        {
            Debug.Log($"- {plant.name} (Rating: {plant.rating}, Rarity: {plant.rarity}, Probability: {plant.probability}%)");
            Inventory.Instance.Add(plant);
        }
    }
}
