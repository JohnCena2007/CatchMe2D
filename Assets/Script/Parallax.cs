using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect; //tốc độ cuộn
    //Nếu cho parallax = 1 thì nó luôn bám sát màn hình camera => Nhìn như k thay đổi gì, k có độ cuốn của ảnh
    //Càng gần vs 1 thì càng k thay đổi
    void Start()
    {
        startPos=transform.position.x;
        length=GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate() {
        float temp= cam.transform.position.x * (1-parallaxEffect);
        float dis =cam.transform.position.x * parallaxEffect; 
//Khi mà tốc độ cuộn bằng 0 => Nó bám sát camera . Nếu nó là 0.5 thì nó sẽ giảm đi 1 nữa 
//=> Nó chỉ bám sát 50% tốc độ của camera, kiểu pos ở của camera ở giữa bức tranh thì pos của background chỉ ở
//đầu bức tranh thôi

        transform.position=new Vector3(startPos + dis, transform.position.y, transform.position.z);

        if(temp>startPos +length) startPos +=length;
//VD với parallaxEffect là 0 thì temp chính là vị trí của camera => Background sẽ bám theo camera 100%
//Khi mà temp > startPos +length, tức là nó đã đến cuối giữa bức tranh thứ 3, vì startPos ở là ở bức tranh thứ 2
//Thì nó phải cộng thêm length (chiều dài của bức tranh) để nó đến trc camera 1 bức tranh.
        else if(temp<startPos- length) startPos-=length;
    }

}

/*
parallaxEffect là hệ số xác định mức độ chuyển động ngược chiều của background so với camera
Giá trị từ 0 đến 1.
Vị trí camera là vị trí hiện tại mà camera đang ở.
(1 - parallaxEffect) là phần vị trí mà background sẽ bám theo camera.
Ví dụ parallaxEffect = 0.5 thì (1 - 0.5) = 0.5. Nghĩa là background sẽ bám theo 50% chuyển động của camera.

Nhân vị trí camera với (1 - parallaxEffect) ta được vị trí mới mà background cần bám theo camera
, lưu vào biến temp. Như vậy temp là vị trí mới của background sau khi bám theo phần chuyển động của camera
*/