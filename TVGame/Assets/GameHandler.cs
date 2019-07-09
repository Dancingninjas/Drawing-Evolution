using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;
using System.Text;


public class GameHandler : MonoBehaviour {
    int ClientsConnected;
    public GameObject PreparingGame;
    public GameObject PhaseIntro;
    public Text PhaseIntroText1;
    public Text PhaseIntroText2;
    public Image PreparingGameBar;
    public GameObject Clouds;
    public Image IntroductionRedCircle;
    public GameObject Mouth;
    public Text PostGameOriginalWord;
    public Text PostGameOriginalWord0;
    public Text WordDrew1;
    public Text WordDrew2;
    public Text WordDrewWord1;
    public Text WordDrewWord2;
    public Text WordCompare1;
    public Text WordCompare2;
    public Text TitleText;
    public Image Arrow;
    public Image PostGameImage2;
    public Text WordGuessed1;
    public Text WordGuessed2;
    public Text WordGuessedWord1;
    public Text WordGuessedWord2;
    public GameObject PostGameHolder1;
    public GameObject PostGameHolder2;
    public Text testguesstext;
    public Image PostGameImage;
    public SpriteRenderer hostImage;
    public InputField inputNumber;
    public Canvas MenuCanvas;
    public Canvas DrawingCanvas;
    public Canvas GuessingCanvas;
    public Canvas IntroductionCanvas;
    public Text IntroductionText;
    public Canvas InitialPhraseCanvas;
    public Canvas PostGameCanvas;
    public Image IntroductionImage;
    public Text screenMode;
    public Text screenLength;
    public Text screenPhases;
    public Image screenIntroduction;
    public Text drawtimerText;
    public Text initialPhraseText;
    public Image initialPhraseImage;
    public Image drawtimerImage;
    public Text guesstimerText;
    public Text drawPhaseReadyText;
    public Text guessPhaseReadyText;
    public Text writePhaseReadyText;
    public Text playersConnectedText;
    public Image guesstimerImage;
    int masterControllerID;
    public AudioClip[] clips;
    public AudioSource audio1;
    public AudioSource audio2;
    string requiredPlayerNum = "2";
    int actualNumberofPhases = -1;
    int[] WhoThingisSentTo;
    int topPostGameHandler = 1;
    int[] ReviewOrder;
    string[][] jaggedArray;
    bool[] PlayerReadyState;
    bool isPostGameReady;
    int currentPhaseNumber = 0;
    bool isGameStarted = false;

    int[] customPlayerNumber;
    List<string> randomList;
    string[] randomDrawIntro = new string[5] { "Time to draw something amazing, or maybe awful.", "Time to let everyone down with your awful drawings!", "Don't know how to draw something? Good luck!", "Use a variety of colors to bring the prompt to life.", "Time to scribble something down and call it art."};

    string[] randomGuessIntro = new string[5] { "I like to call this phase 'WHAT IS THAT?'", "'Throwing Up Spaghetti?' 'Sandwich With Eyes?'", "Even if you know you're wrong, take a guess.", "Don't feel bad if you know your guess is wrong.", "If your guess is right, I'll give you a cookie." };

    string[] randomInitialPhraseIntro = new string[5] { "Write about a person, inside joke, or anything!", "You decide what others have to draw!", "'Joe Spilling Coffee on the Copier Again'", "Think of something you want someone else to draw.", "You can write anything you want, funny or serious." };

    string[] randomPostGameIntro = new string[5] { "Let's see how your drawings evolved!", "Get ready to be blown away by the amazing art.", "Time for everyone to judge you and your drawings.", "Let's have a look at the amazing art you made!", "Let's see how to bad your guesses were!"};
    //string[] randomList = new string[2] { "test test test test test test", "test2 test2 test2 test2 test2 test2 test2 test2" };
    string[] randomListArray = new string[302] {"Presidential Speech", "Thanksgiving Dinner", "Road Rage", "Dirty Socks", "Taco Tuesday", "First Time Riding a Bicycle", "Blind Man Seeing For the First Time", "Professional Bowler", "Last Minute Homework Rush", "Skype Interview", "Stargazing", "Midnight Snack", "Forgetting to Put Pants On", "Helping Someone Cross the Street", "Knock Knock Joke", "Playing a Board Game", "Hide and Go Seek", "High School Gossip", "New Person at School", "An Apple a Day Keeps the Doctor Away", "Crying Over Spilled Milk", "Milking a Goat", "Hay Ride", "Vacation", "Lumberjack", "Twins", "Dog Training", "Couch Potato", "Heroic Firefighter", "Hoola Hooping", "Dance Battle", "Take Your Child To Work Day", "Mowing the Lawn", "Summer", "Superhero", "Sad Song", "Playing Horseshoes", "Staying Home Sick", "Water Balloon Fight", "Doing Chores", "Grocery Shopping", "Sleepover", "Home Run", "Leprechauns Dancing", "Karaoke", "Missing the Bus", "Family Party", "News Anchor", "Spelling Bee", "Monster Under the Bed", "Homework Excuse", "Pickpocketing", "Education", "Cartoons", "Beating Around the Bush", "Tug of War", "Frisbee Golf", "'Put a Sock In It'", "Car Commercial", "Stubbing Your Toe", "Best Friend", "Nature Lover", "Cat Taking a Bath", "Taking the Cake", "Inventing Something", "Going for a Walk", "Making a Sandwich", "Losing Your Keys", "River of Tears", "Nightmare", "Taste Testing", "Blind Date", "All-You-Can-Eat Buffet", "1 Star Restaurant", "Taking Candy from a Baby", "Broken Umbrella", "Popular Kid", "Diving in the Pool", "Ice Skating", "Playing Tag", "Volleyball Spike", "Proposal Gone Wrong", "Class Clown", "Cool Guys Don't Look at Exposions", "Cleaning the Litter Box", "'Get Off My Lawn!'", "Pizza Delivery", "Class Presentation", "School Play", "Sledding Down a Hill", "Pool Games", "Asking Someone Out", "'The Cat is Out of the Bag'", "Skateboarding Trick", "Winning the Lottery", "Hitchhiking", "Anniversary Gift", "First Day on the Job", "Sucking Up to the Boss", "Teacher's Pet", "Turning into a Zombie"/*<---104*/, "Surprise Party", "Building an Igloo", "True Love", "Burning the Popcorn", "Garbage Man", "Art Auction", "Parade", "First Plane Ride", "Running Out of Breath", "Wild West Duel", "Heat Wave", "Bossy Chef", "Road Trip", "Dodgeball", "Walking into a Trap", /*<--119*/"Caught Red Handed", "Stuck in a Loop", "One Man's Trash is Another Man's Treasure", "Bank Robbery", "First Man on Mars", "Hailing a Taxi Cab", "Jumping Jacks", "Tourist in a Foreign Country", "Playing Golf", "School Bully", "Texting a Friend", "Science Fair Project", "Parents Embarrassing Their Kids", "Show and Tell", "Loud Vacuum", "Drive-In Theatre", "Disney Cruise", "Seagulls Stealing Food", "Waiting in Line for Water Ice", "Lazy River", "Water Park", "Applying Sunscreen", "Singing in the Shower", "Baby Bird Leaving the Nest", "Earwax", "Slipping on a Banana Peel", "Planting a Tree", "Customer Complaining", "Climbing a Mountain", "Letting Go of a Balloon Outside", "Family Photo", "Shaving a Beard", "Surfing", "Step on a Crack, Break Your Mother's Back", "Grinch Stealing Christmas", "Pig in a Blanket", "Cinderella Losing Her Shoe", "Dropping Your Phone", "Pushing Someone into the Pool", "Tightrope Walker", "Magician", "Dog Afraid of a Thunderstorm", "Three Little Pigs", "Sitting on a Whoopie Cushion", "Watching a Scary Movie", "Falling Asleep in Class", "Getting a Massage", "Getting Mad at Your Alarm Clock", "Using Your Mouth to Blow Up a Balloon", "Eating Loudly", "Tripping on Your Shoelaces", "Trying to Fit Your Hand in a Pringles Can", "Banana Phone", "Staring Contest", "Camping Outside a Store for a Sale", "A Store on Black Friday", "Resisting Arrest", "Shrink Ray", "Opening a Soda After Shaking It", "Tossing Dough in the Air", "Sinking Ship", "Poison Ivy Rash", "Playing Duck Duck Goose", "Blinking in the Photo", "Chinese Finger Trap", "Going Up Two Stairs at a Time", "Making a Mess With Your Food", "Using Your Neighbor's WiFi", "Goalie Save", "Seeing a Ghost", "Squish the Lemon", "Pillow Fight", "Signing Someone's Cast", "Climbing a Flagpole", "Defusing a Bomb", "Putting a Necklace on Someone", "Removing the Crust From a Sandwich", "Protesting", "Cat Chasing a Laser Pointer", "Bird Pooping on Someone", "Burping the Baby", "Jumping in Puddles", "Firing a Confetti Cannon", "A Fork in the Road", "Building a New PC", "Trying to do a Handstand", "Trolls Under the Bridge", "The Floor is Lava", "Meditation", "Talking With Your Mouth Full", "Hot Dog Eating Contest", "Playing Fetch With a Dog", "Puting Your Coat on the Rack", "Changing a Light Bulb", "Movie Theater Date", "Phone Ringing During Class", "Relay Race", "Walking Barefoot on Grass", "Texting During a Date", "Cheerleader Falling", "Parallel Parking", "School Bus Evacuation", "Obstacle Course", "Book Worm", "Hard Taco Shell Falling Apart as You Eat" /* */, "Tackling an Alligator", "Secret Handshake", "Robin Hood", "Getting Pied in the Face", "Socks and Sandals", "Daredevil Stunt", "Picking Flowers", "Stepping on a Lego", "Trimming a Bush", "Groundhog Seeing Its Shadow", "Window Washer", "Launching a Car Into Space", "Double Dipping", "Birthday Punches", "Sneezing Directly Into Someone's Eyes", "Shoe Shiner", "Painting a Picket Fence", "Giving Your Veggies to the Dog", "Crowd Surfing", "Closing the Door on Your Finger", "Monkey Stealing Something", "Voodoo Doll", "Bug Zapper", "Breakdancing", "Pin the Tail on the Donkey", "Biting Your Tongue", "Shivering in the Cold", "Plumber Fixing a Pipe", "Rocking Your Rocks Off", "Tapping Someone on the Shoulder Opposite to Where You're Standing", "Whale Watching", "Pillow Fort", "Hitting Your Neighbor's Window With a Baseball", "Spilling Coffee on Yourself", "Reading Someone's Mind", "Scratching Your Back", "Cat Scratching Furniture", "School Lunch", "Car Dealer", "Cards Up Your Sleeve", "Armpit Farting", "Prank Call", "Dancing in the Rain", "Stuck in Quicksand", "Fire Drill", "Rock Climbing", "Deer in Headlights", "Peeking at a Classmate's Test", "Closing the Door on a Salesperson", "Wig Falling Off", "Stairway to Heaven", "Earthquake", "Bull Riding", "Rounding Up Cattle", "Spitting in Someone's Face While Talking", "Drawing on a Sleeping Person's Face", "Accidentally Ripping Your Pants", "Child Pretending to Be an Adult", "Ignoring Someone", "Static Electricity", "Stone Skipping", "Cats Always Land on Their Feet", "Waving at Someone Who Wasn't Waving at You", "Volume Too High", "Freeze Tag", "Trying to Make a Child Smile in a Photo", "Puppet Show", "Waterbed", "Hard Hats Save Lives", "Fishing For Compliments", "Upstairs Neighbor Stomping", "Shaking a Vending Machine", "Knitting a Sweater", "Bedtime Story", "Teleportation Device", "Going Faster than the Speed of Light", "Walking in Thin Ice", "Sign Language", "Pushing Someone's Face into a Cake", "'I got your nose'", "Stacking Boxes"};
    
