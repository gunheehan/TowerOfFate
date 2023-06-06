using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    private Bullet bulletPrefab = null;
    private GameObject bulletContents = null;

    private List<Bullet> bulletList = new List<Bullet>();
    private Stack<Bullet> bulletPool = new Stack<Bullet>();
    
    void Start()
    {
        GameObject bulletObject = ObjectManager.Instance.GetObject("Bullet");
        bulletPrefab = bulletObject.GetComponent<Bullet>();
        
        bulletContents = new GameObject();
        bulletContents.name = "BulletContents";
    }

    private void PushAllBullet()
    {
        foreach (Bullet bulletobj in bulletList)
        {
            bulletPool.Push(bulletobj);
            bulletobj.gameObject.SetActive(false);
        }
        bulletList.Clear();
    }

    public Bullet GetBullet()
    {
        Bullet bulletobj;

        if (bulletPool.Count > 0)
        {
            bulletobj = bulletPool.Pop();
        }
        else
        {
            bulletobj = Instantiate(bulletPrefab,bulletContents.transform);
        }
        bulletList.Add(bulletobj);
        
        return bulletobj;
    }

    public void SpawnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Push(bullet);
        bulletList.Remove(bullet);
    }
}
