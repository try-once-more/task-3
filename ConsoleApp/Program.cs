﻿using System;
using System.Linq;
using FileSystemVisitor;
using FileSystemVisitor.Interfaces;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string input;
            while (ReadImput())
            {
                try
                {
                    var visitor = FileSystemVisitor.FileSystemVisitor.CreateInstance(input);
                    visitor.Start += OnStart;
                    visitor.Finish += OnFinish;
                    visitor.DirectoryFinded += OnDirectoryFinded;
                    visitor.FileFinded += OnFileFinded;
                    WriteLineCenter($"Found: {visitor.Search().Count()} item(s)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            bool ReadImput()
            {
                Console.WriteLine("Write full path to base catalog or exit:");
                input = Console.ReadLine().Trim();
                return !string.Equals(input, "exit", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private static void OnStart(object sender, EventArgs eventArgs)
        {
            if (sender is IFileSystemVisitor visitor)
            {
                WriteLineCenter($"Start search in {visitor.Path}");
            }
        }

        private static void OnFinish(object sender, EventArgs eventArgs)
        {
            if (sender is IFileSystemVisitor visitor)
            {
                WriteLineCenter($"Finish search in {visitor.Path}");
            }
        }

        private static void OnDirectoryFinded(object sender, EventArgs eventArgs)
        {
            if (eventArgs is FileSystemVisitorEventArgs e)
            {
                Console.WriteLine(e.Path);
            }
        }

        private static void OnFileFinded(object sender, EventArgs eventArgs)
        {
            if (eventArgs is FileSystemVisitorEventArgs e)
            {
                Console.WriteLine(e.Path);
            }
        }

        private static void WriteLineCenter(string text)
        {
            int centerX = (Console.WindowWidth / 2) - (text.Length / 2);
            Console.SetCursorPosition(centerX, Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
