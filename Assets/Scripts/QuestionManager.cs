using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public RawImage image;
    public RawImage image2;
    public Text questionText;

    public GameObject[] spawnAreas;
    public GameObject spawnObject;

    private Camera _cam;

    public LayerMask layer;

    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
        // if (!(questionText is null))
        //     GetQuestion();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var rayHit, 100, layer)) return;
        var component = rayHit.collider?.GetComponent<Obj>();
        if (component is null) return;
        if (component.isCorrectAnswer)
            GetQuestion();
        Debug.Log(component.isCorrectAnswer);
    }

    private void ClearObjects()
    {
        var objects =
            GameObject.FindGameObjectsWithTag("Cube");
        if (objects is null) return;
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
    }

    public async void GetQuestion()
    {
        ClearObjects();
        var question = await Constants.ServiceManager.RandomQuestion();
        if (question is null) return;
        questionText.text = question.QuestionText;
        GetAnswers(question.QuestionId.ToString());
    }

    public async void UploadImage()
    {
        // Answers ans = new Answers();
        // ans.AnswerId = 10;
        // ans.ImageUrl = GetBytes();
        // ans.AnswerText = "Test";
        // var t = await Constants.ServiceManager.PostAnswer(ans);
    }

    public async void GetImage()
    {
        // var answers = await Constants.ServiceManager.GetAnswers("0");
        // var answer = answers.FirstOrDefault();
        // if (answer is null) return;
        var bytes = GetBytes(image.texture as Texture2D);
        image2.texture = GetTexture(bytes);
    }

    private byte[] GetBytes(Texture2D texture)
    {
        return texture == null ? null : texture.GetRawTextureData();
        // if (!texture.Resize(100, 100)) return null;
        // texture.Apply();
    }

    private static Texture GetTexture(byte[] data)
    {
        // Create a 16x16 texture with PVRTC RGBA4 format
        // and fill it with raw PVRTC bytes.
        // Texture2D tex = new Texture2D(2, 2);
        Texture2D tex = new Texture2D(100, 100, TextureFormat.RGBA32, false);
        // Raw PVRTC4 data for a 16x16 texture. This format is four bits
        // per pixel, so data should be 16*16/2=128 bytes in size.
        // Texture that is encoded here is mostly green with some angular
        // blue and red lines.

        // Load data into the texture and upload it to the GPU.
        tex.LoadImage(data); //LoadRawTextureData(data);
        // tex.LoadRawTextureData(data); //LoadRawTextureData(data);
        tex.Apply();
        // Assign texture to renderer's material.
        return tex;
    }

    private async void GetAnswers(string questionId)
    {
        var answers = await Constants.ServiceManager.GetAnswers(questionId);
        if (answers is null || !answers.Any()) return;
        var startIndex = Random.Range(0, 3);
        var isNext = Random.Range(0, 2);
        foreach (var answer in answers)
        {
            //Debug.Log(startIndex);
            var position = spawnAreas[startIndex].transform.position;
            SpawnObject(position, answer.IsCorrect, float.Parse(answer.AnswerText));
            if (isNext % 2 == 0)
                startIndex++;
            else
            {
                startIndex--;
            }

            if (startIndex < 0)
                startIndex = spawnAreas.Length - 1;
            else if (startIndex >= spawnAreas.Length)
                startIndex = 0;
        }
    }

    private void SpawnObject(Vector3 position, bool isCorrect, float scaleY)
    {
        var instantiate = Instantiate(spawnObject, position, Quaternion.identity);
        instantiate.GetComponent<Obj>().isCorrectAnswer = isCorrect;
        var localScale = instantiate.transform.localScale;
        localScale =
            new Vector3(localScale.x, scaleY, localScale.z);
        instantiate.transform.localScale = localScale;
    }
}