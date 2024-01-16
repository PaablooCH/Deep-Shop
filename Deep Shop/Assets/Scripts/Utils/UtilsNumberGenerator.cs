using UnityEngine;

public static class UtilsNumberGenerator
{
    // WeightDirection > 0: generates a number with more probability in the lower numbers
    // WeightDirection < 0: generates a number with more probability in the upper numbers
    public static int GenerateNumberWithWeight(int minValor, int maxValor, int exponent, int weightDirection)
    {
        // Genera un número aleatorio entre 0 y 1
        float random01 = Random.Range(0f, 1f);

        // Apply inverse exponential function to assign weight
        float numberGenerated = minValor + Mathf.Pow(random01, exponent) * (maxValor - minValor) * weightDirection;

        return Mathf.RoundToInt(numberGenerated);
    }
}
