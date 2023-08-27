using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using System.Collections.Generic;

namespace AvaloniaScrollBar;
public partial class MainWindow : Window
{
	private const int Size = 100;

	private ScrollViewer _scrollViewer;
	private Grid _grid;

	private int _counter = 0;

	private IBrush GetBrush()
	{
		return _counter++ % 2 == 0 ? Brushes.LightBlue : Brushes.LightGreen;
	}

	public MainWindow()
	{
		InitializeComponent();

		_grid = new Grid()
		{
			HorizontalAlignment = HorizontalAlignment.Left,
			RowDefinitions = new RowDefinitions("*"),
		};

		for (int i = 0; i < 10; i++)
		{
			AddColumn();
		}

		_scrollViewer = new ScrollViewer()
		{
			HorizontalAlignment = HorizontalAlignment.Stretch,
			VerticalAlignment = VerticalAlignment.Stretch,
			HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
			VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
			Content = _grid,
		};
		_scrollViewer.Tapped += _scrollViewer_Tapped;
		_scrollViewer.Offset = new Avalonia.Vector(4 * Size, 0);

		Content = _scrollViewer;
	}

	private void AddColumn()
	{
		_grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));

		// Adding a Panel, TextBox, or ScrollViewer leaves the ScollViewer X Position in it's current spot
		/*var panel = new Panel()
		{
			Width = Size,
			Height = Size,
			Background = GetBrush(),
			[Grid.ColumnProperty] = _grid.Children.Count,
		};
		_grid.Children.Add(panel);*/

		// Adding a DataGrid will move the ScrollViewer X Position back to 0
		var dataGrid = new DataGrid()
		{
			Width = Size,
			Height = Size,
			Background = GetBrush(),
			ItemsSource = new List<string> { _grid.Children.Count.ToString() },
			AutoGenerateColumns = true,
			[Grid.ColumnProperty] = _grid.Children.Count,
		};
		_grid.Children.Add(dataGrid);
	}

	private void _scrollViewer_Tapped(object? sender, Avalonia.Input.TappedEventArgs e)
	{
		AddColumn();
	}
}
