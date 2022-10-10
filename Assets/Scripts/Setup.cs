using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    public CelullarAutomata ca;

    private void Start() {

        ca.Automata(10);
        ca.RandomCondition();
        ca.Evolution();
        ca.paint();
       /* ca = ca.Automata(10);
        ca.randomCondition();
        //ca.basicCondition();
        ca.setRule(40);
        ca.Evolution();*/
    }

}
