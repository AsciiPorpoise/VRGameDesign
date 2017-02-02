using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class TextSync : MonoBehaviour
{

    DatabaseReference tref;

    // Use this for initialization
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://droidremote-6f969.firebaseio.com/");
        tref = FirebaseDatabase.DefaultInstance.RootReference.Child("text");
        tref.SetValueAsync("Hello World!");
        tref.ValueChanged += HandleChange;
    }

    void HandleChange(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        gameObject.GetComponent<TextMesh>().text = (string)args.Snapshot.Value;
    }
}