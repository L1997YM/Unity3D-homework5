using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

namespace Com.Mygame{
  public class CCActionManager {
    public SceneController sense;
    private static CCActionManager instance;

    private CCActionManager()
    {
        scene = SceneController.getInstance();
    }

    public static CCActionManager getInstance()
    {
        instance = new CCActionManager();
        return instance;
    }

    public void addForce(GameObject gaobject)
    {
      gaobject.GetComponent<Rigidbody>().useGravity = false;
      gaobject.GetComponent<Rigidbody>().velocity = new Vector3(3, 10, 3);
    }
  }
}
