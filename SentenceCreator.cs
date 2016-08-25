// <copyright file="SentenceCreator.cs" company="Lewis Moten">
// Copyright (c) Lewis Moten. All rights reserved.
// </copyright>

namespace LewisMoten.Sentencesmith
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A sentence creator to make new Sentences.
    /// </summary>
    public class SentenceCreator
    {
        /// <summary>
        /// Letters that are considered as vowels.
        /// </summary>
        private const string Vowels = "AEIOUY";

        /// <summary>
        /// A thread-safe singleton instance that is not read from until it is assigned.
        /// </summary>
        private static volatile SentenceCreator instance = new SentenceCreator();

        /// <summary>
        /// A synchronization object used for thread-safe operations.
        /// </summary>
        private static object synchronization = new object();

        /// <summary>
        /// A synchronization object used for thread-safe operations specific to progress reporting.
        /// </summary>
        private static object progressSynchronization = new object();

        /// <summary>
        /// A collection of sentence wordPairs.
        /// </summary>
        private Collection<SentencePart> wordPairs = new Collection<SentencePart>();

        /// <summary>
        /// A collection of sentences used to create sentence wordPairs, and identify if a newly created sentences combination is an existing sentences.
        /// </summary>
        private string[] sentences;

        /// <summary>
        /// An object used to generate random numbers.
        /// </summary>
        private Random randomizer = new Random();

        /// <summary>
        /// The current value of progress.
        /// </summary>
        private int progressValue;

        /// <summary>
        /// The maximum value that progress will allow.
        /// </summary>
        private int progressMax;

        /// <summary>
        /// The last value reported between 0 and 100.
        /// </summary>
        private int progressLastValue;

        /// <summary>
        /// A collection of Sentences that have been created.
        /// </summary>
        private Collection<string> createdSentences = new Collection<string>();

        /// <summary>
        /// Prevents a default instance of the <see cref="SentenceCreator"/> class from being created.
        /// </summary>
        private SentenceCreator() 
        {
        }

        /// <summary>
        /// Occurs when [progress changed].
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Gets a thread-safe singleton instance.
        /// </summary>
        public static SentenceCreator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (synchronization)
                    {
                        if (instance == null)
                        {
                            instance = new SentenceCreator();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the last set of Sentences created by this instance.
        /// </summary>
        public Collection<string> Sentences
        {
            get
            {
                lock (synchronization)
                {
                    return this.createdSentences;
                }
            }
        }

        /// <summary>
        /// Starts reading a sentence from a text file.
        /// </summary>
        /// <param name="path">The file to open for reading a book.</param>
        /// <param name="options">The options.</param>
        public void StartReadingSentences(string path, ParallelOptions options)
        {
            lock (progressSynchronization)
            {
                this.progressValue = 0;
                this.progressMax = 100;
                this.progressLastValue = -1;
                this.OnProgressChanged(0);
            }

            this.Load(path);

            lock (progressSynchronization)
            {
                this.progressMax = this.sentences.Length;
            }

            try
            {
                Parallel.ForEach(
                    this.sentences, 
                    options, 
                    sentence =>
                {
                    if (!options.CancellationToken.IsCancellationRequested)
                    {
                        this.ParseSentence(sentence);
                        lock (progressSynchronization)
                        {
                            progressValue++;
                            OnProgressChanged(progressValue * 100 / progressMax);
                        }
                    }
                });
            }
            catch (OperationCanceledException)
            {
            }
            catch (AggregateException)
            {
            }
        }

        /// <summary>
        /// Starts creating new Sentences.
        /// </summary>
        /// <param name="wordCount">The wordCount.</param>
        /// <param name="count">The count.</param>
        /// <param name="isRare">if set to <c>true</c> [is rare].</param>
        /// <param name="options">The options.</param>
        public void StartCreatingSentences(int wordCount, int count, bool isRare, ParallelOptions options)
        {
            if (this.wordPairs.Count == 0)
            {
                return;
            }

            lock (progressSynchronization)
            {
                this.progressValue = 0;
                this.progressMax = count;
                this.progressLastValue = -1;
                this.OnProgressChanged(0);
            }

            this.createdSentences.Clear();

            try
            {
                Parallel.For(
                    0, 
                    count, 
                    options, 
                    (i) =>
                {
                    if (options.CancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    bool isCreated = false;
                    string sentence = string.Empty;
                    do
                    {
                        sentence = SentenceCreator.Instance.CreateSentence(wordCount, isRare);

                        lock (synchronization)
                        {
                            if (!createdSentences.Any(newSentence => newSentence == sentence))
                            {
                                createdSentences.Add(sentence);
                                isCreated = true;
                            }
                        }

                        if (options.CancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                    } 
                    while (!isCreated);

                    lock (progressSynchronization)
                    {
                        progressValue++;
                        OnProgressChanged(progressValue * 100 / progressMax);
                    }
                });
            }
            catch (OperationCanceledException)
            {
            }
            catch (AggregateException)
            {
            }
        }

        /// <summary>
        /// Loads a list of Sentences from the specified path.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        private void Load(string path)
        {
            string text = System.IO.File.ReadAllText(path);
            text = text.ToUpperInvariant();
            text = text.Replace('\r', ' ');
            text = text.Replace('\n', ' ');
            StringBuilder sb = new StringBuilder();
            sb.Append(text.Where(c => "ABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890\r\n.?!".IndexOf(c) != -1).ToArray());
            text = sb.ToString();

            this.sentences = text.Split(new char[] { '.', '?', '!'}, StringSplitOptions.RemoveEmptyEntries);
            //this.sentences = this.sentences.Select(s => Regex.Replace(s, @"^\s*[\d]+\s+", "")).ToArray();
            //this.sentences = this.sentences.Select(s => Regex.Replace(s, @"^\s*CHAPTER\s+[\d]+\s*$", "")).ToArray();
            this.sentences = this.sentences.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();            
        }

        /// <summary>
        /// Parses the sentence.
        /// </summary>
        /// <param name="sentence">The sentence to parse.</param>
        private void ParseSentence(string sentence)
        {
            var words = sentence.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 2) return;

            string lastWord = string.Empty;
            foreach(var word in words)
            {
                bool canBeLast = word == words.Last();
                bool canBeSecondLast = word == words[words.Length - 2];

                lock (progressSynchronization)
                {
                    SentencePart found = this.wordPairs.FirstOrDefault(part => part.Text == lastWord);

                    if (found != null)
                    {
                        found.Add(word, canBeLast, canBeSecondLast);
                    }
                    else
                    {
                        this.wordPairs.Add(new SentencePart(lastWord, word, canBeLast, canBeSecondLast));
                    }
                }

                lastWord = word;
            }
        }

        /// <summary>
        /// Creates a new sentence.
        /// </summary>
        /// <param name="wordCount">The wordCount.</param>
        /// <param name="isRare">if set to <c>true</c> [is rare].</param>
        /// <returns>A new sentence.</returns>
        private string CreateSentence(int wordCount, bool isRare)
        {
            string lastPart = string.Empty;
            StringBuilder sb = new StringBuilder();
            string createdSentence = string.Empty;

            for (var i = 0; i < wordCount; i++)
            {
                bool isLast = i == wordCount - 1;
                bool isSecondLast = i == wordCount - 2;
                lastPart = this.NextPart(lastPart, isRare, isLast, isSecondLast);
                
                if (lastPart == string.Empty)
                {
                    sb.Clear();
                    i = -1;
                    continue;
                }

                if (i != 0)
                {
                    sb.Append(' ');
                }
                sb.Append(lastPart);

                if (i == wordCount - 1)
                {
                    createdSentence = sb.ToString();
                    bool isDuplicate = false;
                    
                    lock (progressSynchronization)
                    {
                        isDuplicate = this.Sentences.Any(s => s == createdSentence);
                    }

                    if (isDuplicate)
                    {
                        sb.Clear();
                        i = -1;
                        lastPart = string.Empty;
                    }
                }
            }

            return createdSentence;
        }

        /// <summary>
        /// Suggests the next part to add onto a sentence.
        /// </summary>
        /// <param name="previousPart">The previous part.</param>
        /// <param name="isRare">if set to <c>true</c> [is rare].</param>
        /// <param name="canBeLast">part can be a terminator.</param>
        /// <returns>The next part to add onto a sentence.</returns>
        private string NextPart(string previousPart, bool isRare, bool isLast, bool isSecondLast)
        {
            var availableParts =
                from part in this.wordPairs
                where part.Text == previousPart
                && (part.CanBeLast == isLast || !isLast)
                && (part.CanBeSecondLast == isSecondLast || !isSecondLast)
                orderby part.Occurrences descending
                select part;

            int chances = availableParts.Sum(part => part.Occurrences);

            if (chances == 0)
            {
                return string.Empty;
            }

            int chance = this.randomizer.Next(0, chances - 1);

            var nextParts = availableParts.First().Next;
            int rareSkip = nextParts.Count() - 1;

            foreach (var part in nextParts)
            {
                if (chance < part.Occurrences)
                {
                    // chance covers the existing part
                    if (isRare)
                    {
                        // we want to go for rare combinations
                        return nextParts.Skip(rareSkip).First().Text;
                    }
                    else
                    {
                        // go with more common combination
                        return part.Text;
                    }
                }

                chance -= part.Occurrences;
                rareSkip--;
            }

            return nextParts.Last().Text;
        }

        /// <summary>
        /// Called when [progress changed].
        /// </summary>
        /// <param name="progressPercentage">The progress percentage.</param>
        private void OnProgressChanged(int progressPercentage)
        {
            if (this.ProgressChanged == null)
            {
                return;
            }

            lock (progressSynchronization)
            {
                if (progressPercentage == this.progressLastValue)
                {
                    return;
                }

                this.progressLastValue = progressPercentage;
            }

            ProgressChangedEventArgs e = new ProgressChangedEventArgs(progressPercentage, null);
            this.ProgressChanged(this, e);
        }
    }
}
