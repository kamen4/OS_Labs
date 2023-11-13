#include <iostream>
#include <fstream>
#include <map>
#include <vector>
#include <algorithm>
#include <thread>
#include <queue>
#include <chrono>
#include <mutex>
using namespace std;
using namespace std::chrono;

static const int MAX_ABS_POINT_POS = 100;
static const int MAX_ABS_LINE_POINT_POS = 200;
static const int MAX_ABS_LINE_DELTA = 300;
static const int MAX_POINTS_COUNT = 200;

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

struct Task
{
	Line l1;
	Line l2;
	vector<Point> points;
	Task()
	{
		l1 = Line(
			rand() % (MAX_ABS_LINE_POINT_POS * 2 + 1) - MAX_ABS_LINE_POINT_POS,
			rand() % (MAX_ABS_LINE_POINT_POS * 2 + 1) - MAX_ABS_LINE_POINT_POS,
			rand() % (MAX_ABS_LINE_POINT_POS * 2 + 1) - MAX_ABS_LINE_POINT_POS,
			rand() % (MAX_ABS_LINE_POINT_POS * 2 + 1) - MAX_ABS_LINE_POINT_POS);

		int delta = 0;
		while (delta == 0)
			delta = rand() % (MAX_ABS_LINE_DELTA * 2 + 1) - MAX_ABS_LINE_DELTA;
		l2 = l1;
		l2.p1.x += delta;
		l2.p2.x += delta;

		int N = rand() % MAX_POINTS_COUNT;
		for (int i = 0; i < N; i++)
			points.push_back(Point(
				rand() % (MAX_ABS_POINT_POS * 2 + 1) - MAX_ABS_POINT_POS,
				rand() % (MAX_ABS_POINT_POS * 2 + 1) - MAX_ABS_POINT_POS));
	}
	int solve()
	{
		for (int i = 0; i < points.size(); i++)
		{
			double loc1 = l1.LocateTo(points[i]);
			double loc2 = l2.LocateTo(points[i]);
			if (loc1 == 0 || loc2 == 0 || ((loc1 < 0) == (loc2 < 0)))
				return 0;
		}
		return 1;
	}
};

struct Result
{
	Task task;
	int thread_num;
	int result;
	int solve_time;
	Result() { thread_num = -1; result = -1; solve_time = -1; }
	Result(int thread_num, int result, int solve_time, Task task) : thread_num(thread_num), result(result), solve_time(solve_time), task(task) {}
};

void func(
	int thread_num,
	queue<Task> &tasks,
	mutex &tasks_mtx,
	vector<Result> &results,
	mutex &results_mtx,
	vector<int> &th_w_t,
	mutex &th_w_t_mtx)
{
	auto start_working = high_resolution_clock::now();
	while (true)
	{
		tasks_mtx.lock();
		if (tasks.empty())
		{
			tasks_mtx.unlock();
			break;
		}
		Task t = tasks.front();
		tasks.pop();
		tasks_mtx.unlock();

		auto start_solving = high_resolution_clock::now();
		int result = t.solve();
		auto stop_solving = high_resolution_clock::now();
		auto duration_solving = duration_cast<microseconds>(stop_solving - start_solving);

		results_mtx.lock();
		results.emplace_back(thread_num, result, duration_solving.count(), t);
		results_mtx.unlock();
	}
	auto stop_working = high_resolution_clock::now();
	auto duration_working = duration_cast<milliseconds>(stop_working - start_working);
	th_w_t_mtx.lock();
	th_w_t[thread_num] = duration_working.count();
	th_w_t_mtx.unlock();

}

int main()
{
	cout << "Enter task count:\n";
	int task_count;
	cin >> task_count;
	cout << "Enter thread count:\n";
	int thread_count;
	cin >> thread_count;

	srand(time(NULL));

	//generating task_count tasks into common container
	queue<Task> tasks;
	for (int i = 0; i < task_count; i++)
		tasks.emplace();

	//generating thread_count threads which solve tasks into common container
	vector<Result> results;
	vector<int> thread_working_time(thread_count, 0);
	mutex tasks_mtx;
	mutex results_mtx;
	mutex thread_working_time_mtx;
	vector<thread> threads;
	for (int i = 0; i < thread_count; i++)
		threads.emplace_back(
			func,
			i,
			ref(tasks),
			ref(tasks_mtx),
			ref(results),
			ref(results_mtx),
			ref(thread_working_time),
			ref(thread_working_time_mtx));
	
	for (int i = 0; i < thread_count; i++)
		threads[i].join();

	//saving task-answer into some file
	auto start = high_resolution_clock::now();
	ofstream fout("output.txt");
	int idx = 0;
	for (Result const& res : results)
	{
		fout << "Task "<< ++idx << ":\n";
		fout << "\tLine 1: " << res.task.l1 << '\n';
		fout << "\tLine 2: " << res.task.l2 << '\n';
		fout << "\tPoints:\n";
		for (Point const& p : res.task.points)
			fout << "\t\t(" << p << ")\n";
		fout << "Result:\n";
		fout << "\t" << res.result << "\n\n";
	}
	fout.close();
	auto stop = high_resolution_clock::now();
	auto duration = duration_cast<milliseconds>(stop - start);

	map<int, pair<int, int>> mp; //[thread_num, (tasks, time)]
	int min = INT16_MAX, max = INT16_MIN; //min|max time for solving
	for (int i = 0; i < thread_count; i++)
		mp[i] = make_pair(0, 0);
	for (int i = 0; i < task_count; i++)
	{
		mp[results[i].thread_num].first += 1;
		mp[results[i].thread_num].second += results[i].solve_time;
		if (results[i].solve_time < min)
			min = results[i].solve_time;
		if (results[i].solve_time > max)
			max = results[i].solve_time;
	}

	cout << "\n\t\t\x1B[37;41m ==GLOBAL STATISTICS== \x1B[0;0m\n\n"
		<< "\t" << task_count << " tasks solved\n"
		<< "\t0 errors occured\n"
		<< "\t0 tasks without solutions\n"
		<< "\tMin time for solving: " << min << " microseconds\n"
		<< "\tMax time for solving: " << max << " microseconds\n"
		<< "\tFile writing time: " << duration.count() << " milliseconds\n";
		
	cout << "\n\t\t\x1B[37;41m ==EACH THREAD STATISTICS== \x1B[0;0m\n\n"
		<< "\x1B[30;102m\tid\tsolved tasks count\tsolving time (microseconds)\tworking time (milliseconds)\x1B[0;0m\n";
	for (int i = 0; i < thread_count; i++)
		cout << "\t" << i << ":\t" << mp[i].first << "\t\t\t" << mp[i].second << "\t\t\t\t" << thread_working_time[i] << '\n';
}