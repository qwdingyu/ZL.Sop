namespace Demo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class ImageUtils
    {
        private static readonly Random _random = new Random(); // 静态Random实例，避免快速连续调用生成相同序列

        /// <summary>
        /// 从指定的图片目录中随机获取一张图片的文件名。
        /// </summary>
        /// <param name="sopImgDir">SOP图片目录的完整路径。</param>
        /// <param name="searchOption">搜索选项，指定是否搜索子目录。默认为 TopDirectoryOnly。</param>
        /// <returns>随机获取到的图片文件的完整路径，如果目录不存在或没有图片，则返回 null。</returns>
        public static string GetRandomImageFileName(string sopImgDir, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            // 1. 检查目录是否存在
            if (string.IsNullOrWhiteSpace(sopImgDir) || !Directory.Exists(sopImgDir))
            {
                Console.WriteLine($"错误：SOP图片目录 '{sopImgDir}' 不存在或无效。");
                return null;
            }

            // 2. 定义支持的图片文件扩展名
            // 可以根据实际需求添加或修改这些扩展名
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };

            // 3. 获取目录中所有文件，并筛选出图片文件
            try
            {
                List<string> imageFiles = Directory.EnumerateFiles(sopImgDir, "*.*", searchOption)
                                                   .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLowerInvariant()))
                                                   .ToList();

                // 4. 如果没有找到图片文件
                if (imageFiles.Count == 0)
                {
                    Console.WriteLine($"警告：在目录 '{sopImgDir}' 中未找到支持的图片文件。");
                    return null;
                }

                // 5. 随机选择一个图片文件
                int randomIndex = _random.Next(0, imageFiles.Count);
                return imageFiles[randomIndex];
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"错误：无权访问目录 '{sopImgDir}'。详情：{ex.Message}");
                return null;
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine($"错误：路径过长。详情：{ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取随机图片文件时发生未知错误：{ex.Message}");
                return null;
            }
        }
    }

}
