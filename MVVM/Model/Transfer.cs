using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Threading.Tasks;
using System.Windows;
using EasySave.Languages;
namespace EasySave.MVVM.Model
{
    public class Transfer
    {

        private Work _work;
        public Transfer(Work work) {
            _work = work;

            if (!Directory.Exists(work.sSource))
                throw new Exception(cLanguage.GetString("SourceNotExists"));
            
            if(!Directory.Exists(work.sTarget))
                Directory.CreateDirectory(work.sTarget);

            if(work.sType == "complete")
            {
                Complete(work.sSource, work.sTarget);
                return;
            }

            if (work.sType == "differential")
            {
                Differential(work.sSource, work.sTarget);
                return;
            }

            throw new Exception(cLanguage.GetString("InvalidType"));

        }

        private void Complete(string source, string target) {
            List<string> files = listFiles(source);

            foreach (string file in files) {
                string fileTarget = target + Path.DirectorySeparatorChar +  Path.GetFileName(file);
                copyFile(file, fileTarget);
            }

        }

        private string fileCrypt(string file)
        {
            string fileSource;
            FileInfo fileinfo = new(file);
            List<string> exts = new Extensions().Get();
            if(exts.Contains(fileinfo.Extension))
            {
                string program = @"C:\Users\Ayman\Downloads\CryptoSoft\bin\Debug\netcoreapp3.0\CryptoSoft.exe";
                string fileNotCrypted = Path.Combine(Path.GetDirectoryName(file) ?? "",file);
                string fileEncrypt = Path.Combine(Path.GetDirectoryName(file) ?? "","Encrypted" + fileinfo.Name);
                string args = $" {fileNotCrypted} {fileEncrypt}";
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = program;
                startInfo.Arguments = args;
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit(); // attend la fin de l'exécution du processus
                

                fileSource = fileEncrypt;
                process.Kill();
            }
            else
            {
                fileSource = file;
            }

            return fileSource;

        }

        private void Differential(string source, string target)
        {
            List<string> files = listFiles(source); 
            foreach(string fileSource in files)
            {
                string fileTarget = target + Path.DirectorySeparatorChar + Path.GetFileName(source);
                if(!File.Exists(fileTarget))
                {
                    copyFile(fileSource, fileTarget);
                    continue;
                }
                FileInfo fileSourceInfo = new(fileSource);
                FileInfo fileTargetInfo = new(fileTarget);
                if(fileTargetInfo.LastWriteTime != fileSourceInfo.LastWriteTime) {
                    copyFile(fileSource, fileTarget);
                }
            }

        }

        private List<string> listFiles(string folder)
        {
            List<string> files = new List<string>();
            
            foreach(string file in Directory.GetFiles(folder)) {
                string fileSource = fileCrypt(file);
                files.Add(fileSource);
            }
            foreach(string dir in Directory.GetDirectories(folder))
            {

                files.AddRange(listFiles(dir));
            }
            return files;
        }

        private void copyFile(string source, string target)
        {
            FileInfo fileinfo = new FileInfo(source);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (FileStream sourceStream = new FileStream(source, FileMode.Open, FileAccess.Read))
                using (FileStream targetStream = new FileStream(target, FileMode.Create, FileAccess.Write))
                {
                    sourceStream.CopyTo(targetStream);
                }
                stopwatch.Stop();
                Timestamp timestamp = new Timestamp(_work.sName, target, source, stopwatch.ElapsedMilliseconds, fileinfo.Length);
                new Daily(timestamp);
            }
            catch (IOException ex)
            {
                // Attendre pendant 500 millisecondes avant de réessayer la copie
                Task.Delay(500).Wait();

                // Réessayer la copie du fichier
                try
                {
                    using (FileStream sourceStream = new FileStream(source, FileMode.Open, FileAccess.Read))
                    using (FileStream targetStream = new FileStream(target, FileMode.Create, FileAccess.Write))
                    {
                        sourceStream.CopyTo(targetStream);
                    }
                    stopwatch.Stop();
                    Timestamp timestamp = new Timestamp(_work.sName, target, source, stopwatch.ElapsedMilliseconds, fileinfo.Length);
                    new Daily(timestamp);
                }
                catch (Exception ex2)
                {
                    // Gérer l'erreur de copie de fichier
                    MessageBox.Show(ex2.ToString(), "", MessageBoxButton.OK);
                    stopwatch.Stop();
                    Timestamp timestamp = new Timestamp(_work.sName, target, source, -1, fileinfo.Length);
                    new Daily(timestamp);
                }
            }
            catch (Exception ex)
            {
                // Gérer l'erreur de copie de fichier
                MessageBox.Show(ex.ToString(), "", MessageBoxButton.OK);
                stopwatch.Stop();
                Timestamp timestamp = new Timestamp(_work.sName, target, source, -1, fileinfo.Length);
                new Daily(timestamp);
            }
        }




    }
}
