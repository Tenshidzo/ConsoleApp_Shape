using System;
using System.Collections.Generic;

public abstract class GeometricShape
{
    public abstract double Area { get; }
    public abstract double Perimeter { get; }
}

public interface SimplePolygon
{
    double Height { get; }
    double Base { get; }
    double AngleBetweenBaseAndAdjacentSide { get; }
    int NumberOfSides { get; }
    double[] SideLengths { get; }
    double Area { get; }
    double Perimeter { get; }
}

public class Triangle : GeometricShape
{
    public double SideA { get; }
    public double SideB { get; }
    public double SideC { get; }

    public Triangle(double sideA, double sideB, double sideC)
    {
        if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            throw new ArgumentException("Sides must be positive.");
        if (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA)
            throw new ArgumentException("The sum of any two sides must be greater than the third side.");

        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public override double Area
    {
        get
        {
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }
    }

    public override double Perimeter => SideA + SideB + SideC;
}

public class Square : GeometricShape
{
    public double Side { get; }

    public Square(double side)
    {
        if (side <= 0)
            throw new ArgumentException("Side must be positive.");

        Side = side;
    }

    public override double Area => Side * Side;
    public override double Perimeter => 4 * Side;
}

public class Rectangle : GeometricShape
{
    public double Length { get; }
    public double Width { get; }

    public Rectangle(double length, double width)
    {
        if (length <= 0 || width <= 0)
            throw new ArgumentException("Length and width must be positive.");

        Length = length;
        Width = width;
    }

    public override double Area => Length * Width;
    public override double Perimeter => 2 * (Length + Width);
}

public class CompositeShape : GeometricShape
{
    private List<GeometricShape> shapes = new List<GeometricShape>();

    public void AddShape(GeometricShape shape)
    {
        shapes.Add(shape);
    }

    public override double Area
    {
        get
        {
            double totalArea = 0;
            foreach (var shape in shapes)
            {
                totalArea += shape.Area;
            }
            return totalArea;
        }
    }

    public override double Perimeter
    {
        get
        {
            double totalPerimeter = 0;
            foreach (var shape in shapes)
            {
                totalPerimeter += shape.Perimeter;
            }
            return totalPerimeter;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Создание нескольких фигур
            var triangle = new Triangle(3, 4, 5);
            var square = new Square(4);
            var rectangle = new Rectangle(2, 5);

            // Вывод информации о фигурах
            Console.WriteLine($"Triangle Area: {triangle.Area}, Perimeter: {triangle.Perimeter}");
            Console.WriteLine($"Square Area: {square.Area}, Perimeter: {square.Perimeter}");
            Console.WriteLine($"Rectangle Area: {rectangle.Area}, Perimeter: {rectangle.Perimeter}");

            // Создание составной фигуры и добавление в нее фигур
            var compositeShape = new CompositeShape();
            compositeShape.AddShape(triangle);
            compositeShape.AddShape(square);
            compositeShape.AddShape(rectangle);

            // Вывод информации о составной фигуре
            Console.WriteLine($"Composite Shape Total Area: {compositeShape.Area}, Total Perimeter: {compositeShape.Perimeter}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
