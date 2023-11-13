#include <iostream>
#include <fstream>
#include <vector>
#include <thread>
#include <chrono>
using namespace std;
using namespace std::chrono;

struct Point
{
	int x;
	int y;
	Point()
	{
		x = y = 0;
	}
	Point(int x, int y)
	{
		this->x = x;
		this->y = y;
	}
};
istream& operator >> (istream& in, Point& p)
{
	in >> p.x >> p.y;
	return in;
}
ostream& operator << (ostream& out, const Point& p)
{
	out << p.x << " " << p.y;
	return out;
}

struct Line
{
	Point p1;
	Point p2;
	Line()
	{
		p1 = Point();
		p2 = Point();
	}
	Line(int x1, int y1, int x2, int y2)
	{
		p1 = Point(x1, y1);
		p2 = Point(x2, y2);
	}
	double K()
	{
		return (double)(p2.y - p1.y) / (double)(p2.x - p1.x);
	}
	double LocateTo(Point p)
	{
		return p.y - (double)(p.x - p1.x) / (double)(p2.x - p1.x) * (double)(p2.y - p1.y) + p1.y;
	}
};
istream& operator >> (istream& in, Line& l)
{
	in >> l.p1 >> l.p2;
	return in;
}
ostream& operator << (ostream& out, const Line& l)
{
	out << l.p1 << " " << l.p2;
	return out;
}

void func(string input, string output)
{
	ifstream fin(input);
	ofstream fout(output);
	Line l1, l2;
	fin >> l1 >> l2;
	int N;
	fin >> N;
	bool fits = true;
	for (int i = 0; i < N; i++)
	{
		Point p;
		fin >> p;
		double loc1 = l1.LocateTo(p);
		double loc2 = l2.LocateTo(p);
		if (loc1 == 0 || loc2 == 0 || ((loc1 < 0) == (loc2 < 0)))
		{
			fits = false;
			break;
		}
	}
	fout << fits ? 1 : 0;
	fout.close();
	fin.close();
}

int main()
{
	auto start = high_resolution_clock::now();
	thread helper(func, "input.txt", "output.txt");
	helper.join();
	auto stop = high_resolution_clock::now();
	auto duration = duration_cast<microseconds>(stop - start);
	cout << duration.count() << " microseconds";
	return 0;
}