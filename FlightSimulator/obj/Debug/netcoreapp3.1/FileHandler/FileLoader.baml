   M S B A M L   `   `   `  ����J  FFlightSimulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:FlightSimulatorFlightSimulator  5   	        FlightSimulator.FileLoader   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:FlightSimulator     5   	     � Title$  
FileLoader��5   	   $	��260��6   $	��600��6)    � WindowStartupLocation$ CenterScreen=�65   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��Open Simulator Files��6   $��20(�60   $��10q�6>   $��2��6J   �� 5   
   $��	xml file:��6   $��0��6%   $��1��65   $
��Left=�6B   $��10,5,0,5q�6]   �� 5   
   -   5         ��Name$ 
tb_xmlPath��5      $��1��6B   $��1��6O   $��5q�6_    �� 
IsReadOnly$
 True��6j   ${�False��6|   ��6&   �� *
VM_xmlPath+6|   �� 5   
   -   5         ��Name$ btn_openXml��5      $
��Open��6&   $��1��65   $��2��6B   $��5q�6R   6]   �� 5   
   $��	csv file:��6   $��0��6%   $��2��65   $
��Left=�6B   $��10,5,0,5q�6^   �� 5   
   -   5        $ 
tb_csvPath��5      $��2��6B   $��1��6O   $��5q�6_   $
 True��6j   ${�False��6|   ��6&   �� *
VM_csvPath+6|   �� 5    
   -   5        $ btn_opebCSV��5       $
��Open��6&   $��2��65   $��2��6B   $��5,5,0,5q�6R   $
��Left=�6c   $��33��6~   6�   �� 5#   
   $��	exe file:��6   $��0��6%   $��3��65   $
��Left=�6B   $��10,5,0,5q�6^   �� 5$   
   -   5        $ 
tb_exePath��5$      $��3��6A   $��1��6N   $��5q�6^   $
 True��6i   ${�False��6{   ��6&   �� *	VM_fgPath+6{   �� 5%   
   -   5        $ btn_opebEXE��5%      $
��Open��6&   $��3��65   $��2��6B   $��5q�6R   6]   �� 5'   
   $��anomaly detection file:��6   $��0��63   $��4��6C   $
��Left=�6P   $��10,5,0,5q�6l   �� 5(   
   -   5        $ tb_anomaliesPath��5(      $��4��6O   $��1��6\   $��5q�6l   $
 True��6w   ${�False��6�   ��6,   �� *VM_anomalyCsvPath+6�   �� 5)   
   -   5        $ btn_opebANOMALIES��5)      $
��Open��6,   $��4��6;   $��2��6H   $��5q�6X   6c   �� 5,   
   -	   5        $ 
btn_submit��5,      $��5��6%   $��Finish��62   ${�False��6C   $��10q�6U   $��1��6a   $��25��6q   $��Center=�6}   $��Center=�6�   $��80��6�   6�   5-      5.      