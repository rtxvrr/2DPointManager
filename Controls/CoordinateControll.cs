using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;

namespace _2DPointManager
{
    public class CoordinateControll
    {
        private readonly string CoordinateFolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "2DPointControllCoordinates");

        private const string XCoordinateFileName = "XCoordinate.json";
        private const string YCoordinateFileName = "YCoordinate.json";

        public List<Models.Point> Points { get; } = new List<Models.Point>();

        public CoordinateControll()
        {
            try
            {
                InitializeCoordinateFolder();
                Task.Run(async () => await LoadCoordinatesAsync()).Wait();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка инициализации контроллера координат", ex);
                Environment.Exit(1);
            }
        }

        private void InitializeCoordinateFolder()
        {
            try
            {
                if (!Directory.Exists(CoordinateFolderPath))
                {
                    Directory.CreateDirectory(CoordinateFolderPath);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка создания папки для координат", ex);
            }
        }

        private async Task LoadCoordinatesAsync()
        {
            try
            {
                List<double> xCoordinates = await LoadCoordinateAsync(XCoordinateFileName);
                List<double> yCoordinates = await LoadCoordinateAsync(YCoordinateFileName);

                if (xCoordinates.Count != yCoordinates.Count)
                {
                    ShowErrorMessage("Ошибка загрузки координат", new Exception("Несоответствие числа координат X и Y"));
                    return;
                }

                Points.AddRange(xCoordinates.Zip(yCoordinates, (x, y) => new Models.Point(x, y)));
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка загрузки координат", ex);
            }
        }

        private async Task<List<double>> LoadCoordinateAsync(string fileName)
        {
            List<double> coordinates = new List<double>();
            string filePath = Path.Combine(CoordinateFolderPath, fileName);

            try
            {
                if (File.Exists(filePath))
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                    {
                        byte[] buffer = new byte[stream.Length];
                        await stream.ReadAsync(buffer, 0, buffer.Length);
                        string json = Encoding.UTF8.GetString(buffer);
                        coordinates = JsonConvert.DeserializeObject<List<double>>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка загрузки файла координат", ex);
            }

            return coordinates;
        }

        public async Task UpdateCoordinatesAsync(double x, double y)
        {
            try
            {
                Points.Add(new Models.Point(x, y));
                await Task.WhenAll(SaveCoordinateAsync(XCoordinateFileName, Points.Select(p => p.X).ToList()),
                                   SaveCoordinateAsync(YCoordinateFileName, Points.Select(p => p.Y).ToList()));
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка обновления координат", ex);
            }
        }

        private async Task SaveCoordinateAsync(string fileName, List<double> coordinates)
        {
            string filePath = Path.Combine(CoordinateFolderPath, fileName);

            try
            {
                using (StreamWriter file = File.CreateText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    await Task.Run(() => serializer.Serialize(file, coordinates));
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка сохранения файла координат", ex);
            }
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}. Пожалуйста, закройте приложение и попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}