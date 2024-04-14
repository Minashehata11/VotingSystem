namespace LearnApi.HelperServices
{
    public static class HelperSettings
    {
        public static string UploadImage(IFormFile file,string FolderName)
        {
            //1 get folder path
            var Folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", FolderName);
            //2 get filename And make it Unique 
            var fileName=$"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
          // 3 Get FilePath
            var filePath = Path.Combine(Folderpath, fileName);
            //4 Create File to filepath
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

    }
}
