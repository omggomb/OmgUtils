/*
 * Created by SharpDevelop.
 * User: Ihatenames
 * Date: 12.06.2014
 * Time: 13:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;

using Microsoft.VisualBasic.FileIO;
using OmgUtils.UserInteraction;

namespace OmgUtils.ProcessUt
{
	/// <summary>
	/// Description of ProcessUtils.
	/// </summary>
	public static class ProcessUtils
	{
		/// <summary>
		/// Runs the given process and redirects its sdtout and stderr to a textbox, which is shown afterwards.
		/// </summary>
		/// <param name="process"></param>
		public static void RunProcessWithRedirectedStdErrorStdOut(Process process, string sCaptionOfResultWindow)
		{
			ProcessStartInfo info = process.StartInfo;
			
			info.RedirectStandardOutput = true;
			info.RedirectStandardError = true;
			info.UseShellExecute = false;
			
			process.Start();
			
			StreamReader stdOut = process.StandardOutput;
			StreamReader stdError  = process.StandardError;
			
			string output = stdOut.ReadToEnd() + "\n" + stdError.ReadToEnd();
			
			UserInteractionUtils.DisplayRichTextBox(output, sCaptionOfResultWindow);
		}
		
		/// <summary>
		/// Copies a directory.
		/// </summary>
		/// <param name="sPathToDir">Full path to the directory to be copied</param>
		/// <param name="sTargetDir">Full path to the target directory, including the new directory itself</param>
		/// <param name="bOverwrite">Overwrite existing directory?</param>
		/// <param name="bSilent">Hide progress bar? Still shows errors</param>
		public static void CopyDirectory(string sPathToDir, string sTargetDir, bool bOverwrite = true, bool bSilent = false, string sCustomName = "")
		{
			if (bOverwrite)
			{
				if (sTargetDir == sPathToDir)
				{
					string sTemp = System.IO.Path.GetTempPath();
					sTemp += "\\CewspTempFolder";
					
					FileSystem.CopyDirectory(sPathToDir, sTemp);
					
					sPathToDir = sTemp;
					
					
				}
				if (Directory.Exists(sTargetDir))
					Directory.Delete(sTargetDir, true);
			}
			
			DirectoryInfo info = new DirectoryInfo(sPathToDir);
			
			FileSystem.CopyDirectory(sPathToDir, sTargetDir,
			                         bSilent ? UIOption.OnlyErrorDialogs : UIOption.AllDialogs,
			                         UICancelOption.DoNothing);
		}
		
		/// <summary>
		/// Copies a file
		/// </summary>
		/// <param name="sSourceFile">Full path to the file to be copied</param>
		/// <param name="sTargetFile">Full path to the destination file</param>
		/// <param name="bOverwrite">Overwrite exsting file?</param>
		/// <param name="bSilent">Hide progress bar? Still shows errors</param>
		public static void CopyFile(string sSourceFile, string sTargetFile, bool bOverwrite = true, bool bSilent = false)
		{
			
			if (bOverwrite)
			{
				if (sSourceFile == sTargetFile)
				{
					string sTemp = System.IO.Path.GetTempFileName();
					
					File.Copy(sSourceFile, sTemp);
					
					sSourceFile = sTemp;
					
				}
				if (File.Exists(sTargetFile))
					File.Delete(sTargetFile);
			}
			
			FileSystem.CopyFile(sSourceFile, sTargetFile,
			                         bSilent ? UIOption.OnlyErrorDialogs : UIOption.AllDialogs,
			                         UICancelOption.DoNothing);
		}
		
		/// <summary>
		/// Moves a directory.
		/// </summary>
		/// <param name="sPathToDir">Full path to the directory to be moved</param>
		/// <param name="sTargetDir">Full path to the target directory</param>
		/// <param name="bOverwrite">Overwrite existing directory?</param>
		/// <param name="bSilent">Hide progress bar? Still shows errors</param>
		public static void MoveDirectory(string sPathToDir, string sTargetDir, bool bOverwrite = true, bool bSilent = false)
		{
			if (bOverwrite)
			{
				if (Directory.Exists(sTargetDir))
					Directory.Delete(sTargetDir, true);
			}
			
			FileSystem.MoveDirectory(sPathToDir, sTargetDir,
			                         bSilent ? UIOption.OnlyErrorDialogs : UIOption.AllDialogs,
			                         UICancelOption.DoNothing);
		}
		
		/// <summary>
		/// Moves a file
		/// </summary>
		/// <param name="sSourceFile">Full path to the file to be moved</param>
		/// <param name="sTargetFile">Full path to the destination file</param>
		/// <param name="bOverwrite">Overwrite exsting file?</param>
		/// <param name="bSilent">Hide progress bar? Still shows errors</param>
		public static void MoveFile(string sSourceFile, string sTargetFile, bool bOverwrite = true, bool bSilent = false)
		{
			if (bOverwrite)
			{
				if (File.Exists(sTargetFile))
					File.Delete(sTargetFile);
			}
			
			FileSystem.MoveFile(sSourceFile, sTargetFile,
			                         bSilent ? UIOption.OnlyErrorDialogs : UIOption.AllDialogs,
			                         UICancelOption.DoNothing);
		}
	}
}
