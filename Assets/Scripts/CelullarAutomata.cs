using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelullarAutomata : MonoBehaviour
{
    public GameObject reticula;
    public SpriteRenderer cubes;
    public GameObject myCubes;
    int j_reticula, cellSize, i_caHeight, randomNumber;
    bool[,] matrix;
    bool[] ruleSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Automata(int cells) {
        cellSize = cells;
        j_reticula = (int)reticula.GetComponent<Renderer>().bounds.size.x / cellSize;
        i_caHeight = (int)reticula.GetComponent<Renderer>().bounds.size.y / cellSize;
        matrix = new bool[i_caHeight,j_reticula];
        ruleSet = new bool[8];
    }

    public void paint() {
        for (int i = 0; i <i_caHeight; i++) {
            for(int j=0; j < j_reticula; j++) {
                if (matrix[i, j]) {
                    cubes.color = new Color(0, 0, 0);
                }
                else {
                    cubes.color = new Color(0, 0, 0);
                }
                Instantiate(myCubes, new Vector3(j*cellSize,i*cellSize), Quaternion.identity, reticula.transform);
            }
        }
    }

    public void BasicCondition() {
        matrix[0, j_reticula / 2] = true;
    }

    public void RandomCondition() {
        
        for(int j=0; j < j_reticula; j++) {
            randomNumber = Random.Range(0, 2);
            if (randomNumber == 1) {
                matrix[0, j] = true;
            }
        }
    }

    public void Evolution() {
        for (int i = 0; i < i_caHeight - 1; i++) {
            for (int j = 0; j < j_reticula - 1; j++) {
                if (j == 0) {
                    matrix[i + 1,j] = rule(matrix[i,j - 1], matrix[i,j], matrix[i,0]);
                    continue;
                }
                matrix[i + 1,j] = rule(matrix[i,j - 1], matrix[i,j], matrix[i,j + 1]);
            }
        }
    }


   public bool rule(bool a, bool b, bool c) {
        if (a && b && c) return ruleSet[7];
        if (a && b && !c) return ruleSet[6];
        if (a && !b && c) return ruleSet[5];
        if (a && !b && !c) return ruleSet[4];
        if (!a && b && c) return ruleSet[3];
        if (!a && b && !c) return ruleSet[2];
        if (!a && !b && c) return ruleSet[1];
        if (!a && !b && !c) return ruleSet[0];
        return false;
    }


    /*void setRule(int rule) {
        char[] srule = System.Convert.ToString (rule, 8).toCharArray();
        for (int i = 0; i < srule.length; i++) {
            if (srule[i] == '1') {
                ruleSet[i] = true;
            }
            else {
                ruleSet[i] = false;
            }
        }

    }*/


}
