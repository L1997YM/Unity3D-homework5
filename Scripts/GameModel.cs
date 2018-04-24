using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class GameModel : MonoBehaviour {
	private bool shooting;
	public bool isShooting() {return shooting;}

	private List<GameObject> disks = new List<GameObject>();  //发射的飞碟对象列表
	private List<int> diskIds = new List<int>();              //发射的飞碟id列表
	private int diskScale;                                    //飞碟大小
	private  Color diskColor;                                 //飞碟颜色
	private Vector3 emitPosition;                             //发射位置
	private Vector3 emitDirection;                            //发射方向
	private float emitSpeed;                                  //发射速度
	private int emitNumber;                                   //发射数量
	private bool emitEnable;                                  //是否运行新的发射事件
  private int diskrand;                                     //决定红飞碟还是绿飞碟的随机数
  private int diskScore;                                    //飞碟分数
  private float multiple;                                   //飞碟速度倍数

	private SceneController scene;

  void Awake () {
  		scene = SceneController.getInstance();
  		scene.setGameModel(this);
  	}

  public void setmul(float speed_multiple) {multiple = speed_multiple;}

	public void readyToEmitDisk() { if(!shooting) emitEnable = true;}

  //发射飞碟
	void emitDisks(){
    diskrand = Random.Range(0,10);
    if(diskrand < 5) {
      diskColor = Color.green;
      diskScore = 5;
      diskScale = 2;
      diskScore = 5;
      emitSpeed = 2 * multiple;
      emitNumber = 1;
      emitPosition = new Vector3(-2.5f, 0.2f, -5f);
      emitDirection = new Vector3(20f, 40.0f, 67f);
    }else {
      diskColor = Color.red;
      diskScore = 10;
      diskScale = 1;
      diskScore = 10;
      emitSpeed = 4 * multiple;
      emitNumber =2;
      emitPosition = new Vector3(2.5f, 0.2f, -5f);
      emitDirection = new Vector3(-20f, 35.0f, 67f);
    }
		for(int i = 0;i < emitNumber; ++i)
		{
			diskIds.Add(DiskFactory.getInstance().getDisk());
			disks.Add(DiskFactory.getInstance().getDiskObject(diskIds[i]));
      disks[i].transform.localScale *= diskScale;
			disks[i].GetComponent<Renderer>().material.color = diskColor;
			disks[i].transform.position = new Vector3(
			                     emitPosition.x,emitPosition.y + i,emitPosition.z);
			disks[i].SetActive(true);
			disks[i].GetComponent<Rigidbody>().AddForce(emitDirection.normalized *
              Random.Range(emitSpeed * 5,emitSpeed * 10)/10,ForceMode.Impulse);
		}
	}

	//回收飞碟
	void recycdisk(int i){
		DiskFactory.getInstance().cycle(diskIds[i]);
		disks.RemoveAt(i);
		diskIds.RemoveAt(i);
	}

	void FixedUpdate(){
	  if(emitEnable)
	  {
		  emitDisks(); //发射飞碟
		  emitEnable = false;
		  shooting = true;
	  }
	}

	void Update() {
		for(int i = 0;i < disks.Count; ++i)
		{
			if(!disks[i].activeInHierarchy)
			{
				scene.getrule().scoreADisk(diskScore);
        recycdisk(i);
			}
			else if(disks[i].transform.position.y < 0)
			{
				scene.getrule().failADisk();
				recycdisk(i);
			}
		}
		if(disks.Count == 0)
		{
			shooting = false;
		}
	}
}
