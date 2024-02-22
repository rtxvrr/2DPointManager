﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DPointManager.Models
{
    public struct Point
    {
        public double X { get; set; } 
        public double Y { get; set; } 

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"(X: {X}, Y: {Y})";
        }
    }
}
