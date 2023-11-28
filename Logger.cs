using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sort
{
    public class Logger
    {
        private StringBuilder permutationsLog = new();

        public Logger(int index1, int index2)
        {
            LogPermutation(index1, index2);
            UpdatePermutationsText();
        }

        public Logger()
        {
            ClearLog();
        }

        public void LogPermutation(int index1, int index2)
        {
            permutationsLog.AppendLine($"Элемент {index1} <-> {index2} : значение   {ArrayCreator.Rectangles[index1].Height} <-> {ArrayCreator.Rectangles[index2].Height}");
        }

        public void UpdatePermutationsText()
        {
            MainViewModel._window.permutationsTextBox.Text += permutationsLog.ToString();
        }

        public void ClearLog()
        {
            permutationsLog.Clear();
            MainViewModel._window.permutationsTextBox.Text = string.Empty;
        }
    }
}
