using System.Collections.Generic;
using UnityEngine;

public class ObjectMaterial : MonoBehaviour
{
    [SerializeField] List<GameObject> randomObjectList = new List<GameObject>();
    int randomIndex;

    public void RandomMaterial()
    {
        for(int i = 0; i < randomObjectList.Count; i++)
        {
            randomObjectList[i].GetComponent<MeshRenderer>().materials[0].color = RandomColor();
        }
    }
    Color RandomColor()
    {
        Color randomColor = Color.white;
        randomIndex = 0;
        randomIndex = Random.Range(0 , 5);
        switch (randomIndex)
        {
            case 0:
                randomColor = Color.blue;
                break;
            case 1:
                randomColor = Color.red;
                break;
            case 2:
                randomColor = Color.green;
                break;
            case 3:
                randomColor = Color.cyan;
                break;
            case 4:
                randomColor = Color.magenta;
                break;
            default:
                randomColor = Color.black;
                break;

        }
        return randomColor;
    }
}
