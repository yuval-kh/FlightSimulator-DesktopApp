   M S B A M L   `   `   `  ����  FlightSimulator$    FlightSimulator.LineChart_view   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      .��5      � -   5          �Name$  _grid��5      
������� � RowDefinitions 5	   
   �� 5
      $��* �6   �� 5      $��1 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��1 �6   �� 5      $��* �6   .+�5        OxyPlot.Wpf.Plot  -   5          Name$
 plot��5         TitleFontSize$ 10Q�6%      Title$ Hello��68   $��0,0,20,0q�6F      Series 5        OxyPlot.Wpf.LineSeries  5      ��6%   �� *	VM_list+5      �� 5   
   $��1��6   $��3��6!   
�������65   �� 5   
   $��1��6   $��3��6$   
�������65     5   
   -   5        $ plotCorrlated��5      $��0��6*   $��2��67   $��0,0,20,0q�6H   $ 10Q�5   ;    6   �� *VM_CorralatedName+ 5        5      ��6%   �� *VM_CorralatedList+5        5    
   -   5        $ 
linear_reg��5       $��2��6'   $��0��64   $��0,0,20,0q�6E   $ Linear regression��5!      $ 10Q�61   . 5"        -   5          Name$ A��5"   !      	LineStyle$ 	Automatic=�6S     OxyPlot.Wpf.Series	   Color$
	 Blue��6j   ��6,   �� *VM_lineRegList+6j     5#      -   5        $ B��5#   !   $
 None=�6N   
   
MarkerType$
 Circle=�5$         
MarkerSize$ 2Q�62      
MarkerFill$ Black��6A   ��5#   ,   �� *	VM_points+5$   A   5%      �� 5&      -   5         ��Name$ 	lv_charts��5&      $��2��6*   $��3��67   $��10q�6G   ��6S   �� *
VM_names+6t   5'      5(      