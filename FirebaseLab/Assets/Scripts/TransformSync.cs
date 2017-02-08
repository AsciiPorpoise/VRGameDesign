using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class TransformSync : MonoBehaviour
{

    DatabaseReference tref;

    // Use this for initialization
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://droidremote-6f969.firebaseio.com/");
        tref = FirebaseDatabase.DefaultInstance.RootReference.Child("pos");
        tref.SetValueAsync(transform.position.x);
        tref.ValueChanged += HandleChange;
    }

    void HandleChange(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log("newVal:" + args.Snapshot.Value);
        transform.position = new Vector3(System.Convert.ToSingle(args.Snapshot.Value),0f,0f);
    }

    Vector3 pos;

    void Update()
    {
        transform.Translate(new Vector3((Input.GetKey(KeyCode.D) ? .1f : Input.GetKey(KeyCode.A) ? -.1f : 0), 0, 0));
        tref.SetValueAsync(pos.x);
    }
}