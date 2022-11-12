﻿using Backups.Exceptions;
namespace Backups.Entities;

public class BackupObject
{
    private string _name;
    private string _path;
    public BackupObject(string name, string path)
    {
        if (name == string.Empty || path == string.Empty)
            throw new BackupObjectDataException();
        _name = name;
        _path = path;
    }

    public string GetName() => _name;
    public string GetPath() => _path;
}