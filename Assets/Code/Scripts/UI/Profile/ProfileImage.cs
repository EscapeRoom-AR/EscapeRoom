using UnityEngine;
using UnityEngine.UI;
using Services;
using System;
using UnityEditor;

public class ProfileImage : MonoBehaviour
{
    public Image image;
    public GameObject modal;
    public RESTService restService;
    public ModalService modalService;
       
    public void TakePicture()
    {
        FXService.Instance.PlayClick();
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            if (path == null)
                return;
            Texture2D texture = NativeCamera.LoadImageAtPath(path, 512);
            if (texture == null)
                return;
            image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            PostImage(texture);
        }, 512);
    }

    public void PickImage()
    {
        FXService.Instance.PlayClick();
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path == null)
                return;
            Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
            if (texture == null)
                return;
            Destroy(modal);
            image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            PostImage(texture);
        }, "Select a PNG image", "image/png", 512);
    }

    public void PostImage(Texture2D texture)
    {
        
        try
        {
            Texture2D copy = duplicateTexture(texture);
            byte[] imageData = copy.EncodeToPNG();
            string base64 = Convert.ToBase64String(imageData);
            restService.PostPhoto(base64,apiResponse => {
                restService.GetImage(apiResponse.data, sprite => image.sprite = sprite);
            });
        } catch (Exception e)
        {
            modalService.ShowModal(e.Message);
        }
    }

    private Texture2D duplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(source.width,source.height,0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}
