﻿namespace Backups.Extra.Exceptions;

public class LoggerException : Exception
{
    public LoggerException(string message)
        : base(message) { }
}