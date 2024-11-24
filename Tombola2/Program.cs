// inizio 23/11 alle 21:35
// interruzione 23/11 alle 22:55
// ripresa 24/11 alle 9:00
// fine 24/11 alle 12:17 (TERMINATE LE 4 ORE MENTRE COMMENTAVO alle 11:35! completato alle 12:15)
string REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
string NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

Console.Clear();

Console.WriteLine();
Console.WriteLine($"{REVERSE}BENVENUTO ALLA TOMBOLA!{NOREVERSE}");
Console.WriteLine();

Tombola();

// Creo una funzione Tombola in modo da poter far giocare nuovamente l'utente senza far riapparire il messaggio di avvio
static void Tombola()
{
    /*
    La funzione Tombola contiene il richiamo alla funzione CheckVictory, che richiede come argomenti i valori ritornati dalle funzioni ChooseNumbers (la 
    lista dei numeri scelti dall'utente) e Estraction (la lista dei numeri estratti randomicamente dal programma), quest'ultima richiede il ritorno dalla
    funzione ChooseDifficulty (il numero di volte che verrà estratto un numero per l'estrazione)    
     */
    CheckVictory(ChooseNumbers(), Extraction(ChooseDifficulty()));
}
// La funzione ChooseNumbers si occupa di far scegliere all'utente i numeri della sua scheda
static List<int> ChooseNumbers()
{
    // La lista che conterrà i numeri scelti dall'utente
    List<int> userNumbers = [];
    // Il numero massimo di numeri che l'utente potrà scegliere
    int MAX_USER_INPUTS = 10;

    Console.WriteLine("Inserisci " + MAX_USER_INPUTS + " numeri tra 1 e 100: ");
    // Il ciclo for si occuperà di iterare la richiesta di numeri all'utente, per il numero di volte scelto da me
    for (int i = 0; i < MAX_USER_INPUTS; i++)
    {
        // Il try si assicura che il programma non si blocchi in caso l'utente inserisca qualcosa di diverso da un numero
        try
        {
            // Il numero scelto dall'utente viene letto come stringa e trasformato in numero usando la funzione Parse di int
            int userInput = int.Parse(Console.ReadLine());
            // Questo controllo si assicura che l'utente non inserisca un numero minore di 0 o superiore a 100.
            // se la condizione è rispettata, l'input viene inserito nella lista e cancella l'ultimo numero confermato dal terminale
            if (userInput > -1 && userInput <= 100)
            {
                userNumbers.Add(userInput);
                ClearLastLine();
            }
            else
            {
                // se la condizione dell'if non è rispettata, avvisa l'utente e diminuisce il contatore per garantire il corretto numero di iterazioni
                Console.WriteLine("Inserire un numero intero tra 1 e 100");
                i--;
            }
        }
        catch (Exception)
        {
            // se l'utente non inserisce un numero, evita il blocco e diminuisce il contatore
            Console.WriteLine("Inserire un numero intero tra 1 e 100");
            i--;
        }
    }
    // stampa a video il contenuto della lista, dividendo i numeri con una virgola, mentre l'ultimo viene seguito da un punto
    for (int i = 0; i < userNumbers.Count; i++)
    {
        if ((i + 1) < userNumbers.Count)
        {
            Console.Write(userNumbers[i].ToString() + ", ");
        }
        else
        {
            Console.WriteLine(userNumbers[i].ToString() + ".");
        }
    }
    // ritorna la lista perché venga usata dalla funzione CheckVictory
    return userNumbers;
}


