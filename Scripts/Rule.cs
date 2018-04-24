using UnityEngine;
using System.Collections;
using Com.Mygame;

public class Rule : MonoBehaviour {
  public int failScore = 10;
  public int winscore = 40;
  private SceneController scene;

  void Awake() {
    scene = SceneController.getInstance();
    scene.setrule(this);
  }

  void Start() {
    scene.setRound(1);
  }

  public void scoreADisk(int diskScore) {
    scene.setPoint(scene.getPoint() + diskScore);
    if(scene.getPoint() >= winscore) {
      scene.nextRound();
    }
  }

  public void failADisk() {
    scene.setPoint(scene.getPoint() - failScore);
  }
}
