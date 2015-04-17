using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        //inistalize global variables under here 
        static string usersName = string.Empty;
        //all the possible words to pick from
        static List<string> possibleWords = new List<string> { "TURTLE", "DATSUN", "SPACEDONKEY", "NICKLEBACK", "DOGS", "DOLPHINS", "PICKLE","WAYNE","BABYBIRD"};
        //a generic Random for selecting word from list
        static Random rng = new Random();
        //output of word randomizer or the word that is being guessed
        static string actualWord = string.Empty;
        //counter for lives or guesses remaining 
        static int counterLives = 7;
        //boolean value to loop and set false to end game
        static bool playing = true;
        //global string for the hidden version of the word being guessed
        static string hiddenVersionWord = string.Empty;
        //validated user input
        static string validUserInput = string.Empty;
        //string for guesses incremented
        static string guessesIncrement = string.Empty;
        //string for spaced word
        static string spacedActualWord = string.Empty;


        static void Main(string[] args)
        {
            //Start startScreen
            StartScreen();
            //pick random word from possibleWords
            WordRanomizer(possibleWords);
            MakeWordHidden(actualWord);
            MakeWordSpace(actualWord);
            //start actual game with a while loop with playing as argument
            while (playing == true)
            {
                if (hiddenVersionWord == spacedActualWord)
                {
                    Console.WriteLine("YOU WIN!");
                    playing = false;
                    Console.ReadKey();
                    return;

                }
                //display Lives, underscored word, guesses, and prompt for guess
                //lives displayed
                Console.WriteLine("Lives Left: " + counterLives);
                Console.WriteLine("");
                //hidden version of word
                Console.WriteLine(hiddenVersionWord);
                Console.WriteLine("");
                //guesses made 
                Console.WriteLine("Guesses: " + guessesIncrement);
                Console.WriteLine("");
                ValidateInput(Console.ReadLine());
                System.Threading.Thread.Sleep(500);
                //clear to make the screen look static
                Console.Clear();      
                //lives is not 0
                if (counterLives != 0)
                {

                    
                    if (validUserInput == actualWord)
                    {
                        ChangeHiddenWord(hiddenVersionWord);
                    }
                    else if (IsGuessALetter(validUserInput))
                    {
                        ChangeHiddenWord(hiddenVersionWord);
                    }
                    else
                    {
                        Console.WriteLine("Nope!");
                        System.Threading.Thread.Sleep(500);
                        Console.Clear();
                        counterLives--;
                    }
                }
                //user losses
                else
                {
                    Console.Clear();
                    Console.WriteLine("                             Sorry you lose...                                  ");
                    Console.ReadKey();
                    //end loop
                    playing = false;
                }
  
            }
        }

        /// <summary>
        /// turns acctual word into comparable version for hidden word
        /// </summary>
        /// <param name="input">acctual word</param>
        /// <returns></returns>
        static void MakeWordSpace(string input)
        {
            List<char> tempActualList = new List<char> { };
            for (int i = 0; i < input.Length; i++)
            {
                tempActualList.Add(input[i]);
            }
            tempActualList.ToArray();
            string tempActualstring = string.Join(" ", tempActualList);
            spacedActualWord = tempActualstring;
        }
        /// <summary>
        /// changes the hidden word
        /// </summary>
        /// <returns>the hidden word</returns>
        public static void ChangeHiddenWord(string hiddenWord)
        {
            //make hidden word an array
            string[] hiddenWordArray = hiddenVersionWord.Split(' ');
            //checks if input is a correct letter or the word
            if (IsGuessALetter(validUserInput))
            {
                //if validUserInput is the word make all chars unhidden
                if (validUserInput == actualWord)
                {
                    for (int i = 0; i < actualWord.Length; i++)
                    {
                        hiddenWordArray[i].Replace('_', actualWord[i]);
                    }
                    hiddenVersionWord = string.Join(" ", hiddenWordArray);
                }
                else
	            {

	            
                    //loop through the actual word
                    for (int i = 0; i < actualWord.Length; i++)
                    {
                        //if the input is a letter and equals 
                        if (validUserInput[0] == actualWord[i])
                        {
                            hiddenWordArray[i] = validUserInput[0].ToString();
                        }
                    }
                    hiddenVersionWord = string.Join(" ", hiddenWordArray); 
                }
            }
        }
        
        /// <summary>
        /// Checks if the user's guess is a letter in the word
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static bool IsGuessALetter(string userInput)
        {
            //create string to hold all chars of userinput
            string allUserChars = string.Empty;
            //loop through user input to get all letters
            for (int i = 0; i < userInput.Length; i++)
            {
                allUserChars += userInput[i];
            }
            //check if any user input is contained in actual word
            if (actualWord.Contains(allUserChars))
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// adds to guesses
        /// </summary>
        /// <param name="userInput">the validUserGuess</param>
        public static void GuessesIncrement (string userInput) 
        {
            //take user's guess and add it to a string with a space
            guessesIncrement += userInput;
        }
        
        
        /// <summary>
        /// Makes word hidden by underscores and spaces
        /// </summary>
        /// <param name="word">takes in the word to be guessed</param>
        public static void MakeWordHidden(string word)
        {
            //create a temporary string to hold the hidden word
            string tempHiddenWord = string.Empty;
            //create a loop for every letter in the word to place a _ and space for each letter
            for (int i = 0; i < word.Length; i++)
            {
                tempHiddenWord += "_ ";
            }
            //place tempHiddenWord into a global string
            hiddenVersionWord = tempHiddenWord;

        }

        /// <summary>
        /// Validates that user input is a valid key
        /// </summary>
        /// <param name="userInput"></param>
        public static void ValidateInput(string userInput)
        {
            //check to see if input is valid
            if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(userInput.ToUpper()))
            {
                //user input is valid
                //only accept letters or the full word
                if (userInput.Length == 1 || userInput.Length == actualWord.Length)
                {
                    validUserInput = userInput.ToUpper();
                    GuessesIncrement(validUserInput);
                }
                else
                {
                    Console.WriteLine("Key is invalid");
                }
            }
            else
            {
                //user's input is invalid
                Console.WriteLine("Key is invalid");
            }
        }

        /// <summary>
        /// picks a random word from a word bank
        /// </summary>
        /// <param name="wordBank">a list of words</param>
        public static void WordRanomizer(List<string> wordBank)
        {
            //get random number between 0 and the count of wordBank
            int randomNumber = rng.Next(0, wordBank.Count);
            //set random int at index of wordbank to a string value
            actualWord = wordBank[randomNumber];
        }

        static void BlankPlayScreen()
        {
                //display Lives, underscored word, guesses, and prompt for guess
                //lives displayed
                Console.WriteLine("Lives Left: ");
                //hidden version of word
                Console.WriteLine(hiddenVersionWord);
                //guesses made 
                Console.WriteLine("Guesses: ");
                ValidateInput(Console.ReadLine());
                System.Threading.Thread.Sleep(1000);
                //clear to make the screen look static
                Console.Clear();
        }

        /// <summary>
        /// Displays two screens first asking for user's name second insturting user
        /// </summary>
        static void StartScreen()
        {
            //welcome screen
            Console.WriteLine("                 Welcome to Hangman what is your name user?                     ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("                                     ");
            usersName = Console.ReadLine();
            Console.Clear();
            //instruction screen
            Console.WriteLine("                               Welcome {0}!                                     ", usersName);
            Console.WriteLine("");
            Console.WriteLine("          You have 8 chances to guess the word using either a letter            ");
            Console.WriteLine("                        or by trying to guess the word                          ");
            Console.WriteLine("                         Press any key to continue...                           ");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
