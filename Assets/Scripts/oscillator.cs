using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 directionVector;
    [SerializeField] float period;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {       
        
        float parametricConstant;
        if(! (Mathf.Abs(period)<=Mathf.Epsilon) )
            parametricConstant=Mathf.Sin(2*Mathf.PI /period *Time.time ) +1 ;    // f(t) = sin(2pi ft)+1 = [0,2]
        else
            parametricConstant=0;

        transform.position= startingPosition + parametricConstant*directionVector;
    }
}
