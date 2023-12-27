using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sort
{
    class Animation
    {
        public async Task BubbleSortAnimationAsync()
        {
            for (int i = 0; i < ArrayCreator.ArraySize - 1; i++)
            {
                for (int j = 0; j < ArrayCreator.ArraySize - i - 1; j++)
                {
                    ArrayCreator.Rectangles[j].Fill = Brushes.Red;
                    ArrayCreator.Rectangles[j + 1].Fill = Brushes.Red;

                    if (GetHeight(ArrayCreator.Rectangles[j]) > GetHeight(ArrayCreator.Rectangles[j + 1]))
                    {
                        await SwapAsync(j, j + 1);
                        Logger l = new(j, j + 1);
                    }

                    ArrayCreator.Rectangles[j].Fill = Brushes.Blue;
                    ArrayCreator.Rectangles[j + 1].Fill = Brushes.Blue;
                }

                ArrayCreator.Rectangles[ArrayCreator.ArraySize - i - 1].Fill = Brushes.Green;
            }
        }

        public async Task InsertionSortAnimationAsync()
        {
            Logger clear = new();
            int n = ArrayCreator.ArraySize;

            for (int i = 1; i < n; i++)
            {
                double key = GetHeight(ArrayCreator.Rectangles[i]);
                int j = i - 1;

                while (j >= 0 && GetHeight(ArrayCreator.Rectangles[j]) > key)
                {
                    ArrayCreator.Rectangles[j].Fill = Brushes.Red;
                    ArrayCreator.Rectangles[j + 1].Fill = Brushes.Red;

                    await SwapAsync(j, j + 1);
                    Logger l = new(j, j + 1);

                    ArrayCreator.Rectangles[j].Fill = Brushes.Blue;
                    ArrayCreator.Rectangles[j + 1].Fill = Brushes.Blue;

                    j--;
                }

                SetHeight(ArrayCreator.Rectangles[j + 1], key);
            }

            for (int i = 0; i < n; i++)
            {
                ArrayCreator.Rectangles[i].Fill = Brushes.Green;
                await Task.Delay(100);
            }
        }

        public async Task QuickSortAnimationAsync(int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = await PartitionAsync(low, high);
                await QuickSortAnimationAsync(low, partitionIndex - 1);
                await QuickSortAnimationAsync(partitionIndex + 1, high);
            }
        }

        private async Task<int> PartitionAsync(int low, int high)
        {
            double pivot = GetHeight(ArrayCreator.Rectangles[high]);
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (GetHeight(ArrayCreator.Rectangles[j]) < pivot)
                {
                    ArrayCreator.Rectangles[j].Fill = Brushes.Red;
                    i++;
                    ArrayCreator.Rectangles[i].Fill = Brushes.Red;
                    await SwapAsync(i, j);
                    ArrayCreator.Rectangles[i].Fill = Brushes.Blue;
                    Logger l1 = new(i, j);
                    ArrayCreator.Rectangles[j].Fill = Brushes.Blue;
                }
            }
            ArrayCreator.Rectangles[i + 1].Fill = Brushes.Red;
            ArrayCreator.Rectangles[high].Fill = Brushes.Red;
            await SwapAsync(i + 1, high);
            Logger l2 = new(i + 1, high);
            ArrayCreator.Rectangles[i + 1].Fill = Brushes.Blue;
            ArrayCreator.Rectangles[high].Fill = Brushes.Blue;

            return i + 1;
        }

        public async Task HeapSortAnimationAsync()
        {
            Logger clear = new();
            int n = ArrayCreator.ArraySize;

            for (int i = n / 2 - 1; i >= 0; i--)
                await HeapifyAsync(n, i);

            for (int i = n - 1; i > 0; i--)
            {
                ArrayCreator.Rectangles[i].Fill = Brushes.Red;
                ArrayCreator.Rectangles[0].Fill = Brushes.Red;

                await SwapAsync(0, i);

                ArrayCreator.Rectangles[i].Fill = Brushes.Green;
                ArrayCreator.Rectangles[0].Fill = Brushes.Blue;

                await HeapifyAsync(i, 0);
            }

            for (int i = 0; i < n; i++)
            {
                ArrayCreator.Rectangles[i].Fill = Brushes.Green;
                await Task.Delay(100);
            }
        }

        private async Task HeapifyAsync(int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && GetHeight(ArrayCreator.Rectangles[left]) > GetHeight(ArrayCreator.Rectangles[largest]))
                largest = left;

            if (right < n && GetHeight(ArrayCreator.Rectangles[right]) > GetHeight(ArrayCreator.Rectangles[largest]))
                largest = right;

            if (largest != i)
            {
                ArrayCreator.Rectangles[i].Fill = Brushes.Red;
                ArrayCreator.Rectangles[largest].Fill = Brushes.Red;

                await SwapAsync(i, largest);
                Logger l = new(i, largest);

                ArrayCreator.Rectangles[i].Fill = Brushes.Blue;
                ArrayCreator.Rectangles[largest].Fill = Brushes.Blue;

                await HeapifyAsync(n, largest);
            }
        }

        private async Task SwapAsync(int index1, int index2)
        {
            double tempHeight = GetHeight(ArrayCreator.Rectangles[index1]);
            SetHeight(ArrayCreator.Rectangles[index1], GetHeight(ArrayCreator.Rectangles[index2]));
            SetHeight(ArrayCreator.Rectangles[index2], tempHeight);

            int delayMilliseconds = (int)(MainViewModel._window.speedSlider.Maximum - MainViewModel._window.speedSlider.Value) * 10;
            await Task.Delay(delayMilliseconds);

            Canvas.SetLeft(ArrayCreator.Rectangles[index1], index2 * 40);
            Canvas.SetLeft(ArrayCreator.Rectangles[index2], index1 * 40);

            (ArrayCreator.Rectangles[index2], ArrayCreator.Rectangles[index1]) = (ArrayCreator.Rectangles[index1], ArrayCreator.Rectangles[index2]);
            (ArrayCreator.Rectangles[index2].Height, ArrayCreator.Rectangles[index1].Height) = (ArrayCreator.Rectangles[index1].Height, ArrayCreator.Rectangles[index2].Height);
        }

        private double GetHeight(Rectangle rectangle)
        {
            return rectangle.Height;
        }

        private void SetHeight(Rectangle rectangle, double height)
        {
            rectangle.Height = height;
        }
    }
}
