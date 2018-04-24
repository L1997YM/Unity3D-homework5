using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

namespace Com.Mygame{
  public class PhysicsManager {
  	  public SceneController sense;
      private static PhysisManager instance;

      private PhysicsManager()
      {
          scene = SceneController.getInstance();
      }

      public static PhysisActionManager getInstance()
      {
          instance = new PhysicsManager();
          return instance;
      }

      public void addForce(GameObject gaobject)
      {
          gaobject.GetComponent<Rigidbody>().useGravity = true;
          gaobject.GetComponent<Rigidbody>().AddForce(emitDirection.normalized *
                Random.Range(20,30)/10,ForceMode.Impulse);
      }
    }
}
