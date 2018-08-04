using System.Collections.Generic;
using UnityEngine;
// Abstract class for evolutionaryAlgorithms, using Unity
public abstract class EvolutionaryAlgorithm : MonoBehaviour
{
    // A test sample is represented by a floats (representing genes)
    public List<List<float>> oldSamples, newSamples;
    public List<float> results;
    // number of tests you want to run per trial
    protected int numSamples;
    public bool finishedTest;
    public virtual void Update()
    {
        if (finishedTrial())
        {
            cleanUpTrial();
            newSamples = createNewSamples(results, oldSamples);
            results = new List<float>();
            startNewTrial();
        }
        // For indivdual Tests
        if (finishedTest)
        {
            finishedTest = false;
            cleanUpTest();
            startNewTest();
        }
    }
    bool finishedTrial() {
        return results.Count == numSamples;
    }
    protected abstract void cleanUpTrial();
    protected abstract void cleanUpTest();
    protected abstract void startNewTest();
    protected abstract void startNewTrial();
    List<List<float>> createNewSamples(List<float> results, List<List<float>> oldSamples)
    {
        // The number of results should be equal to numSamples (1 result per numSample)
        List<float> sortedResults = new List<float>(results);
        sortedResults.Sort();

        // Depending on whether you are trying to maximize or minimize
        sortedResults.Reverse();

        List<List<float>> sortedSamples = new List<List<float>>();

        // depending on how many of the samples you want
        int m = numSamples / 2;
        for (int k = m; k < numSamples; k++)
        {
            sortedSamples.Add(oldSamples[results.IndexOf(sortedResults[k])]);
        }
        print("Best subject was " + sortedSamples[sortedSamples.Count - 1][0] * Mathf.Rad2Deg);
        // Now that we have up to the mth best samples, we breed them
        List<List<float>> newSamples = makeChildren(sortedSamples);
        return newSamples;
    }
    // Creates this a list of children that have genes that are the average of their parents
    static public List<List<float>> makeChildren(List<List<float>> parents)
    {
        List<List<float>> children = new List<List<float>>();
        // choose parent i
        for (int i = 0; i < parents.Count; i++)
        {
            // have parent i mate with parents j where j = i+1 ... parents.Count 
            for (int j = i + 1; j < parents.Count; j++)
            {
                List<float> child = makeChild(parents[i], parents[j]);
                children.Add(child);
            }
        }
        return children;
    }
    static List<float> makeChild(List<float> parent1, List<float> parent2)
    {
        List<float> genes = new List<float>();
        // Assuming both parents have the same number of genes

        for (int k = 0; k < parent1.Count; k++)
        {
            genes.Add((parent1[k] + parent2[k])/2);
        }
        return genes;
        // can be modified if you want to have more than 2 parents mating at a time
    }
}
