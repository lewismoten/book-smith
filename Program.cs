// <copyright file="Program.cs" company="Lewis Moten">
// Copyright (c) Lewis Moten. All rights reserved.
// </copyright>

namespace LewisMoten.Sentencesmith
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The main program that an executable program will enter through.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SentenceGeneratorForm());
        }
    }
}
