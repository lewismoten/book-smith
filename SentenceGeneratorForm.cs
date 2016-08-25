// <copyright file="SentenceGeneratorForm.cs" company="Lewis Moten">
// Copyright (c) Lewis Moten. All rights reserved.
// </copyright>

namespace LewisMoten.Sentencesmith
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using LewisMoten.Sentencesmith.Properties;
    using System.IO;

    /// <summary>
    /// Form to display interface to generate Sentences.
    /// </summary>
    public partial class SentenceGeneratorForm : Form
    {
        /// <summary>
        /// A task to be executed in the background.
        /// </summary>
        private Task task;

        /// <summary>
        /// The source of a token to trigger a background task to be cancelled.
        /// </summary>
        private CancellationTokenSource tokenSource;

        /// <summary>
        /// The sentence creator to create new Sentences.
        /// </summary>
        private SentenceCreator creator = SentenceCreator.Instance;

        /// <summary>
        /// Flag to remember that book list has been loaded.
        /// </summary>
        private bool gotBooks;

        /// <summary>
        /// Flag to remember that a book has been loaded.
        /// </summary>
        private bool gotBook = false;

        /// <summary>
        /// Name of the currently loaded book.
        /// </summary>
        private string bookName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SentenceGeneratorForm"/> class.
        /// </summary>
        public SentenceGeneratorForm()
        {
            this.InitializeComponent();
            this.creator.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Creator_ProgressChanged);
        }

        /// <summary>
        /// Handles the ProgressChanged event of the creator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void Creator_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { this.progressBar.Value = e.ProgressPercentage; }));
            }
            else
            {
                this.progressBar.Value = e.ProgressPercentage;
            }
        }

        /// <summary>
        /// Reads the Sentences.
        /// </summary>
        private void ReadBook()
        {
            this.statusLabel.Text = Resources.StatusLoading;

            this.taskTimer.Enabled = true;
            this.taskTimer.Tag = "ReadBook";

            ParallelOptions options = this.CreateParallelOptions();

            this.cancelButton.Enabled = true;
            this.task = Task.Factory.StartNew(
                () =>
                {
                    creator.StartReadingSentences(Path.Combine(@"Books", string.Format("{0}.txt", this.bookName)), options);
                },
                this.tokenSource.Token);
        }

        /// <summary>
        /// Creates the parallel options.
        /// </summary>
        /// <returns>The parallel options.</returns>
        private ParallelOptions CreateParallelOptions()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            this.tokenSource = source;
            this.components.Add(new DisposeActionComponent(delegate 
                {
                    if (source != null) 
                    {
                        source.Dispose();
                        source = null;
                    }
                }));
            ParallelOptions options = new ParallelOptions();
            options.CancellationToken = this.tokenSource.Token;
            options.CancellationToken.ThrowIfCancellationRequested();
            options.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            return options;
        }

        /// <summary>
        /// Handles the Click event of the createButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            bool isRare = this.rareCheckBox.Checked;
            int length = (int)this.lengthNumericUpDown.Value;
            int count = (int)this.BookCountNumericUpDown.Value;

            this.copyButton.Enabled = false;
            this.createButton.Enabled = false;
            this.BookListBox.DataSource = null;

            this.statusLabel.Text = Resources.StatusCreating;

            this.taskTimer.Enabled = true;
            this.taskTimer.Tag = "CreateBooks";

            ParallelOptions options = this.CreateParallelOptions();

            this.task = Task.Factory.StartNew(
                () =>
                {
                    this.creator.StartCreatingSentences(length, count, isRare, options);
                },
                this.tokenSource.Token);
        }

        /// <summary>
        /// Handles the Shown event of the SentenceGeneratorForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BookGeneratorForm_Shown(object sender, EventArgs e)
        {
            if (this.gotBooks) 
            {
                return; 
            }

            this.gotBooks = true;
            this.gotBook = false;

            this.bookComboBox.SelectedIndex = -1;
            this.bookComboBox.Items.Clear();
            var books = Directory.GetFiles("Books", "*.txt");
            foreach (var book in books)
            {
                this.bookComboBox.Items.Add(Path.GetFileNameWithoutExtension(book));
            }
        }

        /// <summary>
        /// Handles the Click event of the copyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (this.creator.Sentences.Count == 0)
            {
                return;
            }

            string[] sorted = this.creator.Sentences.OrderBy(Book => Book).ToArray();
            Clipboard.SetText(string.Join(Environment.NewLine, sorted));
        }

        /// <summary>
        /// Handles the Tick event of the taskTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TaskTimer_Tick(object sender, EventArgs e)
        {
            if (this.task == null)
            {
                return;
            }

            if (this.task.IsCompleted)
            {
                this.taskTimer.Enabled = false;
                this.cancelButton.Enabled = false;
                this.progressBar.Value = 0;

                this.task.Dispose();
                this.task = null;
                
                switch (this.taskTimer.Tag.ToString())
                {
                    case "ReadBooks":
                        break;
                    case "CreateBooks":
                        this.statusLabel.Text = Resources.StatusDisplaying;
                        this.BookListBox.DataSource = this.creator.Sentences.OrderBy(Book => Book).ToArray();
                        break;
                    default:
                        break;
                }

                this.copyButton.Enabled = this.creator.Sentences.Count != 0;
                this.createButton.Enabled = true;
                this.statusLabel.Text = Resources.StatusReady;
            }
        }

        /// <summary>
        /// Handles the Click event of the cancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = Resources.StatusCancelled;
            this.tokenSource.Cancel();
            this.cancelButton.Enabled = false;
        }

        private void readBookButton_Click(object sender, EventArgs e)
        {
            if (this.bookComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Must select a book");
                return;
            }

            this.gotBook = true;
            this.bookName = (string)this.bookComboBox.SelectedItem;

            this.ReadBook();
        }
    }
}
