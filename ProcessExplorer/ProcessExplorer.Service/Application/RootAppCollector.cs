using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace ProcessExplorer.Service.Application
{
    public abstract class RootAppCollector
    {
        protected readonly ILoggerWrapper _logger;
        protected readonly ISessionService _sessionService;
        protected readonly IDateTime _dateTime;

        /// <summary>
        /// Standard app title separator
        /// </summary>
        private static string STANDARD_APP_SEPARATOR = " - ";
        private static string UKNKOWN_APP = "UNKNOWN";

        public RootAppCollector(ILoggerWrapper logger,
            ISessionService sessionService,
            IDateTime dateTime)
        {
            _logger = logger;
            _sessionService = sessionService;
            _dateTime = dateTime;
        }

        public abstract string PlatformSpecificTitleHandler(string fullTitle, string processName);

        /// <summary>
        /// Handler logic for application titles (make them readable)
        /// </summary>
        /// <param name="fullTitle"></param>
        /// <param name="processName"></param>
        /// <returns></returns>
        public string GetBasicApplicationTitle(string fullTitle, string processName)
        {
            //if title null or empty return empty string
            if (string.IsNullOrEmpty(fullTitle))
                return UKNKOWN_APP;

            //Format example: D:\\test\\test\\hello.exe
            //get only hello.exe
            if (!fullTitle.Contains(STANDARD_APP_SEPARATOR) && fullTitle.Contains(Path.DirectorySeparatorChar))
                return fullTitle.Split(Path.DirectorySeparatorChar).Last();

            //Format example: DB Browser - D:\\test\\test\\tekst.db
            //Format example: D:\\test\\test\\tekst.db - Notepad++
            //Get DB Browser
            if (fullTitle.Contains(STANDARD_APP_SEPARATOR) && fullTitle.Contains(Path.DirectorySeparatorChar))
                return FormatName(fullTitle);

            //Format example: Unit test - Google Chrome
            //Get Google Chrome
            if (fullTitle.Contains(STANDARD_APP_SEPARATOR))
                return fullTitle.Split(STANDARD_APP_SEPARATOR).Select(i => i.Trim()).Last();

            //Handle platform specific title
            return PlatformSpecificTitleHandler(fullTitle, processName);
        }


        /// <summary>
        /// Make first letter of word uppercase, other chars lowercase
        /// 
        /// Input: explorer
        /// Output: Explorer
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        protected string FirstLetterToUppercase(string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;

            if (word.Length > 1)
                return char.ToUpper(word[0]) + word.Substring(1).ToLower();

            return word.ToUpper();
        }

        /// <summary>
        /// Use case for example: DB Browser - D:\\test\\test\\tekst.db
        /// Get inly DB Browser
        /// </summary>
        /// <returns></returns>
        private string FormatName(string title)
        {
            var titleParts = title.Split(STANDARD_APP_SEPARATOR);

            var validTitles = titleParts.Where(i => !i.Contains(Path.DirectorySeparatorChar));

            return validTitles.FirstOrDefault() ?? UKNKOWN_APP;
        }
    }
}
