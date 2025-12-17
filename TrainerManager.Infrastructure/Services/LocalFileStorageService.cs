using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;


namespace TrainerManager.Infrastructure.Services
{
    public class LocalFileStorageService(IWebHostEnvironment env) : IFileStorageService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            if (file == null) return null;
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(env.WebRootPath, folder, fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/{folder}/{fileName}";
        }
        public void DeleteFile(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            var fullPath = Path.Combine(env.WebRootPath, path.TrimStart('/'));
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
    }
}
