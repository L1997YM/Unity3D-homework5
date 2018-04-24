using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

namespace Com.Mygame{
  public class DiskFactory : System.Object{
    private static DiskFactory _instance;
    private static List<GameObject> diskList;    //飞碟队列
    public GameObject diskTemplate;              //预设对象

    public static DiskFactory getInstance()
    {
      if(_instance == null)
      {
        _instance = new DiskFactory();
        diskList = new List<GameObject>();
      }
      return _instance;
    }

    public int getDisk(){
      for(int i = 0;i < diskList.Count; ++i)
			{
        if(!diskList[i].activeInHierarchy) {return i;}
      }
      diskList.Add(GameObject.Instantiate(diskTemplate) as GameObject);
      return diskList.Count - 1;
    }

    public GameObject getDiskObject(int id) { return diskList[id];}

    public void cycle(int id){
			diskList[id].GetComponent<Rigidbody>().velocity = Vector3.zero;
			diskList[id].transform.localScale = diskTemplate.transform.localScale;
			diskList[id].SetActive(false);
    }
  }

  public interface IUserInterface{
    void emitDisk();
  }

  public interface IQueryStatus{
    bool isShooting();
    int getRound();
    int getPoint();
  }

  public interface IJudgeEvent{
    void nextRound();
    void setPoint(int point);
  }

  public class SceneController:System.Object,IQueryStatus,
                                 IUserInterface,IJudgeEvent{
    private static SceneController _instance;
    private SceneControllerBC _scene;
    private GameModel _gameModel;
    private Rule _rule;
    private int _round = 0;
    private int _point;
    public static SceneController getInstance(){
      if(_instance == null){
        _instance = new SceneController();
      }
      return _instance;
    }

    public void setGameModel(GameModel obj) {_gameModel = obj;}
    internal GameModel getGameModel() {return _gameModel;}
    public void setrule(Rule obj) {_rule = obj;}
    internal Rule getrule() {return _rule;}
    public void setSceneControllerBC(SceneControllerBC obj) {_scene = obj;}
    internal SceneControllerBC getSceneControllerBC() {return _scene;}

    public void emitDisk() {_gameModel.readyToEmitDisk();}

    public bool isShooting() {return _gameModel.isShooting();}
    public int getRound() {return _round;}
    public int getPoint() {return _point;}

    public void setPoint(int point) {_point = point;}
    public void nextRound() {
      _point = 0;
			if(_round < 4) _scene.loadRoundData(++_round);
    }
		public void setRound(int round_)
		{
			_round = round_;
			_scene.loadRoundData(_round);
		}
  }
}
