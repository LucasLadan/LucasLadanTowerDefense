using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public Enemy SpawnBasedOnHighest(List<Enemy> enemies,float _points)//Harder enemy spawns
    {
        Enemy selectedEnemy = enemies[0];
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetCost() <= _points)
            {
                if (selectedEnemy.GetCost() < enemies[i].GetCost())
                {
                    selectedEnemy = enemies[i];
                }
                else if (selectedEnemy.GetCost() == enemies[i].GetCost() && UnityEngine.Random.Range(1, 2) == 2)
                {
                    selectedEnemy = enemies[i];
                }
            }
        }
        return selectedEnemy;
    }

    public Enemy SpawnBasedOnSpam(List<Enemy> enemies, float _points)//Easier if player has pierce
    {
        Enemy selectedEnemy = enemies[0];
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetCost()*2 <= _points)
            {
                if (selectedEnemy.GetCost() < enemies[i].GetCost())
                {
                    selectedEnemy = enemies[i];
                }
                else if (selectedEnemy.GetCost() == enemies[i].GetCost() && UnityEngine.Random.Range(1, 2) == 2)
                {
                    selectedEnemy = enemies[i];
                }
            }
        }
        return selectedEnemy;
    }


    

    public Enemy SpawnBasedOnWeights(List<Enemy> enemies, float _points)//Somewhat balanced, could lead to wins being completely based on rng
    {
        Debug.Log(_points);
        List<Enemy> selectedEnemys = new List<Enemy>();
        float totalPoints = 0f;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetCost() <= _points)
            {
                selectedEnemys.Add(enemies[i]);
                if (enemies[i].GetCost() <= 1.5f)
                {
                    totalPoints += enemies[i].GetCost()/2;
                }
                else
                {
                    totalPoints += enemies[i].GetCost();
                }
            }
        }

        //selectedNumber = Random.Range(0f, totalPoints);
        Debug.Log(totalPoints);
        System.Random random = new System.Random();
        float number = (float)random.NextDouble();
        totalPoints *= number;
        float selectedNumber = totalPoints;

        float currentNumber = 0;
        Debug.Log(number);
        Debug.Log(selectedNumber);
        for (int i = 0; i < selectedEnemys.Count; i++)
        {
            if (selectedEnemys[i].GetCost() <= 1.5f)
            {
                currentNumber += selectedEnemys[i].GetCost()/2;
            }
            else
            {
                currentNumber += selectedEnemys[i].GetCost();
            }
            if (currentNumber >= selectedNumber)
            {
                return selectedEnemys[i];
            }
        }
        return selectedEnemys[0];
    }
}