// La funzione ChooseDifficulty permette all'utente di scegliere la difficoltà del gioco, modificando il numero di valori estratto per il gioco.
static int ChooseDifficulty()
{   // un array ddi stringhe contiene le tre difficoltà tra cui l'utente potrà scegliere, un foreach le stampa a video
    string[] Difficulty = { "(F)acile ", "(M)edio", "(D)ifficile" };

    foreach (string i in Difficulty)
    {
        Console.WriteLine(i);
    }

    Console.Write("Seleziona la difficoltà: ");
    // lo switch riceve l'input dell'utente dopo essermi assicurato che fosse un carattere minuscolo e, se è tra le scelte possibili,
    // ritorna il numero corrispondente, necessario alla funione Extaction, altrimenti ritorna il metodo ChooseDifficulty
    // per permettere all'utente di fare una scelta corretta.
    switch (Console.ReadLine().ToLower())
    {
        // i case ritornano 
        case "f":
            return 70;
        case "m":
            return 40;
        case "d":
            return 20;
        default:
            Console.WriteLine("Scelta non riconosciuta, riprova");
            return ChooseDifficulty();
    }
}
// Il metodo Extraction riceve il valore ritornato da ChooseDifficulty e, in base a quello, estrae i numeri che verranno confrontati con quelli
// scelti dall'utente
static List<int> Extraction(int numbersToExtract)
{
    List<int> listOfExtractedNumbers = [];
    int extractedNumber;

    Random rnd = new Random();
    // estrae i numeri della tombola in base alla difficolà scelta, controlla che il numero non sia stato già estratto e lo inserisce nella lista
    for (int i = 0; i < numbersToExtract; i++)
    {
        extractedNumber = rnd.Next(1, 101);

        if (listOfExtractedNumbers.Contains(extractedNumber))
        {
            continue;
        }
        listOfExtractedNumbers.Add(extractedNumber);
    }
    // ritorna la lista dei numeri in modo che venga usata da CheckVictory
    return listOfExtractedNumbers;
}
// CheckVictory confronta la lista di numeri scelti dall'utente e quella di numeri estratti, dichiara la vittoria o la sconfitta e si occupa di riavviare
// o chiudere il gioco
static void CheckVictory(List<int> userNumbers, List<int> extractedNumbers)
{
    List<int> winningNumbers = [];
    string playAgain;
    // confronta le due liste e i numeri presenti in entrambe li inserisce in una nuova lista che contiene i numeri vincenti
    foreach (int number in userNumbers)
    {
        if (extractedNumbers.Contains(number))
        {
            winningNumbers.Add(number);
        }
    }
    // lo swich verifica il numero di elementi dentro la lista e stampa a video un messaggio di vittoria o di sconfitta
    switch (winningNumbers.Count)
    {
        case 3:
            Console.WriteLine("Hai fatto terna!");
            break;
        case 4:
            Console.WriteLine("Hai fatto quaterna!");
            break;
        case > 4:
            Console.WriteLine("Hai fatto cinquina!");
            break;
        default:
            Console.WriteLine("Hai perso...");
            break;
    }
    // stampa a video i numeri vincenti se la lista ha almeno 3 elementi
    if (winningNumbers.Count > 2)
    {
        Console.Write("I numeri vincenti sono: ");
        for (int i = 0; i < winningNumbers.Count; i++)
        {
            if ((i + 1) < winningNumbers.Count)
            {
                Console.Write(winningNumbers[i].ToString() + ", ");
            }
            else
            {
                Console.WriteLine(winningNumbers[i].ToString() + "!");
            }
        }
    }
    // chiede all'utente se vuole giocare ancora
    do
    {
        Console.Write("Vuoi giocare ancora (S/N)? ");
        playAgain = Console.ReadLine().ToLower();

    } while (playAgain != "s" && playAgain != "n");
    // si: pulisce il terminale e fa ripartire il programma
    if (playAgain == "s")
    {
        ClearAll();
        Tombola();
    }
    else
    {
        // no: pulisce il terminale, stampa un messaggio di saluto e chiude l'applicazione
        ClearAll();
        Console.WriteLine("Grazie per aver giocato!");
        Environment.Exit(0);
    }


}
// cancella l'ultima riga scritta su terminale
static void ClearLastLine()
{
    Console.SetCursorPosition(0, Console.CursorTop - 1);
    Console.Write(new string(' ', Console.BufferWidth));
    Console.SetCursorPosition(0, Console.CursorTop);
}
// cancella tutto sul teminale fino a sotto il messaggio di benvenuto
static void ClearAll()
{
    do
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.CursorTop);

    } while (Console.GetCursorPosition().Top > 3);

}