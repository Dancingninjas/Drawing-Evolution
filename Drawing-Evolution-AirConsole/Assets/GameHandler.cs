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
    public GameObject WarningText;
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
    public Text screenLanguage;
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
    public Text[] TranslationElements;
    public AudioSource audio1;
    public AudioSource audio2;
    public GameObject checkmarkholder;
    string requiredPlayerNum = "2";
    int actualNumberofPhases = -1;
    int[] WhoThingisSentTo;
    int topPostGameHandler = 1;
    int[] ReviewOrder;
    string[][] jaggedArray;
    bool[] PlayerReadyState;
    bool isPostGameReady;
    int currentPhaseNumber = 0;
    int gameLanguage = 0; //default: english
    int gameLength = 3;
    int gamePhases = -1;
    string gameMode = "RandomPrompt";
    bool isGameStarted = false;
    string[][] TextArray =
    {
        new string[] /*0 ENGLISH*/{ "           English", "Welcome to Drawing Evolution!", "The rules of this game are simple.", "You are given a word or phrase to draw.", "A player will have to guess what word or phrase you drew.","Then, another player will have to draw that guess.", "The more players you have, the more rounds you can play.","At the end, we'll look at how the phrases evolved."/*TitleScreen 8 ---> */, "Welcome to", "Language:", "Mode:", "Random Prompt", "Write Your Own Prompt" , "Drawing Length:", "TIME Minutes", "1 Minute", "Phases:", "Automatic", "Watch Introduction:", "Waiting for players...", "Note: This game is most fun when played with 4+ players", "Write a Word or Phrase", "Someone else will have to draw it", "Drawing Phase", "Draw the word on your device", "Guessing Phase", "Describe the image on your device", "Ready Players", "Original Phrase:", "Final Phrase", "drew:", "guessed:", "wrote:", "Drawing Phase", "Guessing Phase", "Writing Phase", "Review Time!"},
        new string[] /*1 DUTCH*/{ "Nederlands", "Welkom bij Drawing Evolution.", "De spelregels zijn heel simpel.", "Je krijgt een woord of zin om te tekenen.", "Een speler moet raden welk woord of zin je hebt getekend.", "Daarna moet een andere speler die gok moeten tekenen.","Hoe meer spelers je hebt, hoe meer ronden je kunt spelen.", "Aan het eind zullen we kijken hoe de zinnen zich hebben ontwikkeld."/*TitleScreen 8 ---> */, "Welkom bij", "Taal:", "Spelmodus:", "            Willekeurige Tekenprompt", "            Schrijf Je Eigen", "Tekentijd Toegestaan:", "           TIME Minuten", "           1 Minuut", "Aantal Rondes:", "Automatisch", "Bekijk de Introductie:", "Wachten op spelers...", "Notitie: Opmerking: deze game is het leukst als deze met 4+ spelers wordt gespeeld", "Schrijf een woord of een zin op", "Lemand anders zal het moeten schetsen", "Tekenfase", "Teken het woord op uw apparaat", "Beschrijf Modus", "Beschrijf de Tekening", "Spelers Klaar", "Origineel Zin:", "Laatste Zin:", "tekende:", "vermoedde:", "schreef:", "Tekenfase", "Beschrijf Modus", "Schrijffase", "Tijd om te beoordelen!"},
        new string[] /*2 FRENCH*/{ "      Français", "Bienvenue dans Drawing Evolution.", "Les règles de ce jeu sont simples.", "On te donne un mot ou une phrase à dessiner.", "Un joueur devra deviner quel mot ou quelle phrase tu as tiré.", "Ensuite, un autre joueur devra tirer cette hypothèse.", "Plus vous avez de joueurs, plus vous pouvez jouer de rounds.", "À la fin, nous verrons comment les phrases ont évolué."/*TitleScreen 8 ---> */, "Bienvenue dans", "Langue:", "Mode:", "Invitation Aléatoire", "Écrivez Votre Propre Invite", "Temps de Dessiner:", "      TIME Minutes", "      1 Minute", "Nombre de Phases: ", "Automatique", "Regarder l'Introduction:", "En attente de joueurs...", "Remarque: ce jeu est plus amusant lorsqu'il est joué à 4 joueurs ou plus", "Écrire un mot ou une phrase", "Quelqu'un d'autre devra l'esquisser", "Phase de Dessin", "Dessine le mot sur ton appareil", "Phase de Devinette", "Décrire l'image sur votre appareil", "Joueurs Prêts", "Phrase Originale:", "Phrase Finale:", "dessiné:", "deviné:", "a écrit:", "Phase de dessin", "Phase de devinette", "Phase d'écriture", "Il est temps de réviser!"},
        new string[] /*3 GERMAN*/{ "        Deutsch", "Willkommen bei Drawing Evolution.", "Die Regeln dieses Spiels sind einfach.", "Sie erhalten ein Wort oder einen Satz zum Zeichnen.", "Ein Spieler muss raten, welches Wort oder welche Phrase Sie gezeichnet haben.", "Dann muss ein anderer Spieler diese Vermutung ziehen.", "Je mehr Spieler du hast, desto mehr Runden kannst du spielen.", "Am Ende schauen wir uns an, wie sich die Sätze entwickelt haben."/*TitleScreen 8 ---> */, "Willkommen bei", "Sprache:", "Modus:", "  Zufällige Eingabeaufforderung", "  Schreibe Dein Eigenes", "Zeit Zum Zeichnen:", "     TIME Minuten", "     1 Minute", "Anzahl der Phasen:", "Automatisch", "Uhr Einführung:", "Warten auf Spieler...", "Hinweis: Dieses Spiel macht am meisten Spaß, wenn es mit 4 oder mehr Spielern gespielt wird", "Schreiben Sie ein Wort oder eine Phrase", "Jemand anderes muss es zeichnen", "Zeichenmodus", "Zeichnen Sie das Wort auf Ihrem Gerät", "Ratephase", "Beschreiben Sie das Bild auf Ihrem Gerät", "Bereite Spieler", "Ursprüngliche Phrase:", "Letzte Phrase:", "zeichnete:", "schrieb:", "schrieb:", "Zeichenphase", "Ratephase", "Schreibphase", "Zeit für eine Überprüfung!"},
        new string[] /*4 ITALIAN*/{ "     Italiano", "Benvenuto in Drawing Evolution.", "Le regole di questo gioco sono semplici.", "Ti viene data una parola o una frase da disegnare.", "Un giocatore dovrà indovinare la parola o frase che hai disegnato.", "Dopodiché un altro giocatore dovrà disegnare l'ipotesi suggerita dall'altro giocatore.", "Più giocatori sono presenti, più si possono giocare round.", "Alla fine, si osserverà l'evoluzione della frase/parola."/*TitleScreen 8 ---> */, "Benvenuto in", "Lingua:", "Modalità:", "       Suggerimento Casuale", "       Scrivi Il Tuo Suggerimento", "Tempo Di Disegnare:", "        TIME Minuti", "        1 Minuto", "Numero di Fasi:" , "Automatico", "Guarda L'Introduzione:", "Aspettando i giocatori...", "Nota: Questo gioco è più divertente se giocato con più di 4 giocatori", "Scrivi Una Parola O Una Frase", "Qualcun Dovrà Disegnarla", "Fase di Disegno", "Disegna la parola sul tuo dispositivo", "Fase di Indovinare", "Descrivi l'immagine sul tuo dispositivo", "Giocatori Pronti", "Frase Originale:", "Frase Finale:", "disegnato:", "scritto:", "scritto:", "Fase di Disegno", "Fase di Indovinare", "Fase di Scrittura", "È ora di rivedere!"},
        new string[] /*5 PORTUGUESE*/{ "     Português", "Bem-vindo ao Drawing Evolution.", "As regras deste jogo são simples.", "Recebe uma palavra ou frase para desenhar.", "Um jogador terá de adivinhar que palavra ou frase desenhou.", "Depois, outro jogador terá de desenhar esse palpite.", "Quantos mais jogadores tiver, mais rondas terá de jogar.", "No final, iremos observar a forma como as frases evoluíram."/*TitleScreen 8 ---> */, "Bem-vindo ao", "Idioma:", "Modo:", "Palpite aleatório", "Escreva o Seu Próprio Palpite", "Tempo De Desenho:", "      TIME Minutos", "      1 Minuto", "Número de Fases:", "Automático", "Assista à Introdução:", "Esperando por jogadores...", "Nota: Este jogo é mais divertido quando é jogado com mais de 4 jogadores", "Escreva uma palavra ou frase", "Alguém terá que desenhá-lo", "Fase do Desenho", "Desenhe a palavra no seu dispositivo", "Fase do Palpite", "Descreva a imagem no seu dispositivo", "Jogadores Prontos", "Frase Original", "Frase Final", "desenhou:", "escreveu:", "escreveu:", "Fase do Desenho", "Fase do Palpite", "Fase da Escrita", "Hora de rever!"},
        new string[] /*6 SPANISH*/{ "     Español", "Bienvenido a Drawing Evolution", "Las reglas de este juego son simples.", "Te darán una palabra o una frase para que la dibujes", "Un jugador tendrá que adivinar la palabra o frase que dibujaste", "Entonces, otro jugador tendrá que dibujar esa suposición.", "Mientras más jugadores tengas, más rondas podrás jugar.", "Al final, veremos cómo han evolucionado las frases"/*TitleScreen 8 ---> */, "Bienvenido a", "Idioma:", "Modo:", "Pregunta al Azar", "Escribe tu Propia Pregunta", "Tiempo De Dibujar:", "     TIME Minutos", "     1 Minuto", "Número de Fases:", "Automático", "Ver la Introducción:", "Esperando jugadores...", "Nota: Este juego es más divertido cuando se juega con más de 4 jugadores.", "Escribe una palabra o frase", "Alguien más tendrá que dibujarlo", "Fase de Dibujar", "Dibuja la Palabra en tu Dispositivo", "Fase de Adivinar", "Describe la Imagen en tu Dispositivo", "Jugadores Preparados", "Frase Original:", "Frase Final:", "dibujó:", "escribio:", "escribio:", "Fase de Dibujar", "Fase de Adivinar", "Fase de Escritura", "¡Tiempo de Revisar!"},
    };

    int[] customPlayerNumber;
    List<string> randomList;
    string[] randomDrawIntro = new string[6] { "Time to draw something amazing, or maybe awful.", "Don't know how to draw something? Too bad!", "Use a variety of colors to bring the prompt to life.", "Time to scribble something down and call it art.", "Sharpen those imaginary pencils, and get ready to draw!", "Everything is beautiful in its own way, but feel free to prove me wrong."};

    string[] randomGuessIntro = new string[6] { "Get ready for utter confusion!", "'Throwing Up Spaghetti?' 'Sandwich With Eyes?'", "Even if you know you're wrong, take a guess.", "Don't feel bad if you know your guess is wrong.", "If your guess is right, I'll give you a cookie.", "If you don't know what it is, describe the drawing."};

    string[] randomInitialPhraseIntro = new string[5] { "Write about a person, inside joke, or anything!", "You decide what others have to draw!", "'Joe Spilling Coffee on the Copier Again'", "Think of something you want someone else to draw.", "You can write anything you want, funny or serious." };

    string[] randomPostGameIntro = new string[5] { "Let's see how your drawings evolved!", "Get ready to be blown away by the amazing art.", "Time for everyone to judge you and your drawings.", "Let's have a look at the amazing art you made!", "Let's see how to bad your guesses were!"};
    //string[] randomList = new string[2] { "test test test test test test", "test2 test2 test2 test2 test2 test2 test2 test2" };
    string[] randomListArray = new string[302] {"Presidential Speech", "Thanksgiving Dinner", "Road Rage", "Dirty Socks", "Taco Tuesday", "First Time Riding a Bicycle", "Blind Man Seeing For the First Time", "Professional Bowler", "Last Minute Homework Rush", "Skype Interview", "Stargazing", "Midnight Snack", "Forgetting to Put Pants On", "Helping Someone Cross the Street", "Knock Knock Joke", "Playing a Board Game", "Hide and Go Seek", "High School Gossip", "New Person at School", "An Apple a Day Keeps the Doctor Away", "Crying Over Spilled Milk", "Milking a Goat", "Hay Ride", "Vacation", "Lumberjack", "Twins", "Dog Training", "Couch Potato", "Heroic Firefighter", "Hoola Hooping", "Dance Battle", "Take Your Child To Work Day", "Mowing the Lawn", "Summer", "Superhero", "Sad Song", "Playing Horseshoes", "Staying Home Sick", "Water Balloon Fight", "Doing Chores", "Grocery Shopping", "Sleepover", "Home Run", "Leprechauns Dancing", "Karaoke", "Missing the Bus", "Family Party", "News Anchor", "Spelling Bee", "Monster Under the Bed", "Homework Excuse", "Pickpocketing", "Education", "Cartoons", "Beating Around the Bush", "Tug of War", "Frisbee Golf", "'Put a Sock In It'", "Car Commercial", "Stubbing Your Toe", "Best Friend", "Nature Lover", "Cat Taking a Bath", "Taking the Cake", "Inventing Something", "Going for a Walk", "Making a Sandwich", "Losing Your Keys", "River of Tears", "Nightmare", "Taste Testing", "Blind Date", "All-You-Can-Eat Buffet", "1 Star Restaurant", "Taking Candy from a Baby", "Broken Umbrella", "Popular Kid", "Diving in the Pool", "Ice Skating", "Playing Tag", "Volleyball Spike", "Proposal Gone Wrong", "Class Clown", "Cool Guys Don't Look at Exposions", "Cleaning the Litter Box", "'Get Off My Lawn!'", "Pizza Delivery", "Class Presentation", "School Play", "Sledding Down a Hill", "Pool Games", "Asking Someone Out", "'The Cat is Out of the Bag'", "Skateboarding Trick", "Winning the Lottery", "Hitchhiking", "Anniversary Gift", "First Day on the Job", "Sucking Up to the Boss", "Teacher's Pet", "Turning into a Zombie"/*<---104*/, "Surprise Party", "Building an Igloo", "True Love", "Burning the Popcorn", "Garbage Man", "Art Auction", "Parade", "First Plane Ride", "Running Out of Breath", "Wild West Duel", "Heat Wave", "Bossy Chef", "Road Trip", "Dodgeball", "Walking into a Trap", /*<--119*/"Caught Red Handed", "Stuck in a Loop", "One Man's Trash is Another Man's Treasure", "Bank Robbery", "First Man on Mars", "Hailing a Taxi Cab", "Jumping Jacks", "Tourist in a Foreign Country", "Playing Golf", "School Bully", "Texting a Friend", "Science Fair Project", "Parents Embarrassing Their Kids", "Show and Tell", "Loud Vacuum", "Drive-In Theatre", "Disney Cruise", "Seagulls Stealing Food", "Waiting in Line for Water Ice", "Lazy River", "Water Park", "Applying Sunscreen", "Singing in the Shower", "Baby Bird Leaving the Nest", "Earwax", "Slipping on a Banana Peel", "Planting a Tree", "Customer Complaining", "Climbing a Mountain", "Letting Go of a Balloon Outside", "Family Photo", "Shaving a Beard", "Surfing", "Step on a Crack, Break Your Mother's Back", "Grinch Stealing Christmas", "Pig in a Blanket", "Cinderella Losing Her Shoe", "Dropping Your Phone", "Pushing Someone into the Pool", "Tightrope Walker", "Magician", "Dog Afraid of a Thunderstorm", "Three Little Pigs", "Sitting on a Whoopie Cushion", "Watching a Scary Movie", "Falling Asleep in Class", "Getting a Massage", "Getting Mad at Your Alarm Clock", "Using Your Mouth to Blow Up a Balloon", "Eating Loudly", "Tripping on Your Shoelaces", "Trying to Fit Your Hand in a Pringles Can", "Banana Phone", "Staring Contest", "Camping Outside a Store for a Sale", "A Store on Black Friday", "Resisting Arrest", "Shrink Ray", "Opening a Soda After Shaking It", "Tossing Dough in the Air", "Sinking Ship", "Poison Ivy Rash", "Playing Duck Duck Goose", "Blinking in the Photo", "Chinese Finger Trap", "Going Up Two Stairs at a Time", "Making a Mess With Your Food", "Using Your Neighbor's WiFi", "Goalie Save", "Seeing a Ghost", "Squish the Lemon", "Pillow Fight", "Signing Someone's Cast", "Climbing a Flagpole", "Defusing a Bomb", "Putting a Necklace on Someone", "Removing the Crust From a Sandwich", "Protesting", "Cat Chasing a Laser Pointer", "Bird Pooping on Someone", "Burping the Baby", "Jumping in Puddles", "Firing a Confetti Cannon", "A Fork in the Road", "Building a New PC", "Trying to do a Handstand", "Trolls Under the Bridge", "The Floor is Lava", "Meditation", "Talking With Your Mouth Full", "Hot Dog Eating Contest", "Playing Fetch With a Dog", "Putting Your Coat on the Rack", "Changing a Light Bulb", "Movie Theater Date", "Phone Ringing During Class", "Relay Race", "Walking Barefoot on Grass", "Texting During a Date", "Cheerleader Falling", "Parallel Parking", "School Bus Evacuation", "Obstacle Course", "Book Worm", "Hard Taco Shell Falling Apart as You Eat" /* */, "Tackling an Alligator", "Secret Handshake", "Robin Hood", "Getting Pied in the Face", "Socks and Sandals", "Daredevil Stunt", "Picking Flowers", "Stepping on a Lego", "Trimming a Bush", "Groundhog Seeing Its Shadow", "Window Washer", "Launching a Car Into Space", "Double Dipping", "Birthday Punches", "Sneezing Directly Into Someone's Eyes", "Shoe Shiner", "Painting a Picket Fence", "Giving Your Veggies to the Dog", "Crowd Surfing", "Closing the Door on Your Finger", "Monkey Stealing Something", "Voodoo Doll", "Bug Zapper", "Breakdancing", "Pin the Tail on the Donkey", "Biting Your Tongue", "Shivering in the Cold", "Plumber Fixing a Pipe", "Rocking Your Socks Off", "Tapping Someone on the Shoulder Opposite to Where You're Standing", "Whale Watching", "Pillow Fort", "Hitting Your Neighbor's Window With a Baseball", "Spilling Coffee on Yourself", "Reading Someone's Mind", "Scratching Your Back", "Cat Scratching Furniture", "School Lunch", "Car Dealer", "Cards Up Your Sleeve", "Armpit Farting", "Prank Call", "Dancing in the Rain", "Stuck in Quicksand", "Fire Drill", "Rock Climbing", "Deer in Headlights", "Peeking at a Classmate's Test", "Closing the Door on a Salesperson", "Wig Falling Off", "Stairway to Heaven", "Earthquake", "Bull Riding", "Rounding Up Cattle", "Spitting in Someone's Face While Talking", "Drawing on a Sleeping Person's Face", "Accidentally Ripping Your Pants", "Child Pretending to Be an Adult", "Ignoring Someone", "Static Electricity", "Stone Skipping", "Cats Always Land on Their Feet", "Waving at Someone Who Wasn't Waving at You", "Volume Too High", "Freeze Tag", "Trying to Make a Child Smile in a Photo", "Puppet Show", "Waterbed", "Hard Hats Save Lives", "Fishing For Compliments", "Upstairs Neighbor Stomping", "Shaking a Vending Machine", "Knitting a Sweater", "Bedtime Story", "Teleportation Device", "Going Faster than the Speed of Light", "Walking on Thin Ice", "Sign Language", "Pushing Someone's Face into a Cake", "'I got your nose'", "Stacking Boxes"};

    string[][] randomListJaggedArray =
    {
        new string[] /*0 ENGLISH*/{"Presidential Speech", "Thanksgiving Dinner", "Road Rage", "Dirty Socks", "Taco Tuesday", "First Time Riding a Bicycle", "Blind Man Seeing For the First Time", "Professional Bowler", "Last Minute Homework Rush", "Skype Interview", "Stargazing", "Midnight Snack", "Forgetting to Put Pants On", "Helping Someone Cross the Street", "Knock Knock Joke", "Playing a Board Game", "Hide and Go Seek", "High School Gossip", "New Person at School", "An Apple a Day Keeps the Doctor Away", "Crying Over Spilled Milk", "Milking a Goat", "Hay Ride", "Vacation", "Lumberjack", "Twins", "Dog Training", "Couch Potato", "Heroic Firefighter", "Hoola Hooping", "Dance Battle", "Take Your Child To Work Day", "Mowing the Lawn", "Summer", "Superhero", "Sad Song", "Playing Horseshoes", "Staying Home Sick", "Water Balloon Fight", "Doing Chores", "Grocery Shopping", "Sleepover", "Home Run", "Leprechauns Dancing", "Karaoke", "Missing the Bus", "Family Party", "News Anchor", "Spelling Bee", "Monster Under the Bed", "Homework Excuse", "Pickpocketing", "Education", "Cartoons", "Beating Around the Bush", "Tug of War", "Frisbee Golf", "'Put a Sock In It'", "Car Commercial", "Stubbing Your Toe", "Best Friend", "Nature Lover", "Cat Taking a Bath", "Taking the Cake", "Inventing Something", "Going for a Walk", "Making a Sandwich", "Losing Your Keys", "River of Tears", "Nightmare", "Taste Testing", "Blind Date", "All-You-Can-Eat Buffet", "1 Star Restaurant", "Taking Candy from a Baby", "Broken Umbrella", "Popular Kid", "Diving in the Pool", "Ice Skating", "Playing Tag", "Volleyball Spike", "Proposal Gone Wrong", "Class Clown", "Cool Guys Don't Look at Exposions", "Cleaning the Litter Box", "'Get Off My Lawn!'", "Pizza Delivery", "Class Presentation", "School Play", "Sledding Down a Hill", "Pool Games", "Asking Someone Out", "'The Cat is Out of the Bag'", "Skateboarding Trick", "Winning the Lottery", "Hitchhiking", "Anniversary Gift", "First Day on the Job", "Sucking Up to the Boss", "Teacher's Pet", "Turning into a Zombie"/*<---104*/, "Surprise Party", "Building an Igloo", "True Love", "Burning the Popcorn", "Garbage Man", "Art Auction", "Parade", "First Plane Ride", "Running Out of Breath", "Wild West Duel", "Heat Wave", "Bossy Chef", "Road Trip", "Dodgeball", "Walking into a Trap", /*<--119*/"Caught Red Handed", "Stuck in a Loop", "One Man's Trash is Another Man's Treasure", "Bank Robbery", "First Man on Mars", "Hailing a Taxi Cab", "Jumping Jacks", "Tourist in a Foreign Country", "Playing Golf", "School Bully", "Texting a Friend", "Science Fair Project", "Parents Embarrassing Their Kids", "Show and Tell", "Loud Vacuum", "Drive-In Theatre", "Disney Cruise", "Seagulls Stealing Food", "Waiting in Line for Water Ice", "Lazy River", "Water Park", "Applying Sunscreen", "Singing in the Shower", "Baby Bird Leaving the Nest", "Earwax", "Slipping on a Banana Peel", "Planting a Tree", "Customer Complaining", "Climbing a Mountain", "Letting Go of a Balloon Outside", "Family Photo", "Shaving a Beard", "Surfing", "Step on a Crack, Break Your Mother's Back", "Grinch Stealing Christmas", "Pig in a Blanket", "Cinderella Losing Her Shoe", "Dropping Your Phone", "Pushing Someone into the Pool", "Tightrope Walker", "Magician", "Dog Afraid of a Thunderstorm", "Three Little Pigs", "Sitting on a Whoopie Cushion", "Watching a Scary Movie", "Falling Asleep in Class", "Getting a Massage", "Getting Mad at Your Alarm Clock", "Using Your Mouth to Blow Up a Balloon", "Eating Loudly", "Tripping on Your Shoelaces", "Trying to Fit Your Hand in a Pringles Can", "Banana Phone", "Staring Contest", "Camping Outside a Store for a Sale", "A Store on Black Friday", "Resisting Arrest", "Shrink Ray", "Opening a Soda After Shaking It", "Tossing Dough in the Air", "Sinking Ship", "Poison Ivy Rash", "Playing Duck Duck Goose", "Blinking in the Photo", "Chinese Finger Trap", "Going Up Two Stairs at a Time", "Making a Mess With Your Food", "Using Your Neighbor's WiFi", "Goalie Save", "Seeing a Ghost", "Squish the Lemon", "Pillow Fight", "Signing Someone's Cast", "Climbing a Flagpole", "Defusing a Bomb", "Putting a Necklace on Someone", "Removing the Crust From a Sandwich", "Protesting", "Cat Chasing a Laser Pointer", "Bird Pooping on Someone", "Burping the Baby", "Jumping in Puddles", "Firing a Confetti Cannon", "A Fork in the Road", "Building a New PC", "Trying to do a Handstand", "Trolls Under the Bridge", "The Floor is Lava", "Meditation", "Talking With Your Mouth Full", "Hot Dog Eating Contest", "Playing Fetch With a Dog", "Putting Your Coat on the Rack", "Changing a Light Bulb", "Movie Theater Date", "Phone Ringing During Class", "Relay Race", "Walking Barefoot on Grass", "Texting During a Date", "Cheerleader Falling", "Parallel Parking", "School Bus Evacuation", "Obstacle Course", "Book Worm", "Hard Taco Shell Falling Apart as You Eat" /* */, "Tackling an Alligator", "Secret Handshake", "Robin Hood", "Getting Pied in the Face", "Socks and Sandals", "Daredevil Stunt", "Picking Flowers", "Stepping on a Lego", "Trimming a Bush", "Groundhog Seeing Its Shadow", "Window Washer", "Launching a Car Into Space", "Double Dipping", "Birthday Punches", "Sneezing Directly Into Someone's Eyes", "Shoe Shiner", "Painting a Picket Fence", "Giving Your Veggies to the Dog", "Crowd Surfing", "Closing the Door on Your Finger", "Monkey Stealing Something", "Voodoo Doll", "Bug Zapper", "Breakdancing", "Pin the Tail on the Donkey", "Biting Your Tongue", "Shivering in the Cold", "Plumber Fixing a Pipe", "Rocking Your Socks Off", "Tapping Someone on the Shoulder Opposite to Where You're Standing", "Whale Watching", "Pillow Fort", "Hitting Your Neighbor's Window With a Baseball", "Spilling Coffee on Yourself", "Reading Someone's Mind", "Scratching Your Back", "Cat Scratching Furniture", "School Lunch", "Car Dealer", "Cards Up Your Sleeve", "Armpit Farting", "Prank Call", "Dancing in the Rain", "Stuck in Quicksand", "Fire Drill", "Rock Climbing", "Deer in Headlights", "Peeking at a Classmate's Test", "Closing the Door on a Salesperson", "Wig Falling Off", "Stairway to Heaven", "Earthquake", "Bull Riding", "Rounding Up Cattle", "Spitting in Someone's Face While Talking", "Drawing on a Sleeping Person's Face", "Accidentally Ripping Your Pants", "Child Pretending to Be an Adult", "Ignoring Someone", "Static Electricity", "Stone Skipping", "Cats Always Land on Their Feet", "Waving at Someone Who Wasn't Waving at You", "Volume Too High", "Freeze Tag", "Trying to Make a Child Smile in a Photo", "Puppet Show", "Waterbed", "Hard Hats Save Lives", "Fishing For Compliments", "Upstairs Neighbor Stomping", "Shaking a Vending Machine", "Knitting a Sweater", "Bedtime Story", "Teleportation Device", "Going Faster than the Speed of Light", "Walking on Thin Ice", "Sign Language", "Pushing Someone's Face into a Cake", "'I got your nose'", "Stacking Boxes"},
        new string[] /*1 DUTCH*/{ "Presidentiële speech", "Thanksgiving Diner", "Verkeeswode", "Vuile Sokken", "Taco Tuesday", "Eerste keer op een fiets rijden", "Blinde man die voor het eerst kan zien", "Professionele Bowler", "Last Minute huiswerk Stress", "Skype Interview", "Sterrenkijken", "Midnight Snack", "Vergeten de broek aan te doen", "Iemand helpen de straat over te steken", "Klock knock Grap", "Een bordspel spelen", "Verstoppertje spelen", "Roddelen op school", "Nieuw iemand op school", "Een appel per dag houdt de dokter uit de buurt", "Huilen over gemorste melk", "Een geit melken", "Hooirit", "Vakantie", "Houthakker", "Tweeling", "Hondentraining", "Bankhanger", "Heldhaftige", "Hoolahoopen", "Dance Battle", "Neem je kind mee naar het werk dag", "Gazon maaien", "Zomer", "Superheld", "Zielig Liedje", "Hoefijzer werpen", "Ziek melden en Thuis blijven", "Waterballongevecht", "Huishoudklusjes doen", "Boodschappen doen", "Slaapfeestje", "Home Run", "Dansende Dwergen", "Karaoke", "De Bus missen", "Familiefeestje", "Nieuwsvoorlezer", "Spellingsbij", "Monster onder het bed", "Huiswerksmoesje", "Zakkenrollen", "Onderwijs", "Cartoons", "Dingen uitstellen", "Touwtrekken", "Frisbee Golf", "Stop er een sok in'.", "Autoreclame", "Je teen stoten", "Beste vriend", "Natuurliefhebber", "Kat die een bad neemt", "De Cake eten", "Iets uitvinden", "Een wandeling maken", "Een broodje maken", "Je sleutels verliezen", "Rivier van Tranen", "Nachtmerrie", "Smaaktest", "Blind Date", "All-You-Can-Eat Buffet", "1 Ster Restaurant", "Snoep van een baby afpakken", "Gebroken paraplu", "Populair Kind", "Duiken in het zwembad", "Schaatsen", "Tikkertje spelen", "Volleybal", "Voorstel fout gegaan", "Klasse Clown", "Coole Gasten gaan niet naar Exposities", "De Vuilnisbak schoonmaken", "Ga van mijn gazon af!", "Pizzabezorging", "Presentatie in de klas", "Schoolspel", "Van een heuvel af sleeën", "Zwembadspelletjes", "Iemand uit vragen", "'De geest is uit de fles'", "Skateboardtruc", "De loterij winnen", "Liften", "Verjaardagscadeau", "Eerste werkdag", "Slijmen bij de baas", "Huisdier van de Leraar", "In een Zombie Veranderen", "Verrassingsfeestje", "Een Iglo bouwen", "Echte Liefde", "Popcorn opwarmen", "Vuilnisman", "Kunstveiling", "Parade", "Eerste vlucht met vliegtuig", "Buiten adem raken", "Wild West Duel", "Hittegolf", "Bazige chef", "Roadtrip", "Trefbal", "In een val wandelen", "Op Heterdaad betrapt", "In een spiraal Vastzitten", "De een zijn dood is de ander zijn brood", "Bankoverval", "Eerste Man op Mars", "Een taxi wenken", "Jumping Jacks", "Toerist in een vreemd land", "Golfen", "De pestkop op school", "Een vriend een berichtje sturen", "Wetenschapsproject", "Ouders die hun kinderen in verlegenheid brengen", "Show and Tell", "Luidruchtig vacuüm", "Drive-In Theatre", "Disney Cruise", "Meeuwen die voedsel stelen", "In de rij wachten voor Waterijsjes", "Kabbelende Rivier", "Waterpark", "Zonnebrandcrème opdoen", "Zingen in de douche", "Babyvogel die het nest verlaat", "Oorsmeer", "Uitglijden over een bananenschil", "Een boom planten boom", "Klachten van klanten", "Een berg beklimmen", "Buiten een ballon loslaten", "Familie foto", "Een baard scheren", "Surfen", "Stap op een Crack, Breek je moeder's rug", "Grinch die Kerst steelt", "Worstenbroodjes", "Assepoester die haar schoen verliest", "Je telefoon laten vallen", "Iemand in het zwembad duwen", "Koorddanser", "Tovenaar", "Hond bang voor een onweersbui", "Drie kleine varkens", "Op een scheetkussen zitten", "Een enge film kijken", "In de klas in slaap vallen", "Massage krijgen", "Boos worden op je wekker", "Je mond gebruiken om een ballon op te blazen", "Luidkeels eten", "Struikelen over je schoenveters", "Proberen om je hand in een Pringles Can te steken", "Bananentelefoon", "Staar Wedstrijd", "Buiten een winkel slapen om als eerste iets te kopen", "Een winkel op Black Friday", "Verzet tegen de arrestatie", "Shrink Ray", "Een frisdrank openen nadat je die geschud hebt", "Deeg in de lucht gooien", "Zinkend schip", "Poison Ivy Rash", "Zakdoekje leggen spelen", "Knipperend in de foto", "Chinese Vingerval", "Twee trappen tegelijk opgaan", "Een rotzooi maken van je eten", "De WiFi van je buurman gebruiken", "Redding van de keeper", "Een geest zien", "Citroen uitknijpen", "Kussengevecht", "Iemands Cast tekenen", "Een vlaggenmast beklimmen", "Een bom onschadelijk maken", "Iemand een ketting omdoen", "De korst van een broodje weghalen", "Protesteren", "Kat die een laser achterna jaagt", "Vogel poept op iemand", "Boerende Baby", "Springen in plassen", "Een Confetti-kanon afvuren", "Een T-splitsing in de weg", "Een nieuwe PC bouwen", "Proberen een handstand te doen", "Trollen onder de brug", "De vloer is Lava", "Meditatie", "Praten met je mond vol", "Hot Dog Eetwedstrijd", "Pak spelen met een hond", "Je jas op het rek leggen", "Een gloeilamp vervangen", "Filmdate", "Beltoon van de telefoon tijdens de les", "Estafette", "Wandelen op blote voeten op gras", "Sms'en tijdens een afspraakje", "Cheerleader die valt", "Parallel parkeren", "Schoolbus Evacuatie", "Hindernisbaan", "Boekenworm", "Taco die kapotbreekt als je eet", "Een alligator aanpakken", "Geheime handdruk", "Robin Hood", "Taart in het gezicht krijgen", "Sokken en sandalen", "Durfal Stunt", "Bloemen plukken", "Op een Legoblokje stappen", "Een struik bijknippen", "Marmot die zijn eigen schaduw ziet", "Ramenwasser", "Een auto de ruimte in lanceren", "Dubbel Dipping", "Verjaardag punches", "Rechtstreeks iemand in de ogen niesen", "Schoenenpoetser", "Een hek schilderen", "Je groenten aan de hond geven", "Crowdsurfing", "Je vinger achter de deur laten zitten", "Aap die iets steelt", "Voodoopop", "Vliegenklapper", "Breakdancen", "Ezeltje Prikje", "Op je tong bijten", "Bibberend in de Koude", "Loodgieter die een pijp maakt", "Je sokken uitschudden", "Iemand op de schouders tikken aan de andere kant van aan waar jij staat", "Walvissen spotten", "Kussen Fort", "Het raam van je buurman met een bal kapot schieten", "Koffie morsen op jezelf", "Iemands geest lezen", "Je rug krabben", "Kat die aan het meubilair krabt", "Schoollunch", "Autodealer", "Kaarten in je mouw", "Scheten laten met je oksels", "Belletje lellen", "Dansen in de regen", "Vastzitten in Drijfzand", "Brandoefening", "Rotsen beklimmen", "Hert in koplampen", "Spieken bij een klasgenootje tijden een test", "De deur voor een verkoper dichtdoen", "Pruik die eraf valt", "Trap naar de hemel", "Aardbeving", "Stierenrijden", "Vee verzamelen", "Spugen in iemands gezicht terwijl je praat", "Tekenen op het gezicht van iemand die slaapt", "Per ongeluk je broek uitscheuren", "Kind dat zich voordoet als een volwassene", "Iemand negeren", "Statische elektriciteit", "Steen overslaan", "Katten landen altijd op hun voeten", "Zwaaien naar Iemand die niet naar je zwaaide", "Volume te hoog", "Freeze Tag", "Proberen een kind te laten glimlachen in een foto", "Poppenshow", "Waterbed", "Veiligheidshelmen redden levens", "Naar complimentjes hengelen", "Bovenbuurman die stampt", "Een drankautomaat door elkaar rammelen", "Een trui breien", "Slaapverhaaltjes", "Teleportatieapparaat", "Sneller gaan dan de snelheid van het licht", "Op dun ijs wandelen", "Gebarentaal", "Iemands gezicht in een taart duwen", "'Ik heb je neus'", "Verhuisdozen" },
        new string[] /*2 FRENCH*/{ "Discours présidentiel", "Dîner de Thanksgiving", "Rage au volant", "Chaussettes sales", "Taco Tuesday", "Première fois à bicyclette", "Aveugle voyant pour la première fois", "Bowler professionnel", "Rush des devoirs de dernière minute", "Interview sur Skype", "Observation des étoiles", "Oublier de mettre un pantalon", "Aider quelqu'un à traverser la rue", "Jouer à un jeu de société", "Jouer à cache-cache", "Nouvelle personne à l'école", "Une pomme par jour garde le docteur loin", "Pleurer sur le lait renversé", "Traire une chèvre", "Tour de foin", "Vacances", "Bûcheron", "Jumeaux", "Entrainement de chien", "Patate de sofa", "Pompier héroïque", "Bataille de dance", "Amenez votre enfant au travail", "Tondre la pelouse", "Été", "Super-héros", "Chanson triste", "Jouer au fer à cheval", "Rester malade à la maison", "Combat de ballon d'eau", "Faire des corvées", "Épicerie", "Soirée pyjama", "Manquer le bus", "Fête de famille", "News Ancre", "Monstre sous le lit", "Excuse pour les devoirs", "Vol à la tire", "Les dessins animés", "Tir à la corde", "Mettre une chaussette en elle", "Voiture commerciale", "Meilleur ami", "Amoureux de la nature", "Chat prenant un bain", "Prendre le gâteau", "Inventer quelque chose", "Faire une promenade", "Faire un sandwich", "Perdre vos clés", "Rivière des larmes", "Cauchemar", "Test de goût", "Buffet à volonté", "Restaurant 1 étoile", "Prendre Candy à un bébé", "Parapluie cassé", "Kid populaire", "Plongée dans la piscine", "Patinage sur glace", "Jouer tag", "La proposition a mal tourné", "Clown de la classe", "Les gars cool ne regardent pas les expositions", "Nettoyage de la litière", "Descendre ma pelouse!", "Livraison de pizzas", "Présentation en classe", "Jeu d'école", "Traverser une colline en traîneau", "Jeux de billard", "Demander à quelqu'un", "Le chat est sorti du sac", "Gagner à la loterie", "Auto-stop", "Cadeau d'anniversaire", "Premier jour de travail", "Sucer le patron", "L'animal de compagnie de l'enseignant", "Se transformer en zombie", "Fête surprise", "Construire un igloo", "L'amour vrai", "Brûler le Popcorn", "Éboueur", "Premier tour d'avion", "À bout de souffle", "Duel d'Ouest Sauvage", "La canicule", "Chef autoritaire", "Marcher dans un piège", "Pris la main dans le sac", "Coincé dans une boucle", "Le malheur des uns fait le bonheur des autres", "Braquage de banque", "Premier homme sur Mars", "Avoir un taxi", "Touriste dans un pays étranger", "Jouer au golf", "Envoyer un SMS à un ami", "Projet d'expo-sciences", "Les parents embarrassent leurs enfants", "Montrer et dire", "Vide fort", "Théâtre Drive-In", "Croisière Disney", "La nourriture des voleurs de mouettes", "En attente de glace d'eau", "Rivière tranquille", "Parc aquatique", "Appliquer un écran solaire", "Chanter dans la douche", "Bébé oiseau quittant le nid", "Cérumen", "Glisser sur une peau de banane", "Planter un arbre", "Client se plaindre", "Escalader une montagne", "Lâcher un ballon à l'extérieur", "Photo de famille", "Rasage de la barbe", "Surfant", "Faites un pas en avant, brisez le dos de votre mère", "Noël grincheux", "Cochon dans une couverture", "Cendrillon perd sa chaussure", "Lâcher votre téléphone", "Pousser quelqu'un dans la piscine", "Funambule", "Magicien", "Chien peur d'un orage", "Trois petits cochons", "Assis sur un coussin Whoopie", "Regarder un film effrayant", "S'endormir en classe", "Obtenir un massage", "Se fâcher de votre réveil", "Utiliser votre bouche pour faire exploser un ballon", "Manger fort", "Trébucher sur vos lacets", "Essayer de tenir votre main dans un Pringles Can", "Téléphone banane", "Concours Staring", "Camping en dehors d'un magasin pour une vente", "Un magasin le vendredi noir", "Résister à une arrestation", "Rayon rétractable", "Ouvrir un soda après l'avoir secoué", "Lancer de la pâte dans l'air", "Bateau qui coule", "Jouer au canard ou à la canard", "Clignotant sur la photo", "Piège à doigts chinois", "Monter deux escaliers à la fois", "Faire un désordre avec votre nourriture", "Utiliser le WiFi de votre voisin", "Gardien de sauvegarde", "Voir un fantôme", "Bataille d'oreillers", "Signer le casting de quelqu'un", "Grimper sur un mât de drapeau", "Désamorcer une bombe", "Mettre un collier sur quelqu'un", "Enlever la croûte d'un sandwich", "Chat chassant un pointeur laser", "Oiseau caca sur quelqu'un", "Burping le bébé", "Tirer un canon à confettis", "Une fourchette dans la route", "Essayer de faire le poirier", "Trolls sous le pont", "Le sol est de la lave", "Méditation", "Parler avec la bouche pleine", "Concours de restauration à hot-dog", "Jouer à chercher avec un chien", "Mettre votre manteau sur le support", "Changer une ampoule", "Date de cinéma", "Le téléphone sonne pendant le cours", "Course de relais", "Marcher pieds nus sur l'herbe", "SMS pendant une date", "Stationnement parallèle", "Évacuation d'autobus scolaire", "Course d'obstacle", "rat de bibliothèque", "Le dur taco shell se désagrège en mangeant", "S'attaquer à un alligator", "Poignée de main secrète", "Robin des Bois", "Prendre pied dans le visage", "Chaussettes et sandales", "Cueillir des fleurs", "Marcher sur un lego", "Couper un buisson", "Laveur de vitres", "Lancer une voiture dans l'espace", "Double trempage", "Poinçons d'anniversaire", "Éternuer directement dans les yeux de quelqu'un", "Cireur de chaussures", "Peindre une palissade", "Donner vos légumes au chien", "Foule de surf", "Fermer la porte à votre doigt", "Singe volant quelque chose", "Poupée vaudou", "Tue mouche éléctrique", "Épingler la queue sur l'âne", "Mordre ta langue", "Frissons dans le froid", "Plombier réparer un tuyau", "Bascule tes chaussettes", "Taper sur quelqu'un sur l'épaule opposée à l'endroit où vous vous tenez", "L'observation des baleines", "Fort d'oreiller", "Frapper la fenêtre de votre voisin avec un ballon de baseball", "Renverser du café sur vous-même", "Lire l'esprit de quelqu'un", "Se gratter le dos", "Chat grattant le meuble", "Déjeuner d'école", "Concessionnaire", "Cartes dans votre manche", "Péter les aisselles", "Farce", "Danser sous la pluie", "Exercice d'incendie", "Escalade", "Cerf dans les phares", "Jeter un œil à l'examen d'un camarade de classe", "Fermer la porte à un vendeur", "La perruque tombe", "Tremblement de terre", "La monte de taureau", "Rassembler le bétail", "Cracher au visage de quelqu'un en parlant", "S'appuyant sur le visage d'une personne endormie", "Déchirer accidentellement votre pantalon", "Enfant se faisant passer pour un adulte", "Ignorer quelqu'un", "Électricité statique", "Sautant sur des pierres", "Les chats atterrissent toujours sur leurs pieds", "Faire signe à quelqu'un qui ne vous faisait pas signe", "Volume trop élevé", "Essayer de faire sourire un enfant sur une photo", "Spectacle de marionnettes", "Lit d'eau", "Les casques sauvent des vies", "Chercher à recevoir des compliments", "Voisin d'en haut piétinant", "Secouant un distributeur automatique", "Tricoter un pull", "Conte", "Appareil de téléportation", "Aller plus vite que la vitesse de la lumière", "Marcher sur la glace mince", "Langage des signes", "Pousser le visage de quelqu'un dans un gâteau", "J'ai ton nez", "Empilabler les cartons" },
        new string[] /*3 GERMAN*/{ "Präsidentenrede", "Festessen zum Erntedankfest", "Schmutzige Socken", "Zum ersten Mal Fahrrad fahren", "Blinder Mann, der zum ersten Mal sieht", "Sterne beobachten", "Mitternachtssnack", "Hosen anziehen vergessen", "Jemandem helfen, die Straße zu überqueren", "Ein Brettspiel spielen", "Verstecke dich und gehe suchen", "Neue Person in der Schule", "Ein Apfel pro Tag hält den Doktor fern", "Weinen über verschüttete Milch", "Melken einer Ziege", "Urlaub", "Holzfäller", "Zwillinge", "Hundetraining", "Stubenhocker", "Heroischer Feuerwehrmann", "Tanzwettbewerb", "Nehmen Sie Ihr Kind zum Arbeitstag", "Den Rasen mähen", "Sommer", "Superheld", "Trauriges Lied", "Hufeisen spielen", "Zuhause krank bleiben", "Wasserballonschlacht", "Hausarbeiten machen", "Lebensmittel einkaufen", "Übernachten", "Kobolde tanzen", "Karaoke", "Den Bus verpassen", "Familienfeier", "Nachrichtensprecher", "Buchstabier-Wettbewerb", "Monster unter dem Bett", "Hausaufgaben Entschuldigung", "Taschendiebstahl", "Bildung", "Tauziehen", "'Socke rein'", "Bester Freund", "Naturliebhaber", "Katze beim Baden", "Den Kuchen nehmen", "Etwas erfinden", "Spazieren gehen", "Ein Sandwich machen", "Schlüssel verlieren", "Fluss aus Tränen", "Albtraum", "1 Stern Restaurant", "Süßigkeiten von einem Baby nehmen", "Tauchen im Pool", "Schlittschuhlaufen", "Fangen spielen", "Vorschlag falsch", "Klassenclown", "Coole Typen sehen sich keine Exposionen an", "Reinigen der Katzentoilette", "Geh von meinem Rasen runter!", "Pizzalieferdienst", "Klassenpräsentation", "Schulaufführung", "Rodeln einen Hügel hinunter", "Pool-Spiele", "Jemanden fragen", "Die Katze ist aus dem Sack", "Im Lotto gewinnen", "Trampen", "Jubiläumsgeschenk", "Erster Tag bei der Arbeit", "Dem Chef auf die Nerven gehen", "Haustier vom Lehrer", "In einen Zombie verwandeln", "Überraschungsparty", "Ein Iglu bauen", "Wahre Liebe", "Das Popcorn verbrennen", "Müllmann", "Kunstauktion", "Der Atem geht aus", "Wild West Duell", "Hitzewelle", "Ausflug", "Völkerball", "In eine Falle gehen", "Auf frischer Tat ertappt", "In einer Schleife stecken", "Des einen Müll ist des anderen Schatz", "Bankraub", "Erster Mann auf dem Mars", "Ein Taxi rufen", "Tourist in einem fremden Land", "Golf spielen", "SMS an einen Freund", "Science Fair Project", "Eltern, die ihre Kinder in Verlegenheit bringen", "Zeig und sag", "Lautes Vakuum", "Autokino", "Möwen stehlen Essen", "In der Schlange auf Wassereis warten", "Wasserpark", "Anwenden von Sonnenschutzmitteln", "In der Dusche singen", "Vogelbaby verlässt das Nest", "Ohrenschmalz", "Ausrutschen auf einer Bananenschale", "Einen Baum pflanzen", "Kundenbeschwerde", "Einen Berg besteigen", "Einen Ballon draußen loslassen", "Familienfoto", "Bart rasieren", "Surfen", "Tritt auf einen Sprung, breche deiner Mutter den Rücken", "Grinch stiehlt Weihnachten", "Schwein in einer Decke", "Aschenputtel verliert ihren Schuh", "Telefon fallen lassen", "Jemanden in den Pool schieben", "Seiltänzer", "Zauberer", "Hund hat Angst vor einem Gewitter", "Drei kleine Schweine", "Auf einem Whoopie-Kissen sitzen", "Einen Gruselfilm gucken", "Einschlafen in der Klasse", "Eine Massage bekommen", "Wütend auf deinen Wecker", "Mit dem Mund einen Ballon sprengen", "Laut essen", "Stolpern an den Schnürsenkeln", "Versuch, deine Hand in eine Pringles-Dose zu stecken", "Bananen-Telefon", "Wett-Starren", "Camping vor einem Geschäft zum Verkauf", "Ein Geschäft am schwarzen Freitag", "Festnahme widerstehen", "Schrumpfstrahl", "Eine Limo nach dem Schütteln öffnen", "Teig in die Luft werfen", "Sinkendes Schiff", "Poison Ivy Rash", "Duck Duck Goose spielen", "Auf dem Foto blinken", "Chinesische Fingerfalle", "Zwei Stufen gleichzeitig hochgehen", "Machen Sie ein Chaos mit Ihrem Essen", "Verwenden des WiFi Ihres Nachbarn", "Einen Geist sehen", "Kissenschlacht", "Jemanden unterzeichnen", "Klettern einen Fahnenmast", "Eine Bombe entschärfen", "Jemandem eine Halskette anziehen", "Entfernen der Kruste von einem Sandwich", "Protestieren", "Katze jagt einen Laserpointer", "Rülpsen das Baby", "Springen in Pfützen", "Eine Konfettikanone abfeuern", "Eine Gabel in der Straße", "Versuch einen Handstand zu machen", "Trolle unter der Brücke", "Der Boden ist Lava", "Meditation", "Mit vollem Mund reden", "Hot Dog Esswettbewerb", "Fetch mit einem Hund spielen", "Zieh deinen Mantel an", "Glühbirne wechseln", "Kino Date", "Telefon klingelt während des Unterrichts", "Staffellauf", "Barfuß auf Gras laufen", "SMS während eines Datums", "Paralleles Parken", "Schulbus Evakuierung", "Hindernisstrecke", "Bücherwurm", "Harte Taco-Schale zerfällt beim Essen", "Gegen einen Alligator", "Geheimer Händedruck", "Ins Gesicht gescheckt werden", "Socken und Sandalen", "Draufgängerischer Stunt", "Blumen pflücken", "Auf einen Lego treten", "Schneiden eines Busches", "Murmeltier sieht seinen Schatten", "Fensterputzer", "Ein Auto ins All bringen", "Doppeltauchen", "Jemandem direkt in die Augen niesen", "Schuhputzer", "Malen eines Palisadenzauns", "Gib dem Hund dein Gemüse", "Schließ die Tür an deinem Finger", "Affe, der etwas stiehlt", "Voodoo-Puppe", "Steck den Schwanz auf den Esel", "Beißen Sie Ihre Zunge", "Zittern in der Kälte", "Klempner, der ein Rohr repariert", "Jemanden auf die Schulter klopfen, der sich gegenüber befindet, wo du stehst", "Walbeobachtung", "Kissen Festung", "Schlagen Sie mit einem Baseball auf das Fenster Ihres Nachbarn", "Kaffee auf sich selbst verschütten", "Jemandes Gedanken lesen", "Kratz dir den Rücken", "Kratzmöbel für Katzen", "Schuljause", "Autohändler", "Karten im Ärmel", "Achsel furzen", "Scherzanruf", "Im Regen tanzen", "In Treibsand stecken", "Feuerübung", "Felsklettern", "Hirsch im Scheinwerferlicht", "Spähen bei einem Klassenkameraden Test", "Einem Verkäufer die Tür schließen", "Perücke fällt ab", "Himmelsleiter", "Erdbeben", "Bullenreiten", "Rinder aufrunden", "Jemandem beim Sprechen ins Gesicht spucken", "Zeichnen auf das Gesicht einer schlafenden Person", "Zerreißen Sie versehentlich Ihre Hosen", "Kind, das vorgibt, ein Erwachsener zu sein", "Jemanden ignorieren", "Statische Elektrizität", "Katzen landen immer auf ihren Füßen", "Jemandem zuwinken, der nicht auf dich gewinkt hat", "Lautstärke zu hoch", "Tag einfrieren", "Der Versuch, ein Kind auf einem Foto zum Lächeln zu bringen", "Puppenspiel", "Wasserbett", "Schutzhelme retten Leben", "Auf Komplimente aus sein", "Schütteln eines Automaten", "Einen Pullover stricken", "Gute Nacht Geschichte", "Teleportationsgerät", "Schneller als die Lichtgeschwindigkeit", "Gehen auf dünnem Eis", "Zeichensprache", "Jemandes Gesicht in einen Kuchen schieben", "Ich habe deine Nase", "Boxen stapeln" },
        new string[] /*4 ITALIAN*/{ "Discorso Presidenziale", "Cena del Ringraziamento", "Rabbia Al Volante", "Calzini Sporchi", "Martedì Taco", "La Prima Volta Su Una Bicicletta", "Un Cieco Vede Per La Prima Volta", "Giocatore Di Bowling Professionista", "Compiti A Casa Fatti All'Ultimo Minuto", "Intervista su Skype", "Guardare Le Stelle", "Spuntino di Mezzanotte", "Dimenticarsi di Mettere i Pantaloni", "Aiutare Qualcuno ad Attraversare la Strada", "Bussare Per Scherzo", "Giocare Ad Un Gioco Da Tavolo", "Nascondino", "Pettegolezzi Scolastici", "Nuovo Studente", "Una Mela Al Giorno Toglie Il Medico Di Torno", "Piangere Sul Latte Versato", "Mungere Una Capra", "Girare Su Un Carro", "Vacanza", "Lumberjack", "Gemelli", "Addrestrare Il Cane", "Pantofolaio", "Pompiere Eroico", "Giocare Con L'Hoola Hoop", "Battaglia Di Ballo", "Portare I Figli Al Lavoro", "Falciare Il Prato", "Estate", "Supereroe", "Canzone Triste", "Giocare Coi Ferri Di Cavallo", "Stare A Casa Ammalati", "Guerra Coi Gavettoni","Fare I Mestieri", "Fare La Spesa", "Dormire Da Qualcuno", "Fuori Campo", "Danza Del Leprecauno", "Karaoke", "Perdere Il Bus", "Festa Di Famiglia", "Giornalista", "Gara Di Spelling", "Mostro Sotto Il Letto", "Scusa Per I Compiti a Casa", "Scippo", "Titolo Di Studio", "Cartoni Animati", "Tiro Alla Fune", "Giocare Col Frisbee", "'Stai Zitto'", "Auto Commerciale", "Sbattere Il Mignolino", "Migliore Amico", "Amante Della Natura", "Fare Il Bagno Al Gatto", "Portare La Torta", "Inventare Qualcosa", "Passeggiare", "Fare Un Panino", "Perdere Le Chiavi", "Fiume Di Lacrime", "Incubo", "Test Del Gusto", "Appuntamento Al Buio", "Buffet All-You-Can-Eat", "Ristorante A 1 Stella", "Rubare Una Caramella Ad Un Bambino", "Ombrello Rotto", "Bambino Popolare", "Immersioni In Piscina", "Pattinaggio Sul Ghiaccio", "Giocare Ad Acchiapparello", "Lancio a Pallavolo", "Proposta Andata Male", "Buffone Della Classe", "I Bravi Ragazzi Non Badano Alle Apparenze", "Pulire La Lettiera", "'Vai Fuori Dai Piedi!'", "Consegna Pizza", "Dimostrazione In Classe", "Recita Scolastica", "Fare Slittino Giù Da Una Collina", "Giocare A Biliardo", "Chiedere A Qualcuno Di Uscire", "'Vuotare Il Sacco'", "Acrobazia con lo Skateboard", "Vincere Alla Lotteria", "Autostop", "Regalo Di Anniversario", "Primo Giorno Di Lavoro", "Fare Il Lecchino", "Il Cocco Dell'Insegnante", "Trasformarsi In Uno zombi", "Festa A Sorpresa", "Costruire Un Igloo", "Vero Amore", "Bruciare I Popcorn", "Spazzino", "Asta D'Opere D'Arte", "Parata", "Prima Volta In Aereo", "Essere Quasi Senza Fiato", "Duello Nel Selvaggio West", "Ondata Di Caldo", "Capo Dispotico", "Viaggio Su Strada", "Dodgeball", "Cadere In Trappola", "Essere Colti Con Le Mani Nel Sacco", "Essere Bloccati In Un Circolo Vizioso", "La Spazzatura Di Qualcuno è L'oro Di Qualcun Altro", "Rapina In Banca", "Primo Uomo Su Marte", "Chiamare Un Taxi", "Salto A Gambe Divaricate", "Turista In Un Paese Straniero", "Giocare A Golf", "Bulletto Della Scuola", "Mandare Un Messaggio Ad Un Amico", "Progetto Per La Fiera Della Scienza", "Genitori Che Mettono In Imbarazzo I Figli", "Mostra e Racconta", "Aspirapolvere Rumoroso", "Drive-In", "Viaggio a Disneyland", "Gabbiani Che Rubano Il Cibo", "Fare La Coda Per Il Ghiaccio", "Fiume Che Scorre Lento", "Parco Acquatico", "Mettere La Protezione Solare", "Cantare Sotto La Doccia", "Uccellino Che Lascia Il Nido", "Cerume", "Scivolare Su Una Buccia Di Banana", "Piantare Un Albero", "Lamentela Del Cliente", "Scalare Una Montagna", "Lasciar Volare Un Palloncino", "Foto Di Famiglia", "Farsi La Barba", "Fare Surf", "Pesta Una Crepa, Tua Madre E' Una Strega", "Il Grinch Ruba Il Natale", "Salatini Ai Wurstel", "Cenerentola Che Perde La Scarpetta", "Far Cadere Il Telefono", "Spingere Qualcuno Nella Piscina", "Funambolo", "Mago", "Cane Terrorizzato Dal Temporale", "I Tre Porcellini", "Sedersi Su Un Cuscino Spernacchiante", "Guardare Un Film Che Fa Paura", "Addormentarsi In Classe", "Farsi Fare Un Massaggio", "Terrorizzarsi Al Suono Della Sveglia", "Gonfiare Un Palloncino", "Mangiare Rumorosamente", "Inciampare Sui Propri Lacci", "Far Entrare La Mano Nel Tubo Delle Pringles", "Telefono A Forma Di Banana", "Guardare Una Gara", "Accamparsi Fuori Da Un Negozio Per Una Vendita", "Un Negozio Durante Il Black Friday", "Resistenza All'Arresto", "Raggio Restringente", "Aprire Una Lattina Dopo Averla Agitata", "Saltare La Pasta", "Nave Che Affonda", "Rash Cutaneo da Edera Velenosa", "Giocare Alla Baia Dell'Oca", "Avere Gli Occhi Chiusi In Una Foto", "Trappola Per Dita Cinese", "Salire Due Scalini Alla Volta", "Fare Un Disastro In Cucina", "Usare Del WiFi Del Vicino", "Salvataggio Del Portiere", "Vedere Un Fantasma", "Spremere Il Limone", "Guerra Dei Cuscini", "Firmare Al Posto Di Qualcuno", "Arrampicarsi Su Un Palo", "Disinnescare Una Bomba", "Mettere Una Collana A Qualcuno", "Togliere La Crosta A Un Panino", "Protestare", "Gatto Che Insegue Un Puntatore Laser", "Uccello Che Fa La Cacca Su Qualcuno", "Far Fare Un Ruttino Ad Un Bambino", "Saltare Nelle Pozzanghere", "Sparare Con Un Cannone Di Coriandoli", "Bivio Stradale", "Assemblare Un Nuovo PC", "Cercare Di Fare Una Verticale", "Troll Sotto Un Ponte", "Pavimento Di Lava", "Meditazione", "Parlare Con La Bocca Piena", "Gara di Mangiatori di Hot Dog", "Giocare Con Un Cane Che Prende Un Bastone", "Mettere Il Cappotto Sull'attaccapanni", "Cambiare Una Lampadina", "Appuntamento Al Cinema", "Il Telefono Che Squilla Durante La Lezione", "Staffetta", "Camminare A Piedi Nudi Sull'Erba", "Messaggio Durante Un Appuntamento", "Cheerleader Che Cade", "Parcheggio Parallelo", "Evacuazione Scuolabus", "Percorso A ostacoli", "Topo Di Biblioteca", "Un Taco Che Si Rompe Mentre Lo Mangi", "Affrontare Un Alligatore", "Accordo Segreto", "Robin Hood", "Farsi Mettere I Piedi In Testa", "Calzini e Sandali", "Stuntman", "Raccogliere Fiori", "Calpestare Il Lego", "Tagliare Una Siepe", "Marmotta Che Vede La Sua ombra", "Lavavetri", "Lanciare Un'Auto Nello Spazio", "Minestra Riscaldata", "Starnutire In Faccia A Qualcuno", "Lustrascarpe", "Dipingere Una Staccionata", "Dare Le Verdure Al Cane", "Fare Surf Sulla Folla", "Chiudersi Il Dito Nella Porta", "Una Scimmia Che Ruba Qualcosa", "Bambola Voodoo", "Lampada Insetticida", "Fare La Break Dance", "Giocare Ad Attacca La Coda All'Asino", "Mordersi La Lingua", "Brividi Di Freddo", "Idraulico Che Ripara Un Tubo", "Sbarellare", "Toccare Qualcuno Sulla Spalla Opposta A Dove Stai", "Guardare Le Balene", "Fortino Di Cuscini", "Colpire La Finestra Del Vicino Con Una Palla Da Baseball", "Rovesciarsi Il Caffè Addosso", "Leggere Nella Mente Di Qualcuno", "Grattarsi La Schiena", "Gatto Che Graffia I Mobili", "Pranzo a Scuola", "Rivenditore D'Auto", "Avere Un Asso Nella Manica", "Fare Le Scoreggie Con L'Ascella", "Scherzo Telefonico", "Ballare Sotto La Pioggia", "Essere Bloccato Nelle Sabbie Mobili", "Esercitazione Antincendio", "Scalare Una Montagna", "Cervo Illuminato Dai Fari", "Dare Un'Occhiata AlLa Prova Di Un Compagno Di Classe", "Chiudere La Porta In Faccia Ad Un Venditore Porta A Porta", "Parrucca Che Cade", "Scalinata Verso Il Cielo", "Terremoto", "Cavalcare Un Toro", "Radunare Una Mandria", "Sputare In Faccia A Qualcuno Mentre Parli", "Disegnare Sulla Faccia Di Una Persona Che Dorme", "Strappare Per Errore i pantaloni", "Bambino Che Si Atteggia da Adulto", "Ignorare Qualcuno", "Elettricità Statica", "Lanciare Un Sasso", "I Gatti Atterrano Sempre Sulle Zampe", "Salutare Qualcuno Che Non Sta Salutando Te", "Volume Troppo Alto", "Giocare Alle Belle Statuine", "Cercare Di Far Sorridere Un Bambino In Una Fotografia", "Spettacolo Di Marionette", "Letto Ad Acqua", "Gli Elmetti Salvano Vite", "Cercare Complimenti", "Il Vicino Del Piano Di Sopra Che Cammina Con Passo Pesante", "Scuotere Un Distributore Automatico", "Fare Un Maglione Di Lana", "Raccontare La Favola Della Buonanotte", "Macchina Per Il Teletrasporto", "Andare Più Veloci Della Luce", "Giocare Col Fuoco", "Usare Il Linguaggio Dei Segni", "Spingere La Faccia Di Qualcuno In Una Torta", "'Ti Ho Preso Il Naso'", "Scatole Impilabili" },
        new string[] /*5 PORTUGUESE*/{ "Discurso presidencial", "Jantar de Dia de Ação de Graças", "Fúria na estrada", "Meias sujas", "Terça-feira de tacos", "Primeira vez a andar de bicicleta", "Homem cego a ver pela primeira vez", "Jogador de bowling profissional", "Fazer os trabalhos de casa à última hora", "Entrevista por Skype", "Veras estrelas", "Petisco à meia-noite", "Esquecer de vestir calças", "Ajudar alguém a atravessar a estrada", "Piada de bater à porta", "Jogar um jogo de tabuleiro", "Esconder e procurar", "Fofoca de secundário", "Pessoa nova na escola", "Uma maçã por dia nem sabes o bem que te fazia", "Chorar sobre leite derramado", "Ordenhar uma cabra", "Passeio hayrack", "Férias", "Lenhador", "Gémeos", "Treino de cães", "Sedentário", "Bombeiro heróico", "Fazer hoola hoop", "Batalha de dança", "Dia de levar os filhos para o trabalho", "Cortar a relva", "Verão", "Super-herói", "Canção triste", "Jogar ao jogo da ferradura", "Ficar em casa por doença", "Guerra de balões de água", "Fazer tarefas domésticas", "Compras de mercearia", "Dormir em casa de amigos", "Home Run", "Duendes a dançar", "Karaoke", "Perder o autocarro", "Festa de família", "Apresentador de noticiário", "Soletrar", "Monstro debaixo da cama", "Desculpa para não fazer os trabalhos de casa", "Furtar", "Educação", "Desenhos animados", "Pôr-se com rodeios", "Cabo de guerra", "Golfe de disco", "’Cala a matraca’", "Anúncio de carros", "Bater com o dedo do pé", "Melhor amigo", "Amante da natureza", "Gato a tomar banho", "Ser o fim da picada", "Inventar algo", "Dar um passeio", "Fazer uma sanduíche", "Perder as chaves", "Mar de lágrimas", "Pesadelo", "Exame organolético", "Encontro às cegas", "Bufê livre", "Restaurante de 1 estrela", "Tirar o doce à criança", "Guarda-chuva partido", "Miúdo popular", "Mergulhar na piscina", "Patinar no Gelo", "Jogar à apanhada", "Remate de voleibol em suspensão", "Pedido de casamento que deu para o torto", "Palhaço da turma", "Gajos fixes não olham para as explosões", "Limpar a caixa de areia", "’Sai do meu jardim!’", "Entrega de pizza", "Apresentação na aula", "Teatro da escola", "Descer colina de tronó", "Jogos de piscina", "Convidar alguém para sair", "’Gato escondido com o rabo de fora’", "Truque de skateboarding", "Ganhar a lotaria", "Andar à boleia", "Presente de aniversário", "Primeiro dia de trabalho", "Lamber as botas ao patrão", "O queridinho do professor", "Transformar-se num zombie", "Festa surpresa", "Construir um iglô", "Amor verdadeiro", "Queimar a pipoca", "Homem do lixo", "Leilão de arte", "Parada", "Primeiro voo de avião", "Ficar sem fôlego", "Duelo do Oeste Selvagem", "Onda de calor", "Chef mandão", "Viagem por estrada", "Jogo do mata", "Cair na armadilha", "Apanhado com a boca na botija", "Ficar preso num ciclo", "O lixo de um homem é o tesouro de outro", "Assalto a um banco", "Primeiro homem em Marte", "Chamar um táxi", "Saltos de tesoura", "Turista num país estrangeiro", "Jogar golfe", "Agressor escolar", "Enviar mensagem a amigo", "Projeto de feira de ciência", "Pais a envergonhar os filhos", "Mostre e conte", "Aspirador barulhento", "Cinema ao ar livre", "Cruzeiro Disney", "Gaivotas a roubar comida", "À espera na fila para o gelo", "Rio lento", "Parque aquático", "Colocar protetor solar", "Cantar no chuveiro", "Passarinho bebé a sair do ninho", "Cera dos ouvidos", "Escorregar numa casca de banana", "Plantar uma árvore", "Cliente a reclamar", "Subir uma montanha", "Soltar um balão no exterior", "Fotografia de família", "Fazer a barba", "Surfar", "Pisa a rachadura e parte as costas da tua mãe", "Grinch a roubar o Natal", "Enroladinho de salsicha", "Cinderela a perder o sapatinho", "Deixares cair o telefone", "Empurrar alguém para a piscina", "Equilibrista", "Mágico", "Cão com medo de trovoada", "Três porquinhos", "Sentar numa almofada de flatulência", "Ver um filme de terror", "Adormecer durante a aula", "Receber uma massagem", "Irritar-se com o despertador", "Encher um balão com a boca", "Fazer barulho a comer", "Tropeçar nos cordões", "Tentar enfiar a mão numa lata de Pringles", "Telefone banana", "Jogar ao sério", "Acampar à porta de uma loja por uma promoção", "Loja em dia de Black Friday", "Resistir a detenção", "Raio redutor", "Abrir uma lata de refrigerante depois de a agitar", "Atirar massa ao ar", "Navio a afundar", "Erupção cutânea por hera venenosa", "Jogar ao jogo do lencinho", "Pestanejar na fotografia", "Armadilha de dedo chinesa", "Subir duas escadas de cada vez", "Sujar tudo com a comida", "Usar o WiFi do vizinho", "Defesa de guarda-redes", "Ver um fantasma", "Espremer o limão", "Guerra de almofadas", "Assinar o gesso de alguém", "Subir ao mastro de uma bandeira", "Desativar uma bomba", "Pôr o laço no pescoço de alguém", "Remover a crosta de uma sanduíche", "Protestar", "Gato a perseguir um laser", "Pássaro a fazer cocó em alguém", "Pôr o bebé a arrotar", "Saltar em poças", "Disparar um canhão de confetes", "Uma encruzilhada", "Construir um PC novo", "Tentar fazer o pino", "Trolls debaixo da ponte", "O chão é lava", "Meditação", "Falar com a boca cheia", "Concurso de comer cachorros-quentes", "Brincar ao busca com um cão", "Colocar o casaco no cabide", "Mudar uma lâmpada", "Ida ao cinema", "Telefone a tocar durante a aula", "Corrida de estafetas", "Andar descalço na relva", "Enviar mensagens durante um encontro", "Cheerleader a cair", "Estacionamento paralelo", "Evacuação de autocarro", "Pista de obstáculos", "Rato de biblioteca", "Taco a desmoronar-se quando come", "Combater um crocodilo", "Aperto de mão secreto", "Robin Hood", "Levar com uma tarte na cara", "Meias e sandálias", "Proeza arrojada", "Colher flores", "Pisar um Lego", "Aparar um arbusto", "Marmota a ver a sombra", "Limpador de janelas", "Lançar um carro para o espaço", "Comissão dupla", "Socos de aniversário", "Espirrar diretamente para os olhos de alguém", "Engraxador de sapatos", "Pintar uma cerca de estacas", "Dar os vegetais ao cão", "Crowd Surfing", "Entalar o dedo na porta", "Macaco a roubar alguma coisa", "Boneca de voodoo", "Eletrocutador de insetos", "Fazer breakdance", "Jogo de colocar a cauda do burro", "Morder a língua", "Tremer com o frio", "Canalizador a reparar um cano", "Ficar de queixo caído", "Tocar no ombro de alguém do lado oposto onde estás", "Ver baleias", "Fortaleza de almofadas", "Acerta com a bola de beisebol na janela do vizinho", "Entornar o café sobre ti mesmo", "Ler a mente de alguém", "Coçar as costas", "Gato a arranhar os móveis", "Almoço da escola", "Concessionário de carros", "Ter um truque na manga", "Dar um pum com o sovaco", "Partida telefónica", "Dançar à chuva", "Preso nas areias movediças", "Simulacro de incêndio", "Escalada em rocha", "Ser apanhado de surpresa", "Espreitar para o teste do colega", "Bater com a porta na cara de um vendedor", "Peruca a cair", "Escada para o céu", "Terramoto", "Andar de touro", "Recolha do gado", "Cuspir na cara de alguém quando se fala", "Desenhar na cara de uma pessoa que está a dormir", "Rasgar as calças por acidente", "Criança a fingir ser adulto", "Ignorar alguém", "Eletricidade estática", "Fazer saltar pedras na água", "Os gatos caem sempre de pé", "Acenar a alguém que não estava a acenar para ti", "Volume demasiado alto", "Jogo da estátua", "Tentar fazer uma criança rir para uma fotografia", "Espetáculo de marionetas", "Cama de água", "Capacetes salvam vidas", "Andar à pesca de elogios", "Vizinhos de cima a bater com os pés", "Abanar uma máquina de venda automática", "Tricotar uma camisola", "História de embalar", "Dispositivo de teletransporte", "Ir mais rápido do que a velocidade da luz", "Andar a pisar ovos", "Língua gestual", "Empurrar a cara de alguém contra o bolo", "’Roubei-te o nariz’", "Empilhar caixas" },
        new string[] /*6 SPANISH*/{ "Discurso Presidencial", "Cena de Acción de Gracias", "Ira de Carretera", "Medias Sucias", " Martes de Tacos", "Primera Vez Montando una Bicicleta", "Hombre Ciego Viendo por Primera Vez", "Jugador de Bolos Profesional", "Tarea de Último Minuto", "Entrevista por Skype", "Estudio de las Estrellas", "Aperitivo de Media Noche", "Olvidé Ponerme los Pantalones", "Ayudando a Alguien a Cruzar la Calle", "Broma Toc Toc", "Jugando un Juego de Mesa", "Las Escondidas", "Chisme de Secundaria", "Nueva Persona en la Escuela", "Una Manzana al Día Mantiene al Médico Alejado", "Llorar Sobre la Leche Derramada", "Ordeñando una Cabra", "Paseo en Carreta", "Vacaciones", "Leñador", "Gemelos", "Perro Entrenando", "Flojo", "Bombero Heroico", "Hula Hula", "Batalla de Baile", "Día de Llevar a tu Hijo al Trabajo", "Podando el césped", "Verano", "Superhéroe", "Canción Triste", "Herradura", "Quedarse en Casa Enfermo", "Guerra de Globos de Agua", "Hacer los Quehaceres", "Comprar Comestibles", "Pijamada", "Home Run", "Duendes Bailando", "Karaoke", "Perdiendo el Autobús", "Fiesta en Familia", "Presentador de Noticias", "Abeja de Ortografía", "Monstruo Debajo de la Cama", "Excusa para la Tarea", "Robar", "Educación", "Dibujos Animados", "Andar con Rodeos", "Tira y Afloja", "Golf con Disco", "Comercial de Coches", "Golpearse el Dedo del Pie", "Mejor Amigo", "Amante de la Naturaleza", "Gato Bañándose", "Inventando Algo", "Saliendo a Caminar", "Haciendo un Emparedado", "Perdiendo tus Llaves", "Río de Lágrimas", "Pesadilla", "Prueba de Sabor", "Cita a Ciegas", "Buffet Todo lo que Puedas Comer", "Restaurante de 1 Estrella", "Robar un Dulce de un Niño", "Sombrilla Rota", "Niño Popular", "Buceando en la Piscina", "Patinaje sobre Hielo", "Jugando a las Persecuciones", "Remate de Voleibol", "Propuesta de Matrimonio Rechazada", "Payaso de la Clase", "Chicos Geniales No Miran Explosiones", "Limpiando la Caja de Arena", "¡Fuera de mi Jardín!", "Entrega de Pizza", "Presentación para la Clase", "Juego Escolar", "Deslizarse por la Colina", "Juegos de Piscina", "Pedirle a Alguien una Cita", "El Gato Está Fuera de la Bolsa", "Truco de Patineta", "Ganando la Lotería", "Autoestop", "Regalo de Aniversario", "Primer Día en el Trabajo", "Succionando las Medias del Jefe", "Mascota del Maestro", "Convirtiéndose en Zombi", "Fiesta Sorpresa", "Creando un Iglú", "Amor Verdadero", "Quemando las Palomitas de Maíz", "Hombre de la Basura", "Subasta de Arte", "Desfile", "Primer Vuelo en Avión", "Quedarse sin Aliento", "Duelo del Antiguo Oeste", "Ola de Calor", "Chef Mandón", "Viaje de Carretera", "Balón Prisionero", "Caminando hacia una Trampa", "Atrapado con las Manos en la Masa", "Atrapado en un Ciclo", "La Basura de un Hombre es el Tesoro de Otro", "Robo de un Banco", "Primer Hombre en Marte", "Llamando a un Taxi", "Salto de Estrella", "Turista en un País Extranjero", "Jugando Golf", "Abusón de la Escuela", "Escribiendo a un Amigo", "Proyecto de la Feria de Ciencias", "Padres Avergonzando a sus Hijos", "Mostrar y Contar", "Aspiradora Ruidosa", "Autocinema", "Crucero de Disney", "Gaviotas Robando Comida", "Esperando en la Cola para el Helado", "Río Lento", "Parque Acuático", "Aplicando Bloqueador Solar", "Cantando en la Ducha", "Bebé Pájaro Dejando el Nido", "Cera de Oídos", "Tropezándose con una Concha de Banana", "Plantando un Árbol", "Cliente Quejándose", "Subiendo una Montaña", "Soltando un Globo al Exterior", "Foto Familiar", "Afeitando la Barba", "Surfeando", "Grinch Robándose la Navidad", "Cerdo en una Manta", "Cenicienta Perdiendo su Zapato", "Dejando Caer tu Teléfono", "Empujar a Alguien a la Piscina", "Funambulista", "Mago", "Perro Asustado de los Rayos", "Los Tres Cerditos", "Sentado en un Cojín de Bromas", "Mirando una Película de Miedo", "Durmiendo en Clase", "Recibiendo un Mensaje", "Molestándose con la Alarma de la Mañana", "Usar tu Boca para Inflar un Globo", "Comiendo con la Boca Abierta", "Tropezarse con las Trenzas de los Zapatos", "Intentar Meter tu Mano en una Lata de Pringles", "Concurso de Miradas", "Acampando a las Afueras de una Tienda para una Venta", "Una Tienda en Viernes Negro","Resistirse al Arresto","Rayo Reductor","Abrir una Soda Luego de Agitarla","Lanzando una Masa de Pizza al Aire","Barco Hundiéndose","Alergia a la Hiedra Venenosa","Jugando Pato Pato Ganso","Parpadeando en una Foto","Trampa de Dedos China","Subiendo Hasta Dos Escalones a la Vez","Haciendo un Desastre con la Comida","Usando el WiFi del Vecino","Parada en Portería","Ver un Fantasma","Exprimir un Limón","Pelea de Almohadas","Subiendo el Asta de una Bandera","Desactivando una Bomba","Colocándole un Collar a Alguien", "Remover la Corteza de un Emparedado", "Protestar", "Gato Persiguiendo un Puntero Láser", "Ave Defecando en Alguien", "Bebé Heructando", "Saltando en Charcos", "Disparando un Cañón de Confeti", "Un Desvío en el Camino", "Intentar Pararse de Manos", "Troles Debajo del Puente", "El Suelo es Lava", "Meditación", "Hablando con la Boca Llena", "Concurso de Comer Perros Calientes", "Jugando a la Pelota con un Perro", "Colocando una Chaqueta en el Perchero", "Cambiar un Bombillo", "Cita en el Cine", "Teléfono Sonando en Clase", "Carrera de Relevos", "Caminando Descalzo en la Grama", "Mandando Mensajes de Texto Durante una Cita", "Candelabros Cayendo", "Estacionamiento en Paralelo", "Evacuación de Bus Escolar", "Pista de Obstáculos", "Gusano Marrón", "Corteza de un Taco Cayendo Mientras Comes", "Peleando con un Cocodrilo", "Saludo Secreto", "Robin Hood", "Recibir un Pie en la Cara", "Medias y Sandalias", "Doble de Riesgo", "Recoger Flores", "Pararse en un Lego", "Cortando un Arbusto", "Marmota Viendo su Sombra", "Limpiaventanas", "Lanzando Carro al Espacio", "Golpes de Cumpleaños", "Estornudar Directamente en los Ojos de Alguien", "Comerse un Zapato", "Pintando una Cerca", "Darle los Vegetales al Perro", "Surfear la Multitud", "Cerrar la Puerta en tus Dedos", "Mono Robando Algo", "Muñeca Voodoo", "Mata Insectos", "Breakdance", "Ponle la Cola al Burro", "Morder tu Lengua", "Temblar en el Frío", "Plomero Arreglando una Tubería","Tocar el Hombro de Alguien al Contrario de Donde te Encuentras","Mirar Ballenas", "Fuerte de Almohadas", "Romper la Ventana del Vecino con una Pelota de Beisbol", "Derramarte el Café Encima", "Leer la Mente de Alguien", "Rascarte la Espalda", "Gato Arañando los Muebles", "Almuerzo Escolar", "Vendedor de Autos", "Cartas Bajo la Manga", "Gases con la Axila", "Llamada de Bromas", "Bailando Bajo la Lluvia", "Atrapado en Arenas Movedizas", "Simulacro de Incendios", "Escalar Rocas", "Ver la Prueba de un Compañero de Clase", "Cerrarle la Puerta a un Vendedor", "Peluca Cayéndose", "Escaleras Hacia el Cielo", "Terremoto", "Montar un Toro", "Castillo Inflable", "Escupirle a Alguien en la Cara Mientras Hablas", "Dibujar en la Cara de una Persona Durmiendo", "Romper tus Pantalones Accidentalmente", "Niño Pretendiendo Ser un Adulto", "Ignorando a Alguien", "Electricidad Estática", "Cabrillas", "Los Gatos Siempre Caen de Pie", "Saludar a Alguien que no te Saluda", "Volumen Muy Alto", "Toque Congelado", "Intentando Hacer que un Niño Sonría en una Foto", "Show de Marionetas", "Cama de Agua", "Los Cascos Salvan Vidas", "Buscar Recibir Cumplidos", "Vecino del Piso de Arriba Haciendo Ruido", "Máquina Expendedora de Snacks", "Tejer un Sweater", "Historia para ir a Dormir", "Dispositivo de Teletransportación", "Ir Más Rápido que la Velocidad de la Luz", "Caminando sobre Hielo Fino", "Lenguaje de Señas", "Empujar la Cara de Alguien a una Torta", "Tengo tu nariz", "Apilar Cajas" },

    };

    string[][] randomDrawIntroArray =
    {
        new string[] /*0 ENGLISH*/{ "Time to draw something amazing, or maybe awful.", "Don't know how to draw something? Too bad!", "Use a variety of colors to bring the prompt to life.", "Time to scribble something down and call it art.", "Sharpen those imaginary pencils, and get ready to draw!", "Everything is beautiful in its own way, but feel free to prove me wrong."},
        new string[] /*1 DUTCH*/{"Tijd om iets geweldigs te tekenen, of misschien iets heel lelijks.", "Weet je niet hoe je iets moet tekenen? Jammer dan!","Gebruik verschillende kleuren om het leven te brengen.","Tijd om iets te krabbelen en het kunst te noemen.","Scherp die denkbeeldige potloden aan, en bereid je voor om te gaan tekenen!","Alles is mooi op zijn eigen manier, maar voel je vrij om te bewijzen dat ik het mis heb."},
        new string[] /*2 FRENCH*/{"Il est temps de dessiner quelque chose d'incroyable ou peut-être affreux.", "Je ne sais pas comment dessiner quelque chose? Dommage!", "Utilisez une variété de couleurs pour donner vie à l'invite.", "Il est temps de griffonner quelque chose et de parler d'art.", "Aiguisez ces crayons imaginaires et préparez-vous à dessiner!","Tout est beau à sa manière, mais n'hésitez pas à me prouver le contraire."},
        new string[] /*3 GERMAN*/{"Zeit, etwas Erstaunliches oder vielleicht Schreckliches zu zeichnen.", "Weiß nicht, wie man etwas zeichnet? Schade!","Verwenden Sie eine Vielzahl von Farben, um die Aufforderung zum Leben zu erwecken.","Zeit, etwas aufzuschreiben und es Kunst zu nennen.","Spitzen Sie diese imaginären Stifte an und machen Sie sich zum Zeichnen bereit!","Alles ist auf seine eigene Weise schön, aber ich kann mich jederzeit als falsch erweisen." },
        new string[] /*4 ITALIAN*/{ "È tempo di disegnare qualcosa di straordinario, o di terribile.", "Non sai come disegnare una cosa? Male!" , "Usa una più colori per dare vita al suggerimento." , "È tempo di scarabocchiare qualcosa e chiamarlo arte." , "Affila le matite immaginarie e preparati a disegnare!" , "Ogni cosa ha la sua bellezza, ma dimostrami che mi sbaglio." },
        new string[] /*5 PORTUGUESE*/{"É hora de desenhar algo incrível, ou talvez horrível.", "Não sabe como desenhar? Que pena!", "Utilize uma variedade de cores para dar vida ao seu palpite.", "É hora de fazer uns rabiscos e chamar-lhes arte.", "Aguce esses lápis imaginários, e prepare-se para desenhar!", "Tudo é belo à sua maneira, mas esteja à vontade para provar que estou errado."},
        new string[] /*6 SPANISH*/{ "Es tiempo de dibujar algo asombroso, o tal vez algo terrible." , "¿No sabes cómo dibujar algo? ¡Qué mal!" , "Usa una variedad de colores para hacer que tus dibujos tengan vida" , "Tiempo para garabatear algo y llamarlo arte" , "Afila tus lápices imaginarios y alístate para dibujar" , "Todo es hermoso de su manera, pero puedes demostrar que me equivoco." }
    };
    string[][] randomGuessIntroArray =
    {
        new string[] /*0 ENGLISH*/{ "Get ready for utter confusion!", "'Throwing Up Spaghetti?' 'Sandwich With Eyes?'", "Even if you know you're wrong, take a guess.", "Don't feel bad if you know your guess is wrong.", "If your guess is right, I'll give you a cookie.", "If you don't know what it is, describe the drawing."},
        new string[] /*1 DUTCH*/{"Bereid je voor op totale verwarring","'Spaghetti gooien?' 'Broodje met ogen?'", "Zelfs als je weet dat je het mis hebt, mag je raden.","Voel je niet slecht als je weet dat je het fout hebt geraden.","Als je het goed hebt geraden, geef ik je een koekje.","Als je niet weet wat het is kun je de tekening beschrijven."},
        new string[] /*2 FRENCH*/{"Préparez-vous à une confusion totale!","'Jeter des spaghettis?' 'Sandwich aux yeux?' ","Même si vous savez que vous avez tort, devinez.","Ne vous sentez pas mal si vous savez que votre proposition est fausse.","Si votre proposition est exacte, je vous donnerai un cookie.","Si vous ne savez pas ce que c'est, décrivez le dessin."},
        new string[] /*3 GERMAN*/{"Machen Sie sich bereit für völlige Verwirrung!","Spaghetti kotzen? Sandwich mit Augen?","Auch wenn Sie wissen, dass Sie sich irren, raten Sie mal.","Fühle dich nicht schlecht, wenn du weißt, dass deine Vermutung falsch ist.","Wenn Ihre Vermutung richtig ist, gebe ich Ihnen einen Keks.","Wenn Sie nicht wissen, was es ist, beschreiben Sie die Zeichnung."},
        new string[] /*4 ITALIAN*/{ "Preparati alla confusione totale!", "'Stai Buttando La Pasta?' 'Stai Facendo Dei Panini Con Gli Occhi?' ", "Anche se pensi di sbagliare, prova ad indovinare.", "Non rimanerci male se la tua ipotesi è errata.", "Se la tua ipotesi è giusta, ti darò un biscotto.", "Se non sai cos'è, descrivi il disegno."},
        new string[] /*5 PORTUGUESE*/{"Prepare-se para uma grande confusão!", "’Vomitar esparguete?’ ‘Sanduíche com olhos?’", "Dê um palpite, mesmo sabendo que está errado.", "Não se sinta mal se souber que o seu palpite está errado.", "Se o palpite estiver certo, vou dar-lhe uma bolacha.", "Se não sabe o que é, descreva o desenho."},
        new string[] /*6 SPANISH*/{ "Prepárate para la confusión total" , "'¿Un Espagueti Vomitando? '¿Un Emparedado con Ojos?'" , "Incluso si te equivocas, intenta adivinar." , "No te sientas mal si lo que adivinas no es correcto." , "Si lo que adivinas es correcto, te daré una galleta." , "Si no sabes lo que es, describe el dibujo." }

    };
    string[][] randomInitialPhraseIntroArray =
    {
        new string[] /*0 ENGLISH*/{ "Write about a person, inside joke, or anything!", "You decide what others have to draw!", "'Joe Spilling Coffee on the Copier Again'", "Think of something you want someone else to draw.", "You can write anything you want, funny or serious." },
        new string[] /*1 DUTCH*/{"Schrijf over een persoon, een inside joke, of wat dan ook!","Jij bepaalt wat anderen moeten tekenen!","'Joe morst alweer koffie op de kopieermachine'", "Bedenk iets wat je wilt dat iemand anders tekent.","Je kunt alles schrijven wat je maar wilt, grappig of serieus."},
        new string[] /*2 FRENCH*/{"Ecrire sur une personne, blague ou quoi que ce soit!","Vous décidez ce que les autres doivent dessiner!","Joe Spilling Coffee Again sur le Copieur Again'","Pensez à quelque chose que vous voulez que quelqu'un d'autre dessine.","Vous pouvez écrire tout ce que vous voulez, drôle ou sérieux."},
        new string[] /*3 GERMAN*/{"Schreiben Sie über eine Person, Insider-Witz oder irgendetwas!","Sie entscheiden, was andere zeichnen müssen!","Joe verschüttet wieder Kaffee auf dem Kopierer","Denken Sie an etwas, das jemand anderes zeichnen soll.","Du kannst alles schreiben, was du willst, lustig oder ernst."},
        new string[] /*4 ITALIAN*/{ "Scrivi di una persona, di uno scherzo o altro!", "Decidi tu cosa gli altri devono disegnare!", "'Joe sta di nuovo buttando il caffè sulla fotocopiatrice'", "Pensa a cosa vuoi che un altro disegni.", "Puoi scrivere qualsiasi cosa tu voglia, sia divertente che seria." },
        new string[] /*5 PORTUGUESE*/{"Escreva sobre uma pessoa, piada privada, ou qualquer outra coisa!", "Você decide o que os outros têm de desenhar!", "’Joel a derramar café na fotocopiadora novamente’", "Pense em qualquer coisa que queira que alguém desenhe.", "Pode escrever o que quiser, quer seja divertido ou ´serio."},
        new string[] /*6 SPANISH*/{ "¡Escribe acerca de una persona, broma interna o cualquier cosa!" , "¡Decides lo que los demás tiene que dibujar!" , "'Joe Derramó Café sobre la copiadora de nuevo'" , "Piensa en algo que quieres que otra persona dibuje." , "Puedes escribir cualquier cosa que quieras, divertida o seria." }

    };
    string[][] randomPostGameIntroArray =
    {
        new string[] /*0 ENGLISH*/{ "Let's see how your drawings evolved!", "Get ready to be blown away by the amazing art.", "Time for everyone to judge you and your drawings.", "Let's have a look at the amazing art you made!", "Let's see how to bad your guesses were!"},
        new string[] /*1 DUTCH*/{"Laten we eens kijken hoe je tekeningen zijn geëvolueerd!","Bereid je voor om te worden weggeblazen door de ongekende kunstzinnigheid.","Tijd voor iedereen om over jou en je tekeningen te oordelen.","Laten we eens kijken naar de geweldige kunst die je hebt gemaakt!","Laten we eens kijken hoe slecht je gokjes waren!"},
        new string[] /*2 FRENCH*/{"Voyons comment vos dessins ont évolué!","Préparez-vous à être époustouflé par cet art extraordinaire.","Il est temps que tout le monde vous juge et juge vos dessins.","Jetons un coup d'oeil à l'art incroyable que vous avez fait!","Voyons comment faire pour que vos suppositions soient mauvaises!"},
        new string[] /*3 GERMAN*/{"Mal sehen, wie sich Ihre Zeichnungen entwickelt haben!","Bereiten Sie sich darauf vor, von der unglaublichen Kunst umgehauen zu werden.","Zeit für alle, Sie und Ihre Zeichnungen zu beurteilen.","Lassen Sie uns einen Blick auf die erstaunliche Kunst werfen, die Sie gemacht haben!","Mal sehen, wie schlecht deine Vermutungen waren!"},
        new string[] /*4 ITALIAN*/{ "Vediamo come si sono evoluti i tuoi disegni!", "Preparati ad essere spazzato via da un'arte magnifica.", "È il momento che tutti giudichino te e i tuoi disegni.", "Diamo un'occhiata alla magnifica arte che hai creato!", "Vediamo quanto male sono andate le tue ipotesi!" },
        new string[] /*5 PORTUGUESE*/{"Vamos ver como evoluem os seus desenhos!", "Prepare-se para se maravilhar com incríveis obras de arte.", "É hora de todos analisarem os seus desenhos.", "Vamos analisar a obra de arte incrível que criou!", "Vamos ver quão maus foram os seus palpites!"},
        new string[] /*6 SPANISH*/{ "¡Veamos cómo evolucionan tus dibujos!" , "Prepárate para asombrarte por el arte asombroso" , "Tiempo para que todos juzguen tus dibujos" , "¡Echemos un vistazo a tu obra maestra!" , "¡Veamos qué tanto te puedes equivocar adivinando!" }
    };

    string[] randomListArrayDutch = new string[302] { "Presidentiële speech", "Thanksgiving Diner", "Verkeeswode", "Vuile Sokken", "Taco Tuesday", "Eerste keer op een fiets rijden", "Blinde man die voor het eerst kan zien", "Professionele Bowler", "Last Minute huiswerk Stress", "Skype Interview", "Sterrenkijken", "Midnight Snack", "Vergeten de broek aan te doen", "Iemand helpen de straat over te steken", "Klock knock Grap", "Een bordspel spelen", "Verstoppertje spelen", "Roddelen op school", "Nieuw iemand op school", "Een appel per dag houdt de dokter uit de buurt", "Huilen over gemorste melk", "Een geit melken", "Hooirit", "Vakantie", "Houthakker", "Tweeling", "Hondentraining", "Bankhanger", "Heldhaftige", "Hoolahoopen", "Dance Battle", "Neem je kind mee naar het werk dag", "Gazon maaien", "Zomer", "Superheld", "Zielig Liedje", "Hoefijzer werpen", "Ziek melden en Thuis blijven", "Waterballongevecht", "Huishoudklusjes doen", "Boodschappen doen", "Slaapfeestje", "Home Run", "Dansende Dwergen", "Karaoke", "De Bus missen", "Familiefeestje", "Nieuwsvoorlezer", "Spellingsbij", "Monster onder het bed", "Huiswerksmoesje", "Zakkenrollen", "Onderwijs", "Cartoons", "Dingen uitstellen", "Touwtrekken", "Frisbee Golf", "Stop er een sok in'.", "Autoreclame", "Je teen stoten", "Beste vriend", "Natuurliefhebber", "Kat die een bad neemt", "De Cake eten", "Iets uitvinden", "Een wandeling maken", "Een broodje maken", "Je sleutels verliezen", "Rivier van Tranen", "Nachtmerrie", "Smaaktest", "Blind Date", "All-You-Can-Eat Buffet", "1 Ster Restaurant", "Snoep van een baby afpakken", "Gebroken paraplu", "Populair Kind", "Duiken in het zwembad", "Schaatsen", "Tikkertje spelen", "Volleybal", "Voorstel fout gegaan", "Klasse Clown", "Coole Gasten gaan niet naar Exposities", "De Vuilnisbak schoonmaken", "Ga van mijn gazon af!", "Pizzabezorging", "Presentatie in de klas", "Schoolspel", "Van een heuvel af sleeën", "Zwembadspelletjes", "Iemand uit vragen", "'De geest is uit de fles'", "Skateboardtruc", "De loterij winnen", "Liften", "Verjaardagscadeau", "Eerste werkdag", "Slijmen bij de baas", "Huisdier van de Leraar", "In een Zombie Veranderen", "Verrassingsfeestje", "Een Iglo bouwen", "Echte Liefde", "Popcorn opwarmen", "Vuilnisman", "Kunstveiling", "Parade", "Eerste vlucht met vliegtuig", "Buiten adem raken", "Wild West Duel", "Hittegolf", "Bazige chef", "Roadtrip", "Trefbal", "In een val wandelen", "Op Heterdaad betrapt", "In een spiraal Vastzitten", "De een zijn dood is de ander zijn brood", "Bankoverval", "Eerste Man op Mars", "Een taxi wenken", "Jumping Jacks", "Toerist in een vreemd land", "Golfen", "De pestkop op school", "Een vriend een berichtje sturen", "Wetenschapsproject", "Ouders die hun kinderen in verlegenheid brengen", "Show and Tell", "Luidruchtig vacuüm", "Drive-In Theatre", "Disney Cruise", "Meeuwen die voedsel stelen", "In de rij wachten voor Waterijsjes", "Kabbelende Rivier", "Waterpark", "Zonnebrandcrème opdoen", "Zingen in de douche", "Babyvogel die het nest verlaat", "Oorsmeer", "Uitglijden over een bananenschil", "Een boom planten boom", "Klachten van klanten", "Een berg beklimmen", "Buiten een ballon loslaten", "Familie foto", "Een baard scheren", "Surfen", "Stap op een Crack, Breek je moeder's rug", "Grinch die Kerst steelt", "Worstenbroodjes", "Assepoester die haar schoen verliest", "Je telefoon laten vallen", "Iemand in het zwembad duwen", "Koorddanser", "Tovenaar", "Hond bang voor een onweersbui", "Drie kleine varkens", "Op een scheetkussen zitten", "Een enge film kijken", "In de klas in slaap vallen", "Massage krijgen", "Boos worden op je wekker", "Je mond gebruiken om een ballon op te blazen", "Luidkeels eten", "Struikelen over je schoenveters", "Proberen om je hand in een Pringles Can te steken", "Bananentelefoon", "Staar Wedstrijd", "Buiten een winkel slapen om als eerste iets te kopen", "Een winkel op Black Friday", "Verzet tegen de arrestatie", "Shrink Ray", "Een frisdrank openen nadat je die geschud hebt", "Deeg in de lucht gooien", "Zinkend schip", "Poison Ivy Rash", "Zakdoekje leggen spelen", "Knipperend in de foto", "Chinese Vingerval", "Twee trappen tegelijk opgaan", "Een rotzooi maken van je eten", "De WiFi van je buurman gebruiken", "Redding van de keeper", "Een geest zien", "Citroen uitknijpen", "Kussengevecht", "Iemands Cast tekenen", "Een vlaggenmast beklimmen", "Een bom onschadelijk maken", "Iemand een ketting omdoen", "De korst van een broodje weghalen", "Protesteren", "Kat die een laser achterna jaagt", "Vogel poept op iemand", "Boerende Baby", "Springen in plassen", "Een Confetti-kanon afvuren", "Een T-splitsing in de weg", "Een nieuwe PC bouwen", "Proberen een handstand te doen", "Trollen onder de brug", "De vloer is Lava", "Meditatie", "Praten met je mond vol", "Hot Dog Eetwedstrijd", "Pak spelen met een hond", "Je jas op het rek leggen", "Een gloeilamp vervangen", "Filmdate", "Beltoon van de telefoon tijdens de les", "Estafette", "Wandelen op blote voeten op gras", "Sms'en tijdens een afspraakje", "Cheerleader die valt", "Parallel parkeren", "Schoolbus Evacuatie", "Hindernisbaan", "Boekenworm", "Taco die kapotbreekt als je eet", "Een alligator aanpakken", "Geheime handdruk", "Robin Hood", "Taart in het gezicht krijgen", "Sokken en sandalen", "Durfal Stunt", "Bloemen plukken", "Op een Legoblokje stappen", "Een struik bijknippen", "Marmot die zijn eigen schaduw ziet", "Ramenwasser", "Een auto de ruimte in lanceren", "Dubbel Dipping", "Verjaardag punches", "Rechtstreeks iemand in de ogen niesen", "Schoenenpoetser", "Een hek schilderen", "Je groenten aan de hond geven", "Crowdsurfing", "Je vinger achter de deur laten zitten", "Aap die iets steelt", "Voodoopop", "Vliegenklapper", "Breakdancen", "Ezeltje Prikje", "Op je tong bijten", "Bibberend in de Koude", "Loodgieter die een pijp maakt", "Je sokken uitschudden", "Iemand op de schouders tikken aan de andere kant van aan waar jij staat", "Walvissen spotten", "Kussen Fort", "Het raam van je buurman met een bal kapot schieten", "Koffie morsen op jezelf", "Iemands geest lezen", "Je rug krabben", "Kat die aan het meubilair krabt", "Schoollunch", "Autodealer", "Kaarten in je mouw", "Scheten laten met je oksels", "Belletje lellen", "Dansen in de regen", "Vastzitten in Drijfzand", "Brandoefening", "Rotsen beklimmen", "Hert in koplampen", "Spieken bij een klasgenootje tijden een test", "De deur voor een verkoper dichtdoen", "Pruik die eraf valt", "Trap naar de hemel", "Aardbeving", "Stierenrijden", "Vee verzamelen", "Spugen in iemands gezicht terwijl je praat", "Tekenen op het gezicht van iemand die slaapt", "Per ongeluk je broek uitscheuren", "Kind dat zich voordoet als een volwassene", "Iemand negeren", "Statische elektriciteit", "Steen overslaan", "Katten landen altijd op hun voeten", "Zwaaien naar Iemand die niet naar je zwaaide", "Volume te hoog", "Freeze Tag", "Proberen een kind te laten glimlachen in een foto", "Poppenshow", "Waterbed", "Veiligheidshelmen redden levens", "Naar complimentjes hengelen", "Bovenbuurman die stampt", "Een drankautomaat door elkaar rammelen", "Een trui breien", "Slaapverhaaltjes", "Teleportatieapparaat", "Sneller gaan dan de snelheid van het licht", "Op dun ijs wandelen", "Gebarentaal", "Iemands gezicht in een taart duwen", "'Ik heb je neus'", "Verhuisdozen" };
    string[] randomListArrayItalian = new string[300] { "Discorso Presidenziale", "Cena del Ringraziamento", "Rabbia Al Volante", "Calzini Sporchi", "Martedì Taco", "La Prima Volta Su Una Bicicletta", "Un Cieco Vede Per La Prima Volta", "Giocatore Di Bowling Professionista", "Compiti A Casa Fatti All'Ultimo Minuto", "Intervista su Skype", "Guardare Le Stelle", "Spuntino di Mezzanotte", "Dimenticarsi di Mettere i Pantaloni", "Aiutare Qualcuno ad Attraversare la Strada", "Bussare Per Scherzo", "Giocare Ad Un Gioco Da Tavolo", "Nascondino", "Pettegolezzi Scolastici", "Nuovo Studente", "Una Mela Al Giorno Toglie Il Medico Di Torno", "Piangere Sul Latte Versato", "Mungere Una Capra", "Girare Su Un Carro", "Vacanza", "Lumberjack", "Gemelli", "Addrestrare Il Cane", "Pantofolaio", "Pompiere Eroico", "Giocare Con L'Hoola Hoop", "Battaglia Di Ballo", "Portare I Figli Al Lavoro", "Falciare Il Prato", "Estate", "Supereroe", "Canzone Triste", "Giocare Coi Ferri Di Cavallo", "Stare A Casa Ammalati", "Guerra Coi Gavettoni","Fare I Mestieri", "Fare La Spesa", "Dormire Da Qualcuno", "Fuori Campo", "Danza Del Leprecauno", "Karaoke", "Perdere Il Bus", "Festa Di Famiglia", "Giornalista", "Gara Di Spelling", "Mostro Sotto Il Letto", "Scusa Per I Compiti a Casa", "Scippo", "Titolo Di Studio", "Cartoni Animati", "Tiro Alla Fune", "Giocare Col Frisbee", "'Stai Zitto'", "Auto Commerciale", "Sbattere Il Mignolino", "Migliore Amico", "Amante Della Natura", "Fare Il Bagno Al Gatto", "Portare La Torta", "Inventare Qualcosa", "Passeggiare", "Fare Un Panino", "Perdere Le Chiavi", "Fiume Di Lacrime", "Incubo", "Test Del Gusto", "Appuntamento Al Buio", "Buffet All-You-Can-Eat", "Ristorante A 1 Stella", "Rubare Una Caramella Ad Un Bambino", "Ombrello Rotto", "Bambino Popolare", "Immersioni In Piscina", "Pattinaggio Sul Ghiaccio", "Giocare Ad Acchiapparello", "Lancio a Pallavolo", "Proposta Andata Male", "Buffone Della Classe", "I Bravi Ragazzi Non Badano Alle Apparenze", "Pulire La Lettiera", "'Vai Fuori Dai Piedi!'", "Consegna Pizza", "Dimostrazione In Classe", "Recita Scolastica", "Fare Slittino Giù Da Una Collina", "Giocare A Biliardo", "Chiedere A Qualcuno Di Uscire", "'Vuotare Il Sacco'", "Acrobazia con lo Skateboard", "Vincere Alla Lotteria", "Autostop", "Regalo Di Anniversario", "Primo Giorno Di Lavoro", "Fare Il Lecchino", "Il Cocco Dell'Insegnante", "Trasformarsi In Uno zombi", "Festa A Sorpresa", "Costruire Un Igloo", "Vero Amore", "Bruciare I Popcorn", "Spazzino", "Asta D'Opere D'Arte", "Parata", "Prima Volta In Aereo", "Essere Quasi Senza Fiato", "Duello Nel Selvaggio West", "Ondata Di Caldo", "Capo Dispotico", "Viaggio Su Strada", "Dodgeball", "Cadere In Trappola", "Essere Colti Con Le Mani Nel Sacco", "Essere Bloccati In Un Circolo Vizioso", "La Spazzatura Di Qualcuno è L'oro Di Qualcun Altro", "Rapina In Banca", "Primo Uomo Su Marte", "Chiamare Un Taxi", "Salto A Gambe Divaricate", "Turista In Un Paese Straniero", "Giocare A Golf", "Bulletto Della Scuola", "Mandare Un Messaggio Ad Un Amico", "Progetto Per La Fiera Della Scienza", "Genitori Che Mettono In Imbarazzo I Figli", "Mostra e Racconta", "Aspirapolvere Rumoroso", "Drive-In", "Viaggio a Disneyland", "Gabbiani Che Rubano Il Cibo", "Fare La Coda Per Il Ghiaccio", "Fiume Che Scorre Lento", "Parco Acquatico", "Mettere La Protezione Solare", "Cantare Sotto La Doccia", "Uccellino Che Lascia Il Nido", "Cerume", "Scivolare Su Una Buccia Di Banana", "Piantare Un Albero", "Lamentela Del Cliente", "Scalare Una Montagna", "Lasciar Volare Un Palloncino", "Foto Di Famiglia", "Farsi La Barba", "Fare Surf", "Pesta Una Crepa, Tua Madre E' Una Strega", "Il Grinch Ruba Il Natale", "Salatini Ai Wurstel", "Cenerentola Che Perde La Scarpetta", "Far Cadere Il Telefono", "Spingere Qualcuno Nella Piscina", "Funambolo", "Mago", "Cane Terrorizzato Dal Temporale", "I Tre Porcellini", "Sedersi Su Un Cuscino Spernacchiante", "Guardare Un Film Che Fa Paura", "Addormentarsi In Classe", "Farsi Fare Un Massaggio", "Terrorizzarsi Al Suono Della Sveglia", "Gonfiare Un Palloncino", "Mangiare Rumorosamente", "Inciampare Sui Propri Lacci", "Far Entrare La Mano Nel Tubo Delle Pringles", "Telefono A Forma Di Banana", "Guardare Una Gara", "Accamparsi Fuori Da Un Negozio Per Una Vendita", "Un Negozio Durante Il Black Friday", "Resistenza All'Arresto", "Raggio Restringente", "Aprire Una Lattina Dopo Averla Agitata", "Saltare La Pasta", "Nave Che Affonda", "Rash Cutaneo da Edera Velenosa", "Giocare Alla Baia Dell'Oca", "Avere Gli Occhi Chiusi In Una Foto", "Trappola Per Dita Cinese", "Salire Due Scalini Alla Volta", "Fare Un Disastro In Cucina", "Usare Del WiFi Del Vicino", "Salvataggio Del Portiere", "Vedere Un Fantasma", "Spremere Il Limone", "Guerra Dei Cuscini", "Firmare Al Posto Di Qualcuno", "Arrampicarsi Su Un Palo", "Disinnescare Una Bomba", "Mettere Una Collana A Qualcuno", "Togliere La Crosta A Un Panino", "Protestare", "Gatto Che Insegue Un Puntatore Laser", "Uccello Che Fa La Cacca Su Qualcuno", "Far Fare Un Ruttino Ad Un Bambino", "Saltare Nelle Pozzanghere", "Sparare Con Un Cannone Di Coriandoli", "Bivio Stradale", "Assemblare Un Nuovo PC", "Cercare Di Fare Una Verticale", "Troll Sotto Un Ponte", "Pavimento Di Lava", "Meditazione", "Parlare Con La Bocca Piena", "Gara di Mangiatori di Hot Dog", "Giocare Con Un Cane Che Prende Un Bastone", "Mettere Il Cappotto Sull'attaccapanni", "Cambiare Una Lampadina", "Appuntamento Al Cinema", "Il Telefono Che Squilla Durante La Lezione", "Staffetta", "Camminare A Piedi Nudi Sull'Erba", "Messaggio Durante Un Appuntamento", "Cheerleader Che Cade", "Parcheggio Parallelo", "Evacuazione Scuolabus", "Percorso A ostacoli", "Topo Di Biblioteca", "Un Taco Che Si Rompe Mentre Lo Mangi", "Affrontare Un Alligatore", "Accordo Segreto", "Robin Hood", "Farsi Mettere I Piedi In Testa", "Calzini e Sandali", "Stuntman", "Raccogliere Fiori", "Calpestare Il Lego", "Tagliare Una Siepe", "Marmotta Che Vede La Sua ombra", "Lavavetri", "Lanciare Un'Auto Nello Spazio", "Minestra Riscaldata", "Starnutire In Faccia A Qualcuno", "Lustrascarpe", "Dipingere Una Staccionata", "Dare Le Verdure Al Cane", "Fare Surf Sulla Folla", "Chiudersi Il Dito Nella Porta", "Una Scimmia Che Ruba Qualcosa", "Bambola Voodoo", "Lampada Insetticida", "Fare La Break Dance", "Giocare Ad Attacca La Coda All'Asino", "Mordersi La Lingua", "Brividi Di Freddo", "Idraulico Che Ripara Un Tubo", "Sbarellare", "Toccare Qualcuno Sulla Spalla Opposta A Dove Stai", "Guardare Le Balene", "Fortino Di Cuscini", "Colpire La Finestra Del Vicino Con Una Palla Da Baseball", "Rovesciarsi Il Caffè Addosso", "Leggere Nella Mente Di Qualcuno", "Grattarsi La Schiena", "Gatto Che Graffia I Mobili", "Pranzo a Scuola", "Rivenditore D'Auto", "Avere Un Asso Nella Manica", "Fare Le Scoreggie Con L'Ascella", "Scherzo Telefonico", "Ballare Sotto La Pioggia", "Essere Bloccato Nelle Sabbie Mobili", "Esercitazione Antincendio", "Scalare Una Montagna", "Cervo Illuminato Dai Fari", "Dare Un'Occhiata AlLa Prova Di Un Compagno Di Classe", "Chiudere La Porta In Faccia Ad Un Venditore Porta A Porta", "Parrucca Che Cade", "Scalinata Verso Il Cielo", "Terremoto", "Cavalcare Un Toro", "Radunare Una Mandria", "Sputare In Faccia A Qualcuno Mentre Parli", "Disegnare Sulla Faccia Di Una Persona Che Dorme", "Strappare Per Errore i pantaloni", "Bambino Che Si Atteggia da Adulto", "Ignorare Qualcuno", "Elettricità Statica", "Lanciare Un Sasso", "I Gatti Atterrano Sempre Sulle Zampe", "Salutare Qualcuno Che Non Sta Salutando Te", "Volume Troppo Alto", "Giocare Alle Belle Statuine", "Cercare Di Far Sorridere Un Bambino In Una Fotografia", "Spettacolo Di Marionette", "Letto Ad Acqua", "Gli Elmetti Salvano Vite", "Cercare Complimenti", "Il Vicino Del Piano Di Sopra Che Cammina Con Passo Pesante", "Scuotere Un Distributore Automatico", "Fare Un Maglione Di Lana", "Raccontare La Favola Della Buonanotte", "Macchina Per Il Teletrasporto", "Andare Più Veloci Della Luce", "Giocare Col Fuoco", "Usare Il Linguaggio Dei Segni", "Spingere La Faccia Di Qualcuno In Una Torta", "'Ti Ho Preso Il Naso'", "Scatole Impilabili" };
    string[] randomListArrayPortuguese = new string[302] { "Discurso presidencial", "Jantar de Dia de Ação de Graças", "Fúria na estrada", "Meias sujas", "Terça-feira de tacos", "Primeira vez a andar de bicicleta", "Homem cego a ver pela primeira vez", "Jogador de bowling profissional", "Fazer os trabalhos de casa à última hora", "Entrevista por Skype", "Veras estrelas", "Petisco à meia-noite", "Esquecer de vestir calças", "Ajudar alguém a atravessar a estrada", "Piada de bater à porta", "Jogar um jogo de tabuleiro", "Esconder e procurar", "Fofoca de secundário", "Pessoa nova na escola", "Uma maçã por dia nem sabes o bem que te fazia", "Chorar sobre leite derramado", "Ordenhar uma cabra", "Passeio hayrack", "Férias", "Lenhador", "Gémeos", "Treino de cães", "Sedentário", "Bombeiro heróico", "Fazer hoola hoop", "Batalha de dança", "Dia de levar os filhos para o trabalho", "Cortar a relva", "Verão", "Super-herói", "Canção triste", "Jogar ao jogo da ferradura", "Ficar em casa por doença", "Guerra de balões de água", "Fazer tarefas domésticas", "Compras de mercearia", "Dormir em casa de amigos", "Home Run", "Duendes a dançar", "Karaoke", "Perder o autocarro", "Festa de família", "Apresentador de noticiário", "Soletrar", "Monstro debaixo da cama", "Desculpa para não fazer os trabalhos de casa", "Furtar", "Educação", "Desenhos animados", "Pôr-se com rodeios", "Cabo de guerra", "Golfe de disco", "’Cala a matraca’", "Anúncio de carros", "Bater com o dedo do pé", "Melhor amigo", "Amante da natureza", "Gato a tomar banho", "Ser o fim da picada", "Inventar algo", "Dar um passeio", "Fazer uma sanduíche", "Perder as chaves", "Mar de lágrimas", "Pesadelo", "Exame organolético", "Encontro às cegas", "Bufê livre", "Restaurante de 1 estrela", "Tirar o doce à criança", "Guarda-chuva partido", "Miúdo popular", "Mergulhar na piscina", "Patinar no Gelo", "Jogar à apanhada", "Remate de voleibol em suspensão", "Pedido de casamento que deu para o torto", "Palhaço da turma", "Gajos fixes não olham para as explosões", "Limpar a caixa de areia", "’Sai do meu jardim!’", "Entrega de pizza", "Apresentação na aula", "Teatro da escola", "Descer colina de tronó", "Jogos de piscina", "Convidar alguém para sair", "’Gato escondido com o rabo de fora’", "Truque de skateboarding", "Ganhar a lotaria", "Andar à boleia", "Presente de aniversário", "Primeiro dia de trabalho", "Lamber as botas ao patrão", "O queridinho do professor", "Transformar-se num zombie", "Festa surpresa", "Construir um iglô", "Amor verdadeiro", "Queimar a pipoca", "Homem do lixo", "Leilão de arte", "Parada", "Primeiro voo de avião", "Ficar sem fôlego", "Duelo do Oeste Selvagem", "Onda de calor", "Chef mandão", "Viagem por estrada", "Jogo do mata", "Cair na armadilha", "Apanhado com a boca na botija", "Ficar preso num ciclo", "O lixo de um homem é o tesouro de outro", "Assalto a um banco", "Primeiro homem em Marte", "Chamar um táxi", "Saltos de tesoura", "Turista num país estrangeiro", "Jogar golfe", "Agressor escolar", "Enviar mensagem a amigo", "Projeto de feira de ciência", "Pais a envergonhar os filhos", "Mostre e conte", "Aspirador barulhento", "Cinema ao ar livre", "Cruzeiro Disney", "Gaivotas a roubar comida", "À espera na fila para o gelo", "Rio lento", "Parque aquático", "Colocar protetor solar", "Cantar no chuveiro", "Passarinho bebé a sair do ninho", "Cera dos ouvidos", "Escorregar numa casca de banana", "Plantar uma árvore", "Cliente a reclamar", "Subir uma montanha", "Soltar um balão no exterior", "Fotografia de família", "Fazer a barba", "Surfar", "Pisa a rachadura e parte as costas da tua mãe", "Grinch a roubar o Natal", "Enroladinho de salsicha", "Cinderela a perder o sapatinho", "Deixares cair o telefone", "Empurrar alguém para a piscina", "Equilibrista", "Mágico", "Cão com medo de trovoada", "Três porquinhos", "Sentar numa almofada de flatulência", "Ver um filme de terror", "Adormecer durante a aula", "Receber uma massagem", "Irritar-se com o despertador", "Encher um balão com a boca", "Fazer barulho a comer", "Tropeçar nos cordões", "Tentar enfiar a mão numa lata de Pringles", "Telefone banana", "Jogar ao sério", "Acampar à porta de uma loja por uma promoção", "Loja em dia de Black Friday", "Resistir a detenção", "Raio redutor", "Abrir uma lata de refrigerante depois de a agitar", "Atirar massa ao ar", "Navio a afundar", "Erupção cutânea por hera venenosa", "Jogar ao jogo do lencinho", "Pestanejar na fotografia", "Armadilha de dedo chinesa", "Subir duas escadas de cada vez", "Sujar tudo com a comida", "Usar o WiFi do vizinho", "Defesa de guarda-redes", "Ver um fantasma", "Espremer o limão", "Guerra de almofadas", "Assinar o gesso de alguém", "Subir ao mastro de uma bandeira", "Desativar uma bomba", "Pôr o laço no pescoço de alguém", "Remover a crosta de uma sanduíche", "Protestar", "Gato a perseguir um laser", "Pássaro a fazer cocó em alguém", "Pôr o bebé a arrotar", "Saltar em poças", "Disparar um canhão de confetes", "Uma encruzilhada", "Construir um PC novo", "Tentar fazer o pino", "Trolls debaixo da ponte", "O chão é lava", "Meditação", "Falar com a boca cheia", "Concurso de comer cachorros-quentes", "Brincar ao busca com um cão", "Colocar o casaco no cabide", "Mudar uma lâmpada", "Ida ao cinema", "Telefone a tocar durante a aula", "Corrida de estafetas", "Andar descalço na relva", "Enviar mensagens durante um encontro", "Cheerleader a cair", "Estacionamento paralelo", "Evacuação de autocarro", "Pista de obstáculos", "Rato de biblioteca", "Taco a desmoronar-se quando come", "Combater um crocodilo", "Aperto de mão secreto", "Robin Hood", "Levar com uma tarte na cara", "Meias e sandálias", "Proeza arrojada", "Colher flores", "Pisar um Lego", "Aparar um arbusto", "Marmota a ver a sombra", "Limpador de janelas", "Lançar um carro para o espaço", "Comissão dupla", "Socos de aniversário", "Espirrar diretamente para os olhos de alguém", "Engraxador de sapatos", "Pintar uma cerca de estacas", "Dar os vegetais ao cão", "Crowd Surfing", "Entalar o dedo na porta", "Macaco a roubar alguma coisa", "Boneca de voodoo", "Eletrocutador de insetos", "Fazer breakdance", "Jogo de colocar a cauda do burro", "Morder a língua", "Tremer com o frio", "Canalizador a reparar um cano", "Ficar de queixo caído", "Tocar no ombro de alguém do lado oposto onde estás", "Ver baleias", "Fortaleza de almofadas", "Acerta com a bola de beisebol na janela do vizinho", "Entornar o café sobre ti mesmo", "Ler a mente de alguém", "Coçar as costas", "Gato a arranhar os móveis", "Almoço da escola", "Concessionário de carros", "Ter um truque na manga", "Dar um pum com o sovaco", "Partida telefónica", "Dançar à chuva", "Preso nas areias movediças", "Simulacro de incêndio", "Escalada em rocha", "Ser apanhado de surpresa", "Espreitar para o teste do colega", "Bater com a porta na cara de um vendedor", "Peruca a cair", "Escada para o céu", "Terramoto", "Andar de touro", "Recolha do gado", "Cuspir na cara de alguém quando se fala", "Desenhar na cara de uma pessoa que está a dormir", "Rasgar as calças por acidente", "Criança a fingir ser adulto", "Ignorar alguém", "Eletricidade estática", "Fazer saltar pedras na água", "Os gatos caem sempre de pé", "Acenar a alguém que não estava a acenar para ti", "Volume demasiado alto", "Jogo da estátua", "Tentar fazer uma criança rir para uma fotografia", "Espetáculo de marionetas", "Cama de água", "Capacetes salvam vidas", "Andar à pesca de elogios", "Vizinhos de cima a bater com os pés", "Abanar uma máquina de venda automática", "Tricotar uma camisola", "História de embalar", "Dispositivo de teletransporte", "Ir mais rápido do que a velocidade da luz", "Andar a pisar ovos", "Língua gestual", "Empurrar a cara de alguém contra o bolo", "’Roubei-te o nariz’", "Empilhar caixas" };
    string[] randomListArraySpanish = new string[293] { "Discurso Presidencial", "Cena de Acción de Gracias", "Ira de Carretera", "Medias Sucias", " Martes de Tacos", "Primera Vez Montando una Bicicleta", "Hombre Ciego Viendo por Primera Vez", "Jugador de Bolos Profesional", "Tarea de Último Minuto", "Entrevista por Skype", "Estudio de las Estrellas", "Aperitivo de Media Noche", "Olvidé Ponerme los Pantalones", "Ayudando a Alguien a Cruzar la Calle", "Broma Toc Toc", "Jugando un Juego de Mesa", "Las Escondidas", "Chisme de Secundaria", "Nueva Persona en la Escuela", "Una Manzana al Día Mantiene al Médico Alejado", "Llorar Sobre la Leche Derramada", "Ordeñando una Cabra", "Paseo en Carreta", "Vacaciones", "Leñador", "Gemelos", "Perro Entrenando", "Flojo", "Bombero Heroico", "Hula Hula", "Batalla de Baile", "Día de Llevar a tu Hijo al Trabajo", "Podando el césped", "Verano", "Superhéroe", "Canción Triste", "Herradura", "Quedarse en Casa Enfermo", "Guerra de Globos de Agua", "Hacer los Quehaceres", "Comprar Comestibles", "Pijamada", "Home Run", "Duendes Bailando", "Karaoke", "Perdiendo el Autobús", "Fiesta en Familia", "Presentador de Noticias", "Abeja de Ortografía", "Monstruo Debajo de la Cama", "Excusa para la Tarea", "Robar", "Educación", "Dibujos Animados", "Andar con Rodeos", "Tira y Afloja", "Golf con Disco", "Comercial de Coches", "Golpearse el Dedo del Pie", "Mejor Amigo", "Amante de la Naturaleza", "Gato Bañándose", "Inventando Algo", "Saliendo a Caminar", "Haciendo un Emparedado", "Perdiendo tus Llaves", "Río de Lágrimas", "Pesadilla", "Prueba de Sabor", "Cita a Ciegas", "Buffet Todo lo que Puedas Comer", "Restaurante de 1 Estrella", "Robar un Dulce de un Niño", "Sombrilla Rota", "Niño Popular", "Buceando en la Piscina", "Patinaje sobre Hielo", "Jugando a las Persecuciones", "Remate de Voleibol", "Propuesta de Matrimonio Rechazada", "Payaso de la Clase", "Chicos Geniales No Miran Explosiones", "Limpiando la Caja de Arena", "¡Fuera de mi Jardín!", "Entrega de Pizza", "Presentación para la Clase", "Juego Escolar", "Deslizarse por la Colina", "Juegos de Piscina", "Pedirle a Alguien una Cita", "El Gato Está Fuera de la Bolsa", "Truco de Patineta", "Ganando la Lotería", "Autoestop", "Regalo de Aniversario", "Primer Día en el Trabajo", "Succionando las Medias del Jefe", "Mascota del Maestro", "Convirtiéndose en Zombi", "Fiesta Sorpresa", "Creando un Iglú", "Amor Verdadero", "Quemando las Palomitas de Maíz", "Hombre de la Basura", "Subasta de Arte", "Desfile", "Primer Vuelo en Avión", "Quedarse sin Aliento", "Duelo del Antiguo Oeste", "Ola de Calor", "Chef Mandón", "Viaje de Carretera", "Balón Prisionero", "Caminando hacia una Trampa", "Atrapado con las Manos en la Masa", "Atrapado en un Ciclo", "La Basura de un Hombre es el Tesoro de Otro", "Robo de un Banco", "Primer Hombre en Marte", "Llamando a un Taxi", "Salto de Estrella", "Turista en un País Extranjero", "Jugando Golf", "Abusón de la Escuela", "Escribiendo a un Amigo", "Proyecto de la Feria de Ciencias", "Padres Avergonzando a sus Hijos", "Mostrar y Contar", "Aspiradora Ruidosa", "Autocinema", "Crucero de Disney", "Gaviotas Robando Comida", "Esperando en la Cola para el Helado", "Río Lento", "Parque Acuático", "Aplicando Bloqueador Solar", "Cantando en la Ducha", "Bebé Pájaro Dejando el Nido", "Cera de Oídos", "Tropezándose con una Concha de Banana", "Plantando un Árbol", "Cliente Quejándose", "Subiendo una Montaña", "Soltando un Globo al Exterior", "Foto Familiar", "Afeitando la Barba", "Surfeando", "Grinch Robándose la Navidad", "Cerdo en una Manta", "Cenicienta Perdiendo su Zapato", "Dejando Caer tu Teléfono", "Empujar a Alguien a la Piscina", "Funambulista", "Mago", "Perro Asustado de los Rayos", "Los Tres Cerditos", "Sentado en un Cojín de Bromas", "Mirando una Película de Miedo", "Durmiendo en Clase", "Recibiendo un Mensaje", "Molestándose con la Alarma de la Mañana", "Usar tu Boca para Inflar un Globo", "Comiendo con la Boca Abierta", "Tropezarse con las Trenzas de los Zapatos", "Intentar Meter tu Mano en una Lata de Pringles", "Concurso de Miradas", "Acampando a las Afueras de una Tienda para una Venta", "Una Tienda en Viernes Negro","Resistirse al Arresto","Rayo Reductor","Abrir una Soda Luego de Agitarla","Lanzando una Masa de Pizza al Aire","Barco Hundiéndose","Alergia a la Hiedra Venenosa","Jugando Pato Pato Ganso","Parpadeando en una Foto","Trampa de Dedos China","Subiendo Hasta Dos Escalones a la Vez","Haciendo un Desastre con la Comida","Usando el WiFi del Vecino","Parada en Portería","Ver un Fantasma","Exprimir un Limón","Pelea de Almohadas","Subiendo el Asta de una Bandera","Desactivando una Bomba","Colocándole un Collar a Alguien", "Remover la Corteza de un Emparedado", "Protestar", "Gato Persiguiendo un Puntero Láser", "Ave Defecando en Alguien", "Bebé Heructando", "Saltando en Charcos", "Disparando un Cañón de Confeti", "Un Desvío en el Camino", "Intentar Pararse de Manos", "Troles Debajo del Puente", "El Suelo es Lava", "Meditación", "Hablando con la Boca Llena", "Concurso de Comer Perros Calientes", "Jugando a la Pelota con un Perro", "Colocando una Chaqueta en el Perchero", "Cambiar un Bombillo", "Cita en el Cine", "Teléfono Sonando en Clase", "Carrera de Relevos", "Caminando Descalzo en la Grama", "Mandando Mensajes de Texto Durante una Cita", "Candelabros Cayendo", "Estacionamiento en Paralelo", "Evacuación de Bus Escolar", "Pista de Obstáculos", "Gusano Marrón", "Corteza de un Taco Cayendo Mientras Comes", "Peleando con un Cocodrilo", "Saludo Secreto", "Robin Hood", "Recibir un Pie en la Cara", "Medias y Sandalias", "Doble de Riesgo", "Recoger Flores", "Pararse en un Lego", "Cortando un Arbusto", "Marmota Viendo su Sombra", "Limpiaventanas", "Lanzando Carro al Espacio", "Golpes de Cumpleaños", "Estornudar Directamente en los Ojos de Alguien", "Comerse un Zapato", "Pintando una Cerca", "Darle los Vegetales al Perro", "Surfear la Multitud", "Cerrar la Puerta en tus Dedos", "Mono Robando Algo", "Muñeca Voodoo", "Mata Insectos", "Breakdance", "Ponle la Cola al Burro", "Morder tu Lengua", "Temblar en el Frío", "Plomero Arreglando una Tubería","Tocar el Hombro de Alguien al Contrario de Donde te Encuentras","Mirar Ballenas", "Fuerte de Almohadas", "Romper la Ventana del Vecino con una Pelota de Beisbol", "Derramarte el Café Encima", "Leer la Mente de Alguien", "Rascarte la Espalda", "Gato Arañando los Muebles", "Almuerzo Escolar", "Vendedor de Autos", "Cartas Bajo la Manga", "Gases con la Axila", "Llamada de Bromas", "Bailando Bajo la Lluvia", "Atrapado en Arenas Movedizas", "Simulacro de Incendios", "Escalar Rocas", "Ver la Prueba de un Compañero de Clase", "Cerrarle la Puerta a un Vendedor", "Peluca Cayéndose", "Escaleras Hacia el Cielo", "Terremoto", "Montar un Toro", "Castillo Inflable", "Escupirle a Alguien en la Cara Mientras Hablas", "Dibujar en la Cara de una Persona Durmiendo", "Romper tus Pantalones Accidentalmente", "Niño Pretendiendo Ser un Adulto", "Ignorando a Alguien", "Electricidad Estática", "Cabrillas", "Los Gatos Siempre Caen de Pie", "Saludar a Alguien que no te Saluda", "Volumen Muy Alto", "Toque Congelado", "Intentando Hacer que un Niño Sonría en una Foto", "Show de Marionetas", "Cama de Agua", "Los Cascos Salvan Vidas", "Buscar Recibir Cumplidos", "Vecino del Piso de Arriba Haciendo Ruido", "Máquina Expendedora de Snacks", "Tejer un Sweater", "Historia para ir a Dormir", "Dispositivo de Teletransportación", "Ir Más Rápido que la Velocidad de la Luz", "Caminando sobre Hielo Fino", "Lenguaje de Señas", "Empujar la Cara de Alguien a una Torta", "Tengo tu nariz", "Apilar Cajas" };
    string[] randomListArrayFrench = new string[269] { "Discours présidentiel", "Dîner de Thanksgiving", "Rage au volant", "Chaussettes sales", "Taco Tuesday", "Première fois à bicyclette", "Aveugle voyant pour la première fois", "Bowler professionnel", "Rush des devoirs de dernière minute", "Interview sur Skype", "Observation des étoiles", "Oublier de mettre un pantalon", "Aider quelqu'un à traverser la rue", "Jouer à un jeu de société", "Jouer à cache-cache", "Nouvelle personne à l'école", "Une pomme par jour garde le docteur loin", "Pleurer sur le lait renversé", "Traire une chèvre", "Tour de foin", "Vacances", "Bûcheron", "Jumeaux", "Entrainement de chien", "Patate de sofa", "Pompier héroïque", "Bataille de dance", "Amenez votre enfant au travail", "Tondre la pelouse", "Été", "Super-héros", "Chanson triste", "Jouer au fer à cheval", "Rester malade à la maison", "Combat de ballon d'eau", "Faire des corvées", "Épicerie", "Soirée pyjama", "Manquer le bus", "Fête de famille", "News Ancre", "Monstre sous le lit", "Excuse pour les devoirs", "Vol à la tire", "Les dessins animés", "Tir à la corde", "Mettre une chaussette en elle", "Voiture commerciale", "Meilleur ami", "Amoureux de la nature", "Chat prenant un bain", "Prendre le gâteau", "Inventer quelque chose", "Faire une promenade", "Faire un sandwich", "Perdre vos clés", "Rivière des larmes", "Cauchemar", "Test de goût", "Buffet à volonté", "Restaurant 1 étoile", "Prendre Candy à un bébé", "Parapluie cassé", "Kid populaire", "Plongée dans la piscine", "Patinage sur glace", "Jouer tag", "La proposition a mal tourné", "Clown de la classe", "Les gars cool ne regardent pas les expositions", "Nettoyage de la litière", "Descendre ma pelouse!", "Livraison de pizzas", "Présentation en classe", "Jeu d'école", "Traverser une colline en traîneau", "Jeux de billard", "Demander à quelqu'un", "Le chat est sorti du sac", "Gagner à la loterie", "Auto-stop", "Cadeau d'anniversaire", "Premier jour de travail", "Sucer le patron", "L'animal de compagnie de l'enseignant", "Se transformer en zombie", "Fête surprise", "Construire un igloo", "L'amour vrai", "Brûler le Popcorn", "Éboueur", "Premier tour d'avion", "À bout de souffle", "Duel d'Ouest Sauvage", "La canicule", "Chef autoritaire", "Marcher dans un piège", "Pris la main dans le sac", "Coincé dans une boucle", "Le malheur des uns fait le bonheur des autres", "Braquage de banque", "Premier homme sur Mars", "Avoir un taxi", "Touriste dans un pays étranger", "Jouer au golf", "Envoyer un SMS à un ami", "Projet d'expo-sciences", "Les parents embarrassent leurs enfants", "Montrer et dire", "Vide fort", "Théâtre Drive-In", "Croisière Disney", "La nourriture des voleurs de mouettes", "En attente de glace d'eau", "Rivière tranquille", "Parc aquatique", "Appliquer un écran solaire", "Chanter dans la douche", "Bébé oiseau quittant le nid", "Cérumen", "Glisser sur une peau de banane", "Planter un arbre", "Client se plaindre", "Escalader une montagne", "Lâcher un ballon à l'extérieur", "Photo de famille", "Rasage de la barbe", "Surfant", "Faites un pas en avant, brisez le dos de votre mère", "Noël grincheux", "Cochon dans une couverture", "Cendrillon perd sa chaussure", "Lâcher votre téléphone", "Pousser quelqu'un dans la piscine", "Funambule", "Magicien", "Chien peur d'un orage", "Trois petits cochons", "Assis sur un coussin Whoopie", "Regarder un film effrayant", "S'endormir en classe", "Obtenir un massage", "Se fâcher de votre réveil", "Utiliser votre bouche pour faire exploser un ballon", "Manger fort", "Trébucher sur vos lacets", "Essayer de tenir votre main dans un Pringles Can", "Téléphone banane", "Concours Staring", "Camping en dehors d'un magasin pour une vente", "Un magasin le vendredi noir", "Résister à une arrestation", "Rayon rétractable", "Ouvrir un soda après l'avoir secoué", "Lancer de la pâte dans l'air", "Bateau qui coule", "Jouer au canard ou à la canard", "Clignotant sur la photo", "Piège à doigts chinois", "Monter deux escaliers à la fois", "Faire un désordre avec votre nourriture", "Utiliser le WiFi de votre voisin", "Gardien de sauvegarde", "Voir un fantôme", "Bataille d'oreillers", "Signer le casting de quelqu'un", "Grimper sur un mât de drapeau", "Désamorcer une bombe", "Mettre un collier sur quelqu'un", "Enlever la croûte d'un sandwich", "Chat chassant un pointeur laser", "Oiseau caca sur quelqu'un", "Burping le bébé", "Tirer un canon à confettis", "Une fourchette dans la route", "Essayer de faire le poirier", "Trolls sous le pont", "Le sol est de la lave", "Méditation", "Parler avec la bouche pleine", "Concours de restauration à hot-dog", "Jouer à chercher avec un chien", "Mettre votre manteau sur le support", "Changer une ampoule", "Date de cinéma", "Le téléphone sonne pendant le cours", "Course de relais", "Marcher pieds nus sur l'herbe", "SMS pendant une date", "Stationnement parallèle", "Évacuation d'autobus scolaire", "Course d'obstacle", "rat de bibliothèque", "Le dur taco shell se désagrège en mangeant", "S'attaquer à un alligator", "Poignée de main secrète", "Robin des Bois", "Prendre pied dans le visage", "Chaussettes et sandales", "Cueillir des fleurs", "Marcher sur un lego", "Couper un buisson", "Laveur de vitres", "Lancer une voiture dans l'espace", "Double trempage", "Poinçons d'anniversaire", "Éternuer directement dans les yeux de quelqu'un", "Cireur de chaussures", "Peindre une palissade", "Donner vos légumes au chien", "Foule de surf", "Fermer la porte à votre doigt", "Singe volant quelque chose", "Poupée vaudou", "Tue mouche éléctrique", "Épingler la queue sur l'âne", "Mordre ta langue", "Frissons dans le froid", "Plombier réparer un tuyau", "Bascule tes chaussettes", "Taper sur quelqu'un sur l'épaule opposée à l'endroit où vous vous tenez", "L'observation des baleines", "Fort d'oreiller", "Frapper la fenêtre de votre voisin avec un ballon de baseball", "Renverser du café sur vous-même", "Lire l'esprit de quelqu'un", "Se gratter le dos", "Chat grattant le meuble", "Déjeuner d'école", "Concessionnaire", "Cartes dans votre manche", "Péter les aisselles", "Farce", "Danser sous la pluie", "Exercice d'incendie", "Escalade", "Cerf dans les phares", "Jeter un œil à l'examen d'un camarade de classe", "Fermer la porte à un vendeur", "La perruque tombe", "Tremblement de terre", "La monte de taureau", "Rassembler le bétail", "Cracher au visage de quelqu'un en parlant", "S'appuyant sur le visage d'une personne endormie", "Déchirer accidentellement votre pantalon", "Enfant se faisant passer pour un adulte", "Ignorer quelqu'un", "Électricité statique", "Sautant sur des pierres", "Les chats atterrissent toujours sur leurs pieds", "Faire signe à quelqu'un qui ne vous faisait pas signe", "Volume trop élevé", "Essayer de faire sourire un enfant sur une photo", "Spectacle de marionnettes", "Lit d'eau", "Les casques sauvent des vies", "Chercher à recevoir des compliments", "Voisin d'en haut piétinant", "Secouant un distributeur automatique", "Tricoter un pull", "Conte", "Appareil de téléportation", "Aller plus vite que la vitesse de la lumière", "Marcher sur la glace mince", "Langage des signes", "Pousser le visage de quelqu'un dans un gâteau", "J'ai ton nez", "Empilabler les cartons" };
    string[] randomListArrayGerman = new string[260] { "Präsidentenrede", "Festessen zum Erntedankfest", "Schmutzige Socken", "Zum ersten Mal Fahrrad fahren", "Blinder Mann, der zum ersten Mal sieht", "Sterne beobachten", "Mitternachtssnack", "Hosen anziehen vergessen", "Jemandem helfen, die Straße zu überqueren", "Ein Brettspiel spielen", "Verstecke dich und gehe suchen", "Neue Person in der Schule", "Ein Apfel pro Tag hält den Doktor fern", "Weinen über verschüttete Milch", "Melken einer Ziege", "Urlaub", "Holzfäller", "Zwillinge", "Hundetraining", "Stubenhocker", "Heroischer Feuerwehrmann", "Tanzwettbewerb", "Nehmen Sie Ihr Kind zum Arbeitstag", "Den Rasen mähen", "Sommer", "Superheld", "Trauriges Lied", "Hufeisen spielen", "Zuhause krank bleiben", "Wasserballonschlacht", "Hausarbeiten machen", "Lebensmittel einkaufen", "Übernachten", "Kobolde tanzen", "Karaoke", "Den Bus verpassen", "Familienfeier", "Nachrichtensprecher", "Buchstabier-Wettbewerb", "Monster unter dem Bett", "Hausaufgaben Entschuldigung", "Taschendiebstahl", "Bildung", "Tauziehen", "'Socke rein'", "Bester Freund", "Naturliebhaber", "Katze beim Baden", "Den Kuchen nehmen", "Etwas erfinden", "Spazieren gehen", "Ein Sandwich machen", "Schlüssel verlieren", "Fluss aus Tränen", "Albtraum", "1 Stern Restaurant", "Süßigkeiten von einem Baby nehmen", "Tauchen im Pool", "Schlittschuhlaufen", "Fangen spielen", "Vorschlag falsch", "Klassenclown", "Coole Typen sehen sich keine Exposionen an", "Reinigen der Katzentoilette", "Geh von meinem Rasen runter!", "Pizzalieferdienst", "Klassenpräsentation", "Schulaufführung", "Rodeln einen Hügel hinunter", "Pool-Spiele", "Jemanden fragen", "Die Katze ist aus dem Sack", "Im Lotto gewinnen", "Trampen", "Jubiläumsgeschenk", "Erster Tag bei der Arbeit", "Dem Chef auf die Nerven gehen", "Haustier vom Lehrer", "In einen Zombie verwandeln", "Überraschungsparty", "Ein Iglu bauen", "Wahre Liebe", "Das Popcorn verbrennen", "Müllmann", "Kunstauktion", "Der Atem geht aus", "Wild West Duell", "Hitzewelle", "Ausflug", "Völkerball", "In eine Falle gehen", "Auf frischer Tat ertappt", "In einer Schleife stecken", "Des einen Müll ist des anderen Schatz", "Bankraub", "Erster Mann auf dem Mars", "Ein Taxi rufen", "Tourist in einem fremden Land", "Golf spielen", "SMS an einen Freund", "Science Fair Project", "Eltern, die ihre Kinder in Verlegenheit bringen", "Zeig und sag", "Lautes Vakuum", "Autokino", "Möwen stehlen Essen", "In der Schlange auf Wassereis warten", "Wasserpark", "Anwenden von Sonnenschutzmitteln", "In der Dusche singen", "Vogelbaby verlässt das Nest", "Ohrenschmalz", "Ausrutschen auf einer Bananenschale", "Einen Baum pflanzen", "Kundenbeschwerde", "Einen Berg besteigen", "Einen Ballon draußen loslassen", "Familienfoto", "Bart rasieren", "Surfen", "Tritt auf einen Sprung, breche deiner Mutter den Rücken", "Grinch stiehlt Weihnachten", "Schwein in einer Decke", "Aschenputtel verliert ihren Schuh", "Telefon fallen lassen", "Jemanden in den Pool schieben", "Seiltänzer", "Zauberer", "Hund hat Angst vor einem Gewitter", "Drei kleine Schweine", "Auf einem Whoopie-Kissen sitzen", "Einen Gruselfilm gucken", "Einschlafen in der Klasse", "Eine Massage bekommen", "Wütend auf deinen Wecker", "Mit dem Mund einen Ballon sprengen", "Laut essen", "Stolpern an den Schnürsenkeln", "Versuch, deine Hand in eine Pringles-Dose zu stecken", "Bananen-Telefon", "Wett-Starren", "Camping vor einem Geschäft zum Verkauf", "Ein Geschäft am schwarzen Freitag", "Festnahme widerstehen", "Schrumpfstrahl", "Eine Limo nach dem Schütteln öffnen", "Teig in die Luft werfen", "Sinkendes Schiff", "Poison Ivy Rash", "Duck Duck Goose spielen", "Auf dem Foto blinken", "Chinesische Fingerfalle", "Zwei Stufen gleichzeitig hochgehen", "Machen Sie ein Chaos mit Ihrem Essen", "Verwenden des WiFi Ihres Nachbarn", "Einen Geist sehen", "Kissenschlacht", "Jemanden unterzeichnen", "Klettern einen Fahnenmast", "Eine Bombe entschärfen", "Jemandem eine Halskette anziehen", "Entfernen der Kruste von einem Sandwich", "Protestieren", "Katze jagt einen Laserpointer", "Rülpsen das Baby", "Springen in Pfützen", "Eine Konfettikanone abfeuern", "Eine Gabel in der Straße", "Versuch einen Handstand zu machen", "Trolle unter der Brücke", "Der Boden ist Lava", "Meditation", "Mit vollem Mund reden", "Hot Dog Esswettbewerb", "Fetch mit einem Hund spielen", "Zieh deinen Mantel an", "Glühbirne wechseln", "Kino Date", "Telefon klingelt während des Unterrichts", "Staffellauf", "Barfuß auf Gras laufen", "SMS während eines Datums", "Paralleles Parken", "Schulbus Evakuierung", "Hindernisstrecke", "Bücherwurm", "Harte Taco-Schale zerfällt beim Essen", "Gegen einen Alligator", "Geheimer Händedruck", "Ins Gesicht gescheckt werden", "Socken und Sandalen", "Draufgängerischer Stunt", "Blumen pflücken", "Auf einen Lego treten", "Schneiden eines Busches", "Murmeltier sieht seinen Schatten", "Fensterputzer", "Ein Auto ins All bringen", "Doppeltauchen", "Jemandem direkt in die Augen niesen", "Schuhputzer", "Malen eines Palisadenzauns", "Gib dem Hund dein Gemüse", "Schließ die Tür an deinem Finger", "Affe, der etwas stiehlt", "Voodoo-Puppe", "Steck den Schwanz auf den Esel", "Beißen Sie Ihre Zunge", "Zittern in der Kälte", "Klempner, der ein Rohr repariert", "Jemanden auf die Schulter klopfen, der sich gegenüber befindet, wo du stehst", "Walbeobachtung", "Kissen Festung", "Schlagen Sie mit einem Baseball auf das Fenster Ihres Nachbarn", "Kaffee auf sich selbst verschütten", "Jemandes Gedanken lesen", "Kratz dir den Rücken", "Kratzmöbel für Katzen", "Schuljause", "Autohändler", "Karten im Ärmel", "Achsel furzen", "Scherzanruf", "Im Regen tanzen", "In Treibsand stecken", "Feuerübung", "Felsklettern", "Hirsch im Scheinwerferlicht", "Spähen bei einem Klassenkameraden Test", "Einem Verkäufer die Tür schließen", "Perücke fällt ab", "Himmelsleiter", "Erdbeben", "Bullenreiten", "Rinder aufrunden", "Jemandem beim Sprechen ins Gesicht spucken", "Zeichnen auf das Gesicht einer schlafenden Person", "Zerreißen Sie versehentlich Ihre Hosen", "Kind, das vorgibt, ein Erwachsener zu sein", "Jemanden ignorieren", "Statische Elektrizität", "Katzen landen immer auf ihren Füßen", "Jemandem zuwinken, der nicht auf dich gewinkt hat", "Lautstärke zu hoch", "Tag einfrieren", "Der Versuch, ein Kind auf einem Foto zum Lächeln zu bringen", "Puppenspiel", "Wasserbett", "Schutzhelme retten Leben", "Auf Komplimente aus sein", "Schütteln eines Automaten", "Einen Pullover stricken", "Gute Nacht Geschichte", "Teleportationsgerät", "Schneller als die Lichtgeschwindigkeit", "Gehen auf dünnem Eis", "Zeichensprache", "Jemandes Gesicht in einen Kuchen schieben", "Ich habe deine Nase", "Boxen stapeln" };
    
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
        IntroductionRedCircle.enabled = false;

    }

    void updateLanguage(int langnum)
    {
        gameLanguage = langnum;
        screenLanguage.text = TextArray[langnum][0];
        if (langnum == 0)
        {
            TranslationElements[22].transform.localPosition = new Vector3(-35, -94, 0);
            checkmarkholder.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (langnum == 1)
        {
            TranslationElements[22].transform.localPosition = new Vector3(70, -94, 0);
            checkmarkholder.transform.localPosition = new Vector3(17, 0, 0);
        }
        else if (langnum == 2)
        {
            TranslationElements[22].transform.localPosition = new Vector3(130, -94, 0);
            checkmarkholder.transform.localPosition = new Vector3(50, 0, 0);
        }
        else if (langnum == 3)
        {
            TranslationElements[22].transform.localPosition = new Vector3(135, -94, 0);
            checkmarkholder.transform.localPosition = new Vector3(-53, 0, 0);
        }
        else if (langnum == 4)
        {
            TranslationElements[22].transform.localPosition = new Vector3(80, -94, 0);
            checkmarkholder.transform.localPosition = new Vector3(40, 0, 0);
        }
        else if (langnum == 5)
        {
            TranslationElements[22].transform.localPosition = new Vector3(110, -94, 0);
            checkmarkholder.transform.localPosition = new Vector3(10, 0, 0);
        }
        else if (langnum == 6)
        {
            TranslationElements[22].transform.localPosition = new Vector3(110, -94, 0);
            checkmarkholder.transform.localPosition = new Vector3(-1, 0, 0);
        }
        TranslationElements[0].text = TextArray[langnum][8];
        TranslationElements[2].text = TextArray[langnum][9];
        TranslationElements[3].text = TextArray[langnum][10];
        if (gameMode == "RandomPrompt")
        {
            TranslationElements[20].text = TextArray[langnum][11];
        }
        else
        {
            TranslationElements[20].text = TextArray[langnum][12];
        }
        TranslationElements[4].text = TextArray[langnum][13];
        Debug.Log("LANGNUM: " + langnum);
        Debug.Log("GAMELENGTH: " + gameLength);
        if (gameLength == 1)
        {
            TranslationElements[21].text = TextArray[langnum][15];

        }
        else
        {

            TranslationElements[21].text = TextArray[langnum][14].Replace("TIME", (gameLength.ToString()));
            
        }
        TranslationElements[5].text = TextArray[langnum][16];
        if (gamePhases == -1)
        {
            TranslationElements[22].text = TextArray[langnum][17];
        }
        else
        {
            TranslationElements[22].text = gamePhases.ToString();
        }
        TranslationElements[6].text = TextArray[langnum][18];
        TranslationElements[7].text = TextArray[langnum][19];
        TranslationElements[8].text = TextArray[langnum][20];
        TranslationElements[9].text = TextArray[langnum][21];
        TranslationElements[10].text = TextArray[langnum][22];
        TranslationElements[12].text = TextArray[langnum][23];
        TranslationElements[13].text = TextArray[langnum][24];
        TranslationElements[15].text = TextArray[langnum][25];
        TranslationElements[16].text = TextArray[langnum][26];
        TranslationElements[11].text = TextArray[langnum][27];
        TranslationElements[14].text = TextArray[langnum][27];
        TranslationElements[17].text = TextArray[langnum][27];
        TranslationElements[18].text = TextArray[langnum][28];
        TranslationElements[19].text = TextArray[langnum][29];
        TranslationElements[23].text = TextArray[langnum][28];
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
                
                if (gameMode == "RandomPrompt" && currentPhaseNumber == 1)
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
        if (AirConsole.instance.GetControllerDeviceIds().Count == 2 || AirConsole.instance.GetControllerDeviceIds().Count == 3)
        {
            WarningText.SetActive(true);
        }
        else
        {
            WarningText.SetActive(false);
        }
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
                        foreach (string g in randomListJaggedArray[gameLanguage])
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
        if (AirConsole.instance.GetControllerDeviceIds().Count == 2 || AirConsole.instance.GetControllerDeviceIds().Count == 3)
        {
            WarningText.SetActive(true);
        }
        else
        {
            WarningText.SetActive(false);
        }
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
                if (gamePhases != -1)
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                gamePhases = -1;
                TranslationElements[22].text = TextArray[gameLanguage][17];
                actualNumberofPhases = -1;

            }
            else
            {
                
                actualNumberofPhases = Int32.Parse(finalURL);
                Debug.Log("SETTING TO " + actualNumberofPhases);
                if (gamePhases != (actualNumberofPhases - 1))
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                gamePhases = (actualNumberofPhases - 1);
                TranslationElements[22].text = (actualNumberofPhases - 1).ToString();
            }
        }
        else if (finalURL.Contains("GameHandler.mode:") && currentMode == "Menu")
        {
            
            finalURL = finalURL.Replace("GameHandler.mode:", "");
            if (finalURL == "RandomPrompt")
            {
                if (gameMode != "RandomPrompt")
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                };
                TranslationElements[20].text = TextArray[gameLanguage][11];
                gameMode = "RandomPrompt";
            }
            else
            {
                if (screenMode.text != "Write Your Own Prompt")
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                TranslationElements[20].text = TextArray[gameLanguage][12];
                gameMode = "WriteYourOwnPrompt";
            }
        }
        else if (finalURL.Contains("GameHandler.gamelength:") && currentMode == "Menu")
        {
            finalURL = finalURL.Replace("GameHandler.gamelength:", "");
            if (finalURL == "1")
            {
                if (gameLength != (Int32.Parse(finalURL)))
                {
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                //screenLength.text = (finalURL + " Minute");
                gameLength = Int32.Parse(finalURL);
                updateLanguage(gameLanguage);
            }
            else
            {
                //Debug.Log("TEST555: " + screenLength.text + " != " + (finalURL + " Minutes"));
                if (gameLength != (Int32.Parse(finalURL))) 
                {
                    //Debug.Log("NOT EQUAL");
                    audio2.clip = clips[1];
                    audio2.Play();
                }
                //screenLength.text = (finalURL + " Minutes");
                gameLength = Int32.Parse(finalURL);
                updateLanguage(gameLanguage);
            }
        }
        else if (finalURL.Contains("GameHandler.language:") && currentMode == "Menu")
        {
            finalURL = finalURL.Replace("GameHandler.language:", "");
            if (gameLanguage != Int32.Parse(finalURL))
            {
                audio2.clip = clips[1];
                audio2.Play();
                updateLanguage(Int32.Parse(finalURL));
                AirConsole.instance.SetCustomDeviceStateProperty("language", finalURL);
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
        AirConsole.instance.Message(AirConsole.instance.GetMasterControllerDeviceId(), "GameHandler.UpdatePreferences.Length:" + gameLength);
        if (gamePhases == -1)
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
            randomList = new List<string>();
            foreach (string i in randomListJaggedArray[gameLanguage])
            {
                //Debug.Log("ADDING " + i);
                randomList.Add(i);
            }
            if (gameMode == "RandomPrompt")
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


            if (gamePhases == -1)
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

                if (gameMode == "RandomPrompt")
                {
                    if (randomList.Count == 0)
                    {
                        foreach (string g in randomListJaggedArray[gameLanguage])
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
            //Debug.Log("ONE: " + PostGameHolder1.transform.localPosition.y);
            PostGameHolder1.GetComponent<Animator>().SetTrigger("TriggerAnimation1Part1");
            while (PostGameHolder1.transform.localPosition.y < 0)
            {
                //    PostGameOriginalWord.transform.parent.localPosition = (new Vector3(PostGameOriginalWord.transform.parent.localPosition.x, PostGameOriginalWord.transform.parent.localPosition.y + 2.5f, 0));
                //    PostGameHolder1.GetComponent<Animator>().SetTrigger("")
                //    PostGameHolder1.transform.localPosition = ( new Vector3(PostGameHolder1.transform.localPosition.x, PostGameHolder1.transform.localPosition.y + 2.5f, 0));
                //    PostGameHolder2.transform.localPosition = ( new Vector3(PostGameHolder2.transform.localPosition.x, PostGameHolder2.transform.localPosition.y + 2.5f, 0));
                    yield return new WaitForSeconds(0.01f);
            }
            //Debug.Log("ONE DONE: " + PostGameHolder1.transform.localPosition.y);
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
            PostGameHolder1.GetComponent<Animator>().SetTrigger("TriggerAnimation1Part2");
            while (PostGameHolder1.transform.localPosition.y < 700)
            {
                yield return new WaitForSeconds(0.01f);
            }
            //Debug.Log("ONE SUPER DONE: " + PostGameHolder1.transform.localPosition.y);
            PostGameHolder1.transform.localPosition = new Vector3(PostGameHolder1.transform.localPosition.x, -950, PostGameHolder1.transform.localPosition.z);
            //topPostGameHandler = 2;

        }
        
        else
        {
            //Debug.Log("TWO: " + PostGameHolder2.transform.localPosition.y);
            PostGameHolder2.GetComponent<Animator>().SetTrigger("TriggerAnimation2Part1");
            while (PostGameHolder2.transform.localPosition.y < -6)
            {
                //PostGameOriginalWord.transform.parent.localPosition = (new Vector3(PostGameOriginalWord.transform.parent.localPosition.x, PostGameOriginalWord.transform.parent.localPosition.y + 2.5f, 0));
                //PostGameHolder1.transform.localPosition = (new Vector3(PostGameHolder1.transform.localPosition.x, PostGameHolder1.transform.localPosition.y + 2.5f, 0));
                //PostGameHolder2.transform.localPosition = (new Vector3(PostGameHolder2.transform.localPosition.x, PostGameHolder2.transform.localPosition.y + 2.5f, 0));
                yield return new WaitForSeconds(0.01f);
                //Debug.Log("POSITION Y:: " + PostGameHolder2.transform.localPosition.y);
            }
            //Debug.Log("TWO DONE: " + PostGameHolder2.transform.localPosition.y);
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
            PostGameHolder2.GetComponent<Animator>().SetTrigger("TriggerAnimation2Part2");
            while (PostGameHolder2.transform.localPosition.y < 700)
            {
                yield return new WaitForSeconds(0.01f);
            }
            
            //Debug.Log("TWO SUPER DONE: " + PostGameHolder2.transform.localPosition.y);
            PostGameHolder2.transform.localPosition = new Vector3(PostGameHolder2.transform.localPosition.x, -950, PostGameHolder2.transform.localPosition.z);
            
        }
    }
    public IEnumerator PostGameReview()
    {
        
        for(int playerNum = 0; playerNum < AirConsole.instance.GetActivePlayerDeviceIds.Count; playerNum++)
        {
            // yield return new WaitForSeconds(10);
            int currentPlayer = playerNum;
            if (gameMode == "RandomPrompt")
            {
                PostGameOriginalWord.text = ("<b>"+jaggedArray[playerNum][0] + "</b>");
            }
            else
            {
                if (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(playerNum)) == null)
                {
                    PostGameOriginalWord0.text = ("Player "+playerNum + " "+TextArray[gameLanguage][32]);
                }
                else
                {
                    PostGameOriginalWord0.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(playerNum)) + " "+ TextArray[gameLanguage][32]);
                }
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
            PostGameOriginalWord.transform.parent.GetComponent<Animator>().SetTrigger("OriginalWordAnimationTrigger");
            yield return new WaitForSeconds(3.5f);
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

            yield return new WaitForSeconds(4);
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
        string playernick;
        if (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) != null/* && !AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)).Contains("Guest")*/)
        {
            playernick = AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num));
        }
        else
        {
            playernick = "Player " + num;
        }
        // if (postGameHandler)
        //Text targetText
            //Debug.Log("NUM1: " + num + " ----- NUM2: " + num2);
        if (jaggedArray[num][num2].Contains("data:image/png;base64"))
        {
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
            else
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
                }
                else
                {
                    PostGameImage2.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 20000f);
                }
            }

            if (postGameHandler == 1)
            {
                //PostGameImage.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 20000f);
                //Debug.Log("DRAW 1");
                //WordDrew1.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    ");
                if (gameMode == "RandomPrompt" && num2 == 1)
                {
                    WordDrew1.text = (playernick + " "+ TextArray[gameLanguage][30] +"    " + "<b>" + jaggedArray[num][0] + "</b>");
                    //WordDrew1.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew:    " + "<b>" + jaggedArray[num][0] + "</b>");
                    WordDrewWord1.text = jaggedArray[num][0];
                    
                    //Debug.Log("TESTERINO!!!!!!");
                }
                else
                {
                    //Debug.Log("ELSERINO!!!!!!!!");
                    if (num == 0)
                    {
                        WordDrew1.text = (playernick + " "+ TextArray[gameLanguage][30] + "    " + "<b>"+jaggedArray[customPlayerNumber.Length - 1][num2 - 1])+ "</b>";
                        WordDrewWord1.text = jaggedArray[customPlayerNumber.Length - 1][num2 - 1];
                    }
                    else
                    {
                        WordDrew1.text = (playernick + " "+ TextArray[gameLanguage][30] + "    " + "<b>"+jaggedArray[num - 1][num2 - 1] + "</b>");
                        WordDrewWord1.text = jaggedArray[num - 1][num2 - 1];
                    }


                }

                
            }
            else
            {
                //Debug.Log("DRAW 2");
                //WordDrew2.text = (AirConsole.instance.GetNickname(convertCustomPlayerNumberToDeviceId(num)) + " Drew: ");
                if (gameMode == "RandomPrompt" && num2 == 1)
                {
                    WordDrew2.text = (playernick + " "+ TextArray[gameLanguage][30] + "    " + "<b>"+jaggedArray[num][0]+ "</b>");
                    WordDrewWord2.text = jaggedArray[num][0];
                }
                else
                {

                    if (num == 0)
                    {
                        //Debug.LogWarning(playernick + " Drew:    " + "<b>" + jaggedArray[customPlayerNumber.Length - 1][num2 - 1] + "</b>");
                        WordDrew2.text = (playernick + " "+ TextArray[gameLanguage][30] + "    " + "<b>"+jaggedArray[customPlayerNumber.Length - 1][num2 - 1]+ "</b>");
                        WordDrewWord2.text = jaggedArray[customPlayerNumber.Length - 1][num2 - 1];
                    }
                    else
                    {
                        //Debug.LogWarning(playernick + " Drew:    " + "<b>" + jaggedArray[num - 1][num2 - 1] + "</b>");
                        WordDrew2.text = (playernick + " "+ TextArray[gameLanguage][30] + "    " + "<b>"+jaggedArray[num - 1][num2 - 1]+ "</b>");
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
                WordGuessed1.text = (playernick + " "+ TextArray[gameLanguage][31] + "    " + "<b>"+jaggedArray[num][num2] + "</b>");
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
                WordGuessed2.text = (playernick + " "+ TextArray[gameLanguage][31] + "    " + "<b>"+jaggedArray[num][num2] + " </b>");
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
            //Debug.Log("Custom # " + num + " Guessed: " + "<b>"+jaggedArray[num][num2]+ "</b>");


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
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language"+gameLanguage+"/1"); //Blank canvas image
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
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));

        if (gameLanguage == 0){
            audio2.Play();
            //IntroductionText.text = "Welcome to Drawing Evolution.";

        }
        IntroductionText.text = TextArray[gameLanguage][1];
        /*
        else if (screenLanguage.text == "Nederlands")
        {
            IntroductionText.text = "Welkom bij Drawing Evolution.";
        }
        else if (screenLanguage.text == "Italiana")
        {
            IntroductionText.text = "Benvenuto in Drawing Evolution.";
        }
        else if (screenLanguage.text == "Português")
        {
            IntroductionText.text = "Bem - vindo ao Drawing Evolution.";
        }
        else if (screenLanguage.text == "Español")
        {
            IntroductionText.text = "Bienvenido a Drawing Evolution";
        }
        else if (screenLanguage.text == "Français")
        {
            IntroductionText.text = "Bienvenue dans Drawing Evolution.";
        }
        else if (screenLanguage.text == "Deutsch")
        {
            IntroductionText.text = "Willkommen bei Drawing Evolution.";
        }*/

        yield return new WaitForSeconds(5);
        audio2.clip = clips[3];
        if (gameLanguage == 0)
        {
            audio2.Play();
        }
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        //IntroductionText.text = "The rules of this game are simple.";
        IntroductionText.text = TextArray[gameLanguage][2];
        IntroductionImage.enabled = true;
        yield return new WaitForSeconds(4);
        audio2.clip = clips[4];
        if (gameLanguage == 0)
        {
            audio2.Play();
        }
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = TextArray[gameLanguage][3];
        yield return new WaitForSeconds(1);
        IntroductionRedCircle.enabled = true;
        yield return new WaitForSeconds(4);
        IntroductionRedCircle.enabled = false;
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/2"); //Swinging at pinata image
        IntroductionImage.enabled = true;
        //AUDIO: You are given a word or phrase to draw.
        yield return new WaitForSeconds(6);
        audio2.clip = clips[5];
        if (gameLanguage == 0)
        {
            audio2.Play();
        }
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = TextArray[gameLanguage][4];
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/3"); //guessing "angry pirate" image
        //AUDIO: A player will have to guess what word or phrase you drew.
        yield return new WaitForSeconds(8);
        audio2.clip = clips[6];
        if (gameLanguage == 0)
        {
            audio2.Play();
        }
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = TextArray[gameLanguage][5];
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/4"); //Angry pirate image
        yield return new WaitForSeconds(4);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/5"); //Angry pirate image
        //AUDIO: Then, another player will have to draw the word or phrase that was guessed.
        yield return new WaitForSeconds(4);
        audio2.clip = clips[7];
        if (gameLanguage == 0)
        {
            audio2.Play();
        }
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = TextArray[gameLanguage][6];
        //AUDIO: The more players you have, the more rounds you can play, and the more your drawings can evolve.
        yield return new WaitForSeconds(4);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/6");
        yield return new WaitForSeconds(3);
        //IntroductionText.fontSize = 120;
        audio2.clip = clips[8];
        if (gameLanguage == 0)
        {
            audio2.Play();
        }
        StartCoroutine(RunMouthForSeconds(audio2.clip.length - 0.5f));
        IntroductionText.text = TextArray[gameLanguage][7];
        yield return new WaitForSeconds(3);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/IntroductionFinal1");
        yield return new WaitForSeconds(2);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/IntroductionFinal2");
        yield return new WaitForSeconds(2);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/IntroductionFinal3");
        yield return new WaitForSeconds(2);
        IntroductionImage.sprite = Resources.Load<Sprite>("Sprites/Introduction/Language" + gameLanguage + "/IntroductionFinal4");
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
            seconds = gameLength * 60;
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
                    TranslationElements[24].text = TextArray[gameLanguage][33];
                    int randomint = UnityEngine.Random.Range(0, randomDrawIntroArray[gameLanguage].Length);
                    TranslationElements[25].text = randomDrawIntroArray[gameLanguage][randomint];
                }
                else if (fadein == "Guess")
                {
                    TranslationElements[24].text = TextArray[gameLanguage][34];
                    int randomint = UnityEngine.Random.Range(0, randomGuessIntroArray[gameLanguage].Length);
                    TranslationElements[25].text = randomGuessIntroArray[gameLanguage][randomint];
                }
                else if (fadein == "InitialPhrase")
                {
                    TranslationElements[24].text = TextArray[gameLanguage][35];
                    int randomint = UnityEngine.Random.Range(0, randomInitialPhraseIntroArray[gameLanguage].Length);
                    TranslationElements[25].text = randomInitialPhraseIntroArray[gameLanguage][randomint];
                }
                else if (fadein == "PostGame")
                {
                    TranslationElements[24].text = TextArray[gameLanguage][36];
                    int randomint = UnityEngine.Random.Range(0, randomPostGameIntroArray[gameLanguage].Length);
                    TranslationElements[25].text = randomPostGameIntroArray[gameLanguage][randomint];
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
            seconds = gameLength * 60;
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



