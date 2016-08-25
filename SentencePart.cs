// <copyright file="SentencePart.cs" company="Lewis Moten">
// Copyright (c) Lewis Moten. All rights reserved.
// </copyright>

namespace LewisMoten.Sentencesmith
{
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// A sentence part.
    /// </summary>
    public class SentencePart
    {
        /// <summary>
        /// A collection of sentence wordPairs that follow this one.
        /// </summary>
        private Collection<SentencePart> next;

        /// <summary>
        /// Initializes a new instance of the <see cref="SentencePart"/> class.
        /// </summary>
        /// <param name="text">The wordPairs text.</param>
        /// <param name="nextText">The next text.</param>
        /// <param name="canBeLast">part can be a terminator.</param>
        public SentencePart(string text, string nextText, bool canBeLast, bool canBeSecondLast)
            : this(text, canBeLast, canBeSecondLast)
        {
            this.next = new Collection<SentencePart>();
            this.Next.Add(new SentencePart(nextText, canBeLast, canBeSecondLast));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SentencePart"/> class.
        /// </summary>
        /// <param name="text">The wordPairs text.</param>
        /// <param name="canBeLast">part can be a terminator.</param>
        public SentencePart(string text, bool canBeLast, bool canBeSecondLast)
        {
            this.Text = text;
            this.Occurrences = 1;
            this.CanBeLast = canBeLast;
            this.CanBeSecondLast = canBeSecondLast;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The wordPairs text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the occurrences.
        /// </summary>
        /// <value>
        /// The occurrences.
        /// </value>
        public int Occurrences { get; set; }

        /// <summary>
        /// Indicates that the part may be considered as a terminator.
        /// </summary>
        public bool CanBeLast { get; set; }

        public bool CanBeSecondLast { get; set; }

        /// <summary>
        /// Gets the next sentence part.
        /// </summary>
        public Collection<SentencePart> Next
        {
            get
            {
                return this.next;
            }
        }

        /// <summary>
        /// Adds the specified next text.
        /// </summary>
        /// <param name="nextText">The next text.</param>
        /// <param name="canBeLast">part can be a terminator.</param>
        public void Add(string nextText, bool canBeLast, bool canBeSecondLast)
        {
            this.Occurrences++;
            SentencePart found = this.next.FirstOrDefault(part => part.Text == nextText);
            if (found == null)
            {
                this.Next.Add(new SentencePart(nextText, canBeLast, canBeSecondLast));
            }
            else
            {
                found.Occurrences++;
                if (canBeLast)
                {
                    found.CanBeLast = true;
                }
                if (canBeSecondLast)
                {
                    found.CanBeSecondLast = true;
                }
            }
        }
    }
}
