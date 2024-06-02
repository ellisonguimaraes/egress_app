using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Egress.Application.Services;

public static class FileHelpers
{
    # region Constants
    private const string BASE_FILE_PATH = "files";
    private const string DEFAULT_CONTENT_TYPE = "application/octet-stream";
    # endregion

    /// <summary>
    /// Upload file in files folder (inside the container)
    /// </summary>
    /// <param name="file">IFormFile</param>
    /// <param name="basePath">Local base path (folder)</param>
    /// <param name="filename">File name to save</param>
    /// <returns>Full path where it was saved</returns>
    public static async Task<string> UploadAsync(IFormFile file, string basePath, string filename)
    {
        var rootDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
        var filePath = Path.Combine(rootDirectory, BASE_FILE_PATH, basePath);

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        var extension = Path.GetExtension(file.FileName);

        var completePath = Path.Combine(filePath, $"{filename}{extension}");

        using var fileStream = new FileStream(completePath, FileMode.Create);

        await file.CopyToAsync(fileStream);

        return Path.Combine(basePath, $"{filename}{extension}");
    }

    /// <summary>
    /// Get file in files folder (inside the container)
    /// </summary>
    /// <param name="path">Local file path</param>
    /// <returns>Stream</returns>
    public static FileStreamResult GetFileStream(string path)
    {
        var stream = File.OpenRead(path);

        var filename = Path.GetFileName(path);

        var fileContentTypeProvider = new FileExtensionContentTypeProvider();

        fileContentTypeProvider.TryGetContentType(filename, out var contentType);

        contentType ??= DEFAULT_CONTENT_TYPE;

        return new FileStreamResult(stream, contentType);
    }

    /// <summary>
    /// Delete directory (with files)
    /// </summary>
    /// <param name="path">Local base path (folder)</param>
    public static void DeleteDirectory(string path)
    {
        var rootDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
        var filePath = Path.Combine(rootDirectory, BASE_FILE_PATH, path);
        Directory.Delete(filePath, true);
    }
    
    /// <summary>
    /// Delete file
    /// </summary>
    /// <param name="path">Local base path (folder)</param>
    public static void DeleteFile(string path)
    {
        var rootDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
        var filePath = Path.Combine(rootDirectory, BASE_FILE_PATH, path);
        File.Delete(filePath);
    }
}