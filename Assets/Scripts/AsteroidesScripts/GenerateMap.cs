using UnityEngine;
using System.Collections;

public class GenerateMap : MonoBehaviour {
    float posX, posZ, angle;
    public float rayon = 1;
    public float rotateSpeed = 1;
    public float nbFatAsteroides = 5;
    public float nbMediumAsteroides = 5;
    Vector3 pos = new Vector3(0, 0, 0);
    public GameObject asteroideFat;
    public GameObject asteroideMedium;
    GameObject[] AsteroidsFatArray;
    GameObject[] AsteroidsMediumArray;

   public int pullForce, pullRayon = 1;
    
    // GameObject Rock;
    // Use this for initialization
    void Start ()
    {
        AsteroidsFatArray = new GameObject[(int)nbFatAsteroides];
        AsteroidsMediumArray = new GameObject[(int)nbMediumAsteroides];
        float speedRock;
        angle = (360 / (nbFatAsteroides/2)) * Mathf.Deg2Rad;

        for (int i = 0; i < nbFatAsteroides; i++)
        {
            // speedRock = Random.Range(1, 15);
            if (i < nbFatAsteroides / 2) //extern ring
            {
                posX = rayon*Random.Range(1.5f,2.5f) * Mathf.Cos(angle * i);
                posZ = rayon* Random.Range(1.5f, 2.5f) * Mathf.Sin(angle * i);
                pos.Set(posX, 0, posZ);
                AsteroidsFatArray[i] = Instantiate(asteroideFat);//,pos, new Quaternion(0,0,0,0));
                AsteroidsFatArray[i].transform.position = pos;
                //  AsteroidsFatArray[i].GetComponent<rotateAst>().setSpeed(speedRock);

            }
            else //intern ring
            {
                posX = rayon / Random.Range(1.5f, 2.5f) * Mathf.Cos(angle * i-nbFatAsteroides/2);
                posZ = rayon / Random.Range(1.5f, 2.5f) * Mathf.Sin(angle * i-nbFatAsteroides / 2);
                pos.Set(posX, 0, posZ);
                AsteroidsFatArray[i] = Instantiate(asteroideFat);//,pos, new Quaternion(0,0,0,0));
                AsteroidsFatArray[i].transform.position = pos;
            }
        }

        angle = (360 / (nbMediumAsteroides/2)) * Mathf.Deg2Rad;

        for (int i = 0; i < nbMediumAsteroides; i++) //instatiate mediums asteroids
        {

            if (i < nbMediumAsteroides / 2) { //extern ring
                posX = rayon* Random.Range(1.5f, 2.5f) * Mathf.Cos(angle * i +20);//offset angle = 20
                posZ = rayon* Random.Range(1.5f, 2.5f) * Mathf.Sin(angle * i +20);
                pos.Set(posX, Random.Range(0.5f, 3.5f), posZ);
                AsteroidsMediumArray[i] = Instantiate(asteroideMedium);
                AsteroidsMediumArray[i].transform.position = pos;
                // AsteroidsMediumArray[i].GetComponent<rotateAst>().setSpeed(speedRock);
            }
            else//intern ring
            { 
                posX = (rayon/ Random.Range(1.5f, 2.5f)) * Mathf.Cos(angle * i - nbMediumAsteroides / 2+20); //offset angle = 20
                posZ = (rayon/ Random.Range(1.5f, 2.5f)) * Mathf.Sin(angle * i - nbMediumAsteroides / 2+20);
                pos.Set(posX, 0, posZ);
                AsteroidsMediumArray[i] = Instantiate(asteroideMedium);
                AsteroidsMediumArray[i].transform.position = pos;
                // AsteroidsMediumArray[i].GetComponent<rotateAst>().setSpeed(speedRock);
            }

        }
       
	}
	
	// Update is called once per frame
/*	void Update () {
        for (int i = 0; i < AsteroidsFatArray.Length; i++)
        {
            AsteroidsFatArray[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), Time.deltaTime * rotateSpeed);
            /*if (AsteroidsFatArray[i].transform.position.y >= 10)
            {
                AsteroidsFatArray[i].transform.position.Set(AsteroidsFatArray[i].transform.position.x, AsteroidsFatArray[i].transform.position.y - 10, AsteroidsFatArray[i].transform.position.z);
            }*/
            /* else if (AsteroidsFatArray[i].transform.position.y <= -10)
             {
                 AsteroidsFatArray[i].transform.position.Set(AsteroidsFatArray[i].transform.position.x, AsteroidsFatArray[i].transform.position.y + 10, AsteroidsFatArray[i].transform.position.z);
             }*/
    /*   }
        for (int i = 0; i < AsteroidsMediumArray.Length; i++)
        {
            AsteroidsMediumArray[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), Time.deltaTime * rotateSpeed*2);
        }
    } // */

    public void FixedUpdate()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRayon))
        {
            Vector3 forceDirection = gameObject.transform.position - collider.transform.position;
            collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.deltaTime);
            collider.transform.RotateAround(gameObject.transform.position, new Vector3(0, 1, 0), Time.deltaTime * rotateSpeed * 2);
            Debug.Log(collider.name);
        }
    }
}


//selectionner tout les objects autour du rayon, les faire tourner et les attirer.

//Placer les vaisseax mères
//obtenir le nombre de vaisseaux meres les repartir suivant leurs equipe , les placer en arc de cercle de part et d'autre de la map