using System.Collections;
using System.Collections.Generic;
using SDE;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    private Transform enemyHolder;
    public float speed;
	public float maxLatMov;
    public GameObject shot;
    public Text winText;
    public float fireRate = 0.997f;

    private Vector3 mStartPos;
    private static int mEnemyDeathCount = 0;

    public static event System.Action OnNoEnemiesLeft;
    
    public static void ReduceEnemyCount()
    {
        if (--mEnemyDeathCount <= 0)
            OnNoEnemiesLeft.TryInvoke();
    }
   
    
	// Use this for initialization
	void Awake () {
        enemyHolder = GetComponent<Transform>();

	    mStartPos = transform.localPosition;
	    mEnemyDeathCount = enemyHolder.childCount;
	}

    
    public void Respawn()
    {
        transform.localPosition = mStartPos;
        foreach (Transform enemy in enemyHolder)
            enemy.gameObject.SetActive(true);

        mEnemyDeathCount = enemyHolder.childCount;
    }

    public void StartMoveEnemy()
    {
        //StartCoroutine(MoveEnemy());
        InvokeRepeating("MoveEnemy", 0.1f, 0.3f);
    }

    public void StopMoveEnemy()
    {
        //StopAllCoroutines();
        CancelInvoke();
    }

//    IEnumerator MoveEnemy()
//    {
//        bool enemyReachedBottom = false;
//        do
//        {
//            foreach(Transform enemy in enemyHolder)
//            {
//                Vector2 pos = enemy.localPosition + enemyHolder.localPosition;
//                if(pos.x < -maxLatMov || pos.x > maxLatMov)
//                {
//                    Debug.Log("Hello");
//                    speed = -speed;
//                    enemyHolder.localPosition += Vector3.down * 0.5f;
//                }
//                else
//                {
//                    //EnemyBulletController called too?
//                    if(Random.value > fireRate)
//                    {
//                        Instantiate(shot,enemy.position,enemy.rotation);
//                    }
//
//                    enemyReachedBottom = enemy.localPosition.y <= -4;
//                    if(enemyReachedBottom)
//                        break;
//                }
//            }
//            
//            yield return new WaitForSeconds(0.25f);
//        } while (!enemyReachedBottom && mEnemyDeathCount > 0);
//        MiniGameSpaceInvaders.EndCurrentGame();
//    }
//    
    void MoveEnemy()
    {
        enemyHolder.localPosition += Vector3.right * speed;
        foreach(Transform enemy in enemyHolder)
        {
            Vector2 pos = enemy.localPosition + new Vector3(enemyHolder.localPosition.x, 0, 0);
			if(pos.x < -maxLatMov || pos.x > maxLatMov)
            {
                speed = -speed;
                enemyHolder.localPosition += Vector3.down * 0.5f;
                return;
            }

            //EnemyBulletController called too?
            if(Random.value > fireRate)
            {
                Instantiate(shot,enemy.position,enemy.rotation);
            }

            if(pos.y <= -4)
            {
                SIGameOver.isPlayerDead = true;
                MiniGameSpaceInvaders.EndCurrentGame();
            }
        }

    }

}