    //string[] randomListSummerArray = new string[122] {"Disney Cruise", "Seagulls Stealing Food", "Waiting in Line for Water Ice", "Lazy River", "Water Park", "Applying Sunscreen", ""}
    //shramrock shake, dunking oreos in milk


    public string currentMode = "Menu";
    public string currentScreenMode = "Menu";
    public bool continueCountdown = false;

    void Awake() {
        //AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += onConnection;
        AirConsole.instance.onDisconnect += OnDisconnection;
        AirConsole.instance.onAdComplete += OnAdCompleted;

        AirConsole.instance.onAdShow += OnAdShown;

        //AudioSource audio = GetComponent<AudioSource>();
        //audio.Play();

        audio1 = gameObject.AddComponent<AudioSource>(); //music
        audio1.volume = 0.15f;
        audio2 = gameObject.AddComponent<AudioSource>(); //voice / buttons
        audio2.volume = 4f;
        DrawingCanvas.enabled = false;
        GuessingCanvas.enabled = false;
        IntroductionCanvas.enabled = false;
        InitialPhraseCanvas.enabled = false;
        PostGameCanvas.enabled = false;
        screenIntroduction.enabled = true;
        PhaseIntro.SetActive(false);
        //PhaseIntro.GetComponent<>().enabled = false;
        WordCompare1.transform.parent.gameObject.SetActive(false);
        WordCompare2.transform.parent.gameObject.SetActive(false);
    }
    void Start()
    {
        Arrow.enabled = false;
        randomList = new List<string>();
        IntroductionRedCircle.enabled = false;
        foreach(string i in randomListArray)
        {
            //Debug.Log("ADDING " + i);
            randomList.Add(i);
        }

    }


