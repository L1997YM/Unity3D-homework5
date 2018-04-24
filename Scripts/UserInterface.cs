using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Com.Mygame;

public class UserInterface : MonoBehaviour{
  public Text mainText;
  public Text scoreText;
  public Text roundText;
  private int round;
  public GameObject bullet;
  public ParticleSystem explosion;
  public float speed = 1000f;
  private IUserInterface userInt;
  private IQueryStatus queryInt;

  void Start(){
    bullet = GameObject.Instantiate(bullet) as GameObject;
    explosion = GameObject.Instantiate(explosion) as ParticleSystem;
    userInt = SceneController.getInstance() as IUserInterface;
    queryInt = SceneController.getInstance() as IQueryStatus;
  }

  void Update(){
    if(Input.GetKeyDown("space"))
    {
      userInt.emitDisk();
    }
    if(queryInt.isShooting())
    {
      mainText.text = "";
    }
    if(queryInt.isShooting() && Input.GetMouseButtonDown(0))
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
      bullet.transform.position = transform.position;
      bullet.GetComponent<Rigidbody>().AddForce(
                      ray.direction * speed,ForceMode.Impulse);
      RaycastHit hit;
      if(Physics.Raycast(ray,out hit) &&
                      hit.collider.gameObject.tag == "Disk")
      {
        explosion.transform.position = hit.collider.gameObject.transform.position;
        explosion.GetComponent<Renderer>().material.color =
              hit.collider.gameObject.GetComponent<Renderer>().material.color;
        explosion.Play();
        hit.collider.gameObject.SetActive(false);
      }
    }
    roundText.text = "Round: " + queryInt.getRound().ToString();
    scoreText.text = "Score: " + queryInt.getPoint().ToString();
    if(round != queryInt.getRound())
    {
      round = queryInt.getRound();
      mainText.text = "空格键发射";
    }
  }
}
