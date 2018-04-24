using UnityEngine;
using System.Collections;
using Com.Mygame;

public class SceneControllerBC : MonoBehaviour {
	private float speed_multiple;

	void Awake() {
		SceneController.getInstance().setSceneControllerBC(this);
	}

	public void loadRoundData(int round) {
		switch(round) {
		case 1:
			speed_multiple = 0.8f;
			break;
		case 2:
			speed_multiple = 1.0f;
			break;
    case 3:
      speed_multiple = 1.2f;
      break;
    case 4:
      speed_multiple = 1.5f;
      break;
		}
    SceneController.getInstance().getGameModel().setmul(speed_multiple);
	}
}
