using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersAutomata : MonoBehaviour
{
    [SerializeField] List<GameObject> flowers;
    [SerializeField] GameObject mouse;
    [SerializeField] GameObject cat;
    [SerializeField] int randomRule;
    [SerializeField] int randomFlower;
    [SerializeField] int randomMouse;
    [SerializeField] int randomCat;
    [SerializeField] Transform targetPlatform;
    [SerializeField] int j_reticula, i_caHeight, cellSize, caRule;
    [SerializeField] bool isBasicCondition;
    bool[,] matrix;
    bool[] ruleSet;
    // Start is called before the first frame update
    void Start() {
        randomRule = Random.Range(1, 257);
        matrix = new bool[i_caHeight, j_reticula];
        ruleSet = new bool[8];
        RandomCondition();
        setRule(randomRule);
        Evolution();
        paint();
    }

    public void paint() {
        for (int i = 0; i < i_caHeight; i++) {
            for (int j = 0; j < j_reticula; j++) {
                randomFlower = Random.Range(0, flowers.Count);
                randomMouse = Random.Range(0, 2);
                randomCat = Random.Range(0, 2);
                if (matrix[i, j]) {
                    Instantiate(flowers[randomFlower], new Vector3((transform.position.x-10) + (i* cellSize), targetPlatform.position.y+2f, (transform.position.z - 10) + (j * cellSize)), Quaternion.identity, transform);
                    if (randomMouse == 1) {
                        Instantiate(mouse, new Vector3((transform.position.x - 10) + (i * cellSize), targetPlatform.position.y + 2f, (transform.position.z - 10) + (j * cellSize)), Quaternion.identity, transform);
                    }
                    if (randomCat == 1) {
                        Instantiate(cat, new Vector3((transform.position.x - 10) + (i * cellSize), targetPlatform.position.y + 2f, (transform.position.z - 10) + (j * cellSize)), Quaternion.identity, transform);
                    }
                }
            }
        }
    }

    public void BasicCondition() {
        matrix[0, j_reticula / 2] = true;
    }

    public void RandomCondition() {

        for (int j = 0; j < j_reticula; j++) {
            if ((UnityEngine.Random.Range(0, 2)) == 1) {
                matrix[0, j] = true;
            }
        }
    }

    public void Evolution() {
        for (int i = 0; i < i_caHeight - 1; i++) {
            for (int j = 0; j < j_reticula; j++) {
                if (j == 0) {
                    matrix[i + 1, j] = rule(matrix[i, j_reticula - 1], matrix[i, j], matrix[i, j + 1]);
                    continue;
                }
                if (j == (j_reticula - 1)) {
                    matrix[i + 1, j] = rule(matrix[i, j - 1], matrix[i, j], matrix[i, 0]);
                    continue;
                }
                matrix[i + 1, j] = rule(matrix[i, j - 1], matrix[i, j], matrix[i, j + 1]);
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
