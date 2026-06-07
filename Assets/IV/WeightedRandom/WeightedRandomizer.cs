using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeightedRandomizer : MonoBehaviour
{

    public T Randomize<T>(List<T> availableOptions) where T : IWeightedRandomOption
    {
        T selectedOption = default;

        if (availableOptions != null && availableOptions.Count != 0)
        {
            var weightSum = availableOptions.Sum(option => option.GetRandomWeight());
            var randomRoll = Random.Range(0, weightSum);

            var remainingRoll = randomRoll;
            for (var i = 0; i < availableOptions.Count && remainingRoll > 0; i++)
            {
                remainingRoll -= availableOptions[i].GetRandomWeight();
                if (remainingRoll <= 0)
                {
                    selectedOption = availableOptions[i];
                }
            }
        }
        else
        {
            Debug.LogError($"Couldn't randomize empty or null list of options.");
        }

        return selectedOption;
    }
}
