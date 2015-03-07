/*
 * Created by SharpDevelop.
 * User: Ihatenames
 * Date: 12.06.2014
 * Time: 13:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

using OmgUtils.UserInteraction;

namespace OmgUtils.Path
{
	/// <summary>
	/// Description of PathUtils.
	/// </summary>
	public class PathUtils
	{
		/// <summary>
		/// Retrievies the file name from a file path, using string operations.
		/// </summary>
		/// <param name="path">Full path to the file</param>
		/// <returns></returns>
		public static string GetFilename(string path)
		{
            if (IsEmpty(path))
                return path;

			int dirPos = path.LastIndexOf('\\');
			
			dirPos = (dirPos == -1) ? 0 : dirPos;
			
			int dotPos = path.LastIndexOf('.');
			
			dotPos = (dotPos == -1) ? (path.Length) : dotPos;
			
			int dirAdjust = (dirPos == 0) ? 0 : (dirPos + 1);
			
			return path.Substring(dirPos + dirAdjust, dotPos - dirPos - dirAdjust);
		}
		
		/// <summary>
		/// Retrivies the filename of a file from a FileInfo class
		/// </summary>
		/// <param name="sPathToFile">Path to the file</param>
		/// <returns></returns>
		public static string GetFilenameSave(string sPathToFile)
		{
			try
			{
				var info = new FileInfo(sPathToFile);
				
				if (info.Exists)
					return info.Name;
				else
					throw new ArgumentException("Path to file is invalid!");
			}
			catch (Exception)
			{
				throw;
			}
		}
		
		/// <summary>
		/// Changes the extension of the given filepath
		/// </summary>
		/// <param name="sTargetPath"></param>
		/// <param name="sNewExtensionWithDot"></param>
		/// <returns></returns>
		public static string ChangeExtension(string sTargetPath, string sNewExtensionWithDot)
		{
            if (IsEmpty(sTargetPath))
                return sTargetPath;
            
			string noExtension = RemoveExtension(sTargetPath);
			
			if (sTargetPath != noExtension)
			{
				sTargetPath = noExtension + sNewExtensionWithDot;
			}
			
			return sTargetPath;
		}
		
		/// <summary>
		/// Returns the path to the directory of the specified file
		/// </summary>
		/// <param name="fullFilePath"></param>
		/// <returns></returns>
		public static string GetFilePath(string fullFilePath)
		{
            if (IsEmpty(fullFilePath))
                return fullFilePath;

			int lastDirPos = fullFilePath.LastIndexOf('\\');
			
			if (lastDirPos != -1)
			{
				fullFilePath = fullFilePath.Substring(0, lastDirPos);
			}
			
			return fullFilePath;
		}
		
		/// <summary>
		/// Removes the extension from the given filepath
		/// </summary>
		/// <param name="sFilePath"></param>
		/// <returns></returns>
		public static string RemoveExtension(string sFilePath)
		{
            if (IsEmpty(sFilePath))
                return sFilePath;

			int dotPos = sFilePath.LastIndexOf('.');
			
			if (dotPos > 0) // meaning != -1 and != 0 (0 would mean it's a relative path)
			{
				sFilePath =  sFilePath.Substring(0, dotPos);
			}
			
			return sFilePath;
		}
		
		/// <summary>
		/// Returns a file's extension (if any) WITHOUT the dot.
		/// </summary>
		/// <param name="sFilePath"></param>
		/// <returns></returns>
		public static string GetExtension(string sFilePath)
        {
            if (IsEmpty(sFilePath))
                return sFilePath;

			int lastDir = sFilePath.LastIndexOf('\\');
			int lastDot = sFilePath.LastIndexOf('.');
			
			if (lastDot > lastDir && lastDot != -1)
			{
				string sExtension = sFilePath.Substring(lastDot);
				
				sExtension = sExtension.TrimStart('.');
				
				if (sExtension != "")
					return sExtension;
			}
			
			return sFilePath;
		}

        /// <summary>
        /// Checks whether the given path ends with \, if not adds it.
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public static string CheckFolderPath(string sPath)
        {
            if (IsEmpty(sPath))
                return sPath;

            string sReturn = sPath;

            if (sPath[sPath.Length - 1] != '\\')
                sReturn += '\\';

            return sReturn;
        }

        static bool IsEmpty(string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
	}
}
