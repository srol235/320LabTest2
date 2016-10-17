using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
    public GameObject myPrefab;
    // Put your URL here
    public string _WebsiteURL = "http://srol235.azurewebsites.net/tables/TreeSurvey?zumo-api-version=2.0.0";

    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        TreeSurvey[] TreeSurvey = JsonReader.Deserialize<TreeSurvey[]>(jsonResponse);

        int totalCubes = 12;
        int totalDistance = 5;
        int i = 0;
        //We can now loop through the array of objects and access each object individually
        foreach (TreeSurvey Location in TreeSurvey)
        {
            //Example of how to use the object
            Debug.Log(TreeSurvey.TreeSurvey);
            float perc = i / (float)totalCubes;
            i++;
            float x = perc * totalDistance;
            float y = 5.0f;
            float z = 0.0f;
            GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
            newCube.GetComponent<myCubeScript>().setSize((1.0f - perc) * 2);
            newCube.GetComponent<myCubeScript>().ratateSpeed = perc;
            newCube.GetComponentInChildren<TextMesh>().text = TreeSurvey.TreeSurvey;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