    void updateReadyState()
    {
        int numberready = 0;
        foreach (bool d in PlayerReadyState)
        {
            if (d)
            {
                numberready++;
            }
        }
        if (currentScreenMode == "Draw")
        {
            drawPhaseReadyText.text = (numberready + "/"+PlayerReadyState.Length);
        }
        else if (currentScreenMode == "Guess")
        {
            guessPhaseReadyText.text = (numberready + "/" + PlayerReadyState.Length);
        }
        else if (currentScreenMode == "InitialPhrase")
        {
            writePhaseReadyText.text = (numberready + "/" + PlayerReadyState.Length);
        }
    }
    void onConnection(int device_id)
    {
        //Debug.Log("CONNECTION IS A THING!");
        //Debug.Log("COW POO" + AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id));
        
        if ((currentMode != "Menu"))
        {
            setView(currentMode);
            bool tempbool = true;
            foreach (int i in AirConsole.instance.GetActivePlayerDeviceIds)
            {
                if (i == device_id)
                {
                    tempbool = false;
                }
            }
            if (tempbool == false)
            {
                //setView(currentMode);
                //Debug.Log("Reconnecting Player With Number : " + AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id));
                if (customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)] - 1 == -1)
                {
                    AirConsole.instance.Message(AirConsole.instance.ConvertPlayerNumberToDeviceId(AirConsole.instance.GetActivePlayerDeviceIds.Count - 1), "Following:" + device_id);
                }
                else
                {
                    AirConsole.instance.Message(AirConsole.instance.ConvertPlayerNumberToDeviceId(customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)] - 1), "Following:" + device_id);
                }
                if (customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)] + 1 == AirConsole.instance.GetActivePlayerDeviceIds.Count)
                {
                    AirConsole.instance.Message(device_id, "Following:" + convertCustomPlayerNumberToDeviceId(0));
                    //AirConsole.instance.Message(device_id, "Following:" + convertCustomPlayerNumberToDeviceId(0));
                }
                else
                {
                    AirConsole.instance.Message(device_id, "Following:" + convertCustomPlayerNumberToDeviceId(customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)] + 1));
                }
                
                if (screenMode.text == "Random Prompt" && currentPhaseNumber == 1)
                {
                    
                    if (jaggedArray[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)]][currentPhaseNumber - 1].Contains("data:image/png;base64"))
                    {
                        AirConsole.instance.Message(device_id, jaggedArray[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)]][currentPhaseNumber - 1]);

                    }
                    else
                    {
                        AirConsole.instance.Message(device_id, "Guess:" + jaggedArray[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)]][currentPhaseNumber - 1]);
                    }

                }
                else
                {
                    int followerOf = customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id)] -1;
                    //AirConsole.instance.Message(device_id, "Following:" + AirConsole.instance.ConvertPlayerNumberToDeviceId(AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id) + 1));
                    if (followerOf == -1)
                    {
                        followerOf = AirConsole.instance.GetActivePlayerDeviceIds.Count - 1;
                        
                        //should this subtract 1?
                    }
                    //Debug.Log("RECONNECT: FOLLOWER OF PLAYER #" + followerOf);
                    //Debug.Log("CURRENT PHASE NUMBER: " + currentPhaseNumber);
                    if (currentMode != "InitialPhrase")
                    {
                        if (jaggedArray[followerOf][(currentPhaseNumber - 1)] != null)
                        {
                            if (jaggedArray[followerOf][(currentPhaseNumber - 1)].Contains("data:image/png;base64"))
                            {
                                //Debug.Log("RECONNECT1");
                                AirConsole.instance.Message(device_id, jaggedArray[followerOf][currentPhaseNumber - 1]);
                            }
                            else
                            {
                                //Debug.Log("RECONNECT2");
                                AirConsole.instance.Message(device_id, "Guess:" + jaggedArray[followerOf][currentPhaseNumber - 1]);
                            }
                        }

                        //Below is where it checks if the person sending to them already submitted something, in case the connecting player had been disconnected when the image/word was sent
                        if (jaggedArray[followerOf][currentPhaseNumber] != null)
                        {
                            if (jaggedArray[followerOf][currentPhaseNumber].Contains("data:image/png;base64"))
                            {
                                AirConsole.instance.Message(device_id, jaggedArray[followerOf][currentPhaseNumber]);
                            }
                            else
                            {
                                AirConsole.instance.Message(device_id, "Guess:" + jaggedArray[followerOf][currentPhaseNumber]);
                            }
                        }
                    }
                   
                }
                //retrieve past
                //jaggedArray[AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id) - 1][currentPhaseNumber - 1]
            }
        }
        else
        {
            AirConsole.instance.SetActivePlayers();
            
        }
        playersConnectedText.text = (AirConsole.instance.GetControllerDeviceIds().Count + "/" + requiredPlayerNum + "+");
        AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "gamehandler.updateplayercount");
        Debug.Log("If " + AirConsole.instance.GetMasterControllerDeviceId() + "= " + device_id);
        if (AirConsole.instance.GetMasterControllerDeviceId() == device_id)
        {
            masterControllerID = device_id;
            //Debug.Log("SETVIEWMENU");
            SendCurrentPreferencesToMaster();
            if (currentMode == "Menu")
            {
                setView("none");
                setView("Menu");
            }
        }
        



    }

    int convertCustomPlayerNumberToDeviceId(int playerNumb)
    {
        int savevar = -1;
        foreach(int inty in AirConsole.instance.GetActivePlayerDeviceIds)
        {
            if (customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(inty)] == playerNumb)
            {
                savevar = inty;
            }
        }
        return savevar;
    }

    /*IEnumerator resizeTitle()
    {
        bool increase = true;
        while (currentMode == "Menu")
        {
            yield return new WaitForSeconds(0.1f);
            if (increase)
            {
                TitleText.fontSize += 1;
                if (TitleText.fontSize > 165)
                {
                    increase = false;
                }
                //.fontSize += 0.25
                //if (.fontSize > 350)
                //increase = false
            }
            else
            {
                TitleText.fontSize -= 1;
                if (TitleText.fontSize < 160)
                {
                    increase = true;
                }
            }
        }
    }*/

    void forceSubmit()
    {
        //int saveState = currentPhaseNumber;
        audio2.Stop();
        if (currentMode == "InitialPhrase")
        {
            for (int i = 0; i < PlayerReadyState.Length; i++)
            {
                if (PlayerReadyState[i] == false)
                {
                    int targetplayer = i + 1;
                    if (targetplayer == AirConsole.instance.GetActivePlayerDeviceIds.Count)
                        
                    {
                        targetplayer = 0;
                    }
                    //Debug.Log("(i)" + i + " ===== " + targetplayer);
                    //set this to a random phrase
                    if (randomList.Count < 1)
                    {
                        foreach (string g in randomListArray)
                        {
                            randomList.Add(g);
                        }
                    }
                    int randomint = UnityEngine.Random.Range(0, randomList.Count);
                    string randomWord = randomList[randomint];
                    Debug.Log("RANDOM WORD: " + randomWord);
                    randomList.RemoveAt(randomint);
                    // Debug.Log("Player #" + targe)
                    AirConsole.instance.Message(convertCustomPlayerNumberToDeviceId(targetplayer), "Guess:" + randomWord);
                    Debug.Log("Sending 'Guess:" + randomWord + "'" + " to custom player #" + targetplayer + " (Device ID: " + convertCustomPlayerNumberToDeviceId(targetplayer) + ")");
                    jaggedArray[i][currentPhaseNumber] = randomWord;

                }
            }
            //get player number of all unreadys and message their target a 'guess:' + set jaggedarray
        }
        if (currentMode == "Draw")
        {
            foreach (int id in AirConsole.instance.GetActivePlayerDeviceIds)
            {
                if (PlayerReadyState[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(id)]] == false)
                {
                    jaggedArray[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(id)]][currentPhaseNumber] = "data:image/png;base64, BLANK";
                }
                PlayerReadyState[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(id)]] = false;
            }
            foreach (int id in AirConsole.instance.GetActivePlayerDeviceIds)
            {

                AirConsole.instance.Message(id, "gamehandler.sendfinal");
            }


            //BELOW: SETS THE GAME TO THE NEXT MODE
            if (actualNumberofPhases != currentPhaseNumber + 1)
            {
                //Debug.Log("Current State: " + AirConsole.instance.GetCustomDeviceState(0).ToString());
                if (currentMode == "Draw")
                {
                    //setView("Guess");
                    setView("Waiting");
                    setScreenView("Guess");
                    //Debug.Log("SET TO GUESS");
                    // StartCoroutine(fadeCanvas("Draw", "Guess", 5));
                }
                else
                {
                    //setView("Draw");
                    setView("Waiting");
                    setScreenView("Draw");
                    //Debug.Log("SET TO DRAW");
                    // StartCoroutine(fadeCanvas("Guess", "Draw", 5));
                }
            }
            else
            {
                setView("WatchTVAfter");
                setScreenView("PostGame");
                //Debug.Log("STARTING POST GAME REVIEW");
                //StartCoroutine(PostGameReview());
            }



        }
        else
        {
            //Debug.Log("Test111");
            for (int i = 0; i < PlayerReadyState.Length; i++)
            {
                if (currentMode == "Guess")
                {
                    if (PlayerReadyState[i] == false)
                    {
                        Debug.Log("PlayerReadyState[" + i + "] == false");
                        jaggedArray[i][currentPhaseNumber] = " ";
                        AirConsole.instance.Message(convertCustomPlayerNumberToDeviceId(WhoThingisSentTo[i]), "Guess:" + " ");
                        //PlayerReadyState[i] = true;
                    }
                }
                
                PlayerReadyState[i] = false;


            }
            if (actualNumberofPhases != currentPhaseNumber + 1)
            {
                assignNewSendToTarget();
                currentPhaseNumber++;
                //Debug.Log("Current State: " + AirConsole.instance.GetCustomDeviceState(0).ToString());
                //if (currentMode == "Draw")
                //{
                //    setView("Guess");
                //    setScreenView("Guess");
                    //StartCoroutine(fadeCanvas("Draw", "Guess", 5));
                //}
                    setView("Waiting");
                    setScreenView("Draw");
                    //StartCoroutine(fadeCanvas("Guess", "Draw", 5));
            }
            else
            {
                setView("WatchTVAfter");
                setScreenView("PostGame");
                //Debug.Log("STARTING POST GAME REVIEW");
                //StartCoroutine(PostGameReview());
            }
        }

    }
    void OnDisconnection(int device_id)
    {
        Debug.Log("CURRENT MODE: " + currentMode);
        //AirConsole.instance.SetActivePlayers();
        Debug.Log("TEST1");
        playersConnectedText.text = (AirConsole.instance.GetControllerDeviceIds().Count + "/" + requiredPlayerNum + "+");
        AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "gamehandler.updateplayercount");
        Debug.Log("MASTER CONTROLLER VAR: "+ masterControllerID);
        Debug.Log("THIS DEVICE'S PLAYER NUMBER: " + AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id));
        if (device_id == masterControllerID)
        {

            masterControllerID = AirConsole.instance.GetMasterControllerDeviceId();
            if (currentMode == "Menu")
            {
                setView("none");
                setView("Menu");
            }
            SendCurrentPreferencesToMaster();
            

        }
        else
        {
            Debug.Log("TEST2");
        }
        
    }
    //void getImageFromClient()
    //{
    //    int num = AirConsole.instance.ConvertPlayerNumberToDeviceId(0);
        //num.document.getElementById("myCanvas");
    //}
    
    void OnMessage(int deviceId, JToken imgURL)
    {

        //Debug.Log("INFO: " + deviceId);
        //Debug.Log("URL: " + imgURL.ToString());
        //Debug.Log("LENGTH: " + imgURL.ToString().Length);
        //StartCoroutine(convertThis(imgURL.ToString()));
        string finalURL = imgURL.ToString();
        //Debug.Log("FINALURL " + finalURL);
        if (finalURL.Contains("GameHandler.phases:") && currentMode == "Menu")
        {
            finalURL = finalURL.Replace("GameHandler.phases:", "");
            if (Int32.Parse(finalURL) == -1)
            {
                if (screenPhases.text != "Automatic")
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                screenPhases.text = "Automatic";
                actualNumberofPhases = -1;

            }
            else
            {
                
                actualNumberofPhases = Int32.Parse(finalURL);
                Debug.Log("SETTING TO " + actualNumberofPhases);
                if (screenPhases.text != (actualNumberofPhases - 1).ToString())
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                screenPhases.text = (actualNumberofPhases - 1).ToString();
            }
        }
        else if (finalURL.Contains("GameHandler.mode:") && currentMode == "Menu")
        {
            
            finalURL = finalURL.Replace("GameHandler.mode:", "");
            if (finalURL == "RandomPrompt")
            {
                if (screenMode.text != "Random Prompt")
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                screenMode.text = "Random Prompt";
            }
            else
            {
                if (screenMode.text != "Write Your Own Prompt")
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                screenMode.text = "Write Your Own Prompt";
            }
        }
        else if (finalURL.Contains("GameHandler.gamelength:") && currentMode == "Menu")
        {
            finalURL = finalURL.Replace("GameHandler.gamelength:", "");
            if (finalURL == "1")
            {
                if (screenLength.text != (finalURL + " Minute"))
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                screenLength.text = (finalURL + " Minute");
            }
            else
            {
                //Debug.Log("TEST555: " + screenLength.text + " != " + (finalURL + " Minutes"));
                if (screenLength.text != (finalURL + " Minutes"))
                {
                    //Debug.Log("NOT EQUAL");
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                screenLength.text = (finalURL + " Minutes");
            }
        }
        else if (finalURL == "GameHandler.checkmark" && currentMode == "Menu")
        {
            if (!screenIntroduction.enabled)
            {
                audio2.clip = clips[1];
                audio2.Play();
            }
            screenIntroduction.enabled = true;
        }
        else if (finalURL == "GameHandler.uncheckmark" && currentMode == "Menu")
        {
            if (screenIntroduction.enabled)
            {
                audio2.clip = clips[1];
                audio2.Play();
            }
            screenIntroduction.enabled = false;
        }
        else if (finalURL == "gameHandler.unready" && currentMode == "Draw")
        {
            //Debug.Log("UNREADYING");
            PlayerReadyState[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId)]] = false;
            updateReadyState();
        }
        else if (finalURL.Contains("GameHander.SaveSubmission:"))
        {
            //Debug.Log("----------------------------");
            finalURL = finalURL.Replace("GameHander.SaveSubmission:", "");
            if (((finalURL.Contains("data:image/png;base64,")) && currentMode == "Draw" || currentMode == "Waiting") || (!finalURL.Contains("data:image/png;base64,") && currentMode != "Draw"))
            {
                jaggedArray[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId)]][currentPhaseNumber] = finalURL;
            }
                //Debug.Log("Test33: " + customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId)]);
                
            //Debug.Log("Setting Player #" + AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId) + "'s " + currentPhaseNumber + " value to " + finalURL);
            //Debug.Log("Testthis: " + jaggedArray[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId)][currentPhaseNumber]);
            //Debug.Log("----------------------------");
        }
        else if (finalURL == "startGame" && currentMode == "Menu")
        {
            if (screenIntroduction.enabled)
            {
                setView("Introduction");
                //setScreenView("Introduction");
                StartCoroutine(fadeCanvas(currentScreenMode, "Introduction", 3));
                currentScreenMode = "Introduction";
                StartCoroutine(startIntroduction());
            }
            else
            {
                StartCoroutine(StartTheGame());
            }
            
        }
        
        else if (finalURL == "submitandready")
        {
            PlayerReadyState[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId)]] = true;
            updateReadyState();
            bool allready = true;
            foreach (bool d in PlayerReadyState)
            {
                if (!d)
                {
                    allready = false;
                }
            }
            if (allready)
            {
                continueCountdown = false;
                forceSubmit();
                

                

            }
        }
        else if (finalURL == "submitfinal")
            //after a message is sent to all devices saying all players are ready, the devices automatically send "submitfinal" + image
            //then it will change to the next round
        {
            /*PlayerReadyState[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId)]] = true;
            bool allready = true;
            foreach (int d in AirConsole.instance.GetActivePlayerDeviceIds)
            {
                if (!PlayerReadyState[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(d)]])
                {
                    allready = false;
                }
            }
            if (allready)
            {

                //Debug.Log("Everyone is ready");
                if (actualNumberofPhases != currentPhaseNumber + 1)
                {
                    assignNewSendToTarget();
                    currentPhaseNumber++;
                    //Debug.Log("Current State: " + AirConsole.instance.GetCustomDeviceState(0).ToString());
                    if (currentMode == "Draw")
                    {
                        //setView("Guess");
                        setView("Waiting");
                        setScreenView("Guess");
                        //Debug.Log("SET TO GUESS");
                       // StartCoroutine(fadeCanvas("Draw", "Guess", 5));
                    }
                    else
                    {
                        //setView("Draw");
                        setView("Waiting");
                        setScreenView("Draw");
                        //Debug.Log("SET TO DRAW");
                        // StartCoroutine(fadeCanvas("Guess", "Draw", 5));
                    }
                }
                else
                {
                    setView("WatchTVAfter");
                    setScreenView("PostGame");
                    //Debug.Log("STARTING POST GAME REVIEW");
                    //StartCoroutine(PostGameReview());
                }*/



            }





        
        else if (finalURL == "assignNewTargets")
        {
            assignNewSendToTarget();
        }
        //else if (finalURL.Contains("data:image/png;base64,") && currentMode == "Draw")
        //{
            
            //finalURL = finalURL.Replace("data:image/png;base64,", "");
            //byte[] Bytes = System.Convert.FromBase64String(finalURL);
           // Debug.Log("FINALURL: " + finalURL);
           // Debug.Log("FinalURLLength: " + finalURL.Length);
            //byte[] decodedBytes = Encoding.UTF8.GetBytes(imgURL.ToString()+"=");


            // byte[] encryptedBytes = Encoding.ASCII.GetBytes(
            //TextureImporter textureImporter = ;
            //textureImporter.alphaIsTransparency = false;
            //Texture2D tex = new Texture2D(5, 5);
            //tex.LoadImage(Bytes);
            //tex.filterMode = FilterMode.Point;
            //RemoveAlpha(tex);
            //Rect rect = new Rect(0, 0, tex.width, tex.height);
            //Debug.Log("CREATING SPRITE");
            //PostGameImage.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 20000f);
            
            //hostImage.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 20000f);
            //hostImage.sprite = hostImage.sprite.associatedAlphaSplitTexture;
            //hostImage.spriteSortPoint = SpriteSortPoint.Center;
        //}
        else if (finalURL.Contains("gamehandler.updateRequiredPlayers:"))
        {
            finalURL = finalURL.Replace("gamehandler.updateRequiredPlayers:", "");
            requiredPlayerNum = finalURL;
            playersConnectedText.text = (AirConsole.instance.GetControllerDeviceIds().Count + "/" + requiredPlayerNum + "+");
        }
        else
        {
            //Debug.Log("TESTTTT");
        }

    }





    void OnReady(string code)
    {
        //Initialize Game State
        JObject newGameState = new JObject();
        newGameState.Add("view", new JObject());
        newGameState.Add("playerColors", new JObject());

        AirConsole.instance.SetCustomDeviceState(newGameState);
        //Debug.Log(newGameState);

        //now that AirConsole is ready, the buttons can be enabled 
        for (int i = 0; i < 3; ++i)
        {
            //Debug.Log("test");
        }
        //AirConsole.instance.SetCustomDeviceStateProperty("view", "Menu");
        //AirConsole.instance.GetDeviceId();
        setView("Menu");
        //audio2 = gameObject.AddComponent<AudioSource>();
        audio1.volume = 0.15f;
        audio1.clip = clips[0];
        audio1.loop = true;
        audio1.Play();
        //PostGameOriginalWord.GetComponent<RectTransform>().offsetMax = new Vector2(1600, 0);
        //PostGameOriginalWord.GetComponent<RectTransform>().offsetMin = new Vector2(-1600, 0);
        /*Debug.Log("SCREEN WIDTH: " + Screen.width);
        Debug.Log("SCREEN HEIGHT: " + Screen.height);
        if (Screen.height / Screen.width > 0.78)
        {
            Debug.Log("YE");
            PostGameOriginalWord.GetComponent<RectTransform>().sizeDelta = new Vector2(2700, 500);
            WordDrew1.GetComponent<RectTransform>().sizeDelta = new Vector2(3600, 522);
            WordDrew2.GetComponent<RectTransform>().sizeDelta = new Vector2(3600, 522);
            WordGuessed1.GetComponent<RectTransform>().sizeDelta = new Vector2(3600, 522);
            WordGuessed2.GetComponent<RectTransform>().sizeDelta = new Vector2(3600, 522);
        }
        else
        {
            Debug.Log("NO");
            PostGameOriginalWord.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width / 0.4f), 500);
            WordDrew1.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width / 0.28f), 500);
            WordDrew2.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width / 0.28f), 500);
            WordGuessed1.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width / 0.28f), 500);
            WordGuessed2.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width / 0.28f), 500);
            //wordcompare1 2
        }*/
        
        
        //StartCoroutine(MoveClouds());
        //Application.targetFrameRate = 300;
        //QualitySettings.vSyncCount = 0;


        //AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "Game.ShowMain:");
    }
    IEnumerator MoveClouds()
    {
        List<RectTransform> cloudtrans = new List<RectTransform>();
        //foreach (GameObject g in Clouds.)
        foreach (RectTransform i in Clouds.GetComponentsInChildren(typeof(RectTransform)))
        {
            if (i.gameObject.tag != "DontMove")
            {
                cloudtrans.Add(i);
            }
        }

        while (true)
        {
            //Clouds.GetComponent<RectTransform>().position = new Vector3(Clouds.GetComponent<RectTransform>().position.x - 1, Clouds.GetComponent<RectTransform>().position.y, 0);
            foreach (RectTransform i in cloudtrans)
            { 
                    i.anchoredPosition = new Vector3(i.anchoredPosition.x - 1f, i.anchoredPosition.y, 0);
                    if (i.anchoredPosition.x < -800)
                    {
                        i.anchoredPosition = new Vector3(800, i.anchoredPosition.y, 0);
                    }
                
            }
            yield return new WaitForSeconds(0.02f);
        }
    }




    //public void sendMessageToDevice(int deviceNumber)
    //{
    //    AirConsole.instance.Message(deviceNumber, inputNumber.text);
    //}
    public void setScreenView(string viewName)
    {
        //Debug.Log("SETSCREENVIEW: " + viewName);
        //StopCoroutine(fadeCanvas("Draw", "Guess",3));
        //StopCoroutine(fadeCanvas("Guess", "PostGame", 3));
        //StopCoroutine(fadeCanvas("PostGame", "Menu", 3));
        //StopCoroutine(fadeCanvas("Menu", "Draw", 3));
        //StopCoroutine(fadeCanvas("Menu", "InitialPhrase", 3));
        //Debug.Log("SETSCREENVIEW: (fadeCanvas"+ "("+(currentScreenMode)+","+viewName+","+3+"))");

        StartCoroutine(fadeCanvas(currentScreenMode, viewName, 8));
        currentScreenMode = viewName;
    }
    public void setView(string viewName)
    {
        //Debug.Log("SETVIEW: " + viewName);
        //StartCoroutine(fadeCanvas(currentMode, viewName, 5));
        AirConsole.instance.SetCustomDeviceStateProperty("view", viewName);
        currentMode = viewName;
        /*if (currentMode == "Menu")
        {
            StartCoroutine(resizeTitle());
        }*/
    }
    //hostImage.sprite = img.sprite;
    /*public IEnumerator convertThis(string URL)
    {
        WWW www = new WWW(URL);
        yield return www;
        hostImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }*/
    public void SendCurrentPreferencesToMaster()
    {
        AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "GameHandler.UpdatePreferences.Mode:"+screenMode.text);
        AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "GameHandler.UpdatePreferences.Length:" + screenLength.text);
        if (screenPhases.text == "Automatic")
        {
            AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "GameHandler.UpdatePreferences.Phases:" + -1);
        }
        else
        {
            AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "GameHandler.UpdatePreferences.Phases:" + actualNumberofPhases);
        }
        if (screenIntroduction.enabled == false)
        {
            Debug.Log("DISIABLED CHECKMARK");
            AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "GameHandler.UpdatePreferences.Introduction: NONE");
        }
        else
        {
            AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "GameHandler.UpdatePreferences.Introduction: CHECKED");
        }
        
    }


    public IEnumerator StartTheGame()
    {
        if (isGameStarted == false)
        {
            isGameStarted = true;
            setView("Waiting");
            currentPhaseNumber = 0;
            if (screenMode.text == "Random Prompt")
            {
                currentPhaseNumber++;
                //setView("Draw");
                //setScreenView("Draw");
                //int[] deviceids = AirConsole.instance.GetActivePlayerDeviceIds;
                if (AirConsole.instance.GetActivePlayerDeviceIds.Count > 35)
                {
                    StartCoroutine(fadeCanvas(currentScreenMode, "Draw", ((AirConsole.instance.GetActivePlayerDeviceIds.Count * 0.2f) - 4)));
                }
                else
                {
                    StartCoroutine(fadeCanvas(currentScreenMode, "Draw", 8));
                }
                //StartCoroutine(fadeCanvas(currentScreenMode, "Draw", 3));
                currentScreenMode = "Draw";
                //StartCoroutine(fadeCanvas("Menu", "Draw", 5));
                //StartCoroutine(CountDown("Draw"));
                //fadeCanvas("Menu");
                //StartCoroutine(CountDown("Guess"));
                //it already sets the word above. this just changes the phase number after all players get a word.
            }
            if (screenMode.text == "Write Your Own Prompt")
            {
                //setView("InitialPhrase");
                //setScreenView("InitialPhrase");
                if (AirConsole.instance.GetActivePlayerDeviceIds.Count > 35)
                {
                    StartCoroutine(fadeCanvas(currentScreenMode, "InitialPhrase", ((AirConsole.instance.GetActivePlayerDeviceIds.Count * 0.2f) - 4)));
                }
                else
                {
                    StartCoroutine(fadeCanvas(currentScreenMode, "InitialPhrase", 8));
                }
                
                currentScreenMode = "InitialPhrase";
                //open the 'write-your-own-prompt' view for all players
                //when everyone is done, it will assign a new target and add 1 to currentphase
            }


            if (screenPhases.text == "Automatic")
            {
                if (screenMode.text == "Write Your Own Prompt")
                {
                    /*if (AirConsole.instance.GetControllerDeviceIds().Count >= 9)
                    {
                        actualNumberofPhases = 9;
                    }*/
                    if (AirConsole.instance.GetControllerDeviceIds().Count >= 7)
                    {
                        actualNumberofPhases = 7;
                    }
                    else if (AirConsole.instance.GetControllerDeviceIds().Count >= 5)
                    {
                        actualNumberofPhases = 5;
                    }
                    else if (AirConsole.instance.GetControllerDeviceIds().Count >= 3)
                    {
                        actualNumberofPhases = 3;
                    }
                }
                else
                {
                    /*if (AirConsole.instance.GetControllerDeviceIds().Count >= 10)
                    {
                        actualNumberofPhases = 11;
                    }
                    else if (AirConsole.instance.GetControllerDeviceIds().Count >= 8)
                    {
                        actualNumberofPhases = 9;
                    }*/
                    if (AirConsole.instance.GetControllerDeviceIds().Count >= 6)
                    {
                        actualNumberofPhases = 7;
                    }
                    else if (AirConsole.instance.GetControllerDeviceIds().Count >= 4)
                    {
                        actualNumberofPhases = 5;
                    }
                    else if (AirConsole.instance.GetControllerDeviceIds().Count >= 2)
                    {
                        actualNumberofPhases = 3;
                    }
                }
            }
            AirConsole.instance.SetActivePlayers();
            List<int> unassigned = new List<int>();//[AirConsole.instance.GetActivePlayerDeviceIds.Count];
            customPlayerNumber = new int[AirConsole.instance.GetActivePlayerDeviceIds.Count];
            for (int s = 0; s < AirConsole.instance.GetActivePlayerDeviceIds.Count; s++)
            {
                unassigned.Add(s);
            }
            //convertCustomPlayerNumberToDeviceId    (get the actual player num then convert)
            foreach (int y in AirConsole.instance.GetActivePlayerDeviceIds)
            {
                int temprand = UnityEngine.Random.Range(0, unassigned.Count);
                customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(y)] = unassigned[temprand];
                Debug.Log("Assigning Real #" + AirConsole.instance.ConvertDeviceIdToPlayerNumber(y) + " : Custom #" + customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(y)]);
                unassigned.RemoveAt(temprand);
                //set customplayernumber to a random element of unassigned
                // remove that random element from unassigned
            }
            //customPlayerNumber
            //List<int> PlayerNumber = new List<int>();
            WhoThingisSentTo = new int[AirConsole.instance.GetActivePlayerDeviceIds.Count];
            jaggedArray = new string[AirConsole.instance.GetActivePlayerDeviceIds.Count][];
            for (int i = 0; i < AirConsole.instance.GetActivePlayerDeviceIds.Count; i++)
            //jaggedArray[] is the player number so you set it to size of all players
            //jaggedArray[][] is the individual phases, ex: jaggedArray[0][2] is player 0 phase 2
            //ex2: jaggedArray[4][3] is player 4 phase 3 (saves as player 4's phase 3 item, so if player 4 set their phase 3 to 'cow' it saves as 'cow')
            {
                //Debug.Log("I = " + i);
                jaggedArray[i] = new string[actualNumberofPhases];//change this to totalphasenumber
                                                                  //Debug.Log("jaggedArrayLength = " + jaggedArray.Length);
                                                                  //Debug.Log("jaggedArrayLength[i] = " + jaggedArray[i].Length);
            }

            //jaggedArray[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId)] = new int[currentPhaseNumber]
            PlayerReadyState = new bool[AirConsole.instance.GetActivePlayerDeviceIds.Count];
            //2 players (0 and 1)
            //Debug.Log("Amount of players: " + WhoThingisSentTo.Length);
            foreach (int i in AirConsole.instance.GetActivePlayerDeviceIds)
            {
                //WhoThingisSentTo.Add(i, )
                int playerNumber = customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(i)];
                Debug.Log("PLAYERNUMBERVAR: " + playerNumber);

                if (screenMode.text == "Random Prompt")
                {
                    if (randomList.Count == 0)
                    {
                        foreach (string g in randomListArray)
                        {
                            randomList.Add(g);
                        }
                    }
                    //UnityEngine.Random ran = new UnityEngine.Random();
                    int randomint = UnityEngine.Random.Range(0, randomList.Count);
                    string therandom = randomList[randomint];
                    randomList.RemoveAt(randomint);
                    jaggedArray[playerNumber][0] = therandom;
                    AirConsole.instance.Message(i, "Guess:" + therandom);
                    //randomly assign each player a word and then add 1 to currentphase
                }
                //Debug.Log("IF: " + (playerNumber + 1) + " > " + AirConsole.instance.GetActivePlayerDeviceIds.Count + "....");
                if ((playerNumber + 1) == AirConsole.instance.GetActivePlayerDeviceIds.Count)
                //player numbers start from 0, so it checks if the next player number up is the amount of numbers (ex: 4 players means player 3 is the highest number)
                {
                    WhoThingisSentTo[playerNumber] = 0;
                    AirConsole.instance.Message(i, "Following:" + convertCustomPlayerNumberToDeviceId(0)/*AirConsole.instance.ConvertPlayerNumberToDeviceId(0)*/);
                }
                else
                {
                    //custom 0 sends to custom 2, but it should send to custom 1

                    WhoThingisSentTo[playerNumber] = playerNumber + 1;
                    Debug.Log("WHOTHINGISSENTO[PLAYERNUMBER]: " + WhoThingisSentTo[playerNumber]);
                    Debug.Log("i = " + i + ", custom player #" + customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(i)]);
                    Debug.Log("AirConsole.instance.Message(" + i + ", 'Following: '" + convertCustomPlayerNumberToDeviceId(WhoThingisSentTo[playerNumber]) + ")");
                    AirConsole.instance.Message(i, "Following:" + convertCustomPlayerNumberToDeviceId(WhoThingisSentTo[playerNumber]) /*AirConsole.instance.ConvertPlayerNumberToDeviceId((WhoThingisSentTo[playerNumber]))*/);
                }
                PlayerReadyState[customPlayerNumber[AirConsole.instance.ConvertDeviceIdToPlayerNumber(i)]] = false;
                //yield return new WaitForSeconds(0.2f);
                //Debug.Log("Player #" + playerNumber + " is sending to " + WhoThingisSentTo[playerNumber]);
                //Debug.Log("Device: " + i + " = Player #"+ AirConsole.instance.ConvertDeviceIdToPlayerNumber(i));
                yield return new WaitForSeconds(0.2f);
            }
            updateReadyState();
            
            //List<int> WhoDrawingisSentTo = new List<int>();
            //each element will represent the original player and the value will represent the player number of who is receiving the image or text


            //Debug.Log(AirConsole.instance.GetActivePlayerDeviceIds);
            isGameStarted = false;
        }
    }
        //remove this when you actually specify the number of phases

    public void assignNewSendToTarget()
    {
        for (int i = 0; i < WhoThingisSentTo.Length; i++)
        {
            /*if (WhoThingisSentTo[i] + 1 == WhoThingisSentTo.Length)
            {
                WhoThingisSentTo[i] = 0;
                //AirConsole.instance.Message(AirConsole.instance.ConvertPlayerNumberToDeviceId(i), "Following:" + AirConsole.instance.ConvertPlayerNumberToDeviceId(0));
            }
            else
            {
                WhoThingisSentTo[i] = WhoThingisSentTo[i] + 1;
                //AirConsole.instance.Message(AirConsole.instance.ConvertPlayerNumberToDeviceId(i), "Following:" + AirConsole.instance.ConvertPlayerNumberToDeviceId((WhoThingisSentTo[i])));

            }
            */
            PlayerReadyState[i] = false;
            //Debug.Log("Player #" + i + " is sending to " + WhoThingisSentTo[i]);
        }
    }

    
    public IEnumerator movePostGameUp()
    {
        if (topPostGameHandler == 1)
        {
            Debug.Log("ONE: " + PostGameHolder1.transform.localPosition.y);
            while (PostGameHolder1.transform.localPosition.y < 0)
            {
                PostGameOriginalWord.transform.parent.localPosition = (new Vector3(PostGameOriginalWord.transform.parent.localPosition.x, PostGameOriginalWord.transform.parent.localPosition.y + 2.5f, 0));
                PostGameHolder1.transform.localPosition = ( new Vector3(PostGameHolder1.transform.localPosition.x, PostGameHolder1.transform.localPosition.y + 2.5f, 0));
                PostGameHolder2.transform.localPosition = ( new Vector3(PostGameHolder2.transform.localPosition.x, PostGameHolder2.transform.localPosition.y + 2.5f, 0));
                yield return new WaitForSeconds(0.01f);
            }
            Debug.Log("ONE DONE: " + PostGameHolder1.transform.localPosition.y);
            yield return new WaitForSeconds(4);

            Component[] fadeComponents;
            Component[] fadeText;
            //ParentObject2.enabled = true;
            //ParentObject.enabled = false;
            fadeComponents = WordGuessed1.transform.parent.gameObject.GetComponentsInChildren(typeof(Image), true);
            fadeText = WordGuessed1.transform.parent.gameObject.GetComponentsInChildren(typeof(Text), true);

            float fadeNum = 0;
            while (fadeNum < 1)
            {
                fadeNum += 0.015f;
                foreach (Image i in fadeComponents)
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
                foreach (Text i in fadeText)
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
                yield return new WaitForSeconds(0.01f);
            }
                yield return new WaitForSeconds(4);
            topPostGameHandler = 2;
            isPostGameReady = true;
            //if (PostGameHolder1.transform.position.y == 350)
            //Debug.Log("DONE, Y = " + PostGameHolder1.transform.position.y);

            while (PostGameHolder1.transform.localPosition.y < 700)
            {
                yield return new WaitForSeconds(0.01f);
            }
            Debug.Log("ONE SUPER DONE: " + PostGameHolder1.transform.localPosition.y);
            PostGameHolder1.transform.localPosition = new Vector3(PostGameHolder1.transform.localPosition.x, -950, PostGameHolder1.transform.localPosition.z);
            //topPostGameHandler = 2;

        }
        
        else
        {
            Debug.Log("TWO: " + PostGameHolder2.transform.localPosition.y);
            while (PostGameHolder2.transform.localPosition.y < 0)
            {
                PostGameOriginalWord.transform.parent.localPosition = (new Vector3(PostGameOriginalWord.transform.parent.localPosition.x, PostGameOriginalWord.transform.parent.localPosition.y + 2.5f, 0));
                PostGameHolder1.transform.localPosition = (new Vector3(PostGameHolder1.transform.localPosition.x, PostGameHolder1.transform.localPosition.y + 2.5f, 0));
                PostGameHolder2.transform.localPosition = (new Vector3(PostGameHolder2.transform.localPosition.x, PostGameHolder2.transform.localPosition.y + 2.5f, 0));
                yield return new WaitForSeconds(0.01f);
            }
            Debug.Log("TWO DONE: " + PostGameHolder2.transform.localPosition.y);
            //Debug.Log("DONE, Y = " + PostGameHolder2.transform.position.y);
            yield return new WaitForSeconds(4);

            Component[] fadeComponents;
            Component[] fadeText;
            //ParentObject2.enabled = true;
            //ParentObject.enabled = false;
            fadeComponents = WordGuessed2.transform.parent.gameObject.GetComponentsInChildren(typeof(Image), true);
            fadeText = WordGuessed2.transform.parent.gameObject.GetComponentsInChildren(typeof(Text), true);

            float fadeNum = 0;
            while (fadeNum < 1)
            {
                fadeNum += 0.015f;
                foreach (Image i in fadeComponents)
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
                foreach (Text i in fadeText)
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(4);
            topPostGameHandler = 1;
            isPostGameReady = true;
            while (PostGameHolder2.transform.localPosition.y < 700)
            {
                yield return new WaitForSeconds(0.01f);
            }
            
            Debug.Log("TWO SUPER DONE: " + PostGameHolder2.transform.localPosition.y);
            PostGameHolder2.transform.localPosition = new Vector3(PostGameHolder2.transform.localPosition.x, -950, PostGameHolder2.transform.localPosition.z);
            
        }
    }
    public IEnumerator PostGameReview()
    {
        
        for(int playerNum = 0; playerNum < AirConsole.instance.GetActivePlayerDeviceIds.Count; playerNum++)
        {
            // yield return new WaitForSeconds(10);
            int currentPlayer = playerNum;
            if (screenMode.text == "Random Prompt")
            {
                PostGameOriginalWord0.text = "Original Phrase:";
                PostGameOriginalWord.text = ("<b>"+jaggedArray[playerNum][0] + "</b>");
            }
            else
            {
                PostGameOriginalWord0.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(playerNum)) + " Wrote:");
                PostGameOriginalWord.text = "<b>"+jaggedArray[playerNum][0]+ "</b>";
                //Debug.Log("Player " + playerNum + " wrote " + jaggedArray[playerNum][0]);
                currentPlayer++;
                if (currentPlayer == AirConsole.instance.GetActivePlayerDeviceIds.Count)
                {
                    currentPlayer = 0;
                }
            }
            int countNum = 1;
            PostGameOriginalWord.transform.parent.localPosition =  (new Vector3(PostGameOriginalWord.transform.parent.localPosition.x, -500, 0));
            PostGameHolder1.transform.localPosition = ( new Vector3(PostGameHolder1.transform.localPosition.x, -1200, 0));
            PostGameHolder2.transform.localPosition = (new Vector3(PostGameHolder2.transform.localPosition.x, -2000, 0));
            topPostGameHandler = 1;
            //isPostGameReady = false;
            //StartCoroutine(movePostGameUp());

            //while (isPostGameReady == false)
            //{
            //    yield return new WaitForSeconds(1);
            //}
            for (int phasenumber = 1; phasenumber < actualNumberofPhases; phasenumber++)
                //bug: in write-your-own mode it should only go up to
            {
                //yield return new WaitForSeconds(5);
                if (phasenumber == 1||phasenumber == 2)
                    //assigns the first image info
                {
                    //Debug.Log("IS ONE");
                    dotestreview(currentPlayer, phasenumber, 1);


                    //StartCoroutine(movePostGameUp());
                }
                else if (topPostGameHandler == 1)
                {
                    //postgameNextPlayer = currentPlayer;
                    //Debug.Log("TOPPOST IS ONE");
                    //postgameNextPlayer = phasenumber;

                    //postgametop = 2;
                    dotestreview(currentPlayer, phasenumber, 1);
                }
                else
                {
                    //Debug.Log("ELLLSE: "+ phasenumber);
                    //postgameNextPlayer = currentPlayer;

                    //postgameNextPlayer = phasenumber;

                    //postgametop = 1;
                    dotestreview(currentPlayer, phasenumber, 2);
                }
                if (!(jaggedArray[currentPlayer][phasenumber].Contains("data:image/png;base64"))){
                    countNum++;
                    //checks if its a guess
                    isPostGameReady = false;
                    StartCoroutine(movePostGameUp());
                    //while (atcenter == false)
                    //{
                    //    yield return new WaitForSeconds(1);
                    //}
                    while (isPostGameReady == false)
                    {
                        yield return new WaitForSeconds(1);
                    }
                    
                }


                //yield return new WaitForSeconds(5);
                //yield return new WaitForSeconds(10);



                currentPlayer++;
                if (currentPlayer == AirConsole.instance.GetActivePlayerDeviceIds.Count)
                {
                    currentPlayer = 0;
                }

            }


            PostGameOriginalWord.transform.parent.localPosition = (new Vector3(PostGameOriginalWord.transform.parent.localPosition.x, -500, 0));
            PostGameHolder1.transform.localPosition = (new Vector3(PostGameHolder1.transform.localPosition.x, -1200, 0));
            PostGameHolder2.transform.localPosition = (new Vector3(PostGameHolder2.transform.localPosition.x, -2000, 0));

            yield return new WaitForSeconds(1);
            WordCompare1.text = jaggedArray[playerNum][0];
            int determinefinalplayer = currentPlayer - 1;
            if (determinefinalplayer == -1)
            {
                determinefinalplayer = AirConsole.instance.GetActivePlayerDeviceIds.Count - 1; //?
            }
            WordCompare2.text = jaggedArray[determinefinalplayer][actualNumberofPhases - 1];
            WordCompare1.transform.parent.gameObject.SetActive(true);
            WordCompare1.transform.parent.gameObject.GetComponent<Animator>().SetBool("WordCompareStay", true);
            yield return new WaitForSeconds(0.1f);
            audio2.clip = clips[10];
            audio2.Play();
            yield return new WaitForSeconds(0.25f);
            Arrow.enabled = true;
            yield return new WaitForSeconds(0.5f);
            WordCompare2.transform.parent.gameObject.SetActive(true);
            WordCompare2.transform.parent.gameObject.GetComponent<Animator>().SetBool("WordCompareStay", true);
            yield return new WaitForSeconds(0.1f);
            audio2.clip = clips[10];
            audio2.Play();
            yield return new WaitForSeconds(6);
            Arrow.enabled = false;
            WordCompare1.transform.parent.gameObject.GetComponent<Animator>().SetBool("WordCompareStay", false);
            WordCompare2.transform.parent.gameObject.GetComponent<Animator>().SetBool("WordCompareStay", false);


            yield return new WaitForSeconds(0.35f);
            WordCompare1.text = "";
            WordCompare2.text = "";
            WordCompare1.transform.parent.gameObject.SetActive(false);
            WordCompare2.transform.parent.gameObject.SetActive(false);
            //show comparetexts


            //show original word again, and show final word


            //testguesstext.text = 
        }
        yield return new WaitForSeconds(3);
        AirConsole.instance.ShowAd();
    }

    void OnAdShown()
    {
        audio1.Pause();
    }

    void OnAdCompleted(bool adWasShown)
    {
        setView("Waiting");
        StartCoroutine(fadeCanvas(currentScreenMode, "Menu", 2));
        currentScreenMode = "Menu";
        PostGameOriginalWord.transform.parent.localPosition = (new Vector3(PostGameOriginalWord.transform.parent.localPosition.x, -500, 0));
        PostGameHolder1.transform.localPosition = (new Vector3(PostGameHolder1.transform.localPosition.x, -1200, 0));
        PostGameHolder2.transform.localPosition = (new Vector3(PostGameHolder2.transform.localPosition.x, -2000, 0));
        audio1.UnPause();
    }

    //this is completely for testing. remove after.
    public void dotestreview(int num, int num2, int postGameHandler)
    {
        // if (postGameHandler)
        //Text targetText
        Debug.Log("NUM1: " + num + " ----- NUM2: " + num2);
        if (jaggedArray[num][num2] == "data:image/png;base64, BLANK")
        {
            if (postGameHandler == 1)
            {
                PostGameImage.sprite = null;
            }
            else
            {
                PostGameImage2.sprite = null;
            }
        }
        else if (jaggedArray[num][num2].Contains("data:image/png;base64"))
        {
            
            string newString = jaggedArray[num][num2].Replace("data:image/png;base64,", "");
            //jaggedArray[num][num2] = jaggedArray[num][num2].Replace("data:image/png;base64,", "");
            byte[] Bytes = System.Convert.FromBase64String(newString);

            Texture2D tex = new Texture2D(5, 5);
            tex.LoadImage(Bytes);
            tex.filterMode = FilterMode.Point;
            //RemoveAlpha(tex);
            Rect rect = new Rect(0, 0, tex.width, tex.height);
            //hostImage.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 20000f);
            //hostImage.sprite = hostImage.sprite.associatedAlphaSplitTexture;
            //hostImage.spriteSortPoint = SpriteSortPoint.Center;
            
            //testguesstext.text = ("Player " + num + " Drew: ");
            if (postGameHandler == 1)
            {
                PostGameImage.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 20000f);
                //Debug.Log("DRAW 1");
                WordDrew1.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    ");
                if (screenMode.text == "Random Prompt" && num2 == 1)
                {
                    WordDrew1.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    " + "<b>"+jaggedArray[num][0]+ "</b>");
                    WordDrewWord1.text = jaggedArray[num][0];
                    //Debug.Log("TESTERINO!!!!!!");
                }
                else
                {
                    //Debug.Log("ELSERINO!!!!!!!!");
                    if (num == 0)
                    {
                        WordDrew1.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    " + "<b>"+jaggedArray[AirConsole.instance.GetActivePlayerDeviceIds.Count - 1][num2 - 1])+ "</b>";
                        WordDrewWord1.text = jaggedArray[AirConsole.instance.GetActivePlayerDeviceIds.Count - 1][num2 - 1];
                    }
                    else
                    {
                        WordDrew1.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    " + "<b>"+jaggedArray[num - 1][num2 - 1] + "</b>");
                        WordDrewWord1.text = jaggedArray[num - 1][num2 - 1];
                    }


                }

                
            }
            else
            {
                PostGameImage2.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 20000f);
                //Debug.Log("DRAW 2");
                //WordDrew2.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew: ");
                if (screenMode.text == "Random Prompt" && num2 == 1)
                {
                    WordDrew2.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    " + "<b>"+jaggedArray[num][0]+ "</b>");
                    WordDrewWord2.text = jaggedArray[num][0];
                }
                else
                {

                    if (num == 0)
                    {
                        WordDrew2.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    " + "<b>"+jaggedArray[AirConsole.instance.GetActivePlayerDeviceIds.Count - 1][num2 - 1]+ "</b>");
                        WordDrewWord2.text = jaggedArray[AirConsole.instance.GetActivePlayerDeviceIds.Count - 1][num2 - 1];
                    }
                    else
                    {
                        WordDrew2.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    " + "<b>"+jaggedArray[num - 1][num2 - 1]+ "</b>");
                        WordDrewWord2.text = jaggedArray[num - 1][num2 - 1];
                    }


                }
            }
            //Debug.Log("Player " + num + " Drew: ");
        }
        else
        {
           if (postGameHandler == 1)
            {
                //Debug.Log("GUESS 1");
                WordGuessed1.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Guessed:    " + "<b>"+jaggedArray[num][num2] + "</b>");
                WordGuessedWord1.text = jaggedArray[num][num2];
                Component[] fadeComponents = WordGuessed1.transform.parent.gameObject.GetComponentsInChildren(typeof(Image), true);
                Component[] fadeText = WordGuessed1.transform.parent.gameObject.GetComponentsInChildren(typeof(Text), true);
                foreach (Image i in fadeComponents)
                {
                    Color newColor = i.color;
                    newColor.a = 0;
                    i.color = newColor;
                }
                foreach (Text i in fadeText)
                {
                    Color newColor = i.color;
                    newColor.a = 0;
                    i.color = newColor;
                }
                //WordDrewWord2.text = jaggedArray[num][num2];
            }
           else
            {
                //Debug.Log("GUESS 2");
                WordGuessed2.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Guessed:    " + "<b>"+jaggedArray[num][num2] + " </b>");
                WordGuessedWord2.text = jaggedArray[num][num2];
                Component[] fadeComponents = WordGuessed2.transform.parent.gameObject.GetComponentsInChildren(typeof(Image), true);
                Component[] fadeText = WordGuessed2.transform.parent.gameObject.GetComponentsInChildren(typeof(Text), true);
                foreach (Image i in fadeComponents)
                {
                    Color newColor = i.color;
                    newColor.a = 0;
                    i.color = newColor;
                }
                foreach (Text i in fadeText)
                {
                    Color newColor = i.color;
                    newColor.a = 0;
                    i.color = newColor;
                }
                //WordDrewWord1.text = jaggedArray[num][num2];
            }
           //testguesstext.text = ("Player " + num + " Guessed: " + jaggedArray[num][num2]);
            Debug.Log("Custom # " + num + " Guessed: " + "<b>"+jaggedArray[num][num2]+ "</b>");


                //you dont have a way to figure out the player who drew or wrote it's number
        }
    }
    IEnumerator startIntroduction()
    {
        //audio.Stop();
        //setView("WatchTVAfter");
        //setScreenView("Introduction");
        //IntroductionCanvas.enabled = true;
        //MenuCanvas.enabled = false;
        IntroductionText.fontSize = 120;
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/1"); //Blank canvas image
        IntroductionText.text = "";
        yield return new WaitForSeconds(2);
        while (audio1.volume > 0.05f)
        {
            audio1.volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        //audio1.volume = 0.01f;
        yield return new WaitForSeconds(2);
        audio2.clip = clips[2];
        audio2.loop = false;
        audio2.Play();
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = "Welcome to Drawing Evolution.";
        yield return new WaitForSeconds(5);
        audio2.clip = clips[3];
        audio2.Play();
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = "The rules of this game are simple.";
        IntroductionImage.enabled = true;
        yield return new WaitForSeconds(4);
        audio2.clip = clips[4];
        audio2.Play();
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = "You are given a word or phrase to draw.";
        yield return new WaitForSeconds(1);
        IntroductionRedCircle.enabled = true;
        yield return new WaitForSeconds(4);
        IntroductionRedCircle.enabled = false;
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/2"); //Swinging at pinata image
        IntroductionImage.enabled = true;
        //AUDIO: You are given a word or phrase to draw.
        yield return new WaitForSeconds(6);
        audio2.clip = clips[5];
        audio2.Play();
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = "A player will have to guess what word or phrase you drew.";
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/3"); //guessing "angry pirate" image
        //AUDIO: A player will have to guess what word or phrase you drew.
        yield return new WaitForSeconds(8);
        audio2.clip = clips[6];
        audio2.Play();
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = "Then, another player will have to draw that guess.";
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/4"); //Angry pirate image
        yield return new WaitForSeconds(4);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/5"); //Angry pirate image
        //AUDIO: Then, another player will have to draw the word or phrase that was guessed.
        yield return new WaitForSeconds(4);
        audio2.clip = clips[7];
        audio2.Play();
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = "The more players you have, the more rounds you can play.";

        //AUDIO: The more players you have, the more rounds you can play, and the more your drawings can evolve.
        yield return new WaitForSeconds(4);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/6");
        yield return new WaitForSeconds(3);
        //IntroductionText.fontSize = 120;
        audio2.clip = clips[8];
        audio2.Play();
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = "At the end, we'll look at how the phrases evolved.";
        yield return new WaitForSeconds(3);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/IntroductionFinal1");
        yield return new WaitForSeconds(2);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/IntroductionFinal2");
        yield return new WaitForSeconds(2);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/IntroductionFinal3");
        yield return new WaitForSeconds(2);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/IntroductionFinal4");
        //AUDIO: At the end, we'll go through and look at how phrases like 'Swinging at a pinata' turned into 'Halloween Costume'
        yield return new WaitForSeconds(4);
        //set view to either draw or 
        screenIntroduction.enabled = false;
        StartCoroutine(StartTheGame());
        yield return new WaitForSeconds(2);
        while (audio1.volume < 0.15f)
        {
            audio1.volume += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator RunMouthForSeconds(float sec)
    {
        Mouth.GetComponent<Animator>().SetBool("IsTalking", true);
        yield return new WaitForSeconds(sec);
        Mouth.GetComponent<Animator>().SetBool("IsTalking", false);
    }

    IEnumerator CountDown(string type)
    {
        int seconds = 60;
        continueCountdown = true;
        Image timerImage;
        Text timerText;
        if (type == "Draw")
        {
            
            timerText = drawtimerText;
            timerImage = drawtimerImage;
            if (screenLength.text == "1 Minute")
            {
                seconds = 60;
            }
            else if (screenLength.text == "2 Minutes")
            {
                seconds = 120;
            }
            else if (screenLength.text == "3 Minutes")
            {
                seconds = 180;
            }
            else if (screenLength.text == "5 Minutes")
            {
                seconds = 300;
            }
        }
        else
        {
            if (type == "InitialPhrase")
            {
                timerText = initialPhraseText;
                timerImage = initialPhraseImage;
                //timerImage 
            }
            else
            {
                timerText = guesstimerText;
                timerImage = guesstimerImage;
            }
            //GuessingCanvas.enabled = true;
            seconds = 90;
        }
        float numbertoRemovePerSecond = (1.0f / (seconds));
        //Debug.Log("NUMBER TO REMOVE PER SECOND" + numbertoRemovePerSecond);
        timerImage.fillAmount = 1;
        while (seconds > 0 && continueCountdown == true)
        {
            seconds--;
            //timerImage.fillAmount -= numbertoRemovePerSecond;
            for (int i = 0; i < 20; i++)
            {
                timerImage.fillAmount -= numbertoRemovePerSecond / 20;
                yield return new WaitForSeconds(0.05f);
            }
            if (seconds == 15)
            {
                audio2.clip = clips[9];
                audio2.loop = false;
                audio2.Play();
            }
            //yield return new WaitForSeconds(1);
            if (seconds > 60)
            {
                timerText.text = (Mathf.Floor(seconds / 60.0f)).ToString() + "m" + (seconds % 60) + "s";
            }
            else
            {
                timerText.text = seconds + "s";
            }

        }
        if (seconds == 0 && continueCountdown == true)
        {
            Debug.Log("FORCE SUBMIT");
            forceSubmit();
        }
        /*if (type == "Draw")
        {
            StartCoroutine(fadeCanvas(type, "Guess", 5));
        }
        else if (type == "Guess")
        {
            StartCoroutine(fadeCanvas(type,"Draw", 5));
        }*/
    }

    IEnumerator fadeCanvas(string fadedout, string fadein, float waitBetween)
    {
        //setView("Waiting");
        Canvas ParentObject = GuessingCanvas;
        Canvas ParentObject2 = DrawingCanvas;
        continueCountdown = false;
        //set this or it wont let me because it thinks it is unassigned
        if (fadedout == "Guess")
        {
            ParentObject = GuessingCanvas;
        }
        else if (fadedout == "Menu")
        {
            ParentObject = MenuCanvas;
        }
        else if (fadedout == "Draw")
        {
            ParentObject = DrawingCanvas;
        }
        else if (fadedout == "Introduction")
        {
            ParentObject = IntroductionCanvas;
        }
        else if (fadedout == "InitialPhrase")
        {
            ParentObject = InitialPhraseCanvas;
        }
        else if (fadedout == "PostGame")
        {
            ParentObject = PostGameCanvas;
        }
        //Debug.Log("PARENTOBJECCCCCT : " + ParentObject.name);
        ParentObject.enabled = true;
        Component[] fadeComponents;
        Component[] fadeText;
        fadeComponents = ParentObject.GetComponentsInChildren(typeof(Image));
        fadeText = ParentObject.GetComponentsInChildren(typeof(Text));
        //string phaseInstance = currentScreenMode;
        float fadeNum = 1;
        while (fadeNum > 0)
        {

            fadeNum -= 0.015f;

            foreach (Image i in fadeComponents)
            {
                if (i.tag != "Panel")
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
                
            }
            foreach (Text i in fadeText)
            {
                if (i.tag != "Panel")
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
            }
                yield return new WaitForSeconds(0.01f);
        }
        //if (currentScreenMode != phaseInstance)
        //{
        //    Debug.Log("BREAKKKKKKKK!");
        //    yield break;
        //}
        Debug.Log("WaitBetween = " + waitBetween);
        if (waitBetween > 10)
        {
            //fade into the wait warning and wait bar
            Component[] fadeComponents3;
            Component[] fadeText3;
            //PreparingGame.enabled = true;
            //ParentObject.enabled = false;
            fadeComponents3 = PreparingGame.GetComponentsInChildren(typeof(Image), true);
            fadeText3 = PreparingGame.GetComponentsInChildren(typeof(Text), true);
            fadeNum = 0;
            while (fadeNum < 1)
            {
                fadeNum += 0.015f;
                foreach (Image o in fadeComponents3)
                {
                    Color newColor = o.color;
                    newColor.a = fadeNum;
                    o.color = newColor;
                }
                foreach (Text i in fadeText3)
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
                yield return new WaitForSeconds(0.01f);
            }


            float howmuchtoaddpersecond = 1 / waitBetween;
                //how much to remove every 0.01 seconds
            while (PreparingGameBar.fillAmount < 1)
            {
                PreparingGameBar.fillAmount += (howmuchtoaddpersecond / 20);
                yield return new WaitForSeconds(0.05f);
            }

            while (fadeNum > 0)
            {
                fadeNum -= 0.015f;
                foreach (Image i in fadeComponents3)
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
                foreach (Text i in fadeText3)
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
                yield return new WaitForSeconds(0.01f);
            }
            PreparingGameBar.fillAmount = 0;


            //fade out of the wait warning and wait bar
        }
        else
        {
            yield return new WaitForSeconds(1);
            if (fadein == "PostGame"|| fadein == "Draw" || fadein == "Guess" || fadein == "InitialPhrase")
            {
                if (fadein == "Draw")
                {
                    PhaseIntroText1.text = "Drawing Phase";
                    int randomint = UnityEngine.Random.Range(0, randomDrawIntro.Length);
                    PhaseIntroText2.text = randomDrawIntro[randomint];
                }
                else if (fadein == "Guess")
                {
                    PhaseIntroText1.text = "Guessing Phase";
                    int randomint = UnityEngine.Random.Range(0, randomGuessIntro.Length);
                    PhaseIntroText2.text = randomGuessIntro[randomint];
                }
                else if (fadein == "InitialPhrase")
                {
                    PhaseIntroText1.text = "Writing Phase";
                    int randomint = UnityEngine.Random.Range(0, randomInitialPhraseIntro.Length);
                    PhaseIntroText2.text = randomInitialPhraseIntro[randomint];
                }
                else if (fadein == "PostGame")
                {
                    PhaseIntroText1.text = "Review Time!";
                    int randomint = UnityEngine.Random.Range(0, randomPostGameIntro.Length);
                    PhaseIntroText2.text = randomPostGameIntro[randomint];
                }
                PhaseIntro.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                PhaseIntro.SetActive(true);
                PhaseIntro.GetComponent<Animator>().SetBool("PlayBoolAnim", true);
                yield return new WaitForSeconds(0.1f);
                audio2.clip = clips[10];
                audio2.Play();
                yield return new WaitForSeconds(waitBetween - 2);
                PhaseIntro.GetComponent<Animator>().SetBool("PlayBoolAnim", false);
                yield return new WaitForSeconds(0.4f);
                PhaseIntro.SetActive(false);
            }
            else
            {
                yield return new WaitForSeconds(waitBetween - 2);
            }
            yield return new WaitForSeconds(0.5f);
        }
        
        if (PlayerReadyState != null)
        {
            updateReadyState();
        }
        //set value of time
        int seconds = 60;
        //Image timerImage;
        //Text timerText;
        if (fadein == "Draw")
        {

            //timerText = drawtimerText;
            //timerImage = drawtimerImage;
            if (screenLength.text == "1 Minute")
            {
                seconds = 60;
            }
            else if (screenLength.text == "2 Minutes")
            {
                seconds = 120;
            }
            else if (screenLength.text == "3 Minutes")
            {
                seconds = 180;
            }
            else if (screenLength.text == "5 Minutes")
            {
                seconds = 300;
            }
        }
        else
        {
            //timerText = guesstimerText;
            //timerImage = guesstimerImage;
            seconds = 90;
        }
        //timerText.text = (Mathf.Floor(seconds / 60.0f)).ToString() + "m";
        //now fading in
        //Debug.Log("Now fading into : " + fadein);
        if (fadein == "Guess" || fadein == "Draw" || fadein == "InitialPhrase")
        {
            StartCoroutine(CountDown(fadein));
        }
        if (fadein == "Guess")
        {
            ParentObject2 = GuessingCanvas;
            guesstimerText.text = "1m30s";
        }
        else if (fadein == "Menu")
        {
            ParentObject2 = MenuCanvas;
            //MenuCanvas.enabled = true;
        }
        else if (fadein == "Draw")
        {
            ParentObject2 = DrawingCanvas;
            drawtimerText.text = (Mathf.Floor(seconds / 60.0f)).ToString() + "m";
        }
        else if (fadein == "Introduction")
        {
            ParentObject2 = IntroductionCanvas;

        }
        else if (fadein == "InitialPhrase")
        {
            InitialPhraseCanvas.enabled = true;
            ParentObject2 = InitialPhraseCanvas;
            initialPhraseText.text = "1m30s";
        }
        else if (fadein == "PostGame")
        {
            ParentObject2 = PostGameCanvas;
        }
        Component[] fadeComponents2;
        Component[] fadeText2;
        ParentObject2.enabled = true;
        ParentObject.enabled = false;
        fadeComponents2 = ParentObject2.GetComponentsInChildren(typeof(Image), true);
        fadeText2 = ParentObject2.GetComponentsInChildren(typeof(Text), true);

        fadeNum = 0;
        while (fadeNum < 1)
        {
            fadeNum += 0.015f;
            foreach (Image i in fadeComponents2)
            {
                if (i.tag != "Panel")
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
            }
            foreach (Text i in fadeText2)
            {
                if (i.tag != "Panel")
                {
                    Color newColor = i.color;
                    newColor.a = fadeNum;
                    i.color = newColor;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        if (fadein == "PostGame")
        {
            StartCoroutine(PostGameReview());
        }
        Debug.Log("Setting View To: " + fadein);
        setView(fadein);
        if (fadedout == "Draw")
        {
            assignNewSendToTarget();
            currentPhaseNumber++;
        }
        if (fadein == "Menu")
        {
            playersConnectedText.text = (AirConsole.instance.GetControllerDeviceIds().Count + "/" + requiredPlayerNum + "+");
            AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "gamehandler.updateplayercount");
        }
    }
}



