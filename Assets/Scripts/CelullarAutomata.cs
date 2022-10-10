using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelullarAutomata : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] int j_reticula,i_caHeight,cellSize,caRule;
    [SerializeField] bool isBasicCondition;
    bool[,] matrix;
    bool[] ruleSet;
    // Start is called before the first frame update
    void Start()
    {
        matrix = new bool[i_caHeight, j_reticula];
        ruleSet = new bool[8];

        if (isBasicCondition)
        {
            BasicCondition();
        }
        else
        {
            RandomCondition();
        }
        
        
        setRule(caRule);
        Evolution();
        paint();
    }

    public void Automata(int cells) {
        /*cellSize = cells;
        j_reticula = (int)reticula.GetComponent<Renderer>().bounds.size.x / cellSize;
        i_caHeight = (int)reticula.GetComponent<Renderer>().bounds.size.y / cellSize;*/
        
        
    }

    public void paint() {
        for (int i = 0; i <i_caHeight; i++) {
            for(int j=0; j < j_reticula; j++) {
                if (matrix[i, j]) {
                    Instantiate(platform, new Vector3(cellSize * i, 0, cellSize * j), Quaternion.identity,transform);
                }
               /* else {
                    cubes.color = new Color(0, 0, 0);
                }*/
               //Instantiate(myCubes, new Vector3(j*cellSize,i*cellSize), Quaternion.identity, reticula.transform);
            }
        }
    }

    public void BasicCondition() {
        matrix[0, j_reticula / 2] = true;
    }

    public void RandomCondition() {
        
        for(int j=0; j < j_reticula; j++) {
            if ((UnityEngine.Random.Range(0, 2)) == 1) {
                matrix[0, j] = true;
            }
        }
    }

    public void Evolution() {
        for (int i = 0; i < i_caHeight - 1; i++) {
            for (int j = 0; j < j_reticula; j++) {
                if (j == 0) {
                    matrix[i + 1,j] = rule(matrix[i,j_reticula - 1], matrix[i,j], matrix[i,j+1]);
                    continue;
                }
                if (j == (j_reticula - 1))
                {
                    matrix[i + 1, j] = rule(matrix[i, j - 1], matrix[i, j], matrix[i, 0]);
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


    void setRule(int rule) {
        string sBinary = System.Convert.ToString(rule, 2).PadLeft(8, '0');
        char[] srule = sBinary.ToCharArray();
        for (int i = 0; i < srule.Length; i++) {
            if (srule[i] == '1') {
                ruleSet[i] = true;
            }
            else {
                ruleSet[i] = false;
            }
        }

    }


}
