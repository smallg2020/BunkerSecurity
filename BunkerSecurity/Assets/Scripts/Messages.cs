using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Messages : MonoBehaviour
{
    public int currentMessages = 0;
    [SerializeField]
    GameObject messageGO;
    [SerializeField]
    TextMeshPro unreadMessagesTxt;
    [SerializeField]
    AudioSource messagePingSound;

    List<Message> messages = new List<Message>();
    Animator animator;

    DeskJobManager deskJobManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        deskJobManager = FindObjectOfType<DeskJobManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendNewMessage(string mt)
    {
        int mi = 0;
        if (currentMessages < 3)
        {
            mi = currentMessages + 1;
        }
        else
        {
            currentMessages--;
            messages.RemoveAt(currentMessages - 1);
        }
        Message nm = new Message(mi, mt);
        messages.Insert(0, nm);
        NewMessageRecieved();
    }

    void NewMessageRecieved()
    {
        currentMessages++;
        messagePingSound.Play();
        animator.SetTrigger("MessageRecieved");
        unreadMessagesTxt.text = currentMessages.ToString();
    }

    public class Message
    {
        int index;
        string messageText;
        public Message(int id, string message)
        {
            index = id;
            messageText = message;
        }
    }
}
