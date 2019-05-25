using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImage : MonoBehaviour
{
    public Image image;

    public void TakePicture()
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            if (path == null)
                return;
            Texture2D texture = NativeCamera.LoadImageAtPath(path, 512);
            if (texture == null)
                return;
            image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            
        }, 512);
    }

    public void PickImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path == null)
                return;
            Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
            if (texture == null)
                return;
            image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }, "Select a PNG image", "image/png", 512);
    }
}
