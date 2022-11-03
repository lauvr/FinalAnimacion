using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKSwitcher : MonoBehaviour
{
    // este script va en los Target IKs de la mano y pies
   //Acceder desde el inspector a la variable peso y al arreglo , en el target chain asigna todos los huesos que tienen parent contraint
    [SerializeField] [Range(0,1)] private float weight;
    [SerializeField] private MultiParentConstraint[] targetChain;
    
    
    
    public void setConstraintWeights() 
    {

        //revisa los valores antes de usar el cilco for para pasar por cada targetchain cambiando el valor
        if( targetChain == null || targetChain.Length<=0 ) return;
        //a aqi la funsion es publica para poder llamar directamente la funsion al momento de monstar el resto del controlador de personajes y setear lso pesos al momento de requerirlos, (ejemplo agarrar objeto de forma procedural)  
        
        
        
        
    for (int i = 0; 1< targetChain.Length; i++) 
        {
            targetChain[i].weight = weight;
        }
    }
    //


    // cada que muevas el slider de pesos, actualiza el peso de todos incluso fuera del playmode, el debug avisa en consola son molestos para un entorno de desarrollo, recuerda borrarlos
    private void OnValidate()
    {
        Debug.Log("Weights Changed");
        setConstraintWeights();

    }



}
