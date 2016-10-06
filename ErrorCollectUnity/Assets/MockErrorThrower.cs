using System.Collections;
using UnityEngine;

public class MockErrorThrower : MonoBehaviour {
    
    GameObject _nullRef;
    int[] _ioor = new[] { 0, 1, 2 };

    IEnumerator Start() {
        yield return new WaitForSeconds(1.5f);
        ErrorCollect.StartSession("localhost", 2358, "Test User");
    }
    
    void OnGUI() {

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height)); 
        var nullRef = GUILayout.Button("Null Ref", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if (nullRef) {
            //Debug.Log("Casing Null Ref.");
            //Debug.Log(_nullRef.name);
            throw new System.NullReferenceException("Object reference is not set to an instance of an object");
        }
        
        var indexOutOfRange = GUILayout.Button("IndexOOR", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if (indexOutOfRange) {
            Debug.Log("Causing Index out of range.");
            Debug.Log(_ioor[3]);
        }

        var divideByZero = GUILayout.Button("Divide by 0", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if (divideByZero) {
            Debug.Log("Breakin the universe.");
            var zero = 0;
            Debug.Log(1 / zero);
        }

        var showSession = GUILayout.Button("Not Implimented", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if (showSession) {
            Debug.Log("Throwing not NotImplimentedException");
            throw new System.NotImplementedException("But actually, Is implimented!");
        }

        GUILayout.EndArea();

    }
}
