using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Extra;
using Backups.Models;
using Backups.Extra.Logger;
using Backups.Extra.Merge;
using Backups.Extra.Tools;
using Backups.Extra.ClearingRPAlgorhytms;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace ConsoleTest;

internal static class Console
{
    public static void Main(string[] args)
    {
        BackupTask bt = new BackupTask("Task", new FileSystemRepository(), new SingleStorage());
        BackupObject obj1 = new BackupObject("file1.txt", "./file1.txt");
        BackupObject obj2 = new BackupObject("file5.txt", "./file5.txt");
        BackupObject obj3 = new BackupObject("file3.txt", "./file3.txt");
        ConsoleLogger logger = new ConsoleLogger();
        BackupTaskExtra btextra = new BackupTaskExtra(bt, logger);
        btextra.AddObject(obj1);
        btextra.AddObject(obj2);
        btextra.AddObject(obj3);
        btextra.CreateRestorePoint();
        btextra.RemoveObject(obj3);
    }
}