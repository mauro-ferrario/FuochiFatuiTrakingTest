using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {
	public static float ofMap(float value, float inputMin, float inputMax, float outputMin, float outputMax, bool clamp) 
	{
		float outVal = ((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin);
	
		if(clamp){
			if(outputMax < outputMin){
				if( outVal < outputMax )outVal = outputMax;
				else if( outVal > outputMin )outVal = outputMin;
			}else{
				if( outVal > outputMax )outVal = outputMax;
				else if( outVal < outputMin )outVal = outputMin;
			}
		}
		return outVal;
	}
}
