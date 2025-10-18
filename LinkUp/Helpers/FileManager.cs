using Microsoft.AspNetCore.Http;

namespace LinkUp.Helpers
{
    public static class FileManager
    {
        public static string? Upload(IFormFile file, string Id, string folderName, bool isEditMode = false, string? imagePath = "")
        {
            if (isEditMode && file == null)
            {
                return imagePath;
            }

            string basePath = $"Images/{folderName}/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fullFilePath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode && !string.IsNullOrWhiteSpace(imagePath))
            {
                string[] oldImagePath = imagePath.Split("/");
                string oldFileName = oldImagePath[^1];
                string completeOldPath = Path.Combine(path, oldFileName);

                if (File.Exists(completeOldPath))
                {
                    File.Delete(completeOldPath);
                }
            }

            return $"{basePath}/{fileName}";
        }

        public static bool Delete(string Id, string folderName)
        {
            string basePath = $"Images/{folderName}/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basePath}");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
