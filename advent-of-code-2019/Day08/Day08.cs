using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace advent_of_code_2019.Day08
{
    public class Day08
    {
        public int Part1()
        {
            string input = LoadInput();

            var image = LoadImage(25, 6, input);

            var checksum = GetCheckSum(image);

            return checksum;
        }

        public void Part2()
        {
            string input = LoadInput();

            var chars = input.ToCharArray();

            var image = LoadImage(25, 6, input);

            DetermineRenderLayer(image, 25, 6);
        }

        private void DetermineRenderLayer(Image image, int width, int height)
        {
            image.RenderOutput = new Layer() { Rows = new List<List<Pixel>>() };

            for (int i = 0; i < width; i++)
            {
                List<Pixel> row = new List<Pixel>();
                image.RenderOutput.Rows.Add(row);
            }

            // Black 0, White 1, Transparent 2
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pixelCol = 2;
                    for (int l = 0; l < image.Layers.Count; l++)
                    {
                        Layer layer = image.Layers[l];
                        if (pixelCol == 2)
                        {
                            pixelCol = layer.Rows[y][x].Colour;
                        }
                    }

                    Debug.Write(pixelCol == 1 ? "#" : " ");
                }
                Debug.WriteLine("");
            }
        }

        private int GetCheckSum(Image image)
        {
            int zeroCount = 0;
            int minZeroCount = Int32.MaxValue;
            int layerIndex = 0;

            foreach(Layer layer in image.Layers)
            {
                zeroCount = 0;
                foreach (var r in layer.Rows)
                {
                    zeroCount += r.Where(p => p.Colour == 0).Count();
                }

                if (zeroCount < minZeroCount )
                {
                    minZeroCount = zeroCount;
                    layerIndex = image.Layers.IndexOf(layer);
                }
            }

            Layer chosenLayer = image.Layers[layerIndex];

            int OneDigits = 0;
            int TwoDigits = 0;

            foreach(var r in chosenLayer.Rows)
            {
                OneDigits += r.Where(p => p.Colour == 1).Count();
                TwoDigits += r.Where(p => p.Colour == 2).Count();
            }

            return OneDigits * TwoDigits;
        }

        private Image LoadImage(int width, int height, string input)
        {
            var image = new Image();
            image.Layers = new List<Layer>();

            int charPointer = 0;
            while (charPointer < input.Length)
            {
                var layer = new Layer() { Rows = new List<List<Pixel>>() };

                for (int y = 0; y < height; y++)
                {
                    List<Pixel> row = new List<Pixel>();
                    for (int x = 0; x < width; x++)
                    {
                        row.Add(new Pixel { X = x, Y = y, Colour = Convert.ToInt32(input.Substring(charPointer, 1)) });
                        charPointer++;
                    }
                    layer.Rows.Add(row);
                }

                image.Layers.Add(layer);
            }

            return image;
        }

        private string LoadInput()
        {
            string input = File.ReadAllText("Day08\\input.txt");

            return input;
        }
    }

    public class Pixel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Colour { get; set; }
    }

    public class Layer
    {
        public List<List<Pixel>> Rows { get; set; }
    }

    public class Image
    {
        public List<Layer> Layers { get; set; }

        public Layer RenderOutput { get; set; }
    }
}
